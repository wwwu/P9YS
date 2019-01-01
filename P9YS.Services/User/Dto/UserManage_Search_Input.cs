using P9YS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.User.Dto
{
    public class UserManage_Search_Input
    {
        [StringLength(100, MinimumLength = 1)]
        public string Email { get; set; }

        [StringLength(50,MinimumLength = 1)]
        public string NickName { get; set; }

        public UserStatusEnum? Status { get; set; }

        public UserRoleEnum? Role { get; set; }
    }
}
