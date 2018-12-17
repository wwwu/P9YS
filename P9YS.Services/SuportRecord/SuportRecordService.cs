using Microsoft.EntityFrameworkCore;
using P9YS.EntityFramework;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
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
    }
}
