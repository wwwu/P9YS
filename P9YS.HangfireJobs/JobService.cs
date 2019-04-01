using Hangfire;
using Hangfire.Common;
using Hangfire.Storage;
using P9YS.Services.Movie;
using P9YS.Services.Movie.Dto;
using P9YS.Services.MovieDraft;
using P9YS.Services.RatingRecord;
using P9YS.Services.SuportRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.HangfireJobs
{
    public class JobService : IJobService
    {
        private readonly IRatingRecordService _ratingRecordService;
        private readonly ISuportRecordService _suportRecordService;
        private readonly IMovieService _movieService;
        private readonly IMovieDraftService _movieDraftService;

        public JobService(IRatingRecordService ratingRecordService
            , ISuportRecordService suportRecordService
            , IMovieService movieService
            , IMovieDraftService movieDraftService)
        {
            _ratingRecordService = ratingRecordService;
            _suportRecordService = suportRecordService;
            _movieService = movieService;
            _movieDraftService = movieDraftService;
        }

        /// <summary>
        /// 更新评分数据
        /// </summary>
        /// <param name="jobId"></param>
        public async Task<int> UpdRatingsJob(string jobId)
        {
            var connection = JobStorage.Current.GetConnection();
            var job = connection.GetRecurringJobs().FirstOrDefault(s => s.Id == jobId);
            //本次运行时间
            var beginTime = job?.LastExecution ?? DateTime.Now;
            return await _ratingRecordService.UpdRatingsJob(beginTime);
        }

        /// <summary>
        /// 更新点赞数据
        /// </summary>
        /// <param name="jobId"></param>
        public async Task<int> UpdSuportsJob(string jobId)
        {
            var connection = JobStorage.Current.GetConnection();
            var job = connection.GetRecurringJobs().FirstOrDefault(s => s.Id == jobId);
            //本次运行时间
            var beginTime = job?.LastExecution ?? DateTime.Now;
            return await _suportRecordService.UpdSuportsJob(beginTime);
        }

        /// <summary>
        /// 清理数据，并整理碎片
        /// </summary>
        /// <returns></returns>
        public async Task<int> OptimizeDatabaseJob()
        {
            return await _ratingRecordService.OptimizeDatabase();
        }

        /// <summary>
        /// 更新豆瓣数据(评分、在线播放源)
        /// 每次生成x个延迟任务
        /// </summary>
        public async Task<List<MovieOrigin_Douban_Output>> UpdDoubanDataJob()
        {
            var movies = await _movieService.GetMoviesByOriginUpdTime(20);
            //5小时内随机分布抓取时间
            var totalMinutes = 300;
            var random = new Random();
            movies.ForEach(movie =>
            {
                var delay = random.Next(1, totalMinutes);
                BackgroundJob.Schedule<IMovieService>(s => s.UpdDoubanData(movie)
                    , TimeSpan.FromMinutes(delay));
            });
            return movies;
        }

        /// <summary>
        /// 抓取影片信息 每次最多2个
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> DownloadMovieInfoJob()
        {
            return await _movieDraftService.AddMovieDraft(2);
        }
    }
}
