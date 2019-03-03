using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Common;
using P9YS.Common.Enums;
using P9YS.Services;
using P9YS.Services.Carousel;
using P9YS.Services.Carousel.Dto;
using P9YS.Services.Movie;
using P9YS.Services.MovieArea;
using P9YS.Services.MovieArea.Dto;
using P9YS.Services.MovieGenres;
using P9YS.Services.MovieGenres.Dto;
using P9YS.Services.MovieRecommend;
using P9YS.Services.MovieRecommend.Dto;

namespace P9YS.Manage.Controllers
{
    [Authorize(Roles = UserRoleConst.Admin)]
    public class SettingController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICarouselService _carouselService;
        private readonly IMovieRecommendService _movieRecommendService;
        private readonly IMovieAreaService _movieAreaService;
        private readonly IMovieGenreService _movieGenreService;

        public SettingController(IMovieService movieService
            , ICarouselService carouselService
            , IMovieRecommendService movieRecommendService
            , IMovieAreaService movieAreaService
            , IMovieGenreService movieGenreService)
        {
            _movieService = movieService;
            _carouselService = carouselService;
            _movieRecommendService = movieRecommendService;
            _movieAreaService = movieAreaService;
            _movieGenreService = movieGenreService;
        }

        #region 轮播

        public IActionResult Carousel()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCarousels(PagingInput pagingInput)
        {
            var result = new Result
            {
                Content = await _carouselService.GetCarouselsAsync(pagingInput)
            };
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddCarousel(Carouselnput carouselnput)
        {
            var result = await _carouselService.AddCarouselAsync(carouselnput);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdCarousel(Carouselnput carouselnput)
        {
            var result = await _carouselService.UpdCarouselAsync(carouselnput);
            return Json(result);
        }

        #endregion

        #region 推荐

        public IActionResult Recommend()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRecommends(RecommendTypeEnum recommendType)
        {
            var result = new Result
            {
                Content = await _movieRecommendService.GetRecommendsAsync(recommendType)
            };
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddRecommend(MovieRecommendInput movieRecommend)
        {
            var result = new Result
            {
                Content = await _movieRecommendService.AddRecommendAsync(movieRecommend)
            };
            return Json(result);
        }

        [HttpDelete]
        public async Task<JsonResult> DelRecommend(int id)
        {
            var result = new Result
            {
                Content = await _movieRecommendService.DelRecommendAsync(id)
            };
            return Json(result);
        }

        #endregion

        #region 地区

        public IActionResult Area()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAreas(PagingInput<string> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieAreaService.GetMovieAreasAsync(pagingInput)
            };
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddArea(MovieAreaInput movieAreaInput)
        {
            var result = await _movieAreaService.AddMovieAreaAsync(movieAreaInput);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdArea(MovieAreaInput movieAreaInput)
        {
            var result = await _movieAreaService.UpdMovieAreaAsync(movieAreaInput);
            return Json(result);
        }

        #endregion

        #region 类型

        public IActionResult Genre()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetGenres(PagingInput<string> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieGenreService.GetMovieGenresAsync(pagingInput)
            };
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AddGenre(MoiveGenreInput moiveGenreInput)
        {
            var result = await _movieGenreService.AddMovieGenreAsync(moiveGenreInput);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdGenre(MoiveGenreInput moiveGenreInput)
        {
            var result = await _movieGenreService.UpdMovieGenreAsync(moiveGenreInput);
            return Json(result);
        }

        #endregion
    }
}