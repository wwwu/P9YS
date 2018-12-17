using P9YS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services
{
    public class Result<T>
    {
        public Result()
        {
            Code = ErrorCodeEnum.Success;
        }

        public Result(ErrorCodeEnum errorCodeEnum=ErrorCodeEnum.Success)
        {
            Code = errorCodeEnum;
            Message = errorCodeEnum.GetRemark();
        }
        public ErrorCodeEnum Code { get; set; }
        public T Content { get; set; }
        public string Message { get; set; }
    }

    public class Result
    {
        public Result()
        {
            Code = ErrorCodeEnum.Success;
        }

        public Result(ErrorCodeEnum errorCodeEnum = ErrorCodeEnum.Success)
        {
            Code = errorCodeEnum;
            Message = errorCodeEnum.GetRemark();
        }

        public ErrorCodeEnum Code { get; set; }
        public object Content { get; set; }
        public string Message { get; set; }
    }
}
