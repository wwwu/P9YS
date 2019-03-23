﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using P9YS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger
            , IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {//只拦截ajax请求
                string requestBody = string.Empty;
                //context.HttpContext.Request.Body.Seek(0, System.IO.SeekOrigin.Begin);
                _logger.LogError(context.Exception, requestBody, null);

                var result = new Result(Common.CustomCodeEnum.Error);
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(result);
            }
            else
            {//默认异常处理管道
                base.OnException(context);
            }
        }
    }
}
