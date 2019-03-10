using P9YS.Services.MovieResource.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieDraft.Dto
{
    public class MovieDraftDetailOutput
    {
        public int Id { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string OtherName { get; set; }

        public string Director { get; set; }

        public string Actor { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int MovieTime { get; set; }

        public string ImgUrl { get; set; }

        public string Intro { get; set; }

        public decimal Score { get; set; }

        public int MovieAreaId { get; set; }

        public List<string> MovieTypes { get; set; }

        public List<Movie.Dto.MovieSeriesOutput> MovieSeries { get; set; }

        public MovieResource_Manage_Output MovieResource { get; set; }

        public List<MovieOnlinePlayOutput> MovieOnlinePlays { get; set; }
    }
}
