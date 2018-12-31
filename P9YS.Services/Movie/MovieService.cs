using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P9YS.Common.Enums;
using P9YS.EntityFramework;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        public MovieService(MovieResourceContext movieResourceContext
            , IUserService userService)
        {
            _movieResourceContext = movieResourceContext;
            _userService = userService;
        }

        public async Task<PagingOutput<Dto.MovieListOutput>> GetMoviesByConditionAsync(PagingInput<Dto.ConditionInput> pagingInput)
        {
            var condition = pagingInput.Condition??new Dto.ConditionInput();

            #region 条件
            var query = _movieResourceContext.Movies
                .Where(s => (!condition.MovieTypeId.HasValue || s.MovieTypes.Any(t => t.MovieGenreId == condition.MovieTypeId))
                    && (!condition.BeginDate.HasValue || s.UpdTime >= condition.BeginDate.Value)
                    && (!condition.EndDate.HasValue || s.UpdTime < condition.EndDate.Value.AddDays(1))
                    && (string.IsNullOrWhiteSpace(condition.Keyword)
                        || (s.FullName.Contains(condition.Keyword) || s.OtherName.Contains(condition.Keyword))));
            if (condition.MovieAreaId.HasValue && condition.MovieAreaId.Value == 0)
            {//其它地区
                query = query.Where(s => s.MovieArea.Other == condition.MovieAreaId.Value);
            }
            else
            {
                query = query.Where(s => s.MovieAreaId == condition.MovieAreaId);
            }

            //排序
            switch (condition.Sort)
            {
                case MovieListSortEnum.Hot:
                    query = query.OrderByDescending(s => s.Hot).AsQueryable();
                    break;
                case MovieListSortEnum.LastModify:
                    query = query.OrderByDescending(s => s.UpdTime).AsQueryable();
                    break;
                case MovieListSortEnum.ReleaseDate:
                    query = query.OrderByDescending(s => s.ReleaseDate).AsQueryable();
                    break;
                case MovieListSortEnum.Score:
                    query = query.OrderByDescending(s => s.Score).AsQueryable();
                    break;
            }
            #endregion

            var movieList = await query
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<Dto.MovieListOutput>()
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<Dto.MovieListOutput>
            {                
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = movieList,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<Dto.MovieInfoOutput> GetMovieInfoAsync(int movieId)
        {
            var movie = await _movieResourceContext.Movies
                .ProjectTo<Dto.MovieInfoOutput>()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == movieId);

            return movie;
        }

        public async Task<List<Dto.MovieSeriesOutput>> GetMovieSeriesAsync(int seriesId)
        {
            var series = await _movieResourceContext.Movies
                .Where(s => s.SeriesId==seriesId)
                .OrderBy(m => m.ReleaseDate)
                .ProjectTo<Dto.MovieSeriesOutput>()
                .AsNoTracking()
                .ToListAsync();

            return series;
        }

        public async Task<Dto.MovieOriginOutput> GetMovieOriginAsync(int movieId)
        {
            var movieOrigin = await _movieResourceContext.MovieOrigins
                .Where(s => s.MovieId == movieId && s.OriginType == MovieOriginTypeEnum.DouBan)
                .ProjectTo<Dto.MovieOriginOutput>()
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return movieOrigin;
        }
    }
}
