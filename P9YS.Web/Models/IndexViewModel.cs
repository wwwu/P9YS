using P9YS.Services;
using P9YS.Services.Carousel.Dto;
using P9YS.Services.Movie.Dto;
using P9YS.Services.MovieArea.Dto;
using P9YS.Services.MovieGenres.Dto;
using P9YS.Services.MovieRecommend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.Models
{
    public class IndexViewModel
    {
        public List<Carousel_Output> Carousels { get; set; }

        public List<MovieArea_Output> MovieAreas { get; set; }

        public List<MovieGenre_Output> MovieGenres { get; set; }

        /// <summary>
        /// 年度推荐
        /// </summary>
        public List<MovieRecommend_Output> AnnualRecommends { get; set; }

        /// <summary>
        /// 近期推荐
        /// </summary>
        public List<MovieRecommend_Output> RecentRecommends { get; set; }

        public PagingOutput<MovieList_Output> MovieList { get; set; }
    }
}
