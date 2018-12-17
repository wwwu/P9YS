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
using P9YS.Services.MovieQuestion;
using P9YS.Services.MovieQuestion.Dto;
using P9YS.Services.MovieResource;
using P9YS.Services.RatingRecord;
using P9YS.Services.RatingRecord.Dto;
using P9YS.Services.SuportRecord;
using P9YS.Services.SuportRecord.Dto;
using P9YS.Web.Models;

namespace P9YS.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieCommentService _movieCommentService;
        private readonly IMovieQuestionService _movieQuestionService;
        private readonly IMovieResourceService _movieResourceService;
        private readonly IRatingRecordService _ratingRecordService;
        private readonly ISuportRecordService _suportRecordService;
        public MovieController(IMovieService movieService
            , IMovieCommentService movieCommentService
            , IMovieQuestionService movieQuestionService
            , IMovieResourceService movieResourceService
            , IRatingRecordService ratingRecordService
            , ISuportRecordService suportRecordService)
        {
            _movieService = movieService;
            _movieCommentService = movieCommentService;
            _movieQuestionService = movieQuestionService;
            _movieResourceService = movieResourceService;
            _ratingRecordService = ratingRecordService;
            _suportRecordService = suportRecordService;
        }

        #region  movie

        public async Task<IActionResult> Index(int id)
        {
            var viewModel = new MovieViewModel();
            var movie = await _movieService.GetMovieInfoAsync(id);
            if (movie == null)
                return NotFound();

            viewModel.MovieInfo = movie;
            viewModel.MovieSeries = await _movieService.GetMovieSeriesAsync(movie.SeriesId);
            viewModel.MovieOrigin = await _movieService.GetMovieOriginAsync(id);
            viewModel.MovieCommentsCount = await _movieCommentService.GetCommentsCountByMovieAsync(id);
            viewModel.MovieComments = await _movieCommentService
                .GetCommentsAndReplyAsync(new PagingInput<int> { Condition = id });
            viewModel.QuestionTitles = await _movieQuestionService
                .GetQuestionTitlesAsync(new PagingInput<int> { Condition = id, PageSize = 20 });

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> MovieRating([FromBody]RatingRecordInput ratingRecordInput)
        {
            var result = new Result<bool>
            {
                Content = await _ratingRecordService.AddRatingRecord(ratingRecordInput)
            };
            return Json(result);
        }
        
        [HttpPost]
        public async Task<JsonResult> GetPageList([FromBody]PagingInput<ConditionInput> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieService.GetMoviesByConditionAsync(pagingInput)
            };
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetMovieComments(PagingInput<int> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieCommentService.GetCommentsAndReplyAsync(pagingInput)
            };
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> AddMovieComment([FromBody]Services.MovieComment.Dto.MovieCommentInput movieCommentInput)
        {
            var result = new Result
            {
                Content = await _movieCommentService.AddMovieCommentAsync(movieCommentInput)
            };
            return Json(result);
        }

        #endregion

        #region question

        [HttpGet]
        public async Task<JsonResult> GetQuestions(PagingInput<int> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieQuestionService.GetQuestionTitlesAsync(pagingInput)
            };
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> AddQuestion([FromBody]QuestionInput questionInput)
        {
            var result = new Result
            {
                Content = await _movieQuestionService.AddQuestionAsync(questionInput)
            };             
            return Json(result);
        }

        public async Task<IActionResult> Question(int id)
        {
            var viewModel = new MovieQuestionViewModel();
            var question = await _movieQuestionService.GetQuestionAsync(id);
            if (question == null)
                return NotFound();

            var questions = await _movieQuestionService
                .GetQuestionTitlesAsync(new PagingInput<int> { Condition = question.MovieId, PageSize = 10 });
            viewModel.QuestionInfo = question;
            viewModel.MovieInfo = await _movieService.GetMovieInfoAsync(question.MovieId);
            viewModel.QuestionTitles = questions.Data;
            viewModel.QuestionAnswers = await _movieQuestionService
                .GetQuestionAnswersAsync(new PagingInput<int> { Condition= id, PageSize = 10 });

            return View(viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetQuestionAnswers(PagingInput<int> pagingInput)
        {
            var result = new Result
            {
                Content = await _movieQuestionService.GetQuestionAnswersAsync(pagingInput)
            };
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> AddQuestionAnswer([FromBody]QuestionAnswerInput questionAnswerInput)
        {
            var result = new Result
            {
                Content = await _movieQuestionService.AddQuestionAnswerAsync(questionAnswerInput)
            };
            return Json(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> SupportAnswer([FromBody]SuportRecordInput suportRecordInput)
        {
            var result = new Result<bool>
            {
                Content = await _suportRecordService.AddSuportRecord(suportRecordInput)
            };
            return Json(result);
        }

        #endregion

        #region resource

        public async Task<IActionResult> Resource(int id)
        {
            var viewModel = new MovieResourceViewModel
            {
                MovieInfo = await _movieService.GetMovieInfoAsync(id),
                MovieOnlinePlays = await _movieResourceService.GetMovieOnlinePlays(id),
                MovieResources = await _movieResourceService.GetMovieResources(id)
            };
            return View(viewModel);
        }

        #endregion
    }
}