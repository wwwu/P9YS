using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    /// <summary>
    /// 地区(国家)
    /// </summary>
    public class MovieArea
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Area { get; set; }

        public int Other { get; set; }

        public DateTime AddTime { get; set; }
    }
}
