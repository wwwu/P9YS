using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using P9YS.Common;
using P9YS.EntityFramework;
using P9YS.Services.MovieGenres.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.MovieGenres
{
    public class MovieGenreService : IMovieGenreService
    {
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IMemoryCache _memoryCache;
        public MovieGenreService(MovieResourceContext movieResourceContext
            , IMemoryCache memoryCache)
        {
            _movieResourceContext = movieResourceContext;
            _memoryCache = memoryCache;
        }

        public async Task<List<MovieGenreOutput>> GetMovieGenresAsync()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.MovieGenres, out List<Dto.MovieGenreOutput> movieGenres))
            {
                movieGenres = await _movieResourceContext.MovieGenres
                    .ProjectTo<Dto.MovieGenreOutput>()
                    .AsNoTracking()
                    .ToListAsync();

                _memoryCache.Set(CacheKeys.MovieGenres, movieGenres, TimeSpan.FromMinutes(CacheKeys.DefaultMinutes));
            }

            return movieGenres;
        }

        public async Task<PagingOutput<EntityFramework.Models.MovieGenre>> GetMovieGenresAsync(PagingInput<string> pagingInput)
        {
            var query = _movieResourceContext.MovieGenres.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagingInput.Condition))
                query = query.Where(s => s.Name.Contains(pagingInput.Condition));

            var genres = await query.Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<EntityFramework.Models.MovieGenre>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = genres,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<Result> AddMovieGenreAsync(MoiveGenreInput moiveGenreInput)
        {
            var result = new Result();
            var isRepeated = await _movieResourceContext.MovieGenres
                .AnyAsync(s => s.Name == moiveGenreInput.Name.Trim());
            if (isRepeated)
            {
                result.Code = ErrorCodeEnum.Repeated;
                result.Message = ErrorCodeEnum.Repeated.GetRemark();
                return result;
            }

            var moiveGenre = Mapper.Map<EntityFramework.Models.MovieGenre>(moiveGenreInput);
            await _movieResourceContext.MovieGenres.AddAsync(moiveGenre);
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = rows > 0;
            return result;
        }

        public async Task<Result> UpdMovieGenreAsync(MoiveGenreInput moiveGenreInput)
        {
            var result = new Result();
            var isRepeated = await _movieResourceContext.MovieGenres
                .AnyAsync(s => s.Name == moiveGenreInput.Name.Trim() && s.Id != moiveGenreInput.Id);
            if (isRepeated)
            {
                result.Code = ErrorCodeEnum.Repeated;
                result.Message = ErrorCodeEnum.Repeated.GetRemark();
                return result;
            }

            var movieGenre = Mapper.Map<EntityFramework.Models.MovieGenre>(moiveGenreInput);
            _movieResourceContext.MovieGenres.Update(movieGenre);
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = movieGenre;
            return result;
        }
    }
}
