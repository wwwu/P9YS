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
    public class BaseService
    {
        private readonly IOptionsMonitor<AppSettings> _options;
        private readonly CosCloud _cosCloud;
        public BaseService(IOptionsMonitor<AppSettings> options)
        {
            _options = options;
            //创建cos对象
            var txCos = _options.CurrentValue.TxCos;
            _cosCloud = new CosCloud(int.Parse(txCos.Appid), txCos.SecretID, txCos.SecretKey, txCos.Region);
        }

        public Result UploadFile(string bucketName, string remotePath, byte[] bytes)
        {
            var result = new Result();
            try
            {
                var str = _cosCloud.UploadFile(bucketName, remotePath, bytes, null, false, 0);
                var response = JsonConvert.DeserializeObject<TxCosResponse>(str);
                if (response.Code != 0)
                {
                    result.Code = ErrorCodeEnum.Error;
                    result.Message = response.Message;
                }
            }
            catch (Exception ex)
            {
                result.Code = ErrorCodeEnum.Error;
                result.Message = ex.Message;
            }
            return result;
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
