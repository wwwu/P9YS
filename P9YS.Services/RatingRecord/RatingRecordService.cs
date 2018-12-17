using Microsoft.EntityFrameworkCore;
using P9YS.EntityFramework;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Services.RatingRecord
{
    public class RatingRecordService : IRatingRecordService
    {
        private readonly MovieResourceContext _movieResourceContext;
        private readonly IUserService _userService;
        public RatingRecordService(MovieResourceContext movieResourceContext
            , IUserService userService)
        {
            _movieResourceContext = movieResourceContext;
            _userService = userService;
        }

        public async Task<bool> AddRatingRecord(Dto.RatingRecordInput ratingRecordInput)
        {
            var result = false;
            var user = _userService.GetCurrentUser();
            var isExist = await _movieResourceContext.RatingRecords
                .AnyAsync(s => s.MovieId == ratingRecordInput.MovieId
                    && s.UserId == user.UserId
                    && s.AddTime > DateTime.Today);
            if (!isExist)
            {
                var ratingRecord = new EntityFramework.Models.RatingRecord
                {
                    MovieId = ratingRecordInput.MovieId,
                    UserId = user.UserId,
                    Score = ratingRecordInput.Score,
                    AddTime = DateTime.Now
                };
                await _movieResourceContext.RatingRecords.AddAsync(ratingRecord);
                var rows = await _movieResourceContext.SaveChangesAsync();
                result = rows > 0;
            }
            return result;
        }
    }
}
