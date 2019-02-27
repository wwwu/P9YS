using Microsoft.Extensions.Caching.Memory;
using Moq;
using P9YS.EntityFramework.Models;
using P9YS.Services;
using P9YS.Services.Base;
using P9YS.Services.Carousel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace P9YS.ServiceTests
{
    public class CarouselServiceTests:TestBase
    {
        private IMemoryCache memoryCache;
        private Mock<IBaseService> baseService;
        private List<Carousel> carousels;

        public CarouselServiceTests()
        {
            memoryCache = new MemoryCache(new MemoryCacheOptions());
            baseService = new Mock<IBaseService>();

            carousels = new List<Carousel>
            {
                new Carousel{ Id=1,Title="¸´Áª1",ImgUrl="url1",Link="link1",State=Common.Enums.CarouselStateEnum.Hide },
                new Carousel{ Id=2,Title="¸´Áª2",ImgUrl="url2",Link="link2",State=Common.Enums.CarouselStateEnum.Show },
                new Carousel{ Id=3,Title="¸´Áª3",ImgUrl="url3",Link="link3",State=Common.Enums.CarouselStateEnum.Show }
            };
        }

        [Fact]
        public async Task GetCarousels_Success_Test()
        {
            //fake data
            movieResourceContext.Carousels.AddRange(carousels);
            movieResourceContext.SaveChanges();
            //act
            var carouselService = new CarouselService(movieResourceContext
                , memoryCache
                , baseService.Object);
            var actBeforeCache = memoryCache.Get(CacheKeys.Carousels);
            var result = await carouselService.GetCarouselsAsync();
            //assert
            var actAfterCache = memoryCache.Get(CacheKeys.Carousels);
            Assert.True(actBeforeCache == null);
            Assert.True(actAfterCache != null);
            Assert.True(result.Count == 2);
            Assert.True(result.FirstOrDefault()?.Title == carousels.Last().Title);
        }
    }
}
