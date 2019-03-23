using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P9YS.Common.Enums;
using P9YS.EntityFramework;
using P9YS.Services.Base;
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
        private readonly IBaseService _baseService;

        public MovieDraftService(IMapper mapper
            , MovieResourceContext movieResourceContext
            , IMovieAreaService movieAreaService
            , IBaseService baseService)
        {
            _mapper = mapper;
            _movieResourceContext = movieResourceContext;
            _movieAreaService = movieAreaService;
            _baseService = baseService;
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
            if (condition.Status.HasValue)
            {
                query = query.Where(s => s.Status == condition.Status);
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

        public async Task<MovieDraftDetailInput> GetMovieDraftDetail(int movieDraftId)
        {
            var movieDraftDetailInput = new MovieDraftDetailInput();
            var movieDraft = await _movieResourceContext.MovieDrafts.FindAsync(movieDraftId);
            movieDraftDetailInput.MovieDraftId = movieDraft.Id;
            movieDraftDetailInput.ShortName = movieDraft.MovieName;
            movieDraftDetailInput.DoubanUrl = movieDraft.DoubanUrl;

            #region 解析html获取影片信息
            movieDraftDetailInput.FullName = Regex.Match(movieDraft.DoubanHtml
                , @"<h1>[\w\W]*?<span.*?>(.*?)</span>").Groups[1]?.Value;
            movieDraftDetailInput.OtherName = Regex.Match(movieDraft.DoubanHtml
                , @"又名:</span>(.*?)<br").Groups[1]?.Value.Trim().Replace(" / ", "\r\n");
            var areaName = Regex.Match(movieDraft.DoubanHtml
                , @"制片国家/地区:</span>\s*?([\u4E00-\u9FFF]+).*?<br").Groups[1]?.Value;
            movieDraftDetailInput.MovieAreaId = await _movieAreaService.GetMovieAreaId(areaName);
            movieDraftDetailInput.Director = Regex.Match(movieDraft.DoubanHtml
                , @"v:directedBy"">(.*?)</a>").Groups[1]?.Value;
            movieDraftDetailInput.Score = decimal.Parse(Regex.Match(movieDraft.DoubanHtml
                , @"v:average"">(.*?)</strong>").Groups[1]?.Value);
            movieDraftDetailInput.DoubanScore = movieDraftDetailInput.Score;
            movieDraftDetailInput.ImgUrl = Regex.Match(movieDraft.DoubanHtml
                , @"<img.*?src=""(.*?)"".*?rel=""v:image").Groups[1]?.Value;
            //演员，取前10个
            foreach (Match m in Regex.Matches(movieDraft.DoubanHtml
                , @"v:starring"">(.*?)</a>").Take(10))
            {
                movieDraftDetailInput.Actor += m.Groups[1]?.Value + "\r\n";
            }
            //类型，取前10个
            movieDraftDetailInput.MovieTypes = new List<string>();
            foreach (Match m in Regex.Matches(movieDraft.DoubanHtml
                , @"v:genre"">(.*?)</span>").Take(10))
            {
                movieDraftDetailInput.MovieTypes.Add(m.Groups[1]?.Value);
            }
            movieDraftDetailInput.ReleaseDate = DateTime.Parse(Regex.Match(movieDraft.DoubanHtml
                , @"content=""(\d\d\d\d-\d\d-\d\d)").Groups[1]?.Value);
            movieDraftDetailInput.MovieTime = int.Parse(Regex.Match(movieDraft.DoubanHtml
                , @"v:runtime""\s*?content=""(\d+)").Groups[1]?.Value);
            //简介
            movieDraftDetailInput.Intro = Regex.Match(movieDraft.DoubanHtml
                , @"property=""v:summary"".*?>([\w\W]+?)</span>").Groups[1]?.Value;
            movieDraftDetailInput.Intro = Regex.Replace(movieDraftDetailInput.Intro
                , @"<[a-zA-Z/!][^<]*?>", "");//去掉所有html标签
            movieDraftDetailInput.Intro = Regex.Replace(movieDraftDetailInput.Intro
                , @"(?-ms:^\s*([\w\W]*?)\s*$)", "${1}");//去掉首尾空行空格 \s*[\n\r]+\s*
            movieDraftDetailInput.Intro = Regex.Replace(movieDraftDetailInput.Intro
                , @"\s*[\r\n]+\s*", "\r\n");//段中多行替换成一行，并去掉空格
            #endregion

            #region 解析html获取在线播放源
            movieDraftDetailInput.MovieOnlinePlays = new List<MovieOnlinePlayOutput>();
            foreach (Match m in Regex.Matches(movieDraft.DoubanHtml
                , @"class=""playBtn"".+?data-cn=""(.+?)"".+?href=""(.+?)""[\w\W]+?class=""buylink-price""><span>([\w\W]*?)</span></span>"))
            {
                var price = m.Groups[3]?.Value ?? "";
                price = Regex.Replace(price, @"(?-ms:^\s*([\w\W]*?)\s*$)", "${1}");//去掉首尾空行空格 \s*[\n\r]+\s*
                movieDraftDetailInput.MovieOnlinePlays.Add(new MovieOnlinePlayOutput
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
            movieDraftDetailInput.MovieResource = new MovieResourceInput
            {
                Dub = Regex.Match(movieDraft.DoubanHtml, @"语言:</span>\s*?(\w+).*?<br/>").Groups[1]?.Value,
                Resolution = "DB-720P",
                Size = 0,
                Subtitle = "中",
                Content = link,
            };
            #endregion

            return movieDraftDetailInput;
        }

        public async Task<Result> AddMovie(MovieDraftDetailInput movieDraftDetailInput)
        {
            var result = new Result();
            //更改状态 乐观锁防止并发
            var movieDraft = await _movieResourceContext.MovieDrafts.FirstOrDefaultAsync(s => 
                s.Id == movieDraftDetailInput.MovieDraftId && s.Status == MovieDraftStatusEnum.Unverified);
            if (movieDraft == null)
            {
                result.Code = Common.CustomCodeEnum.Failed;
                result.Message = "操作失败：状态已变更！";
                return result;
            }

            movieDraft.Status = MovieDraftStatusEnum.Added;
            //添加成功后再上传图片
            var imgUrl = string.Empty;
            var sourcePath = movieDraftDetailInput.ImgUrl;
            if (!string.IsNullOrWhiteSpace(sourcePath))
            {
                imgUrl = $"/poster/{Guid.NewGuid().ToString("N")}{sourcePath.Substring(sourcePath.LastIndexOf("."))}";
                movieDraftDetailInput.ImgUrl = imgUrl;
            }
            var movie = _mapper.Map<EntityFramework.Models.Movie>(movieDraftDetailInput);
            movie.MovieResources = _mapper.Map<IEnumerable<EntityFramework.Models.MovieResource>>(
                new List<MovieResourceInput> { movieDraftDetailInput.MovieResource });
            movie.MovieOrigins = new List<EntityFramework.Models.MovieOrigin>
            {
                new EntityFramework.Models.MovieOrigin
                {
                    OriginType= MovieOriginTypeEnum.DouBan,
                    Score = movieDraftDetailInput.DoubanScore,
                    Url = movieDraftDetailInput.DoubanUrl,
                    AddTime = DateTime.Now,
                    UpdTime = DateTime.Now
                }
            };
            await _movieResourceContext.AddAsync(movie);
            try
            {
                var rows = await _movieResourceContext.SaveChangesAsync();
                result.Content = rows > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Code = Common.CustomCodeEnum.Failed;
                result.Message = "操作失败：状态已变更！";
                return result;
            }

            //图片上传
            if (!string.IsNullOrWhiteSpace(imgUrl))
            {
                var uploadResult = _baseService.UploadFile(imgUrl, sourcePath);
                if (uploadResult.Code != Common.CustomCodeEnum.Success)
                {
                    uploadResult.Message = "添加成功，但图片上传失败。";
                    return uploadResult;
                }
            }
            return result;
        }
    }
}
