using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using P9YS.EntityFramework;
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

        public async Task<List<Dto.MovieGenreOutput>> GetMovieGenresAsync()
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
    }
}
