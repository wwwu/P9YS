using P9YS.Services.MovieResource.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.MovieDraft.Dto
{
    public class MovieDraft_Detail_Input
    {
        public int MovieDraftId { get; set; }

        [Required]
        [StringLength(50)]
        public string ShortName { get; set; }

        [StringLength(200)]
        [Required]
        public string FullName { get; set; }

        [StringLength(200)]
        public string OtherName { get; set; }

        [StringLength(200)]
        public string Director { get; set; }

        [StringLength(200)]
        public string Actor { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public int MovieTime { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public string Intro { get; set; }

        public decimal Score { get; set; }

        public long ScoreCount { get; set; }

        public decimal DoubanScore { get; set; }

        [Required]
        [StringLength(200)]
        public string DoubanUrl { get; set; }

        public int MovieAreaId { get; set; }

        [Required]
        public List<string> MovieTypes { get; set; }

        [Required]
        public MovieResource_Input MovieResource { get; set; }

        public List<MovieOnlinePlay_Output> MovieOnlinePlays { get; set; }
    }
}
