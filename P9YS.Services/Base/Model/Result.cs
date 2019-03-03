using P9YS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services
{
    public class Result<T>: Result
    {
        public new T Content { get; set; }
    }

    public class Result
    {
        public Result(CustomCodeEnum customCodeEnum = CustomCodeEnum.Success)
        {
            SetCode(customCodeEnum);
        }

        public CustomCodeEnum Code { get; set; }
        public object Content { get; set; }
        public string Message { get; set; }

        public void SetCode(CustomCodeEnum errorCodeEnum)
        {
            Code = errorCodeEnum;
            Message = errorCodeEnum.GetRemark();
        }
    }
}
