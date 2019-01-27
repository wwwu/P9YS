using Microsoft.EntityFrameworkCore;
using P9YS.EntityFramework;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.SuportRecord
{
    public class SuportRecordService : ISuportRecordService
    {
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        public SuportRecordService(MovieResourceContext movieResourceContext
            , IUserService userService)
        {
            _movieResourceContext = movieResourceContext;
            _userService = userService;
        }

        public async Task<bool> AddSuportRecord(Dto.SuportRecordInput suportRecordInput)
        {
            var result = false;
            var user = _userService.GetCurrentUser();
            var isExist = await _movieResourceContext.SuportRecords
                .AnyAsync(s => s.MovieQuestionAnswerId == suportRecordInput.AnswerId
                    && s.UserId == user.UserId
                    && s.AddTime > DateTime.Today);
            if (!isExist)
            {
                var suportRecord = new EntityFramework.Models.SuportRecord
                {
                    MovieQuestionAnswerId = suportRecordInput.AnswerId,
                    UserId = user.UserId,
                    AddTime = DateTime.Now
                };
                await _movieResourceContext.SuportRecords.AddAsync(suportRecord);
                var rows = await _movieResourceContext.SaveChangesAsync();
                result = rows > 0;
            }
            return result;
        }

        /// <summary>
        /// 更新点赞数据
        /// </summary>
        /// <param name="endTime"></param>
        public void UpdSuportsJob(DateTime endTime)
        {
            //取最后一次标记
            var markTime = _movieResourceContext.SuportRecords.Max(s => s.Mark);
            //取时间段内数据
            var query = _movieResourceContext.SuportRecords
                .Where(s => s.AddTime <= endTime);
            if (markTime.HasValue)
                query = query.Where(s => s.AddTime > markTime.Value);
            var suportRecords = query.ToList();
            //按MovieQuestionAnswerId分组聚合
            var suportGroups = suportRecords.GroupBy(s => new { s.MovieQuestionAnswerId })
                .Select(s => new { s.Key.MovieQuestionAnswerId, Count = s.Count() })
                .ToList();
            //查出所有movie
            var movieQuestionAnswers = _movieResourceContext.MovieQuestionAnswers
                .Where(s => suportGroups.Select(r => r.MovieQuestionAnswerId).Contains(s.Id))
                .ToList();
            movieQuestionAnswers.ForEach(item =>
            {
                var suportGroup = suportGroups.FirstOrDefault(s => s.MovieQuestionAnswerId == item.Id);
                item.Support += suportGroup.Count;
            });
            //更新标记
            if (suportGroups.Count > 0)
            {
                suportRecords.Last().Mark = endTime;
                var rows = _movieResourceContext.SaveChanges();
            }
        }
    }
}
