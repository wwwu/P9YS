using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P9YS.EntityFramework;
using P9YS.Services.RatingRecord.Dto;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.RatingRecord
{
    public class RatingRecordService : IRatingRecordService
    {
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        public RatingRecordService(MovieResourceContext movieResourceContext
            , IUserService userService)
        {
            _movieResourceContext = movieResourceContext;
            _userService = userService;
        }

        public async Task<bool> AddRatingRecord(RatingRecordInput ratingRecordInput)
        {
            var result = false;
            var user = _userService.GetCurrentUser();
            var isExist = await _movieResourceContext.RatingRecords
                .AnyAsync(s => s.MovieId == ratingRecordInput.MovieId
                    && s.UserId == user.UserId
                    && s.AddTime > DateTime.Today);
            if (!isExist)
            {
                var ratingRecord = new EntityFramework.Models.RatingRecord
                {
                    MovieId = ratingRecordInput.MovieId,
                    UserId = user.UserId,
                    Score = ratingRecordInput.Score,
                    AddTime = DateTime.Now
                };
                await _movieResourceContext.RatingRecords.AddAsync(ratingRecord);
                var rows = await _movieResourceContext.SaveChangesAsync();
                result = rows > 0;
            }
            return result;
        }

        public async Task<PagingOutput<RatingRecordOutput>> GetRatingsAsync(PagingInput<GetRatingsInput> pagingInput)
        {
            var query = _movieResourceContext.RatingRecords.AsQueryable();

            //条件
            if (pagingInput.Condition != null)
            {
                if (pagingInput.Condition.MovieId.HasValue)
                    query = query.Where(s => s.MovieId == pagingInput.Condition.MovieId.Value);
                if (pagingInput.Condition.UserId.HasValue)
                    query = query.Where(s => s.UserId == pagingInput.Condition.UserId.Value);
                if(pagingInput.Condition.BeginTime.HasValue)
                    query = query.Where(s => s.AddTime >= pagingInput.Condition.BeginTime.Value);
                if (pagingInput.Condition.EndTime.HasValue)
                    query = query.Where(s => s.AddTime < pagingInput.Condition.EndTime.Value.AddDays(1));
            }
            //排序
            query = query.OrderByDescending(s => s.Id);
            //分页
            var ratingRecords = await query.Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<RatingRecordOutput>()
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<RatingRecordOutput>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = ratingRecords,
                TotalCount = totalCount
            };
            return result;
        }
    }
}
