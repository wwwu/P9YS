using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P9YS.Common;
using P9YS.Common.Enums;
using P9YS.EntityFramework;
using P9YS.Services.Base;
using P9YS.Services.MovieArea;
using P9YS.Services.MovieDraft.Dto;
using P9YS.Services.MovieResource.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

        public async Task<PagingOutput<MovieDraft_Output>> GetMovieDrafts(PagingInput<GetMovieDrafts_Condition_Input> pagingInput)
        {
            //条件
            var condition = pagingInput.Condition ?? new GetMovieDrafts_Condition_Input();
            var query = _movieResourceContext.MovieDrafts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(condition.Keyword))
                query = query.Where(s => s.MovieName.Contains(condition.Keyword));
            if (condition.Status.HasValue)
                query = query.Where(s => s.Status == condition.Status);
            if (condition.BeginDate.HasValue && condition.EndDate.HasValue)
                query = query.Where(s => s.AddTime >= condition.BeginDate.Value
                    && s.AddTime < condition.EndDate.Value.AddDays(1));
            //排序
            query = query.OrderByDescending(s => s.Id).AsQueryable();
            //分页
            var movieDrafts = await query
                .Skip((pagingInput.PageIndex - 1) * pagingInput.PageSize)
                .Take(pagingInput.PageSize)
                .ProjectTo<MovieDraft_Output>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
            var totalCount = await query.CountAsync();

            var result = new PagingOutput<MovieDraft_Output>
            {
                PageIndex = pagingInput.PageIndex,
                PageSize = pagingInput.PageSize,
                Data = movieDrafts,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<MovieDraft_Detail_Input> GetMovieDraftDetail(int movieDraftId)
        {
            var movieDraftDetailInput = new MovieDraft_Detail_Input();
            var movieDraft = await _movieResourceContext.MovieDrafts.FindAsync(movieDraftId);
            movieDraftDetailInput.MovieDraftId = movieDraft.Id;
            movieDraftDetailInput.ShortName = movieDraft.MovieName;
            movieDraftDetailInput.DoubanUrl = movieDraft.DoubanUrl;
            movieDraftDetailInput.ImgData = movieDraft.ImgData;

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
            //movieDraftDetailInput.ImgUrl = Regex.Match(movieDraft.DoubanHtml
            //    , @"<img.*?src=""(.*?)"".*?rel=""v:image").Groups[1]?.Value;
            //if (!string.IsNullOrWhiteSpace(movieDraftDetailInput.ImgUrl))
            //    movieDraftDetailInput.ImgUrl = movieDraftDetailInput.ImgUrl.Replace(".jpg", ".webp");

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
            movieDraftDetailInput.MovieOnlinePlays = new List<MovieOnlinePlay_Output>();
            foreach (Match m in Regex.Matches(movieDraft.DoubanHtml
                , @"class=""playBtn"".+?data-cn=""(.+?)"".+?href=""(.+?)""[\w\W]+?class=""buylink-price""><span>([\w\W]*?)</span></span>"))
            {
                var price = m.Groups[3]?.Value ?? "";
                price = Regex.Replace(price, @"(?-ms:^\s*([\w\W]*?)\s*$)", "${1}");//去掉首尾空行空格 \s*[\n\r]+\s*
                movieDraftDetailInput.MovieOnlinePlays.Add(new MovieOnlinePlay_Output
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
            var match = Regex.Match(movieDraft.Resoures, @".+\.(.+?\.\d+?p)\.(\w+?)\.\w+$");
            movieDraftDetailInput.MovieResource = new MovieResource_Input
            {
                Dub = Regex.Match(movieDraft.DoubanHtml, @"语言:</span>\s*?(\w+).*?<br/>").Groups[1]?.Value,
                Resolution = match.Groups[1]?.Value,
                Size = 0, //TODO: 获取文件大小，ftp GetFileSize行不通
                Subtitle = match.Groups[2]?.Value?.ToUpper(),
                Content = link,
            };
            #endregion

            return movieDraftDetailInput;
        }

        public async Task<Result> AddMovie(MovieDraft_Detail_Input movieDraftDetailInput)
        {
            var result = new Result();
            //更改状态 乐观锁防止并发
            var movieDraft = await _movieResourceContext.MovieDrafts.FirstOrDefaultAsync(s => 
                s.Id == movieDraftDetailInput.MovieDraftId && s.Status == MovieDraftStatusEnum.Unverified);
            if (movieDraft == null)
            {
                result.SetCode(CustomCodeEnum.StatusChanged);
                return result;
            }

            movieDraft.Status = MovieDraftStatusEnum.Added;
            //添加成功后再上传图片
            var imgUrl = string.Empty;
            byte[] imgBytes = null;
            if (!string.IsNullOrWhiteSpace(movieDraftDetailInput.ImgData))
            {
                var (imgName, dataBytes) = _baseService.Base64ToBytes(movieDraftDetailInput.ImgData);
                imgUrl = $"/poster/{imgName}";
                imgBytes = dataBytes;
            }
            var movie = _mapper.Map<EntityFramework.Models.Movie>(movieDraftDetailInput);
            movie.ImgUrl = imgUrl;
            movie.MovieResources = _mapper.Map<IEnumerable<EntityFramework.Models.MovieResource>>(
                new List<MovieResource_Input> { movieDraftDetailInput.MovieResource });
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
            {//触发乐观锁
                result.SetCode(CustomCodeEnum.StatusChanged);
                return result;
            }

            //图片上传
            if (!string.IsNullOrWhiteSpace(imgUrl))
            {
                var uploadResult = _baseService.UploadFile(imgUrl, imgBytes);
                if (uploadResult.Code != CustomCodeEnum.Success)
                {
                    uploadResult.Message = "添加成功，但图片上传失败。";
                    return uploadResult;
                }
            }
            return result;
        }

        public async Task<List<string>> AddMovieDraft(int maxCount)
        {
            var result = new List<string>();

            #region 获取dytt最新电影链接，跟数据库中的比对取差集
            var dyDomain = "https://www.dytt8.net/";
            var encoding = "gb2312";
            var dyListHtml = await _baseService.WebClientGetStringAsync(dyDomain, encoding);
            var newUrls = Regex.Matches(dyListHtml
                , @"(?<=start:最新电影下载-->[\w\W]*?)href='(.*?/\d+?/\d+?.html)'(?=[\w\W]*?end:最新电影下载--->)")
                .Select(s => dyDomain + s.Groups[1].Value).Distinct().ToList();
            var oldUrls = await _movieResourceContext.MovieDrafts
                .Where(s => s.AddTime > DateTime.Now.AddMonths(-2) && newUrls.Contains(s.DyUrl))
                .Select(s => s.DyUrl).Distinct().AsNoTracking().ToListAsync();
            newUrls = newUrls.Except(oldUrls).Take(maxCount).ToList();
            #endregion

            #region 得到的新链接去豆瓣获取详情
            var moviesDrafts = new List<EntityFramework.Models.MovieDraft>();
            foreach (var dyUrl in newUrls)
            {
                var dyContentHtml = await _baseService.WebClientGetStringAsync(dyUrl, encoding);
                var movieName = Regex.Match(dyContentHtml
                    , @"<h1>.*?《(.+?)》").Groups[1]?.Value;
                if (string.IsNullOrWhiteSpace(movieName))
                    continue;
                result.Add(movieName);
                var resource =  Regex.Match(dyContentHtml
                    , @"<a\s+?href=""(ftp://.+?)""").Groups[1]?.Value;
                var (doubanUrl, doubanHtml) = await _baseService.DownloadDoubanHtml(null, movieName);
                //清理垃圾
                doubanHtml = Regex.Replace(doubanHtml ?? ""  
                    , @"<script.*?>[\w\W]*?</script>|<link.+?>|<style.*?>[\w\W]*?</style>","");
                doubanHtml = Regex.Replace(doubanHtml
                    , @"\s*[\r\n]+\s*", "\r\n");//段中多行替换成一行，并去掉空格 
                doubanHtml = System.Web.HttpUtility.HtmlDecode(doubanHtml);
                //获取图片，转base64
                var imgUrl = Regex.Match(doubanHtml
                    , @"<img.*?src=""(.*?)"".*?rel=""v:image").Groups[1]?.Value;
                var bytes = await new WebClient().DownloadDataTaskAsync(imgUrl);
                var base64String = Convert.ToBase64String(bytes);
                var suffix = imgUrl.Substring(imgUrl.LastIndexOf(".") + 1);
                var imgBase64String =$"data:image/{suffix};base64,{base64String}";

                moviesDrafts.Add(new EntityFramework.Models.MovieDraft
                {
                    MovieName = movieName,
                    ImgData = imgBase64String,
                    DyUrl = dyUrl,
                    Resoures = resource,
                    DoubanUrl = doubanUrl,
                    DoubanHtml = doubanHtml,
                    AddTime = DateTime.Now,
                });

                Thread.Sleep(10000);
            }

            #endregion

            if (moviesDrafts.Any())
            {
                await _movieResourceContext.MovieDrafts.AddRangeAsync(moviesDrafts);
                await _movieResourceContext.SaveChangesAsync();
            }

            return result;
        }
    }
}
