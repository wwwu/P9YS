using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.Movie.Dto
{
    public class Movie_Manage_Output
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

        public long ScoreCount { get; set; }

        public int Hot { get; set; }

        public long HotSum { get; set; }

        public int SeriesId { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime UpdTime { get; set; }

        public int MovieAreaId { get; set; }

        public string MovieAreaArea { get; set; }

        public IEnumerable<int> MovieTypes { get; set; }

        public IEnumerable<MovieSeriesOutput> MovieSeries { get; set; }
    }
}
