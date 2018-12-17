using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    /// <summary>
    /// 影片类型
    /// </summary>
    public class MovieGenre
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public DateTime AddTime { get; set; }
    }
}
