using P9YS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieRecommend.Dto
{
    public class MovieRecommendInput
    {

        public int MovieId { get; set; }

        public RecommendTypeEnum Type { get; set; }

        public int Sort { get; set; }
    }
}
