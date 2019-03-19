﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P9YS.Common;
using QCloud.CosApi.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace P9YS.Services.Base
{
    public class BaseService : IBaseService
    {
        private readonly IOptionsMonitor<AppSettings> _options;
        private readonly ILogger<BaseService> _logger;
        private readonly CosCloud _cosCloud;
        public BaseService(IOptionsMonitor<AppSettings> options
            , ILogger<BaseService> logger)
        {
            _options = options;
            _logger = logger;
            //创建cos对象
            var txCos = _options.CurrentValue.TxCos;
            _cosCloud = new CosCloud(int.Parse(txCos.Appid), txCos.SecretID, txCos.SecretKey, txCos.Region);
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
                var str = _cosCloud.UploadFile(_options.CurrentValue.TxCos.Bucket, savePath, bytes, null, false, 0);
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
                var bytes = new System.Net.WebClient().DownloadData(sourcePath);
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
    }

    public class TxCosResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Request_id { get; set; }
        public object Data { get; set; }
    }
}
