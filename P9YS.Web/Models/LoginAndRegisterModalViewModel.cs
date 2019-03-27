﻿using P9YS.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.Models
{
    public class LoginAndRegisterModalViewModel
    {
        public Login_Input LoginInput { get; set; }
        public Register_Input RegisterInput { get; set; }

        public string geetest_challenge { get; set; }
        public string geetest_validate { get; set; }
        public string geetest_seccode { get; set; }
    }
}
