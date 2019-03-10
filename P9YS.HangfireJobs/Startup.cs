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
                IsReadOnlyFunc = (DashboardContext context) => true //禁止删除排队作业
            });

            #region 注册后台任务

            //更新评分数据
            RecurringJob.AddOrUpdate<IJobService>("Recurring_UpdRatingsJob"
                , s => s.UpdRatingsJob("Recurring_UpdRatingsJob"), "0 0/10 * * * ?");

            //更新点赞数据
            RecurringJob.AddOrUpdate<IJobService>("Recurring_UpdSuportsJob"
                , s => s.UpdSuportsJob("Recurring_UpdSuportsJob"), "0 0/5 * * * ?");

            //更新豆瓣数据(评分、在线播放源)
            RecurringJob.AddOrUpdate<IJobService>("Recurring_UpdDoubanDataJob"
                , s => s.UpdDoubanDataJob(), "0 0 2 * * ?");

            #endregion
        }
    }
}
