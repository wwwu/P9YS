using Hangfire;
using Hangfire.Common;
using Hangfire.Storage;
using P9YS.Services.Movie;
using P9YS.Services.RatingRecord;
using P9YS.Services.SuportRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P9YS.HangfireJobs
{
    public class JobService : IJobService
    {
        private readonly IRatingRecordService _ratingRecordService;
        private readonly ISuportRecordService _suportRecordService;
        private readonly IMovieService _movieService;

        public JobService(IRatingRecordService ratingRecordService
            , ISuportRecordService suportRecordService
            , IMovieService movieService)
        {
            _ratingRecordService = ratingRecordService;
            _suportRecordService = suportRecordService;
            _movieService = movieService;
        }

        /// <summary>
        /// 更新评分数据
        /// </summary>
        /// <param name="jobId"></param>
        public void UpdRatingsJob(string jobId)
        {
            var connection = JobStorage.Current.GetConnection();
            var job = connection.GetRecurringJobs().FirstOrDefault(s => s.Id == jobId);
            //本次运行时间
            var beginTime = job?.LastExecution ?? DateTime.Now;
            //async会丢失上下文
            _ratingRecordService.UpdRatingsJob(beginTime);
        }

        /// <summary>
        /// 更新点赞数据
        /// </summary>
        /// <param name="jobId"></param>
        public void UpdSuportsJob(string jobId)
        {
            var connection = JobStorage.Current.GetConnection();
            var job = connection.GetRecurringJobs().FirstOrDefault(s => s.Id == jobId);
            //本次运行时间
            var beginTime = job?.LastExecution ?? DateTime.Now;
            _suportRecordService.UpdSuportsJob(beginTime);
        }

        /// <summary>
        /// 更新豆瓣数据(评分、在线播放源)
        /// 每次生成x个延迟任务
        /// </summary>
        public void UpdDoubanDataJob()
        {
            var movies = _movieService.GetMoviesByOriginUpdTime(20);
            //5小时内随机分布抓取时间
            var totalMinutes = 300;
            var random = new Random();
            movies.ForEach(movie =>
            {
                var delay = random.Next(1, totalMinutes);
                BackgroundJob.Schedule<IMovieService>(s => s.UpdDoubanData(movie)
                    , TimeSpan.FromMinutes(delay));
            });
        }


    }
}
