using Hangfire;
using Hangfire.Common;
using Hangfire.Storage;
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

        public JobService(IRatingRecordService ratingRecordService
            , ISuportRecordService suportRecordService)
        {
            this._ratingRecordService = ratingRecordService;
            this._suportRecordService = suportRecordService;
        }
        
        /// <summary>
        /// 更新评分数据
        /// </summary>
        /// <param name="jobId"></param>
        public void UpdRatingsJob(string jobId)
        {
            var connection = JobStorage.Current.GetConnection();
            var job = connection.GetRecurringJobs().FirstOrDefault(s => s.Id == jobId);
            //本次次运行时间
            var beginTime = job?.LastExecution ?? DateTime.Now;
            //bug,async会丢失上下文
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
    }
}
