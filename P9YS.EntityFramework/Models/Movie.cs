using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string ShortName { get; set; }

        [StringLength(200)]
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

        public string ImgUrl { get; set; }

        public string Intro { get; set; }

        public decimal Score { get; set; }

        public decimal ScoreSum { get; set; }

        public long ScoreCount { get; set; }

        public int Hot { get; set; }

        public long HotSum { get; set; }

        public int SeriesId { get; set; }

        [Display(Name = "添加时间")]
        public DateTime AddTime { get; set; }

        [Display(Name = "更新时间")]
        public DateTime UpdTime { get; set; }

        public int MovieAreaId { get; set; }

        [ForeignKey("MovieAreaId")]
        public virtual MovieArea MovieArea { get; set; }

        public virtual IEnumerable<MovieType> MovieTypes { get; set; }

        public virtual IEnumerable<MovieOrigin> MovieOrigins { get; set; }

        public virtual IEnumerable<MovieOnlinePlay> MovieOnlinePlays { get; set; }

        public virtual IEnumerable<MovieComment> MovieComments { get; set; }

        public virtual IEnumerable<MovieQuestion> MovieQuestions { get; set; }

        public virtual IEnumerable<MovieResource> MovieResources { get; set; }
    }
}
