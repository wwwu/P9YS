using P9YS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.Carousel.Dto
{
    public class Carouselnput
    {
        public int Id { get; set; }

        [StringLength(200)]
        [Required]
        public string Link { get; set; }

        public string ImgData { get; set; }

        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        public CarouselStateEnum State { get; set; }
    }
}
