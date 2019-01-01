using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P9YS.Common;
using P9YS.Services;
using P9YS.Services.RatingRecord;
using P9YS.Services.RatingRecord.Dto;

namespace P9YS.Manage.Controllers
{
    [Authorize(Roles = UserRoleConst.Admin)]
    public class MovieController : Controller
    {
        private readonly IRatingRecordService _ratingRecordService;

        public MovieController(IRatingRecordService ratingRecordService)
        {
            this._ratingRecordService = ratingRecordService;
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
    }
}