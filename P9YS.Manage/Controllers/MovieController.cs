using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Common;
using P9YS.Services;
using P9YS.Services.Movie;
using P9YS.Services.Movie.Dto;
using P9YS.Services.MovieComment;
using P9YS.Services.MovieDraft;
using P9YS.Services.MovieDraft.Dto;
using P9YS.Services.MovieResource;
using P9YS.Services.MovieResource.Dto;
using P9YS.Services.RatingRecord;
using P9YS.Services.RatingRecord.Dto;
using P9YS.Services.User;

namespace P9YS.Manage.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IRatingRecordService _ratingRecordService;
        private readonly IMovieCommentService _movieCommentService;
        private readonly IMovieResourceService _movieResourceService;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly IMovieDraftService _movieDraftService;

        public MovieController(IRatingRecordService ratingRecordService
            , IMovieCommentService movieCommentService
            , IMovieResourceService movieResourceService
            , IUserService userService
            , IMovieService movieService
            , IMovieDraftService movieDraftService)
        {
            _ratingRecordService = ratingRecordService;
            _movieCommentService = movieCommentService;
            _movieResourceService = movieResourceService;
            _userService = userService;
            _movieService = movieService;
            _movieDraftService = movieDraftService;
        }

        #region 新片审核

        public IActionResult Verify()
        {
            return View();
        }

        public async Task<JsonResult> GetVerifyList(PagingInput<GetMovieDrafts_Condition_Input> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieDraftService.GetMovieDrafts(pagingInput)
            };
            return Json(result);
        }

        public async Task<JsonResult> GetMovieDraftDetail(int movieDraftId)
        {
            var result = new Result
            {
                Content = await _movieDraftService.GetMovieDraftDetail(movieDraftId)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        public async Task<JsonResult> AddMovie(MovieDraft_Detail_Input movieDraftDetailInput)
        {
            var user = _userService.GetCurrentUser();
            movieDraftDetailInput.MovieResource.UserId = user.UserId;
            var result = await _movieDraftService.AddMovie(movieDraftDetailInput);
            return Json(result);
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

        public async Task<JsonResult> GetMovies(PagingInput<GetMovies_Condition_Input> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieService.GetMovies(pagingInput)
            };
            return Json(result);
        }

        public async Task<JsonResult> GetMovie(int movieId)
        {
            var result = new Result
            {
                Content = await _movieService.GetMovie(movieId)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpPost]
        public async Task<Result> UpdMovie(Movie_Manage_Input input)
        {
            return await _movieService.UpdMovie(input);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        [HttpDelete]
        public async Task<Result> DelMovie(int movieId)
        {
            return await _movieService.DelMovie(movieId);
        }

        #endregion

        #region 评分记录

        public IActionResult Rating()
        {
            return View();
        }

        public async Task<JsonResult> GetRatings(PagingInput<GetRatings_Input> pagingInput)
        {
            var result = new Result
            {
                Content = await _ratingRecordService.GetRatings(pagingInput)
            };
            return Json(result);
        }

        #endregion
        
        #region 留言记录

        public IActionResult Comment()
        {
            return View();
        }

        public async Task<JsonResult> GetComments(PagingInput<GetRatings_Input> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieCommentService.GetComments(pagingInput)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        public async Task<JsonResult> DelComment(int id)
        {
            var result = await _movieCommentService.DelComment(id);
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
                Content = await _movieResourceService.GetResources(pagingInput)
            };
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        public async Task<JsonResult> AddResource(MovieResource_Input movieResourceInput)
        {
            movieResourceInput.UserId = _userService.GetCurrentUser()?.UserId ?? 0;
            var result = await _movieResourceService.AddResource(movieResourceInput);
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        public async Task<JsonResult> UpdResource(MovieResource_Input movieResourceInput)
        {
            var result = await _movieResourceService.UpdResource(movieResourceInput);
            return Json(result);
        }

        [Authorize(Roles = UserRoleConst.Admin)]
        public async Task<JsonResult> DelResource(int id)
        {
            var result = await _movieResourceService.DelResource(id);
            return Json(result);
        }

        #endregion
    }
}