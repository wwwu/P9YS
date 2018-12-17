using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Services;
using P9YS.Services.Carousel;
using P9YS.Services.Movie;
using P9YS.Services.MovieArea;
using P9YS.Services.MovieGenres;
using P9YS.Services.MovieRecommend;
using P9YS.Web.Models;

namespace P9YS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarouselService _carouselService;
        private readonly IMovieAreaService _movieAreaService;
        private readonly IMovieGenreService _movieGenreService;
        private readonly IMovieRecommendService _movieRecommendService;
        private readonly IMovieService _movieService;
        public HomeController(ICarouselService carouselService
            , IMovieAreaService movieAreaService
            , IMovieGenreService movieGenreService
            , IMovieRecommendService movieRecommendService
            , IMovieService movieService)
        {
            _carouselService = carouselService;
            _movieAreaService = movieAreaService;
            _movieGenreService = movieGenreService;
            _movieRecommendService = movieRecommendService;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Carousels = await _carouselService.GetCarouselsAsync(),
                MovieAreas = await _movieAreaService.GetMovieAreasAsync(),
                MovieGenres = await _movieGenreService.GetMovieGenresAsync(),
                AnnualRecommends = await _movieRecommendService.GetAnnualRecommendsAsync(),
                RecentRecommends = await _movieRecommendService.GetRecentRecommendsAsync(),
                MovieList = await _movieService
                    .GetMoviesByConditionAsync(new PagingInput<Services.Movie.Dto.ConditionInput> { PageSize = 1 })
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
