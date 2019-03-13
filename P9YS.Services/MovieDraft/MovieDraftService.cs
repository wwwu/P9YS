using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using P9YS.EntityFramework;
using P9YS.Services.MovieArea;
using P9YS.Services.MovieDraft.Dto;
using P9YS.Services.MovieResource.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P9YS.Services.MovieDraft
{
    public class MovieDraftService : IMovieDraftService
    {
        private readonly IMapper _mapper;
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IMovieAreaService _movieAreaService;

        public MovieDraftService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IMovieAreaService movieAreaService)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
            _movieAreaService = movieAreaService;
        }


        public async Task<PagingOutput<MovieDraftOutput>> GetMovieDrafts(PagingInput<ConditionInput> pagingInput)
        {
            var condition = pagingInput.Condition ?? new ConditionInput();
            //条件
            var query = _movieResourceContext.MovieDrafts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(condition.Keyword))
            {
                query = query.Where(s => s.MovieName.Contains(condition.Keyword));
            }
            if (condition.BeginDate.HasValue && condition.EndDate.HasValue)
            {
                query = query.Where(s => s.AddTime >= condition.BeginDate.Value
                    && s.AddTime < condition.EndDate.Value.AddDays(1));
            }
            //排序
            query = query.OrderByDescending(s => s.Id).AsQueryable();
            //分页
            var movieDrafts = await query
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<MovieDraftOutput>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<MovieDraftOutput>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = movieDrafts,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<MovieDraftDetailOutput> GetMovieDraftDetail(int movieDraftId)
        {
            var movieDraftDetailOutput = new MovieDraftDetailOutput();
            var movieDraft = await _movieResourceContext.MovieDrafts.FindAsync(movieDraftId);
            movieDraftDetailOutput.Id = movieDraft.Id;
            movieDraftDetailOutput.ShortName = movieDraft.MovieName;
            movieDraftDetailOutput.DoubanUrl = movieDraft.DoubanUrl;

            #region 解析html获取影片信息
            movieDraftDetailOutput.FullName = Regex.Match(movieDraft.DoubanHtml
                , @"<h1>[\w\W]*?<span.*?>(.*?)</span>").Groups[1]?.Value;
            movieDraftDetailOutput.OtherName = Regex.Match(movieDraft.DoubanHtml
                , @"又名:</span>(.*?)<br").Groups[1]?.Value.Trim().Replace(" / ", "\r\n");
            var areaName = Regex.Match(movieDraft.DoubanHtml
                , @"制片国家/地区:</span>\s*?([\u4E00-\u9FFF]+).*?<br").Groups[1]?.Value;
            movieDraftDetailOutput.MovieAreaId = await _movieAreaService.GetMovieAreaId(areaName);
            movieDraftDetailOutput.Director = Regex.Match(movieDraft.DoubanHtml
                , @"v:directedBy"">(.*?)</a>").Groups[1]?.Value;
            movieDraftDetailOutput.Score = decimal.Parse(Regex.Match(movieDraft.DoubanHtml
                , @"v:average"">(.*?)</strong>").Groups[1]?.Value);
            movieDraftDetailOutput.DoubanScore = movieDraftDetailOutput.Score;
            movieDraftDetailOutput.ImgUrl = Regex.Match(movieDraft.DoubanHtml
                , @"<img.*?src=""(.*?)"".*?rel=""v:image").Groups[1]?.Value;
            //演员，取前10个
            foreach (Match m in Regex.Matches(movieDraft.DoubanHtml
                , @"v:starring"">(.*?)</a>").Take(10))
            {
                movieDraftDetailOutput.Actor += m.Groups[1]?.Value + "\r\n";
            }
            //类型，取前10个
            movieDraftDetailOutput.MovieTypes = new List<string>();
            foreach (Match m in Regex.Matches(movieDraft.DoubanHtml
                , @"v:genre"">(.*?)</span>").Take(10))
            {
                movieDraftDetailOutput.MovieTypes.Add(m.Groups[1]?.Value);
            }
            movieDraftDetailOutput.ReleaseDate = DateTime.Parse(Regex.Match(movieDraft.DoubanHtml
                , @"content=""(\d\d\d\d-\d\d-\d\d)").Groups[1]?.Value);
            movieDraftDetailOutput.MovieTime = int.Parse(Regex.Match(movieDraft.DoubanHtml
                , @"v:runtime""\s*?content=""(\d+)").Groups[1]?.Value);
            //简介
            movieDraftDetailOutput.Intro = Regex.Match(movieDraft.DoubanHtml
                , @"property=""v:summary"".*?>([\w\W]+?)</span>").Groups[1]?.Value;
            movieDraftDetailOutput.Intro = Regex.Replace(movieDraftDetailOutput.Intro
                , @"<[a-zA-Z/!][^<]*?>", "");//去掉所有html标签
            movieDraftDetailOutput.Intro = Regex.Replace(movieDraftDetailOutput.Intro
                , @"(?-ms:^\s*([\w\W]*?)\s*$)", "${1}");//去掉首尾空行空格 \s*[\n\r]+\s*
            movieDraftDetailOutput.Intro = Regex.Replace(movieDraftDetailOutput.Intro
                , @"\s*[\r\n]+\s*", "\r\n");//段中多行替换成一行，并去掉空格
            #endregion

            #region 解析html获取在线播放源
            movieDraftDetailOutput.MovieOnlinePlays = new List<MovieOnlinePlayOutput>();
            foreach (Match m in Regex.Matches(movieDraft.DoubanHtml
                , @"class=""playBtn"".+?data-cn=""(.+?)"".+?href=""(.+?)""[\w\W]+?class=""buylink-price""><span>([\w\W]*?)</span></span>"))
            {
                var price = m.Groups[3]?.Value ?? "";
                price = Regex.Replace(price, @"(?-ms:^\s*([\w\W]*?)\s*$)", "${1}");//去掉首尾空行空格 \s*[\n\r]+\s*
                movieDraftDetailOutput.MovieOnlinePlays.Add(new MovieOnlinePlayOutput
                {                    
                    WebSiteName = m.Groups[1]?.Value ?? "",
                    Url = m.Groups[2]?.Value ?? "",
                    Price = price
                });
            }
            #endregion

            #region 下载资源
            var link = movieDraft.Resoures;
            if (link.StartsWith("ftp"))
            {//原始路径转迅雷链接
                var strArr = Encoding.UTF8.GetBytes($"AA{movieDraft.Resoures}ZZ");
                link = "thunder://" + Convert.ToBase64String(strArr);
            }
            movieDraftDetailOutput.MovieResource = new MovieResourceInput
            {
                Dub = Regex.Match(movieDraft.DoubanHtml, @"语言:</span>\s*?(\w+).*?<br/>").Groups[1]?.Value,
                Resolution = "DB-720P",
                Size = 0,
                Subtitle = "中",
                Content = link,
            };
            #endregion

            return movieDraftDetailOutput;
        }
    }
}
