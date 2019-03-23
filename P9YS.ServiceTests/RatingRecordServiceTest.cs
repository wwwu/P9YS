using Moq;
using P9YS.EntityFramework.Models;
using P9YS.Services;
using P9YS.Services.RatingRecord;
using P9YS.Services.RatingRecord.Dto;
using P9YS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P9YS.ServiceTests
{
    public class RatingRecordServiceTest : TestBase
    {
        #region 数据

        private List<RatingRecord> ratingRecords = new List<RatingRecord>
        {
            new RatingRecord{ MovieId=1,Score=7,UserId=1,AddTime=DateTime.Now },
            new RatingRecord{ MovieId=1,Score=8,UserId=1,AddTime=DateTime.Now },
            new RatingRecord{ MovieId=1,Score=9,UserId=1,AddTime=DateTime.Now },
        };

        #endregion

        public RatingRecordServiceTest()
        {
        }

        [Fact]
        public async Task AddRatingRecord_Test()
        {
            //Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.GetCurrentUser()).Returns(new Services.User.Dto.CurrentUser { UserId = 1 });
            var service = new RatingRecordService(mapper, dbContext, userService.Object);
            //Act
            var result = await service.AddRatingRecord(new RatingRecord_Input
            {
                MovieId = 1,
                Score = 8
            });
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetRatings_Test()
        {
            //Arrange
            dbContext.RatingRecords.AddRange(ratingRecords);
            dbContext.Movies.Add(new Movie { Id = 1, ShortName = "m" });
            dbContext.Users.Add(new User { Id = 1, Email="111@p9ys.com" });
            await dbContext.SaveChangesAsync();
            var service = new RatingRecordService(mapper, dbContext, null);
            //Act
            var input = new PagingInput<GetRatings_Input> { PageSize = 2 };
            var result = await service.GetRatings(input);
            //Assert
            Assert.True(result.TotalCount == ratingRecords.Count);
            Assert.True(result.Data.First().Id == ratingRecords.Last().Id);
        }

        [Fact]
        public async Task UpdRatingsJob_Test()
        {
            //Arrange
            dbContext.RatingRecords.AddRange(ratingRecords);
            dbContext.Movies.Add(new Movie { Id = 1, Score = 0, ScoreCount = 0, ScoreSum = 0 });
            await dbContext.SaveChangesAsync();
            var service = new RatingRecordService(mapper, dbContext, null);
            //Act
            var endTime = DateTime.Now;
            var result = await service.UpdRatingsJob(endTime);
            //Assert
            Assert.True(result>0);
            Assert.True(dbContext.Movies.First().ScoreCount == ratingRecords.Count);
            Assert.True(dbContext.Movies.First().ScoreSum == ratingRecords.Sum(s => s.Score));
            Assert.True(dbContext.Movies.First().Score == ratingRecords.Average(s=>s.Score));
            Assert.True(dbContext.RatingRecords.Last().Mark == endTime);
        }

        //[Fact]
        //public async Task OptimizeDatabase_Test()
        //{
        //    //Arrange
        //    ratingRecords.ForEach(item => item.AddTime = DateTime.Now.AddMonths(-2));
        //    dbContext.RatingRecords.AddRange(ratingRecords);
        //    await dbContext.SaveChangesAsync();
        //    var service = new RatingRecordService(mapper, dbContext, null);
        //    //Act
        //    var result = await service.OptimizeDatabase();
        //    //Assert
        //    Assert.True(result == ratingRecords.Count);
        //}
    }
}
