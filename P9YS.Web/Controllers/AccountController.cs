using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace P9YS.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("http://ht.p9ys.com");
        }
    }
}