using Moq;
using P9YS.Common.Enums;
using P9YS.EntityFramework.Models;
using P9YS.Services;
using P9YS.Services.Movie;
using P9YS.Services.Movie.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using P9YS.Common;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace P9YS.ServiceTests
{
    public class MovieServiceTests : TestBase
    {
        #region 初始数据

        private List<MovieGenre> movieGenres = new List<MovieGenre>
        {
            new MovieGenre{ Id=1,Name="科幻",AddTime=DateTime.Now },
            new MovieGenre{ Id=2,Name="动作",AddTime=DateTime.Now },
            new MovieGenre{ Id=3,Name="冒险",AddTime=DateTime.Now },
        };
        private List<Movie> movies = new List<Movie>
        {
            new Movie{
                Id=1,
                ShortName = "复联1",
                FullName ="复仇者联盟1",
                OtherName ="",
                SeriesId = 100,
                MovieAreaId = 1,
                MovieTypes = new List<MovieType>
                {
                    new MovieType{ MovieGenreId = 1 },
                    new MovieType{ MovieGenreId = 2 },
                }
            },
            new Movie{
                Id=2,
                ShortName = "复联2",
                FullName ="复仇者联盟2",
                OtherName ="",
                SeriesId = 100,
                MovieAreaId = 2,
                MovieTypes = new List<MovieType>
                {
                    new MovieType{ MovieGenreId = 1 },
                    new MovieType{ MovieGenreId = 2 },
                    new MovieType{ MovieGenreId = 3 },
                }
            }
        };
        private List<MovieArea> movieAreas = new List<MovieArea>
        {
            new MovieArea { Id = 1, Area = "美国", Other = 1 },
            new MovieArea { Id = 2, Area = "古巴", Other = 0 }
        };
        private List<MovieOrigin> movieOrigins = new List<MovieOrigin>
        {
            new MovieOrigin{ MovieId =1,OriginType =MovieOriginTypeEnum.DouBan,Score=8 },
            new MovieOrigin{ MovieId =2,OriginType =MovieOriginTypeEnum.DouBan,Score=9 }
        };

        #endregion

        public MovieServiceTests()
        {

        }

        [Theory]
        [InlineData(0, 3)]
        [InlineData(1, 1)]
        public async Task GetMoviesByConditionAsync_Test(int areaId,int genreId)
        {
            //Arrange
            dbContext.MovieAreas.AddRange(movieAreas);
            dbContext.MovieGenres.AddRange(movieGenres);
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var input = new PagingInput<GetMovies_Condition_Input>
            {
                Condition = new GetMovies_Condition_Input
                {
                    Keyword = "复仇者",
                    MovieAreaId = areaId,
                    MovieTypeId = genreId,
                    Sort = MovieListSortEnum.LastModify,
                }
            };
            var result = await movieService.GetMoviesByCondition(input);
            //Assert
            if (areaId == 0)
                Assert.True(result.Data[0].ShortName == movies[1].ShortName);
            else
                Assert.True(result.Data[0].ShortName == movies[0].ShortName);
        }

        [Fact]
        public async Task GetMovieInfoAsync_Test()
        {
            //Arrange
            dbContext.MovieAreas.AddRange(movieAreas);
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var result = await movieService.GetMovieInfo(1);
            //Assert
            Assert.True(result.Id == 1);
        }

        [Fact]
        public async Task GetMovieSeriesAsync_Test()
        {
            //Arrange
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var result = await movieService.GetMovieSeries(100);
            //Assert
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task GetMovieOriginAsync_Test()
        {
            //Arrange
            dbContext.MovieOrigins.AddRange(movieOrigins);
            dbContext.SaveChanges();
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var result = await movieService.GetMovieOrigin(1);
            //Assert
            Assert.True(result.Score == 8);
        }

        [Fact]
        public async Task GetMoviesAsync_Test()
        {
            //Arrange
            dbContext.MovieAreas.AddRange(movieAreas);
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var input = new PagingInput<GetMovies_Condition_Input>
            {
                Condition = new GetMovies_Condition_Input
                {
                    Keyword = "复仇者",
                },
                PageSize = 1
            };
            var result = await movieService.GetMovies(input);
            //Assert
            Assert.True(result.Data.Count == 1);
        }

        [Fact]
        public async Task GetMovieAsync_Test()
        {
            //Arrange
            dbContext.MovieAreas.AddRange(movieAreas);
            dbContext.MovieGenres.AddRange(movieGenres);
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var movieId = 1;
            var result = await movieService.GetMovie(movieId);
            //Assert
            Assert.True(result.ShortName == movies.First(s => s.Id == movieId).ShortName);
        }

        [Fact]
        public async Task UpdMovieAsync_Test()
        {
            //Arrange
            dbContext.MovieAreas.AddRange(movieAreas);
            dbContext.MovieGenres.AddRange(movieGenres);
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            baseService.Setup(s => s.UploadFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(new Result());
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var input = new Movie_Manage_Input
            {
                Id = 1,
                ShortName = "妇联",
                MovieTypes = new List<int>(),
            };
            var result = await movieService.UpdMovie(input);
            //Assert
            Assert.True(result.Code == CustomCodeEnum.Success);
        }

        [Fact]
        public async Task DelMovieAsync_Test()
        {
            //Arrange
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            var entryState = dbContext.Entry(movies[0]).State;  // 此时为Unchanged
            //Mark: movieService中的Remove方法和模拟数据(Arrange)时所用到的是同一个dbContext，此时movies对象的EntryState为Unchanged
            //由于实体对象还在被追踪，导致The instance of entity type 'Movie' cannot be tracked
            dbContext.Movies.Attach(movies[0]).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var movieId = movies[0].Id;
            var result = await movieService.DelMovie(movieId);
            //Assert
            Assert.True(result.Code == CustomCodeEnum.Success);
        }

        [Fact]
        public async Task GetMoviesByOriginUpdTime_Test()
        {
            //Arrange
            dbContext.MovieOrigins.AddRange(movieOrigins);
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //Act
            var result = await movieService.GetMoviesByOriginUpdTime(2);
            //Assert
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task UpdDoubanData_Test()
        {
            //Arrange
            var movieService = new MovieService(mapper, dbContext, baseService.Object);
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddHttpClient();
            //var clientFactory = serviceCollection.BuildServiceProvider().GetService<IHttpClientFactory>();
            //var movieService = new MovieService(mapper, dbContext
            //    , new Services.Base.BaseService(null, null, clientFactory));
            //Act
            var input = new MovieOrigin_Douban_Output
            {
                MovieId = 1,
                FullName = "海王",                
            };
            await movieService.UpdDoubanData(input);
            //Assert
        }
    }
}
