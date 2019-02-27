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
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        private readonly IBaseService _baseService;

        public MovieService(MovieResourceContext movieResourceContext
            , IUserService userService
            , IBaseService baseService)
        {
            _movieResourceContext = movieResourceContext;
            _userService = userService;
            _baseService = baseService;
        }

        public async Task<PagingOutput<MovieListOutput>> GetMoviesByConditionAsync(
            PagingInput<ConditionInput> pagingInput)
        {
            var condition = pagingInput.Condition ?? new ConditionInput();

            #region 条件
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
                if (condition.MovieAreaId.Value == 0)
                {//其它地区
                    query = query.Where(s => s.MovieArea.Other == condition.MovieAreaId.Value);
                }
                else
                {
                    query = query.Where(s => s.MovieAreaId == condition.MovieAreaId);
                }
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
                .ProjectTo<MovieListOutput>()
                .AsNoTracking()
                .ToListAsync();
            //图片路径
            movieList.ForEach(s => s.ImgUrl = _baseService.GetCosAbsoluteUrl(s.ImgUrl));
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<MovieListOutput>
            {                
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = movieList,
                TotalCount = totalCount
            };
            return result;
        }

        public async Task<MovieInfoOutput> GetMovieInfoAsync(int movieId)
        {
            var movie = await _movieResourceContext.Movies
                .ProjectTo<MovieInfoOutput>()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == movieId);
            //图片路径
            movie.ImgUrl = _baseService.GetCosAbsoluteUrl(movie.ImgUrl);
            return movie;
        }

        public async Task<List<MovieSeriesOutput>> GetMovieSeriesAsync(int seriesId)
        {
            var series = await _movieResourceContext.Movies
                .Where(s => s.SeriesId==seriesId)
                .OrderBy(m => m.ReleaseDate)
                .ProjectTo<MovieSeriesOutput>()
                .AsNoTracking()
                .ToListAsync();

            return series;
        }

        public async Task<MovieOriginOutput> GetMovieOriginAsync(int movieId)
        {
            var movieOrigin = await _movieResourceContext.MovieOrigins
                .Where(s => s.MovieId == movieId && s.OriginType == MovieOriginTypeEnum.DouBan)
                .ProjectTo<MovieOriginOutput>()
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return movieOrigin;
        }

        public async Task<PagingOutput<Movie_Manage_Output>> GetMoviesAsync(PagingInput<ConditionInput> pagingInput)
        {
            var condition = pagingInput.Condition ?? new ConditionInput();

            //条件
            var query = _movieResourceContext.Movies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(condition.Keyword))
            {
                query = query.Where(s => s.FullName.Contains(condition.Keyword));
            }
            if (condition.BeginDate.HasValue && condition.EndDate.HasValue)
            {
                query = query.Where(s => s.ReleaseDate>= condition.BeginDate.Value 
                    && s.ReleaseDate<condition.EndDate.Value.AddDays(1));
            }
            //排序
            query = query.OrderByDescending(s => s.UpdTime).AsQueryable();
            //分页
            var movies = await query
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<Movie_Manage_Output>()
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

        public async Task<Movie_Manage_Output> GetMovieAsync(int movieId)
        {
            var entity = await _movieResourceContext.Movies.FindAsync(movieId);
            var movie = Mapper.Map<Movie_Manage_Output>(entity);
            //图片路径
            movie.ImgUrl = _baseService.GetCosAbsoluteUrl(movie.ImgUrl);
            movie.MovieSeries = await GetMovieSeriesAsync(movieId);
            movie.MovieTypes = await _movieResourceContext.MovieType
                .Where(s=>s.MovieId== movieId)
                .AsNoTracking()
                .Select(s=>s.MovieGenreId)
                .ToListAsync();
            return movie;
        }

        public async Task<Result> UpdMovieAsync(Movie_Manage_Input input)
        {
            var result = new Result();
            var movie = await _movieResourceContext.Movies.FindAsync(input.Id);
            if (movie == null)
            {
                result.Code = ErrorCodeEnum.Failed;
                result.Message = ErrorCodeEnum.Failed.GetRemark();
                return result;
            }
            //更新
            movie = Mapper.Map(input, movie);
            //上传新图片
            var imgUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(input.ImgData))
            {
                var dataBytes = Convert.FromBase64String(input.ImgData);
                MemoryStream ms = new MemoryStream(dataBytes);
                var suffix = ImageHelper.GetSuffix(ms);
                ms.Dispose();
                imgUrl = $"/poster/{Guid.NewGuid().ToString("N")}{suffix}";
                var uploadResult = _baseService.UploadFile(imgUrl, dataBytes);
                if (uploadResult.Code != ErrorCodeEnum.Success)
                    return uploadResult;
            }
            //TODO:删除原图，异常流程

            //类型
            var movieTypes = await _movieResourceContext.MovieType
                .Where(s => s.MovieId == movie.Id).ToListAsync();
            _movieResourceContext.MovieType.RemoveRange(movieTypes);
            var newTypes = input.MovieTypes.Select(s => new EntityFramework.Models.MovieType
            {
                MovieId = movie.Id,
                MovieGenreId = s
            });
            _movieResourceContext.MovieType.AddRange(newTypes);
            //执行
            var rows = await _movieResourceContext.SaveChangesAsync();
            if (rows > 0)
            {
                result.Content = await GetMovieAsync(input.Id);
            }
            return result;
        }

        public async Task<Result> DelMovieAsync(int movieId)
        {
            var result = new Result();
            _movieResourceContext.Movies.Remove(new EntityFramework.Models.Movie { Id = movieId });
            var rows = await _movieResourceContext.SaveChangesAsync();
            result.Content = rows > 0;
            return result;
        }

        #region 更新Douban数据

        public List<MovieDoubanOriginOutput> GetMoviesByOriginUpdTime(int count)
        {
            var entities = _movieResourceContext.Movies.Include(s => s.MovieOrigins)
                .GroupJoin(_movieResourceContext.MovieOrigins, m => m.Id, mo => mo.MovieId
                    , (m, mo) => new MovieDoubanOriginOutput
                    {
                        MovieId = m.Id,
                        FullName = m.FullName,
                        Url = mo.FirstOrDefault().Url,
                        UpdTime = mo.FirstOrDefault().UpdTime
                    })
                .OrderBy(s => s.UpdTime)
                .Take(count)
                .ToList();
            return entities;
        }

        public async Task UpdDoubanDataAsync(MovieDoubanOriginOutput movieDoubanOrigin)
        {
            HttpClient client = new HttpClient();
            //没有url，根据片名搜索
            if (string.IsNullOrWhiteSpace(movieDoubanOrigin.Url))
            {
                var searchPageUrl = $"https://www.douban.com/search?cat=1002&q={movieDoubanOrigin.FullName}";
                var searchPageHtml = await client.GetStringAsync(searchPageUrl);
                var sid = Regex.Match(searchPageHtml, @"sid:\s*?(\d+)\s*?,").Groups[1]?.Value;
                if (string.IsNullOrWhiteSpace(sid))
                    return;
                movieDoubanOrigin.Url = $"https://movie.douban.com/subject/{sid}/";
            }
            //影片内容页
            var html = await client.GetStringAsync(movieDoubanOrigin.Url);
            //评分
            var score = Regex.Match(html, @"<strong.*?rating_num.*?>([\d.]+?)</strong>").Groups[1]?.Value;
            //在线播放源
            var matches = Regex.Matches(html, @"class=""playBtn"".+?data-cn=""(.+?)"".+?href=""(.+?)""[\w\W]+?class=""buylink-price""><span>([\w\W]*?)</span></span>");
            var movieOnlinePlays = new List<MovieOnlinePlay>();
            foreach (Match match in matches)
            {
                movieOnlinePlays.Add(new MovieOnlinePlay
                {
                    MovieId = movieDoubanOrigin.MovieId,
                    WebSiteName = match.Groups[1]?.Value ?? "",
                    Url = match.Groups[2]?.Value ?? "",
                    Price = match.Groups[3]?.Value ?? ""
                });
            }
            //插入
            if (!string.IsNullOrWhiteSpace(score))
            {
                _movieResourceContext.MovieOrigins.Add(new MovieOrigin
                {
                    MovieId = movieDoubanOrigin.MovieId,
                    OriginType = MovieOriginTypeEnum.DouBan,
                    Score = decimal.Parse(score),
                    Url = movieDoubanOrigin.Url,
                    AddTime = DateTime.Now,
                    UpdTime = DateTime.Now,
                });
                await _movieResourceContext.SaveChangesAsync();
            }
            if (movieOnlinePlays.Any())
            {
                _movieResourceContext.MovieOnlinePlays.AddRange(movieOnlinePlays);
                await _movieResourceContext.SaveChangesAsync();
            }

        }

        #endregion
    }
}
