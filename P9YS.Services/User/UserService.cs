using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using P9YS.Common;
using P9YS.EntityFramework;
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
        private IHttpContextAccessor _httpContext;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IOptionsMonitor<AppSettings> _options;

        public UserService(IHttpContextAccessor httpContext
            , MovieResourceContext movieResourceContext
            , IOptionsMonitor<AppSettings> options)
        {
            _httpContext = httpContext;
            _movieResourceContext = movieResourceContext;
            _options = options;
        }

        #region 登录、注销

        public async Task<Result<CurrentUser>> LoginAsync(LoginInput input)
        {
            var result = new Result<CurrentUser>();
            //假装加个盐..
            input.Password = GetCiphertext(input.Password, _options.CurrentValue.PasswordSalt);
            var user = await _movieResourceContext.Users
                .FirstOrDefaultAsync(u => u.Email == input.Email && u.Password == input.Password);
            if (user == null)
            {
                result.Code = ErrorCodeEnum.PassworError;
                result.Message = ErrorCodeEnum.PassworError.GetRemark();
                return result;
            }
            else if (user.Status == UserStatusEnum.Locked)
            {
                result.Code = ErrorCodeEnum.AccountLocked;
                result.Message = ErrorCodeEnum.AccountLocked.GetRemark();
                return result;
            }
            //默认头像
            user.Avatar = string.IsNullOrWhiteSpace(user.Avatar) ? "avatar/default.jpg" : user.Avatar;

            //创建身份
            await CreateIdentityAsync(input.Remember, user);

            //更新登录时间
            user.LastLoginTime = DateTime.Now;
            await _movieResourceContext.SaveChangesAsync();

            result.Content = Mapper.Map<CurrentUser>(user);
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

        public async Task LogoutAsync()
        {
            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async Task CreateIdentityAsync(bool remember, EntityFramework.Models.User user)
        {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
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

        public async Task<bool> AccountIsExistAsync(string email)
        {
            return await _movieResourceContext.Users.AnyAsync(s => s.Email == email);
        }

        public const string RegisterVerifyCodeName = "RegisterVerifyCode";
        public async Task<Result<bool>> SendVerifyCodeAsync(string email)
        {
            var result = new Result<bool>();
            //是否已注册
            var isExist = await AccountIsExistAsync(email);
            if (isExist)
            {
                result.Code = ErrorCodeEnum.Registered;
                result.Message = ErrorCodeEnum.Registered.GetRemark();
                result.Content = false;
                return result;
            }

            //发验证码邮件
            try
            {
                string verifyCode = new Random().Next(100000, 999999).ToString();
                _httpContext.HttpContext.Session.SetString(RegisterVerifyCodeName, verifyCode);
                await SendEmailHelper.SendEmailAsync(new EmailConfig
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
                result.Code = ErrorCodeEnum.VerifyCodeSendFailed;
                result.Message = ErrorCodeEnum.VerifyCodeSendFailed.GetRemark();
                result.Content = false;
            }
            return result;
        }

        public async Task<Result<bool>> RegisterAsync(RegisterInput input)
        {
            var result = new Result<bool>();
            //校验验证码
            var verifyCode = _httpContext.HttpContext.Session.GetString(RegisterVerifyCodeName);
            if (verifyCode != input.VerifyCode)
            {
                result.Code = ErrorCodeEnum.VerifyCodeError;
                result.Message = ErrorCodeEnum.VerifyCodeError.GetRemark();
                result.Content = false;
                return result;
            }
            //注册
            var entity = new EntityFramework.Models.User
            {
                Avatar = "",
                Email = input.Email,
                NickName = input.Email,//Guid.NewGuid().ToString("N"),
                Password = GetCiphertext(input.Password, _options.CurrentValue.PasswordSalt),
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now,
                Status = UserStatusEnum.Normal
            };
            var user = await _movieResourceContext.Users.AddAsync(entity);
            if (await _movieResourceContext.SaveChangesAsync() < 1)
            {
                result.Code = ErrorCodeEnum.Failed;
                result.Message = ErrorCodeEnum.Failed.GetRemark();
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

        public XianLiaoOutput GetXianLiaoUserInfo()
        {
            var user = GetCurrentUser();
            XianLiaoOutput xianLiaoOutput = new XianLiaoOutput();
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
                xianLiaoOutput.Avatar = "default.jpg";
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

        public async Task<PagingOutput<UserManageOutput>> GetUsersAsync(PagingInput<UserManage_Search_Input> pagingInput)
        {
            var query = _movieResourceContext.Users.AsQueryable();
            
            //TODO:有必要封装一下分页查询(条件拼接、分页、排序)

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
            if (!string.IsNullOrWhiteSpace(pagingInput.OrderBy?.Field))
            {
                var orderByField = typeof(EntityFramework.Models.User).GetProperty(pagingInput.OrderBy.Field);
                if (orderByField != null)
                {
                    if (orderByField != null && pagingInput.OrderBy?.Type == "desc")
                        query = query.OrderByDescending(s => orderByField);
                    else
                        query = query.OrderBy(s => orderByField);
                }
            }
            //分页
            var users = await query.Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<UserManageOutput>()
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<UserManageOutput>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = users,
                TotalCount = totalCount
            };
            return result;
        }
    }
}
