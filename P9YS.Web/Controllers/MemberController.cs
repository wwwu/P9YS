using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P9YS.Services.User;
using P9YS.Web.Models;

namespace P9YS.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IUserService _userService;
        public MemberController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginAndRegisterModalViewModel input)
        {
            var result = await _userService.Login(input.LoginInput);
            result.Content.Email.HideEmail();
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
    }
}