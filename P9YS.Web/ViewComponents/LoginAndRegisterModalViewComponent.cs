using Microsoft.AspNetCore.Mvc;
using P9YS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.ViewComponents
{
    public class LoginAndRegisterModalViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewData["IsAuthenticated"] = HttpContext.User.Identity.IsAuthenticated;
            return View(new LoginAndRegisterModalViewModel());
        }
    }
}
