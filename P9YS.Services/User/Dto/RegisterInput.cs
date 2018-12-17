using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.User.Dto
{
    public class RegisterInput
    {
        [EmailAddress(ErrorMessage = "请输入正确的邮箱")]
        [Required(ErrorMessage = "邮箱不能为空")]
        public string Email { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "请输入邮箱收到的验证码")]
        [Required(ErrorMessage = "请输入邮箱收到的验证码")]
        public string VerifyCode { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码位数是6-20位")]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        [Required(ErrorMessage = "请再次输入密码")]
        public string PasswordAg { get; set; }
    }
}
