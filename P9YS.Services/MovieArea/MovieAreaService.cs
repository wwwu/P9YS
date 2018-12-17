using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using P9YS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.MovieArea
{
    public class MovieAreaService : IMovieAreaService
    {
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IMemoryCache _memoryCache;
        public MovieAreaService(MovieResourceContext movieResourceContext
            , IMemoryCache memoryCache)
        {
            _movieResourceContext = movieResourceContext;
            _memoryCache = memoryCache;
        }

        public async Task<List<Dto.MovieAreaOutput>> GetMovieAreasAsync()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.MovieAreas, out List<Dto.MovieAreaOutput> movieAreas))
            {
                movieAreas = await _movieResourceContext.MovieAreas
                    .Where(s => s.Other == 0)
                    .ProjectTo<Dto.MovieAreaOutput>()
                    .AsNoTracking()
                    .ToListAsync();

                _memoryCache.Set(CacheKeys.MovieAreas, movieAreas, TimeSpan.FromMinutes(CacheKeys.DefaultMinutes));
            }

            return movieAreas;
        }
    }
}
