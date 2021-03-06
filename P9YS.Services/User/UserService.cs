﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using P9YS.Common;
using P9YS.EntityFramework;
using P9YS.Services.Base;
using P9YS.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace P9YS.Services.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContext;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IOptionsMonitor<AppSettings> _options;
        private readonly IBaseService _baseService;

        public UserService(IMapper mapper
            , IHttpContextAccessor httpContext
            , MovieResourceContext movieResourceContext
            , IOptionsMonitor<AppSettings> options
            , IBaseService baseService)
        {
            _mapper = mapper;
            _httpContext = httpContext;
            _movieResourceContext = movieResourceContext;
            _options = options;
            _baseService = baseService;
        }

        #region 登录、注销

        public const string defaultAvatar = "/images/default.jpg";
        public async Task<Result<CurrentUser>> Login(Login_Input input)
        {
            var result = new Result<CurrentUser>();
            input.Password = GetCiphertext(input.Password, _options.CurrentValue.PasswordSalt);
            var user = await _movieResourceContext.Users
                .FirstOrDefaultAsync(u => u.Email == input.Email && u.Password == input.Password);
            if (user == null)
            {
                result.SetCode(CustomCodeEnum.PassworError);
                return result;
            }
            else if (user.Status == UserStatusEnum.Locked)
            {
                result.SetCode(CustomCodeEnum.AccountLocked);
                return result;
            }

            //创建身份
            await CreateIdentityAsync(input.Remember, user);

            //更新登录时间
            user.LastLoginTime = DateTime.Now;
            await _movieResourceContext.SaveChangesAsync();

            user.Email.HideEmail();
            result.Content = _mapper.Map<CurrentUser>(user);
            return result;
        }

        private string GetCiphertext(string password,string salt=null)
        {
            if (!string.IsNullOrWhiteSpace(password))
            {
                password = SecurityHelper.MD5Encrypt(password, System.Text.Encoding.UTF8);
                if(!string.IsNullOrWhiteSpace(salt))
                    password = SecurityHelper.MD5Encrypt(password + salt, System.Text.Encoding.UTF8);
            }
            return password;
        }

        public async Task Logout()
        {
            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async Task CreateIdentityAsync(bool remember, EntityFramework.Models.User user)
        {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //头像
            user.Avatar = user.Avatar == defaultAvatar ? defaultAvatar
                : _baseService.GetCosAbsoluteUrl(user.Avatar);
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("NickName", user.NickName),
                new Claim("Avatar", user.Avatar),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
            };
            claimsIdentity.AddClaims(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            DateTime? expiresUtc = null;
            if (remember)
                expiresUtc = DateTime.UtcNow.AddMonths(1);

          await _httpContext.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                , claimsPrincipal
                , new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = expiresUtc
                });
        }

        public CurrentUser GetCurrentUser()
        {
            CurrentUser user = null;
            try
            {
                if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    var claims = _httpContext.HttpContext.User.Claims;
                    user = new CurrentUser
                    {
                        UserId = int.Parse(claims.FirstOrDefault(u => u.Type == "UserId").Value),
                        Email = claims.FirstOrDefault(u => u.Type == "Email").Value,
                        Avatar = claims.FirstOrDefault(u => u.Type == "Avatar").Value,
                        NickName = claims.FirstOrDefault(u => u.Type == "NickName").Value,
                    };
                }
            }
            catch { };
            return user;
        }

        #endregion

        #region 注册

        public async Task<bool> AccountIsExist(string email)
        {
            return await _movieResourceContext.Users.AnyAsync(s => s.Email == email);
        }

        public const string RegisterVerifyCodeName = "RegisterVerifyCode";
        public async Task<Result<bool>> SendVerifyCode(string email)
        {
            var result = new Result<bool>();
            //是否已注册
            var isExist = await AccountIsExist(email);
            if (isExist)
            {
                result.SetCode(CustomCodeEnum.Registered);
                result.Content = false;
                return result;
            }

            //发验证码邮件
            try
            {
                string verifyCode = new Random().Next(100000, 999999).ToString();
                _httpContext.HttpContext.Session.SetString(RegisterVerifyCodeName, verifyCode);
                SendEmailHelper.SendEmailAsync(new EmailConfig
                {
                    Host = _options.CurrentValue.EmailServer.SmtpServer,
                    FromEmail = _options.CurrentValue.EmailServer.From,
                    FromPassword = _options.CurrentValue.EmailServer.FromPwd,
                    IsHtml = false,
                    ToEmail = email,
                    Title = "【P9影视】注册验证码",
                    Body = $"您的验证码是：{ verifyCode }。请尽快使用，谢谢您的支持！",
                });
            }
            catch (Exception ex)
            {
                result.SetCode(CustomCodeEnum.VerifyCodeSendFailed);
                result.Content = false;
            }
            return result;
        }

        public async Task<Result<bool>> Register(Register_Input input)
        {
            var result = new Result<bool>();
            //校验验证码
            var verifyCode = _httpContext.HttpContext.Session.GetString(RegisterVerifyCodeName);
            if (verifyCode != input.VerifyCode)
            {
                result.SetCode(CustomCodeEnum.VerifyCodeError);
                result.Content = false;
                return result;
            }
            //注册
            var entity = new EntityFramework.Models.User
            {
                Avatar = defaultAvatar,
                Email = input.Email,
                NickName = "NoName",//Guid.NewGuid().ToString("N"),
                Password = GetCiphertext(input.Password, _options.CurrentValue.PasswordSalt),
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now,
                Status = UserStatusEnum.Normal
            };
            var user = await _movieResourceContext.Users.AddAsync(entity);
            if (await _movieResourceContext.SaveChangesAsync() < 1)
            {
                result.SetCode(CustomCodeEnum.Failed);
                result.Content = false;
                return result;
            }

            //登录
            await CreateIdentityAsync(false, user.Entity);
            //清除Session
            _httpContext.HttpContext.Session.Remove(RegisterVerifyCodeName);

            return result;
        }

        #endregion

        #region 闲聊么

        public XianLiao_Output GetXianLiaoUserInfo()
        {
            var user = GetCurrentUser();
            XianLiao_Output xianLiaoOutput = new XianLiao_Output();
            if (user == null)
            {//游客
                var xlCookieName = "xl";
                var xlCookie = _httpContext.HttpContext.Request.Cookies
                    .FirstOrDefault(c => c.Key == xlCookieName).Value;
                if (string.IsNullOrWhiteSpace(xlCookie))
                {//没有游客cookie,生成
                    xianLiaoOutput.UserId = Guid.NewGuid().ToString("N").Substring(0,10);
                    xianLiaoOutput.NickName = "游客" + xianLiaoOutput.UserId;
                    _httpContext.HttpContext.Response.Cookies.Append(xlCookieName,
                        $"{xianLiaoOutput.UserId}|{xianLiaoOutput.NickName}",
                        new CookieOptions()
                        {                            
                            Expires = DateTimeOffset.MaxValue
                        });
                }
                else
                {//有游客cookie                    
                    string[] visitor = xlCookie.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    xianLiaoOutput.UserId = visitor[0];
                    xianLiaoOutput.NickName = visitor[1];
                }
                xianLiaoOutput.Avatar = defaultAvatar;
            }
            else
            {//已登录，覆盖游客身份
                xianLiaoOutput.UserId = user.UserId.ToString();
                xianLiaoOutput.NickName = user.NickName;
                xianLiaoOutput.Avatar = user.Avatar;
            }
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            xianLiaoOutput.TimeStamp = ts.TotalSeconds * 1000;
            string signStr = $"{_options.CurrentValue.XianLiao.Id}_{xianLiaoOutput.UserId}_{xianLiaoOutput.TimeStamp}_{_options.CurrentValue.XianLiao.SsoKey}";
            xianLiaoOutput.XlmHash = SecurityHelper.SHA512Encrypt(signStr,System.Text.Encoding.UTF8).ToLower();
            return xianLiaoOutput;
        }

        #endregion

        #region 列表

        public async Task<PagingOutput<UserManage_Output>> GetUsers(PagingInput<UserManage_Search_Input> pagingInput)
        {
            var query = _movieResourceContext.Users.AsQueryable();
            
            //TODO:封装分页查询(条件拼接、分页、排序)

            //条件
            if (pagingInput.Condition != null)
            {
                if (!string.IsNullOrWhiteSpace(pagingInput.Condition.Email))
                    query = query.Where(s => s.Email.Contains(pagingInput.Condition.Email));
                if (!string.IsNullOrWhiteSpace(pagingInput.Condition.NickName))
                    query = query.Where(s => s.NickName.Contains(pagingInput.Condition.NickName));
                if (pagingInput.Condition.Status.HasValue)
                    query = query.Where(s => s.Status == pagingInput.Condition.Status.Value);
                if (pagingInput.Condition.Role.HasValue)
                    query = query.Where(s => s.Role == pagingInput.Condition.Role.Value);
            }
            //排序
            if (pagingInput.OrderBy == null)
                pagingInput.OrderBy = new OrderBy { Field = "RegisterTime", Type = "desc" };
            var orderByField = typeof(EntityFramework.Models.User).GetProperty(pagingInput.OrderBy.Field);
            if (pagingInput.OrderBy.Type == "desc")
                query = query.OrderByDescending(s => orderByField.GetValue(s));
            else
                query = query.OrderBy(s => orderByField.GetValue(s));

            //分页
            var users = await query.Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<UserManage_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<UserManage_Output>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = users,
                TotalCount = totalCount
            };
            return result;
        }

        #endregion
    }
}
