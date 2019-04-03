using Microsoft.AspNetCore.Mvc;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.ViewComponents
{
    public class XianLiaoJavascriptViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        public XianLiaoJavascriptViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            var model = _userService.GetXianLiaoUserInfo();
            return View(model);
        }
    }
}
