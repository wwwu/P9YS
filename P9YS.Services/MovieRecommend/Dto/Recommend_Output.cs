using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieRecommend.Dto
{
    public class Recommend_Output
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieShortName { get; set; }
        public int Sort { get; set; }
        public DateTime AddTime { get; set; }
    }
}
