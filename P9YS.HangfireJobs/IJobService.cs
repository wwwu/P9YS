using P9YS.Services.Movie.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P9YS.HangfireJobs
{
    public interface IJobService
    {
        /// <summary>
        /// 更新评分数据
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<int> UpdRatingsJob(string jobId);

        /// <summary>
        /// 更新点赞数据
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<int> UpdSuportsJob(string jobId);

        /// <summary>
        /// 更新豆瓣数据(评分、在线播放源)
        /// 每次生成x个延迟任务
        /// </summary>
        /// <returns></returns>
        Task<List<MovieOrigin_Douban_Output>> UpdDoubanDataJob();

        /// <summary>
        /// 清理数据，并整理碎片
        /// </summary>
        /// <returns></returns>
        Task<int> OptimizeDatabaseJob();
    }
}