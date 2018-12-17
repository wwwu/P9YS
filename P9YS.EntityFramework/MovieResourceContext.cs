﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using P9YS.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.EntityFramework
{
    public class MovieResourceContext : DbContext
    {
        public IConfiguration Configuration { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieArea> MovieAreas { get; set; }
        public virtual DbSet<MovieGenre> MovieGenres { get; set; }
        public virtual DbSet<Carousel> Carousels { get; set; }
        public virtual DbSet<MovieRecommend> MovieRecommends { get; set; }
        public virtual DbSet<MovieOrigin> MovieOrigins { get; set; }
        public virtual DbSet<MovieComment> MovieComments { get; set; }
        public virtual DbSet<MovieQuestion> MovieQuestions { get; set; }
        public virtual DbSet<MovieQuestionAnswer> MovieQuestionAnswers { get; set; }
        public virtual DbSet<MovieResource> MovieResources { get; set; }
        public virtual DbSet<MovieOnlinePlay> MovieOnlinePlays { get; set; }
        public virtual DbSet<RatingRecord> RatingRecords { get; set; }
        public virtual DbSet<SuportRecord> SuportRecords { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = $"appsettings.{envName}.json", ReloadOnChange = true })
                .Build();
            optionsBuilder.UseMySql(Configuration["AppSettings:MovieResourceContext"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 索引

            #endregion

            #region 种子数据

            //更新表结构时也会执行，迁移过程中可能会导致数据丢失，慎用。建议到Program初始化数据
            //modelBuilder.Entity<MovieArea>().HasData(SeedData.GetDefaultMoiveAreas());
            //modelBuilder.Entity<MovieGenre>().HasData(SeedData.GetDefaultMovieGenres());

            #endregion
        }
    }
}
