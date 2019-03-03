using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.Movie.Dto
{
    public class Movie_Manage_Input
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string ShortName { get; set; }

        [StringLength(100)]
        [Required]
        public string FullName { get; set; }

        [StringLength(500)]
        public string OtherName { get; set; }

        [StringLength(200)]
        public string Director { get; set; }

        [StringLength(500)]
        public string Actor { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int MovieTime { get; set; }

        public string Intro { get; set; }

        public int SeriesId { get; set; }

        public int MovieAreaId { get; set; }

        [Required]
        public IEnumerable<int> MovieTypes { get; set; }

        public string ImgData { get; set; }
    }
}
