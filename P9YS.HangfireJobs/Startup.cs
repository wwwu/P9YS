using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.Dashboard;
using Hangfire;

namespace P9YS.HangfireJobs
{
    public static class Startup
    {
        public static void Run(IApplicationBuilder app)
        {
            //启动Hangfire服务
            app.UseHangfireServer();
            //启动Hangfire面板，带授权过滤器，必须在UseAuthentication之后
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() },
                IsReadOnlyFunc = (DashboardContext context) => 
                {
                    var httpContext = context.GetHttpContext();
                    //非Admin权限禁止删除排队作业
                    return !httpContext.User.IsInRole(Common.UserRoleEnum.Admin.ToString());
                }
            });

            #region 注册后台任务

            //更新评分数据
            RecurringJob.AddOrUpdate<IJobService>("Recurring_UpdRatingsJob"
                , s => s.UpdRatingsJob("Recurring_UpdRatingsJob"), "0 0/10 * * * ?");

            //更新点赞数据
            RecurringJob.AddOrUpdate<IJobService>("Recurring_UpdSuportsJob"
                , s => s.UpdSuportsJob("Recurring_UpdSuportsJob"), "0 0/20 * * * ?");

            //更新豆瓣数据(评分、在线播放源)
            RecurringJob.AddOrUpdate<IJobService>("Recurring_UpdDoubanDataJob"
                , s => s.UpdDoubanDataJob(), "0 0 18 * * ?"); //UTC时间

            //清理数据，并整理碎片
            RecurringJob.AddOrUpdate<IJobService>("Recurring_OptimizeDatabaseJob"
                , s => s.OptimizeDatabaseJob(), "0 0 21 1 1/1 ?"); //每月1号21点整运行(北京时间05:00)

            //抓取影片资源
            RecurringJob.AddOrUpdate<IJobService>("Recurring_DownloadMovieInfoJob"
                , s => s.DownloadMovieInfoJob(), "0 0/20 0-16 * * ?");//每天8点-24点之间，每20分钟执行一次

            #endregion
        }
    }
}
