using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using P9YS.Common.Enums;
using P9YS.EntityFramework;
using P9YS.Services.MovieRecommend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.MovieRecommend
{
    public class MovieRecommendService : IMovieRecommendService
    {
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IMemoryCache _memoryCache;
        public MovieRecommendService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 推荐列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieRecommendOutput>> GetAnnualRecommends(int limit = 10)
        {
            if (!_memoryCache.TryGetValue(CacheKeys.AnnualRecommends, out List<MovieRecommendOutput> annualRecommends))
            {
                annualRecommends = await _movieResourceContext.MovieRecommends.Include(s => s.Movie)
                    .Where(s=>s.Type== RecommendTypeEnum.Annual)
                    .OrderByDescending(s => s.Sort)
                    .Take(limit)
                    .ProjectTo<MovieRecommendOutput>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();

                _memoryCache.Set(CacheKeys.AnnualRecommends, annualRecommends, TimeSpan.FromMinutes(CacheKeys.DefaultMinutes));
            }

            return annualRecommends;
        }

        /// <summary>
        /// 近期推荐列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieRecommendOutput>> GetRecentRecommends(int limit = 10)
        {
            if (!_memoryCache.TryGetValue(CacheKeys.RecentRecommends, out List<MovieRecommendOutput> recentRecommends))
            {
                recentRecommends = await _movieResourceContext.MovieRecommends.Include(s => s.Movie)
                    .Where(s => s.Type == RecommendTypeEnum.Recent)
                    .OrderByDescending(s => s.Sort)
                    .Take(limit)
                    .ProjectTo<MovieRecommendOutput>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();

                _memoryCache.Set(CacheKeys.RecentRecommends, recentRecommends, TimeSpan.FromMinutes(CacheKeys.DefaultMinutes));
            }

            return recentRecommends;
        }

        public async Task<List<RecommendOutput>> GetRecommends(RecommendTypeEnum recommendType)
        {
            var recommends = await _movieResourceContext.MovieRecommends.Include(s => s.Movie)
                .Where(s => s.Type == recommendType)
                .OrderByDescending(s => s.Sort).ThenByDescending(s=>s.AddTime)
                .ProjectTo<RecommendOutput>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            return recommends;
        }

        public async Task<bool> AddRecommend(MovieRecommendInput recommendInput)
        {
            var recommend = Mapper.Map<EntityFramework.Models.MovieRecommend>(recommendInput);
            var entity = await _movieResourceContext.MovieRecommends.AddAsync(recommend);
            var rows = await _movieResourceContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DelRecommend(int id)
        {
            _movieResourceContext.MovieRecommends
                .Remove(new EntityFramework.Models.MovieRecommend { Id = id });
            var rows = await _movieResourceContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
