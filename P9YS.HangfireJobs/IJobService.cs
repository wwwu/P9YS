using System.Threading.Tasks;

namespace P9YS.HangfireJobs
{
    public interface IJobService
    {
        /// <summary>
        /// 更新评分数据
        /// </summary>
        /// <param name="jobId"></param>
        void UpdRatingsJob(string jobId);
        /// <summary>
        /// 更新点赞数据
        /// </summary>
        /// <param name="jobId"></param>
        void UpdSuportsJob(string jobId);
        /// <summary>
        /// 更新豆瓣数据(评分、在线播放源)
        /// 每次生成x个延迟任务
        /// </summary>
        void UpdDoubanDataJob();
    }
}