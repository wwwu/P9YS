using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Services;
using P9YS.Services.User;
using P9YS.Services.User.Dto;
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
            var result = await _userService.LoginAsync(input.LoginInput);
            return Json(result);
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SendVerifyCode(string email)
        {
            var result = await _userService.SendVerifyCodeAsync(email);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginAndRegisterModalViewModel input)
        {
            var result = await _userService.RegisterAsync(input.RegisterInput);
            return Json(result);
        }
    }
}