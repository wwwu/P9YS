using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P9YS.Common;
using P9YS.EntityFramework;
using P9YS.Services.SuportRecord;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.MovieQuestion
{
    public class MovieQuestionService : IMovieQuestionService
    {
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        public MovieQuestionService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IUserService userService)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
            _userService = userService;
        }

        public async Task<PagingOutput<Dto.QuestionTitleOutput>> GetQuestionTitles(PagingInput<int> pagingInput)
        {
            var query = _movieResourceContext.MovieQuestions
                .Where(s => s.MovieId == pagingInput.Condition);
            var questions = await query.OrderByDescending(s => s.AddTime)
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<Dto.QuestionTitleOutput>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<Dto.QuestionTitleOutput>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = questions,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<bool> AddQuestion(Dto.QuestionInput questionInput)
        {
            var user = _userService.GetCurrentUser();
            //Add
            var entity = Mapper.Map<EntityFramework.Models.MovieQuestion>(questionInput);
            entity.UserId = user.UserId;
            await _movieResourceContext.MovieQuestions.AddAsync(entity);
            var rows = await _movieResourceContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Dto.QuestionOutput> GetQuestion(int questionId)
        {
            var question = await _movieResourceContext.MovieQuestions
                .Where(s=>s.Id==questionId)
                .ProjectTo<Dto.QuestionOutput>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return question;
        }

        public async Task<PagingOutput<Dto.QuestionAnswerOutput>> GetQuestionAnswers(PagingInput<int> pagingInput)
        {
            var query = _movieResourceContext.MovieQuestionAnswers
                .Where(s => s.MovieQuestionId == pagingInput.Condition);
            var answers = await query.Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<Dto.QuestionAnswerOutput>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<Dto.QuestionAnswerOutput>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = answers,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<bool> AddQuestionAnswer(Dto.QuestionAnswerInput questionAnswerInput)
        {
            var user = _userService.GetCurrentUser();
            //Add
            var entity = Mapper.Map<EntityFramework.Models.MovieQuestionAnswer>(questionAnswerInput);
            entity.UserId = user.UserId;
            await _movieResourceContext.MovieQuestionAnswers.AddAsync(entity);
            //更新AnswerCount
            var question = await _movieResourceContext.MovieQuestions
                .FindAsync(questionAnswerInput.MovieQuestionId);
            question.AnswerCount += 1;

            var rows = await _movieResourceContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
