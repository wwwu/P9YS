using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.User.Dto
{
    public class CurrentUser
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
    }
}
