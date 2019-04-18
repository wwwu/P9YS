using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using P9YS.Common;
using P9YS.Common.Enums;
using P9YS.EntityFramework;
using P9YS.EntityFramework.Models;
using P9YS.Services.Base;
using P9YS.Services.Movie.Dto;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P9YS.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IBaseService _baseService;

        public MovieService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IBaseService baseService)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
            _baseService = baseService;
        }

        public async Task<PagingOutput<MovieList_Output>> GetMoviesByCondition(
            PagingInput<GetMovies_Condition_Input> pagingInput)
        {
            #region 条件
            var condition = pagingInput.Condition ?? new GetMovies_Condition_Input();
            var query = _movieResourceContext.Movies.AsQueryable();
            if (condition.MovieTypeId.HasValue)
                query = query.Where(s => s.MovieTypes.Any(t => t.MovieGenreId == condition.MovieTypeId));
            if (condition.BeginDate.HasValue)
                query = query.Where(s => s.UpdTime >= condition.BeginDate.Value);
            if(condition.EndDate.HasValue)
                query = query.Where(s => s.UpdTime < condition.EndDate.Value.AddDays(1));
            if (!string.IsNullOrWhiteSpace(condition.Keyword))
                query = query.Where(s => s.FullName.Contains(condition.Keyword) || s.OtherName.Contains(condition.Keyword));
            if (condition.MovieAreaId.HasValue)
            {
                if (condition.MovieAreaId.Value == 0)//其它地区
                    query = query.Where(s => s.MovieArea.Other == condition.MovieAreaId);
                else
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
                .ProjectTo<MovieList_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            //图片路径
            movieList.ForEach(s => s.ImgUrl = _baseService.GetCosAbsoluteUrl(s.ImgUrl));
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<MovieList_Output>
            {                
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = movieList,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<MovieInfo_Output> GetMovieInfo(int movieId)
        {
            var movie = await _movieResourceContext.Movies
                .ProjectTo<MovieInfo_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == movieId);
            //图片路径
            movie.ImgUrl = _baseService.GetCosAbsoluteUrl(movie.ImgUrl);
            return movie;
        }

        public async Task<List<MovieSeries_Output>> GetMovieSeries(int seriesId)
        {
            if (seriesId == 0)
                return new List<MovieSeries_Output>();

            var series = await _movieResourceContext.Movies
                .Where(s => s.SeriesId == seriesId && s.Id != seriesId)
                .OrderBy(m => m.ReleaseDate)
                .ProjectTo<MovieSeries_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return series;
        }

        public async Task<MovieOrigin_Output> GetMovieOrigin(int movieId)
        {
            var movieOrigin = await _movieResourceContext.MovieOrigins
                .Where(s => s.MovieId == movieId && s.OriginType == MovieOriginTypeEnum.DouBan)
                .ProjectTo<MovieOrigin_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return movieOrigin;
        }

        public async Task<PagingOutput<Movie_Manage_Output>> GetMovies(PagingInput<GetMovies_Condition_Input> pagingInput)
        {
            var condition = pagingInput.Condition ?? new GetMovies_Condition_Input();
            //条件
            var query = _movieResourceContext.Movies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(condition.Keyword))
                query = query.Where(s => s.FullName.Contains(condition.Keyword));
            if (condition.BeginDate.HasValue && condition.EndDate.HasValue)
                query = query.Where(s => s.ReleaseDate>= condition.BeginDate.Value 
                    && s.ReleaseDate<condition.EndDate.Value.AddDays(1));
            //排序
            query = query.OrderByDescending(s => s.UpdTime).AsQueryable();
            //分页
            var movies = await query
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<Movie_Manage_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            //图片路径
            movies.ForEach(s => s.ImgUrl = _baseService.GetCosAbsoluteUrl(s.ImgUrl));
            //总页数
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<Movie_Manage_Output>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = movies,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<Movie_Manage_Output> GetMovie(int movieId)
        {
            var entity = await _movieResourceContext.Movies.FindAsync(movieId);
            var movie = _mapper.Map<Movie_Manage_Output>(entity);
            //图片路径
            movie.ImgUrl = _baseService.GetCosAbsoluteUrl(movie.ImgUrl);
            movie.MovieSeries = await GetMovieSeries(movieId);
            movie.MovieTypes = await _movieResourceContext.MovieType
                .Where(s => s.MovieId == movieId)
                .AsNoTracking()
                .Select(s => s.MovieGenreId)
                .ToListAsync();
            return movie;
        }

        public async Task<Result> UpdMovie(Movie_Manage_Input input)
        {
            var result = new Result();
            var movie = await _movieResourceContext.Movies.FindAsync(input.Id);
            if (movie == null)
            {
                result.Code = CustomCodeEnum.Failed;
                result.Message = CustomCodeEnum.Failed.GetRemark();
                return result;
            }
            //更新
            movie = _mapper.Map(input, movie);
            //上传新图片
            var imgUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(input.ImgData))
            {
                var (imgName, dataBytes) = _baseService.Base64ToBytes(input.ImgData);
                var uploadResult = _baseService.UploadFile($"/poster/{imgName}", dataBytes);
                if (uploadResult.Code != CustomCodeEnum.Success)
                    return uploadResult;
            }
            //TODO:删除原图，异常流程

            //类型
            var movieTypes = await _movieResourceContext.MovieType
                .Where(s => s.MovieId == movie.Id).ToListAsync();
            _movieResourceContext.MovieType.RemoveRange(movieTypes);
            var newTypes = input.MovieTypes.Select(s => new MovieType
            {
                MovieId = movie.Id,
                MovieGenreId = s
            });
            _movieResourceContext.MovieType.AddRange(newTypes);
            //执行
            var rows = await _movieResourceContext.SaveChangesAsync();
            if (rows > 0)
            {
                result.Content = await GetMovie(input.Id);
            }
            return result;
        }

        public async Task<Result> DelMovie(int movieId)
        {
            var result = new Result();
            _movieResourceContext.Movies.Remove(new EntityFramework.Models.Movie { Id = movieId });
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = rows > 0;
            return result;
        }

        #region 更新Douban数据

        public async Task<List<MovieOrigin_Douban_Output>> GetMoviesByOriginUpdTime(int count)
        {
            var entities = await _movieResourceContext.Movies
                .GroupJoin(_movieResourceContext.MovieOrigins, m => m.Id, mo => mo.MovieId
                    , (m, mo) => new MovieOrigin_Douban_Output
                    {
                        MovieId = m.Id,
                        FullName = m.FullName,
                        Url = mo.FirstOrDefault().Url,
                        UpdTime = mo.FirstOrDefault().UpdTime
                    })
                .OrderBy(s => s.UpdTime)
                .Take(count)
                .ToListAsync();
            return entities;
        }

        public async Task UpdDoubanData(MovieOrigin_Douban_Output movieDoubanOrigin)
        {
            var (url, html) = await _baseService
                .DownloadDoubanHtml(movieDoubanOrigin.Url, movieDoubanOrigin.FullName);
            movieDoubanOrigin.Url = url;
            if (string.IsNullOrWhiteSpace(html))
            {
                _movieResourceContext.MovieOrigins.Add(new MovieOrigin
                {
                    MovieId = movieDoubanOrigin.MovieId,
                    OriginType = MovieOriginTypeEnum.DouBan,
                    Score = 0,
                    Url = movieDoubanOrigin.Url,
                    AddTime = DateTime.Now,
                    UpdTime = DateTime.Now,
                });
                await _movieResourceContext.SaveChangesAsync();
                return;
            }
            //评分
            var score = Regex.Match(html
                , @"<strong.*?rating_num.*?>([\d.]+?)</strong>").Groups[1]?.Value;
            //在线播放源
            var matches = Regex.Matches(html
                , @"class=""playBtn"".+?data-cn=""(.+?)"".+?href=""(.+?)""[\w\W]+?class=""buylink-price""><span>([\w\W]*?)</span></span>");
            var movieOnlinePlays = new List<MovieOnlinePlay>();
            foreach (Match match in matches)
            {
                var price = match.Groups[3]?.Value ?? "";
                price = Regex.Replace(price
                    , @"(?-ms:^\s*([\w\W]*?)\s*$)", "${1}");//去掉首尾空行空格 \s*[\n\r]+\s*
                movieOnlinePlays.Add(new MovieOnlinePlay
                {
                    MovieId = movieDoubanOrigin.MovieId,
                    WebSiteName = match.Groups[1]?.Value ?? "",
                    Url = match.Groups[2]?.Value ?? "",
                    Price = price
                });
            }
            //MovieOrigins
            var movieOrigin = _movieResourceContext.MovieOrigins
                .FirstOrDefault(s => s.MovieId == movieDoubanOrigin.MovieId && s.OriginType == MovieOriginTypeEnum.DouBan);
            if (movieOrigin != null)
            {//修改
                movieOrigin.Score = decimal.Parse(score);
                movieOrigin.UpdTime = DateTime.Now;
            }
            else
            {//新增
                _movieResourceContext.MovieOrigins.Add(new MovieOrigin
                {
                    MovieId = movieDoubanOrigin.MovieId,
                    OriginType = MovieOriginTypeEnum.DouBan,
                    Score = decimal.Parse(score ?? "0"),
                    Url = movieDoubanOrigin.Url,
                    AddTime = DateTime.Now,
                    UpdTime = DateTime.Now,
                });
            }

            //MovieOnlinePlays
            if (movieOnlinePlays.Any())
            {
                var oldDatas = _movieResourceContext.MovieOnlinePlays
                    .Where(s => s.MovieId == movieDoubanOrigin.MovieId)
                    .ToList();
                _movieResourceContext.MovieOnlinePlays.RemoveRange(oldDatas);
                _movieResourceContext.MovieOnlinePlays.AddRange(movieOnlinePlays);
            }
            await _movieResourceContext.SaveChangesAsync();
        }

        #endregion
    }
}
