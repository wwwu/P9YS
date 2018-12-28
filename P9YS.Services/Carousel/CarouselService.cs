using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using P9YS.Common;
using P9YS.Common.Enums;
using P9YS.EntityFramework;
using P9YS.Services.Base;
using P9YS.Services.Carousel.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.Carousel
{
    public class CarouselService : ICarouselService
    {
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IMemoryCache _memoryCache;
        private readonly BaseService _baseService;
        public CarouselService(MovieResourceContext movieResourceContext
            , IMemoryCache memoryCache
            , BaseService baseService)
        {
            _movieResourceContext = movieResourceContext;
            _memoryCache = memoryCache;
            _baseService = baseService;
        }

        private static readonly AsyncLock _carouselsLock = new AsyncLock();
        public async Task<List<CarouselOutput>> GetCarouselsAsync()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.Carousels, out List<CarouselOutput> carousels))
            {
                using (await _carouselsLock.LockAsync())//测试一下AsyncLock
                {
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

        public async Task<PagingOutput<EntityFramework.Models.Carousel>> GetCarouselsAsync(PagingInput pagingInput)
        {
            var carousels = await _movieResourceContext.Carousels
                .OrderByDescending(s => s.Id)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await _movieResourceContext.Carousels.CountAsync();

            var result = new PagingOutput<EntityFramework.Models.Carousel>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = carousels,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<Result> AddCarouselAsync(Carouselnput carouselnput)
        {
            var result = new Result();
            //上传图片
            var imgUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(carouselnput.ImgData))
            {
                var dataBytes = Convert.FromBase64String(carouselnput.ImgData);
                MemoryStream ms = new MemoryStream(dataBytes);
                var suffix = ImageHelper.GetSuffix(ms);
                ms.Dispose();
                imgUrl = $"/carousel/{Guid.NewGuid().ToString("N")}{suffix}";
                var uploadResult = _baseService.UploadFile("pic", imgUrl, dataBytes);
                if (uploadResult.Code != ErrorCodeEnum.Success)
                    return uploadResult;
            }
            //添加
            var entity = Mapper.Map<Carouselnput, EntityFramework.Models.Carousel>(carouselnput);
            entity.ImgUrl = imgUrl;
            var carousel = await _movieResourceContext.Carousels.AddAsync(entity);
            await _movieResourceContext.SaveChangesAsync();
            result.Content = entity;
            return result;
        }

        public async Task<Result> UpdCarouselAsync(Carouselnput carouselnput)
        {
            var result = new Result();
            var carousel = await _movieResourceContext.Carousels.FindAsync(carouselnput.Id);
            if (carousel == null)
            {
                result.Code = ErrorCodeEnum.Failed;
                result.Message = ErrorCodeEnum.Failed.GetRemark();
            }

            //上传新图片
            var imgUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(carouselnput.ImgData))
            {
                var dataBytes = Convert.FromBase64String(carouselnput.ImgData);
                MemoryStream ms = new MemoryStream(dataBytes);
                var suffix = ImageHelper.GetSuffix(ms);
                ms.Dispose();
                imgUrl = $"/carousel/{Guid.NewGuid().ToString("N")}{suffix}";
                var uploadResult = _baseService.UploadFile("pic", imgUrl, dataBytes);
                if (uploadResult.Code != ErrorCodeEnum.Success)
                    return uploadResult;
            }
            //TODO:删除原图

            //更新实体            
            carousel = Mapper.Map(carouselnput, carousel);
            await _movieResourceContext.SaveChangesAsync();
            result.Content = carousel;
            return result;
        }
    }
}
