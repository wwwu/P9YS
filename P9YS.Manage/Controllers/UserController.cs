using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Common;
using P9YS.Services;
using P9YS.Services.User;
using P9YS.Services.User.Dto;

namespace P9YS.Manage.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpGet]
        public async Task<JsonResult> GetUsers(PagingInput<UserManage_Search_Input> pagingInput)
        {
            var result = new Result
            {
                Content = await _userService.GetUsers(pagingInput)
            };
            return Json(result);
        }
    }
}