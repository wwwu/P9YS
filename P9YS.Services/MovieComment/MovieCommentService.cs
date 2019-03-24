using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P9YS.Common;
using P9YS.EntityFramework;
using P9YS.Services.Base;
using P9YS.Services.MovieComment.Dto;
using P9YS.Services.RatingRecord.Dto;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.MovieComment
{
    public class MovieCommentService : IMovieCommentService
    {
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        public MovieCommentService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IUserService userService)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
            _userService = userService;
        }

        public async Task<int> GetCommentsCountByMovie(int movieId)
        {
            var count = await _movieResourceContext.MovieComments
                .CountAsync(s => s.MovieId == movieId);
            return count;
        }

        public async Task<PagingOutput<MovieComment_Output>> GetCommentsAndReply(PagingInput<int> pagingInput)
        {
            //comments
            var query = _movieResourceContext.MovieComments
                .Where(s => s.MovieId == pagingInput.Condition && s.ParentId == 0);
            var comments = await query.OrderByDescending(s => s.AddTime)
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<MovieComment_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            ////reply  TODO: 没设计好，搁置
            //var parentIds = comments.Select(s => s.Id).ToList();
            //var replyQuery = new List<Dto.MovieCommentOutput>().AsQueryable();
            //parentIds.ForEach(parentId =>
            //{
            //    replyQuery = replyQuery.Union(_movieResourceContext.MovieComments
            //        .Where(s => s.ParentId == parentId)
            //        .OrderByDescending(s => s.AddTime)
            //        .Take(pagingInput.PageSize)
            //        .ProjectTo<Dto.MovieCommentOutput>(_mapper.ConfigurationProvider));
            //});
            //var replies = replyQuery.AsNoTracking().ToList();

            comments.ForEach(comment =>
            {
                comment.Content = comment.Content.Replace("\r\n", "<br />");
            });

            var result = new PagingOutput<MovieComment_Output>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = comments,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<bool> AddMovieComment(MovieComment_Input movieCommentInput)
        {
            var user = _userService.GetCurrentUser();
            //Add
            var entity = _mapper.Map<EntityFramework.Models.MovieComment>(movieCommentInput);
            entity.UserId = user.UserId;
            await _movieResourceContext.MovieComments.AddAsync(entity);
            var rows = await _movieResourceContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<PagingOutput<MovieComment_Manage_Output>> GetComments(
            PagingInput<GetRatings_Input> pagingInput)
        {
            var query = _movieResourceContext.MovieComments.AsQueryable();

            //条件
            if (pagingInput.Condition != null)
            {
                if (pagingInput.Condition.MovieId.HasValue)
                    query = query.Where(s => s.MovieId == pagingInput.Condition.MovieId.Value);
                if (pagingInput.Condition.UserId.HasValue)
                    query = query.Where(s => s.UserId == pagingInput.Condition.UserId.Value);
                if (pagingInput.Condition.BeginTime.HasValue)
                    query = query.Where(s => s.AddTime >= pagingInput.Condition.BeginTime.Value);
                if (pagingInput.Condition.EndTime.HasValue)
                    query = query.Where(s => s.AddTime < pagingInput.Condition.EndTime.Value.AddDays(1));
            }
            //排序
            query = query.OrderByDescending(s => s.Id);
            //分页
            var comments = await query.Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<MovieComment_Manage_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<MovieComment_Manage_Output>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = comments,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<Result> DelComment(int id)
        {
            var result = new Result();
            _movieResourceContext.MovieComments.Remove(new EntityFramework.Models.MovieComment { Id = id });
            result.Content = await _movieResourceContext.SaveChangesAsync();
            return result;
        }
    }
}
