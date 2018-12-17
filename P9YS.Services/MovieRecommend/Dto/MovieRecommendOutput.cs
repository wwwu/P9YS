using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieRecommend.Dto
{
    public class MovieRecommendOutput
    {
        public int MovieId { get; set; }
        public string MovieShortName { get; set; }
        public string MovieImgUrl { get; set; }
        public decimal MovieScore { get; set; }
        public string MovieActor { get; set; }
        public string MovieDirector { get; set; }
        public string MovieIntro { get; set; }
    }
}
