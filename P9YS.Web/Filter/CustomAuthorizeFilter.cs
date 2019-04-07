using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using P9YS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web
{
    /// <summary>
    /// 重写AuthorizeFilter
    /// ajax请求时，返回自定义Result，兼容旧的前端代码
    /// </summary>
    public class CustomAuthorizeFilter : AuthorizeFilter
    {
        private static AuthorizationPolicy _policy = new AuthorizationPolicy(
            new[] { new DenyAnonymousAuthorizationRequirement() }, new string[] { });

        public CustomAuthorizeFilter() : base(_policy)
        {
        }

        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Count(s => s is AuthorizeFilter) > 1 //Authorize && 非AllowAnonymous
                && !context.Filters.Any(s => s is AllowAnonymousFilter))
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated //未授权
                    && context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest") //Ajax请求
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new Result(Common.CustomCodeEnum.Unauthorized));
                }
                else
                {
                    await base.OnAuthorizationAsync(context);
                }
            }
        }
    }
}
