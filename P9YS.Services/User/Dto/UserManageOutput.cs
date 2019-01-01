using P9YS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.User.Dto
{
    public class UserManageOutput
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }

        public string Avatar { get; set; }

        public UserStatusEnum Status { get; set; }

        public UserRoleEnum Role { get; set; }

        public DateTime RegisterTime { get; set; }

        public DateTime LastLoginTime { get; set; }
    }
}
