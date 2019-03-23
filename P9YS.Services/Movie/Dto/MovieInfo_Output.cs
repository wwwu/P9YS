using P9YS.Services.MovieGenres.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.Movie.Dto
{
    public class MovieInfo_Output
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string OtherName { get; set; }
        public string MovieAreaName { get; set; }
        public string Director { get; set; }
        public string Actor { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MovieTime { get; set; }
        public string ImgUrl { get; set; }
        public string Intro { get; set; }
        public decimal Score { get; set; }
        public int SeriesId { get; set; }
        public List<MovieGenre_Output> MovieGenres { get; set; }
    }
}
