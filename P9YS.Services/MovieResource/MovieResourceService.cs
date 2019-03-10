using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P9YS.Common;
using P9YS.EntityFramework;
using P9YS.Services.MovieResource.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.MovieResource
{
    public class MovieResourceService : IMovieResourceService
    {
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        public MovieResourceService(IMapper mapper
            , MovieResourceContext movieResourceContext)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
        }

        public async Task<List<MovieResourceOutput>> GetMovieResources(int movieId)
        {
            var movieResources = await _movieResourceContext.MovieResources
                .Where(s => s.MovieId == movieId)
                .OrderBy(s => s.Size)
                .ProjectTo<MovieResourceOutput>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            movieResources.ForEach(item =>
            {
                var size = int.Parse(item.Size);
                item.Size = size < 1024 ? item.Size + "MB" : (size / 1024m).ToString("0.00") + "GB";
            });

            return movieResources;
        }

        public async Task<List<MovieOnlinePlayOutput>> GetMovieOnlinePlays(int movieId)
        {
            var movieOnlinePlays = await _movieResourceContext.MovieOnlinePlays
                .Where(s => s.MovieId == movieId)
                .ProjectTo<MovieOnlinePlayOutput>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            return movieOnlinePlays;
        }


        public async Task<PagingOutput<MovieResource_Manage_Output>> GetResources(
            PagingInput<MovieResource_Search_Input> pagingInput)
        {
            var query = _movieResourceContext.MovieResources.AsQueryable();

            //条件
            if (pagingInput.Condition != null)
            {
                if (pagingInput.Condition.MovieId.HasValue)
                    query = query.Where(s => s.MovieId == pagingInput.Condition.MovieId.Value);
                if (pagingInput.Condition.UserId.HasValue)
                    query = query.Where(s => s.UserId == pagingInput.Condition.UserId.Value);
                if (pagingInput.Condition.BeginTime.HasValue)
                    query = query.Where(s => s.AddTime >= pagingInput.Condition.BeginTime.Value);
                if (pagingInput.Condition.EndTime.HasValue)
                    query = query.Where(s => s.AddTime < pagingInput.Condition.EndTime.Value.AddDays(1));
            }
            //排序
            query = query.OrderByDescending(s => s.UpdTime);
            //分页
            var resources = await query.Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<MovieResource_Manage_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<MovieResource_Manage_Output>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = resources,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<Result> AddResource(MovieResourceInput movieResourceInput)
        {
            var result = new Result();
            var resource = Mapper.Map<EntityFramework.Models.MovieResource>(movieResourceInput);
            await _movieResourceContext.MovieResources.AddAsync(resource);
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = rows > 0;
            return result;
        }

        public async Task<Result> UpdResource(MovieResourceInput movieResourceInput)
        {
            var result = new Result();
            var entity = await _movieResourceContext.MovieResources.FindAsync(movieResourceInput.Id);
            if (entity == null)
            {
                result.Code = CustomCodeEnum.Failed;
                result.Message = CustomCodeEnum.Failed.GetRemark();
                return result;
            }
            entity.Content = movieResourceInput.Content;
            entity.Dub = movieResourceInput.Dub;
            entity.Resolution = movieResourceInput.Resolution;
            entity.Size = movieResourceInput.Size;
            entity.Subtitle = movieResourceInput.Subtitle;
            entity.UpdTime = DateTime.Now;
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = entity;
            return result;
        }

        public async Task<Result> DelResource(int id)
        {
            var result = new Result();
            _movieResourceContext.MovieResources.Remove(new EntityFramework.Models.MovieResource { Id = id });
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = rows > 0;
            return result;
        }
    }
}
