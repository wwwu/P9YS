using Microsoft.AspNetCore.Mvc.Filters;
using P9YS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Manage
{
    public class ModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.FirstOrDefault(r => r.Value.Errors.Count > 0);
                var msg = errors.Value.Errors[0].ErrorMessage;
                if (string.IsNullOrWhiteSpace(msg))
                    msg = errors.Value.Errors[0].Exception.Message;

                var result = new Result(Common.ErrorCodeEnum.Failed)
                {
                    Message = msg
                };
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(result);
            }

            base.OnActionExecuting(context);
        }
    }
}
