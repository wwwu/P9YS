using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P9YS.Common;
using QCloud.CosApi.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P9YS.Services.Base
{
    public class BaseService : IBaseService
    {
        private readonly IOptionsMonitor<AppSettings> _options;
        private readonly ILogger<BaseService> _logger;
        public BaseService(IOptionsMonitor<AppSettings> options
            , ILogger<BaseService> logger)
        {
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// 上传文件到Cos，默认bucket
        /// </summary>
        /// <param name="savePath">要保存的路径 /文件夹名/文件名.jpg</param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public Result UploadFile(string savePath, byte[] bytes)
        {
            var result = new Result();
            try
            {
                var txCos = _options.CurrentValue.TxCos;
                var cosCloud = new CosCloud(int.Parse(txCos.Appid), txCos.SecretID, txCos.SecretKey, txCos.Region);
                var str = cosCloud.UploadFile(_options.CurrentValue.TxCos.Bucket, savePath, bytes, null, false, 0);
                var response = JsonConvert.DeserializeObject<TxCosResponse>(str);
                result.Content = response;
                if (response.Code != 0)
                {
                    result.Code = CustomCodeEnum.Error;
                    result.Message = response.Message;
                    _logger.LogError($"Request_id:{response.Request_id}.Message:{response.Message}");
                }
            }
            catch (Exception ex)
            {
                result.Code = CustomCodeEnum.Error;
                result.Message = ex.Message;
                _logger.LogError(ex,ex.Message,null);
            }
            return result;
        }

        /// <summary>
        /// 上传文件到Cos，默认bucket
        /// </summary>
        /// <param name="savePath">要保存的路径, /文件夹名/文件名.jpg</param>
        /// <param name="sourcePath">要上传文件的完整url</param>
        /// <returns></returns>
        public Result UploadFile(string savePath, string sourcePath)
        {
            var result = new Result();
            try
            {
                var bytes = new WebClient().DownloadData(sourcePath);
                result = UploadFile(savePath, bytes);
            }
            catch (Exception ex)
            {
                result.Code = CustomCodeEnum.Error;
                result.Message = ex.Message;
                _logger.LogError(ex, ex.Message, null);
            }
            return result;
        }

        /// <summary>
        /// 获取TxCos图片绝对路径
        /// </summary>
        /// <param name="relativeUrl">相对路径</param>
        /// <returns></returns>
        public string GetCosAbsoluteUrl(string relativeUrl)
        {
            return _options.CurrentValue.TxCos.CosDomain + relativeUrl;
        }

        public string WebClientGetString(string url, string encoding = null)
        {
            var result = string.Empty;
            WebClient webClient = new WebClient();
            try
            {
                webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                if (encoding != null)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    webClient.Encoding = Encoding.GetEncoding(encoding);
                }
                result = webClient.DownloadString(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return result;
        }

        public async Task<string> WebClientGetStringAsync(string url, string encoding = null)
        {
            var result = string.Empty;
            WebClient webClient = new WebClient();
            try
            {
                webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                if (encoding != null)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    webClient.Encoding = Encoding.GetEncoding(encoding);
                }
                result = await webClient.DownloadStringTaskAsync(url);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 抓取豆瓣html,优先根据url下载，没有url则根据movieName搜索
        /// </summary>
        /// <param name="url"></param>
        /// <param name="movieName"></param>
        /// <returns></returns>
        public async Task<(string url,string html)> DownloadDoubanHtml(string url,string movieName)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                var searchPageUrl = $"https://www.douban.com/search?cat=1002&q={movieName}";
                var searchPageHtml = await WebClientGetStringAsync(searchPageUrl);
                var sid = Regex.Match(searchPageHtml, @"sid:\s*?(\d+)\s*?,").Groups[1]?.Value;
                if (string.IsNullOrWhiteSpace(sid))
                    return (url, string.Empty);
                url = $"https://movie.douban.com/subject/{sid}/";
            }
            //影片内容页
            var html = await WebClientGetStringAsync(url);
            return (url,html);
        }

        public (string imgName, byte[] dataBytes) Base64ToBytes(string base64String)
        {
            var match = Regex.Match(base64String, @".*?:image/(.+?);base64,(.+)");
            var dataBytes = Convert.FromBase64String(match.Groups[2]?.Value);
            var imgName = $"{Guid.NewGuid().ToString("N")}.{match.Groups[1]?.Value}";
            return (imgName, dataBytes);
        }
    }

    public class TxCosResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Request_id { get; set; }
        public object Data { get; set; }
    }
}
