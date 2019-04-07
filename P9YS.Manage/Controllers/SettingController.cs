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
    [Authorize]
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
                Content = await _carouselService.GetCarousels(pagingInput)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpPost]
        public async Task<JsonResult> AddCarousel(Carousel_Input carouselnput)
        {
            var result = await _carouselService.AddCarousel(carouselnput);
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpPost]
        public async Task<JsonResult> UpdCarousel(Carousel_Input carouselnput)
        {
            var result = await _carouselService.UpdCarousel(carouselnput);
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
                Content = await _movieRecommendService.GetRecommends(recommendType)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpPost]
        public async Task<JsonResult> AddRecommend(MovieRecommend_Input movieRecommend)
        {
            var result = new Result
            {
                Content = await _movieRecommendService.AddRecommend(movieRecommend)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpDelete]
        public async Task<JsonResult> DelRecommend(int id)
        {
            var result = new Result
            {
                Content = await _movieRecommendService.DelRecommend(id)
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
                Content = await _movieAreaService.GetMovieAreas(pagingInput)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpPost]
        public async Task<JsonResult> AddArea(MovieArea_Input movieAreaInput)
        {
            var result = await _movieAreaService.AddMovieArea(movieAreaInput);
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpPost]
        public async Task<JsonResult> UpdArea(MovieArea_Input movieAreaInput)
        {
            var result = await _movieAreaService.UpdMovieArea(movieAreaInput);
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
                Content = await _movieGenreService.GetMovieGenres(pagingInput)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpPost]
        public async Task<JsonResult> AddGenre(MoiveGenre_Input moiveGenreInput)
        {
            var result = await _movieGenreService.AddMovieGenre(moiveGenreInput);
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpPost]
        public async Task<JsonResult> UpdGenre(MoiveGenre_Input moiveGenreInput)
        {
            var result = await _movieGenreService.UpdMovieGenre(moiveGenreInput);
            return Json(result);
        }

        #endregion
    }
}