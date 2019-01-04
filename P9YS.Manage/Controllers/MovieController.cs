using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Common;
using P9YS.Services;
using P9YS.Services.MovieComment;
using P9YS.Services.RatingRecord;
using P9YS.Services.RatingRecord.Dto;

namespace P9YS.Manage.Controllers
{
    [Authorize(Roles = UserRoleConst.Admin)]
    public class MovieController : Controller
    {
        private readonly IRatingRecordService _ratingRecordService;
        private readonly IMovieCommentService _movieCommentService;

        public MovieController(IRatingRecordService ratingRecordService
            , IMovieCommentService movieCommentService)
        {
            this._ratingRecordService = ratingRecordService;
            this._movieCommentService = movieCommentService;
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
    }
}