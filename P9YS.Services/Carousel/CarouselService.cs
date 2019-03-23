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
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IMemoryCache _memoryCache;
        private readonly IBaseService _baseService;

        public CarouselService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IMemoryCache memoryCache
            , IBaseService baseService)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
            _memoryCache = memoryCache;
            _baseService = baseService;
        }

        private static readonly AsyncLock _carouselsLock = new AsyncLock();
        public async Task<List<Carousel_Output>> GetCarousels()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.Carousels, out List<Carousel_Output> carousels))
            {
                using (await _carouselsLock.LockAsync())//测试一下AsyncLock
                {
                    if (!_memoryCache.TryGetValue(CacheKeys.Carousels, out carousels))
                    {
                        carousels = await _movieResourceContext.Carousels
                            .Where(s => s.State == CarouselStateEnum.Show)
                            .OrderByDescending(s => s.Id)
                            .AsNoTracking()
                            .ProjectTo<Carousel_Output>(_mapper.ConfigurationProvider)
                            .ToListAsync();
                        //图片地址
                        carousels.ForEach(s => s.ImgUrl = _baseService.GetCosAbsoluteUrl(s.ImgUrl));
                        _memoryCache.Set(CacheKeys.Carousels, carousels, TimeSpan.FromMinutes(CacheKeys.DefaultMinutes));
                    }
                }
            }
            return carousels;            
        }

        public async Task<PagingOutput<EntityFramework.Models.Carousel>> GetCarousels(PagingInput pagingInput)
        {
            var carousels = await _movieResourceContext.Carousels
                .OrderByDescending(s => s.Id)
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .AsNoTracking()
                .ToListAsync();
            //图片地址
            carousels.ForEach(s => s.ImgUrl = _baseService.GetCosAbsoluteUrl(s.ImgUrl));
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

        public async Task<Result> AddCarousel(Carousel_Input carouselnput)
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
                var uploadResult = _baseService.UploadFile(imgUrl, dataBytes);
                if (uploadResult.Code != CustomCodeEnum.Success)
                    return uploadResult;
            }
            //添加
            var entity = _mapper.Map<Carousel_Input, EntityFramework.Models.Carousel>(carouselnput);
            entity.ImgUrl = imgUrl;
            var carousel = await _movieResourceContext.Carousels.AddAsync(entity);
            await _movieResourceContext.SaveChangesAsync();
            //返回实体
            entity.ImgUrl = _baseService.GetCosAbsoluteUrl(entity.ImgUrl);
            result.Content = entity;
            return result;
        }

        public async Task<Result> UpdCarousel(Carousel_Input carouselnput)
        {
            var result = new Result();
            var carousel = await _movieResourceContext.Carousels.FindAsync(carouselnput.Id);
            if (carousel == null)
            {
                result.SetCode(CustomCodeEnum.Failed);
                return result;
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
                var uploadResult = _baseService.UploadFile(imgUrl, dataBytes);
                if (uploadResult.Code != CustomCodeEnum.Success)
                    return uploadResult;
            }
            //TODO:删除原图，异常流程

            //更新实体            
            carousel = _mapper.Map(carouselnput, carousel);
            await _movieResourceContext.SaveChangesAsync();
            //返回
            carousel.ImgUrl = _baseService.GetCosAbsoluteUrl(carousel.ImgUrl);
            result.Content = carousel;
            return result;
        }
    }
}
