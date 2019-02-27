using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P9YS.EntityFramework;
using P9YS.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.ServiceTests
{
    public class TestBase
    {
        /// <summary>
        /// 内存数据库上下文
        /// </summary>
        public MovieResourceContext movieResourceContext;

        public TestBase()
        {
            //AutoMapper
            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfile(null)));

            //内存数据库
            var options = new DbContextOptionsBuilder<MovieResourceContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;
            movieResourceContext = new MovieResourceContext(options);
        }
    }
}
