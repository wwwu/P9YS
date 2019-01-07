using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Common;
using P9YS.Services;
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

        public MovieController(IRatingRecordService ratingRecordService
            , IMovieCommentService movieCommentService
            , IMovieResourceService movieResourceService
            , IUserService userService)
        {
            this._ratingRecordService = ratingRecordService;
            this._movieCommentService = movieCommentService;
            this._movieResourceService = movieResourceService;
            this._userService = userService;
        }

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