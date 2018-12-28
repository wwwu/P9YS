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

        public SettingController(IMovieService movieService
            , ICarouselService carouselService
            , IMovieRecommendService movieRecommendService)
        {
            _movieService = movieService;
            _carouselService = carouselService;
            _movieRecommendService = movieRecommendService;
        }

        #region Carousel

        public IActionResult Carousel()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCarousels(PagingInput pagingInput)
        {
            var result = new Result<object>
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

        #region Recomment

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
    }
}