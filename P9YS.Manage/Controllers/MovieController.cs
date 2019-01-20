using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Common;
using P9YS.Services;
using P9YS.Services.Movie;
using P9YS.Services.Movie.Dto;
using P9YS.Services.MovieComment;
using P9YS.Services.MovieResource;
using P9YS.Services.MovieResource.Dto;
using P9YS.Services.RatingRecord;
using P9YS.Services.RatingRecord.Dto;
using P9YS.Services.User;

namespace P9YS.Manage.Controllers
{
    [Authorize(Roles = UserRoleConst.Admin)]
    public class MovieController : Controller
    {
        private readonly IRatingRecordService _ratingRecordService;
        private readonly IMovieCommentService _movieCommentService;
        private readonly IMovieResourceService _movieResourceService;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;

        public MovieController(IRatingRecordService ratingRecordService
            , IMovieCommentService movieCommentService
            , IMovieResourceService movieResourceService
            , IUserService userService
            , IMovieService movieService)
        {
            this._ratingRecordService = ratingRecordService;
            this._movieCommentService = movieCommentService;
            this._movieResourceService = movieResourceService;
            this._userService = userService;
            this._movieService = movieService;
        }

        #region 新片审核

        public IActionResult Verify()
        {
            return View();
        }

        #endregion

        #region 影片管理

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            return View(id);
        }

        public async Task<JsonResult> GetMovies(PagingInput<ConditionInput> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieService.GetMoviesAsync(pagingInput)
            };
            return Json(result);
        }

        public async Task<JsonResult> GetMovie(int movieId)
        {
            var result = new Result
            {
                Content = await _movieService.GetMovieAsync(movieId)
            };
            return Json(result);
        }

        [HttpPost]
        public async Task<Result> UpdMovie(Movie_Manage_Input input)
        {
            return await _movieService.UpdMovieAsync(input);
        }

        [HttpDelete]
        public async Task<Result> DelMovie(int movieId)
        {
            return await _movieService.DelMovieAsync(movieId);
        }

        #endregion

        #region 评分记录

        public IActionResult Rating()
        {
            return View();
        }

        public async Task<JsonResult> GetRatings(PagingInput<GetRatingsInput> pagingInput)
        {
            var result = new Result
            {
                Content = await _ratingRecordService.GetRatingsAsync(pagingInput)
            };
            return Json(result);
        }

        #endregion
        
        #region 留言记录

        public IActionResult Comment()
        {
            return View();
        }

        public async Task<JsonResult> GetComments(PagingInput<GetRatingsInput> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieCommentService.GetCommentsAsync(pagingInput)
            };
            return Json(result);
        }

        public async Task<JsonResult> DelComment(int id)
        {
            var result = await _movieCommentService.DelCommentAsync(id);
            return Json(result);
        }

        #endregion

        #region 资源管理

        public IActionResult Resource()
        {
            return View();
        }

        public async Task<JsonResult> GetResources(PagingInput<MovieResource_Search_Input> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieResourceService.GetResourcesAsync(pagingInput)
            };
            return Json(result);
        }

        public async Task<JsonResult> AddResource(MovieResourceInput movieResourceInput)
        {
            movieResourceInput.UserId = _userService.GetCurrentUser()?.UserId ?? 0;
            var result = await _movieResourceService.AddResourceAsync(movieResourceInput);
            return Json(result);
        }

        public async Task<JsonResult> UpdResource(MovieResourceInput movieResourceInput)
        {
            var result = await _movieResourceService.UpdResourceAsync(movieResourceInput);
            return Json(result);
        }

        public async Task<JsonResult> DelResource(int id)
        {
            var result = await _movieResourceService.DelResourceAsync(id);
            return Json(result);
        }

        #endregion
    }
}