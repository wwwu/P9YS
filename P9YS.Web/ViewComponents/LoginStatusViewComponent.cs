using Microsoft.AspNetCore.Mvc;
using P9YS.Services.User;
using P9YS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.ViewComponents
{
    public class LoginStatusViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        public LoginStatusViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        /* 
         * 约定的方法名称, Invoke / InvokeAsync
         * 页面文件的路径规则如下
         * /Views/{CurrentControllerName}/Components/{ComponentName}/Default.cshtml
         * /Views/Shared/Components/{ComponentName}/Default.cshtml
         */
        public IViewComponentResult Invoke()
        {
            var model = new CurrentUserViewModel
            {
                CurrentUser = _userService.GetCurrentUser()
            };
            return View(model);
        }
    }
}
