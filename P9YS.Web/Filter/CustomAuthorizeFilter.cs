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
    /// 当未授权请求为ajax请求时，将返回自定义Result而非HttpCode 401（为了迎合前端老代码）
    /// 当未授权请求为非ajax请求时，按Startup中所配置的执行
    /// </summary>
    public class CustomAuthorizeFilter : AuthorizeFilter
    {
        private static AuthorizationPolicy _policy_ = new AuthorizationPolicy(
            new[] { new DenyAnonymousAuthorizationRequirement() }, new string[] { });

        public CustomAuthorizeFilter() : base(_policy_) { }

        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Count(s=>s is AuthorizeFilter)==2  //Authorize
                && !context.Filters.Any(s => s is IAllowAnonymousFilter)) //非AllowAnonymous
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated //未授权
                    && context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest") //Ajax请求
                {
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
