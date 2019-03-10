﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using P9YS.Common;
using P9YS.EntityFramework;
using P9YS.Services.MovieArea.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.MovieArea
{
    public class MovieAreaService : IMovieAreaService
    {
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IMemoryCache _memoryCache;
        public MovieAreaService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
            _memoryCache = memoryCache;
        }

        public async Task<List<MovieAreaOutput>> GetMovieAreas()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.MovieAreas, out List<Dto.MovieAreaOutput> movieAreas))
            {
                movieAreas = await _movieResourceContext.MovieAreas
                    .Where(s => s.Other == 0)
                    .ProjectTo<MovieAreaOutput>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();

                _memoryCache.Set(CacheKeys.MovieAreas, movieAreas, TimeSpan.FromMinutes(CacheKeys.DefaultMinutes));
            }

            return movieAreas;
        }

        public async Task<PagingOutput<EntityFramework.Models.MovieArea>> GetMovieAreas(PagingInput<string> pagingInput)
        {
            var query = _movieResourceContext.MovieAreas.AsQueryable();
            if(!string.IsNullOrWhiteSpace(pagingInput.Condition))
                query = query.Where(s => s.Area.Contains(pagingInput.Condition));

            var areas = await query.Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<EntityFramework.Models.MovieArea>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = areas,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<Result> AddMovieArea(MovieAreaInput movieAreaInput)
        {
            var result = new Result();
            var isRepeated = await _movieResourceContext.MovieAreas
                .AnyAsync(s => s.Area == movieAreaInput.Area.Trim());
            if (isRepeated)
            {
                result.Code = CustomCodeEnum.Repeated;
                result.Message = CustomCodeEnum.Repeated.GetRemark();
                return result;
            }

            var movieArea = _mapper.Map<EntityFramework.Models.MovieArea>(movieAreaInput);
            await _movieResourceContext.MovieAreas.AddAsync(movieArea);
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = rows > 0;
            return result;
        }

        public async Task<Result> UpdMovieArea(MovieAreaInput movieAreaInput)
        {
            var result = new Result();
            var isRepeated = await _movieResourceContext.MovieAreas
                .AnyAsync(s => s.Area == movieAreaInput.Area.Trim() && s.Id != movieAreaInput.Id);
            if (isRepeated)
            {
                result.Code = CustomCodeEnum.Repeated;
                result.Message = CustomCodeEnum.Repeated.GetRemark();
                return result;
            }

            var movieArea = _mapper.Map<EntityFramework.Models.MovieArea>(movieAreaInput);
            _movieResourceContext.MovieAreas.Update(movieArea);
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = movieArea;
            return result;
        }

        public async Task<int> GetMovieAreaId(string areaName)
        {
            areaName = areaName.Replace("中国大陆", "中国");
            var movieAreas = await GetMovieAreas();
            var result = movieAreas.First(s => s.Area == "其它").Id;
            var area = movieAreas.FirstOrDefault(s => s.Area == areaName);
            if (area != null)
                result = area.Id;
            return result;
        }
    }
}
