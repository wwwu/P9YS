using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        public RatingRecordService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IUserService userService)
        {
            _mapper = mapper;
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

        public async Task<PagingOutput<RatingRecordOutput>> GetRatings(PagingInput<GetRatingsInput> pagingInput)
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
                .ProjectTo<RatingRecordOutput>(_mapper.ConfigurationProvider)
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

        /// <summary>
        /// 更新评分数据
        /// </summary>
        /// <param name="endTime"></param>
        public async Task<int> UpdRatingsJob(DateTime endTime)
        {
            //取最后一次标记
            var markTime = await _movieResourceContext.RatingRecords.MaxAsync(s=>s.Mark);
            //取时间段内数据
            var query = _movieResourceContext.RatingRecords
                .Where(s => s.AddTime <= endTime);
            if (markTime.HasValue)
                query = query.Where(s => s.AddTime > markTime.Value);
            var ratingRecords = await query.ToListAsync();
            //按movieId分组聚合
            var ratingGroups = ratingRecords.GroupBy(s => new { s.MovieId })
                .Select(s => new { s.Key.MovieId, ScoreSum = s.Sum(g => g.Score), ScoreCount = s.Count() })
                .ToList();
            //查出所有movie
            var movies = await _movieResourceContext.Movies
                .Where(s => ratingGroups.Select(r => r.MovieId).Contains(s.Id))
                .ToListAsync();
            movies.ForEach(movie =>
            {
                var ratingGroup = ratingGroups.FirstOrDefault(s => s.MovieId == movie.Id);
                movie.ScoreCount += ratingGroup.ScoreCount;
                movie.ScoreSum += ratingGroup.ScoreSum;
                movie.Score = movie.ScoreSum / movie.ScoreCount;
            });
            //更新标记
            var rows = 0;
            if (ratingRecords.Count > 0)
            {
                ratingRecords.Last().Mark = endTime;
                rows = await _movieResourceContext.SaveChangesAsync();
            }
            return rows;
        }

        /// <summary>
        /// 删除RatingRecords和SuportRecords一个月之前的数据,并optimize表
        /// </summary>
        /// <returns>删除行数</returns>
        public async Task<int> OptimizeDatabase()
        {
            _movieResourceContext.RatingRecords.RemoveRange(_movieResourceContext.RatingRecords
                .Where(s => s.AddTime < DateTime.Now.AddMonths(-1)));
            _movieResourceContext.SuportRecords.RemoveRange(_movieResourceContext.SuportRecords
                .Where(s => s.AddTime < DateTime.Now.AddMonths(-1)));
            var rows = await _movieResourceContext.SaveChangesAsync();
            await _movieResourceContext.Database
                .ExecuteSqlCommandAsync($"optimize table RatingRecords;optimize table SuportRecords;");
            return rows;
        }
    }
}
