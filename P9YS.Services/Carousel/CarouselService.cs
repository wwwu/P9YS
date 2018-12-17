using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using P9YS.Common.Enums;
using P9YS.EntityFramework;
using P9YS.Services.Carousel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.Carousel
{
    public class CarouselService : ICarouselService
    {
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IMemoryCache _memoryCache;
        public CarouselService(MovieResourceContext movieResourceContext
            , IMemoryCache memoryCache)
        {
            _movieResourceContext = movieResourceContext;
            _memoryCache = memoryCache;
        }

        private static readonly AsyncLock _carouselsLock = new AsyncLock();
        public async Task<List<CarouselOutput>> GetCarouselsAsync()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.Carousels, out List<CarouselOutput> carousels))
            {
                using (await _carouselsLock.LockAsync())
                {//这种轻量级的查询业务不加锁也没关系，这里只是测试一下AsyncLock，好用！
                    if (!_memoryCache.TryGetValue(CacheKeys.Carousels, out carousels))
                    {
                        carousels = await _movieResourceContext.Carousels
                            .Where(s => s.State == CarouselStateEnum.Show)
                            .OrderByDescending(s => s.Id)
                            .AsNoTracking()
                            .ProjectTo<CarouselOutput>()
                            .ToListAsync();
                        _memoryCache.Set(CacheKeys.Carousels, carousels, TimeSpan.FromMinutes(CacheKeys.DefaultMinutes));
                    }
                }
            }

            return carousels;            
        }
    }
}
