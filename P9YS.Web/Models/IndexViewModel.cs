using P9YS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.Models
{
    public class IndexViewModel
    {
        public List<Services.Carousel.Dto.CarouselOutput> Carousels { get; set; }

        public List<Services.MovieArea.Dto.MovieAreaOutput> MovieAreas { get; set; }

        public List<Services.MovieGenres.Dto.MovieGenreOutput> MovieGenres { get; set; }

        /// <summary>
        /// 年度推荐
        /// </summary>
        public List<Services.MovieRecommend.Dto.MovieRecommendOutput> AnnualRecommends { get; set; }

        /// <summary>
        /// 近期推荐
        /// </summary>
        public List<Services.MovieRecommend.Dto.MovieRecommendOutput> RecentRecommends { get; set; }

        public PagingOutput<Services.Movie.Dto.MovieListOutput> MovieList { get; set; }
    }
}
