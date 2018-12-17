using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.User.Dto
{
    public class LoginInput
    {
        [EmailAddress(ErrorMessage = "请输入正确的邮箱格式")]
        [Required(ErrorMessage = "邮箱不能为空")]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码是6-20位")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
