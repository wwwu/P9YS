using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Common;

namespace P9YS.Manage.Controllers
{
    [Authorize(Roles = UserRoleConst.Admin)]
    public class MovieController : Controller
    {
        public MovieController()
        {
        }
    }
}