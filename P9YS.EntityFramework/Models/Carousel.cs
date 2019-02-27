using P9YS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class Carousel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        [Required]
        public string Link { get; set; }

        [StringLength(200)]
        [Required]
        public string ImgUrl { get; set; }

        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        public CarouselStateEnum State { get; set; }
    }
}
