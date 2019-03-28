using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using P9YS.Common;

namespace P9YS.Manage.Controllers
{
    public class MemberController : Controller
    {
        private readonly IOptionsMonitor<AppSettings> _options;

        public MemberController(IOptionsMonitor<AppSettings> options)
        {
            _options = options;
        }

        public IActionResult Login()
        {
            return Redirect(_options.CurrentValue.LoginUrl);
        }
    }
}