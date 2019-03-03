using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using P9YS.EntityFramework;
using P9YS.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.ServiceTests
{
    public class TestBase: IDisposable
    {
        /// <summary>
        /// 内存数据库上下文
        /// </summary>
        public MovieResourceContext dbContext { get; private set; }

        /// <summary>
        /// AutoMapper
        /// </summary>
        public IMapper mapper { get; private set; }


        public Mock<IBaseService> baseService;

        public TestBase()
        {
            //内存数据库
            var options = new DbContextOptionsBuilder<MovieResourceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            dbContext = new MovieResourceContext(options);

            //AutoMapper
            mapper = new Mapper(new MapperConfiguration(config => {
                config.AddProfile(new AutoMapperProfile(null));
            }));
            
            baseService = new Mock<IBaseService>();
        }

        public void Dispose()
        {
            dbContext?.Dispose();
        }
    }
}
