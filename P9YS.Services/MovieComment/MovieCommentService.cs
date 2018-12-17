using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P9YS.Common;
using P9YS.EntityFramework;
using P9YS.Services.Common;
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
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        public MovieCommentService(MovieResourceContext movieResourceContext
            , IUserService userService)
        {
            _movieResourceContext = movieResourceContext;
            _userService = userService;
        }

        public async Task<int> GetCommentsCountByMovieAsync(int movieId)
        {
            var count = await _movieResourceContext.MovieComments
                .CountAsync(s => s.MovieId == movieId);
            return count;
        }

        public async Task<PagingOutput<Dto.MovieCommentOutput>> GetCommentsAndReplyAsync(PagingInput<int> pagingInput)
        {
            //comments
            var query = _movieResourceContext.MovieComments
                .Where(s => s.MovieId == pagingInput.Condition && s.ParentId == 0);
            var comments = await query.OrderByDescending(s => s.AddTime)
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<Dto.MovieCommentOutput>()
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            ////reply  没设计好，搁置
            //var parentIds = comments.Select(s => s.Id).ToList();
            //var replyQuery = new List<Dto.MovieCommentOutput>().AsQueryable();
            //parentIds.ForEach(parentId =>
            //{
            //    replyQuery = replyQuery.Union(_movieResourceContext.MovieComments
            //        .Where(s => s.ParentId == parentId)
            //        .OrderByDescending(s => s.AddTime)
            //        .Take(pagingInput.PageSize)
            //        .ProjectTo<Dto.MovieCommentOutput>());
            //});
            //var replies = replyQuery.AsNoTracking().ToList();

            comments.ForEach(comment =>
            {
                comment.Content = comment.Content.Replace("\r\n", "<br />");
            });

            var result = new PagingOutput<Dto.MovieCommentOutput>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = comments,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<bool> AddMovieCommentAsync(Dto.MovieCommentInput movieCommentInput)
        {
            var user = _userService.GetCurrentUser();
            //Add
            var entity = Mapper.Map<EntityFramework.Models.MovieComment>(movieCommentInput);
            entity.UserId = user.UserId;
            await _movieResourceContext.MovieComments.AddAsync(entity);
            var rows = await _movieResourceContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
