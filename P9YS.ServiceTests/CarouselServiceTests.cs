using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using P9YS.EntityFramework;
using P9YS.EntityFramework.Models;
using P9YS.Services;
using P9YS.Services.Carousel;
using P9YS.Services.Carousel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace P9YS.ServiceTests
{
    public class CarouselServiceTests: TestBase
    {
        private IMemoryCache memoryCache;
        private List<Carousel> carousels = new List<Carousel>
        {
            new Carousel{ Id=1,Title="∏¥¡™1",ImgUrl="url1",Link="link1",State=Common.Enums.CarouselStateEnum.Hide },
            new Carousel{ Id=2,Title="∏¥¡™2",ImgUrl="url2",Link="link2",State=Common.Enums.CarouselStateEnum.Show },
            new Carousel{ Id=3,Title="∏¥¡™3",ImgUrl="url3",Link="link3",State=Common.Enums.CarouselStateEnum.Show }
        };

        public CarouselServiceTests()
        {
            memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        [Fact]
        public async Task GetCarouselsAsync_Test()
        {
            //Arrange
            dbContext.Carousels.AddRange(carousels);
            dbContext.SaveChanges();
            var carouselService = new CarouselService(mapper, dbContext, memoryCache, baseService.Object);
            //Act
            var actBeforeCache = memoryCache.Get(CacheKeys.Carousels);
            var result = await carouselService.GetCarousels();
            //Assert
            var actAfterCache = memoryCache.Get(CacheKeys.Carousels);
            Assert.True(actBeforeCache == null);
            Assert.True(actAfterCache != null);
            Assert.True(result.Count == 2);
            Assert.True(result.FirstOrDefault()?.Title == carousels.Last().Title);
        }

        [Fact]
        public async Task GetCarouselsAsync_Paging_Test()
        {
            //Arrange
            dbContext.Carousels.AddRange(carousels);
            dbContext.SaveChanges();
            var carouselService = new CarouselService(mapper, dbContext, memoryCache, baseService.Object);
            //Act
            var input = new PagingInput { PageSize = 2 };
            var result = await carouselService.GetCarousels(input);
            //Assert
            Assert.True(result.TotalCount == carousels.Count);
            Assert.True(result.Data.Count == input.PageSize);
        }

        [Fact]
        public async Task AddCarouselAsync_Test()
        {
            //Arrange
            baseService.Setup(s => s.UploadFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(new Result());
            var carouselService = new CarouselService(mapper, dbContext, memoryCache, baseService.Object);
            //Act
            var input = new Carouselnput
            {
                ImgData = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVQImWNgYGBgAAAABQABh6FO1AAAAABJRU5ErkJggg==",
                Link = "http://p9ys.com",
                State = Common.Enums.CarouselStateEnum.Show,
                Title = "≤‚ ‘"
            };
            var result = await carouselService.AddCarousel(input);
            //Assert
            Assert.True(result.Code == Common.CustomCodeEnum.Success);
            Assert.True(result.Content != null);
        }

        [Fact]
        public async Task UpdCarouselAsync_Test()
        {
            //Arrange
            dbContext.Carousels.AddRange(carousels);
            dbContext.SaveChanges();
            var carouselService = new CarouselService(mapper, dbContext, memoryCache, baseService.Object);
            var uploadFailed = new Result();
            baseService.Setup(s => s.UploadFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(uploadFailed);
            //Act
            var input = new Carouselnput
            {
                Id = 1,
                ImgData = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVQImWNgYGBgAAAABQABh6FO1AAAAABJRU5ErkJggg==",
                Link = "http://p9ys.com",
                State = Common.Enums.CarouselStateEnum.Show,
                Title = "≤‚ ‘"
            };
            var result = await carouselService.UpdCarousel(input);
            //Assert
            Assert.True(result.Code == Common.CustomCodeEnum.Success);
            var entity = (Carousel)result.Content;
            Assert.True(entity.Title == input.Title);
        }
    }
}
