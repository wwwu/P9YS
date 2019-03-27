using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeetestSDK;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using P9YS.Services.User;
using P9YS.Web.Models;

namespace P9YS.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOptions<GeetestOptions> _options;

        public MemberController(IUserService userService
            , IOptions<GeetestOptions> options)
        {
            _userService = userService;
            _options = options;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginAndRegisterModalViewModel input)
        {
            GeetestLib geetest = new GeetestLib(_options, new LoggerFactory());
            var gt_server_status_code = Convert.ToByte(HttpContext.Session.GetString(GeetestLib.gtServerStatusSessionKey));
            int gtStatus = 0;
            string challenge = input.geetest_challenge ?? string.Empty;
            string validate = input.geetest_validate ?? string.Empty;
            string seccode = input.geetest_seccode ?? string.Empty;
            if (gt_server_status_code == 1) gtStatus = await geetest.enhencedValidateRequest(challenge, validate, seccode);
            else gtStatus = geetest.failbackValidateRequest(challenge, validate, seccode);
            if (gtStatus != 1)
            {
                return Json(new Services.Result(Common.CustomCodeEnum.VerifyCodeError));
            }
            var result = await _userService.Login(input.LoginInput);
            return Json(result);
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SendVerifyCode(string email)
        {
            var result = await _userService.SendVerifyCode(email);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginAndRegisterModalViewModel input)
        {
            var result = await _userService.Register(input.RegisterInput);
            return Json(result);
        }

        public async Task<string> GetCaptcha()
        {
            GeetestLib geetest = new GeetestLib(_options, new LoggerFactory());
            byte gtServerStatus = await geetest.preProcess("", "web", HttpContext.Connection.RemoteIpAddress.ToString());
            HttpContext.Session.SetString(GeetestLib.gtServerStatusSessionKey,gtServerStatus.ToString());
            var result = geetest.getResponseStr();
            return result;
        }
    }
}