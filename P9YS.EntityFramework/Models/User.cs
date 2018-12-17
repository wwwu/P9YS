using P9YS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(200)]
        [Required]
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        public string NickName { get; set; }

        [StringLength(200)]
        [Required]
        public string Avatar { get; set; }

        [StringLength(100)]
        [Required]
        public string Password { get; set; }

        [Required]
        public UserStatusEnum Status { get; set; }

        public UserRoleEnum Role { get; set; }

        [Required]
        public DateTime RegisterTime { get; set; }

        [Required]
        public DateTime LastLoginTime { get; set; }
    }
}
