﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P9YS.Common;
using P9YS.Services.Base;
using Hangfire;
using P9YS.HangfireJobs;
using Hangfire.MySql.Core;
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;

namespace P9YS.Manage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Base
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();
            //配置文件
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //Json输出时间格式
            services.AddMvc().AddJsonOptions(option =>
            {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            services.AddHttpClient();
            services.AddHttpClient("tls").ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    SslProtocols = System.Security.Authentication.SslProtocols.Tls12
                };
            });
            #endregion

            #region Cookies
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //共享cookies
            services.AddDataProtection()
                .SetApplicationName("cookieshare")
                .PersistKeysToDbContext<EntityFramework.MovieResourceContext>();
                //.PersistKeysToFileSystem(new System.IO.DirectoryInfo("C:\\P9ysCookieShare"));
            //设置身份验证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                {
                    option.Cookie.Domain = Configuration.GetSection("CookieDomain").Value;
                    option.LoginPath = "/Member/Login/?returnurl=/";
                    option.Cookie.HttpOnly = true;
                });
            //Session
            services.AddSession();
            #endregion

            #region 配置DI
            services.AddDbContextPool<EntityFramework.MovieResourceContext>(options =>
                options.UseMySql(Configuration["AppSettings:MovieResourceContext"]), poolSize: 100);
            services.AddScoped<IJobService, JobService>();
            //批量注册Service
            var dics = BaseHelper.GetClassName("P9YS.Services", t => t.Name.EndsWith("Service") && !t.IsInterface);
            foreach (var item in dics)
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }
            #endregion

            #region AutoMapper
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            AutoMapper.IConfigurationProvider mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile(appSettings));
            });
            services.AddSingleton(mapperConfig);
            services.AddScoped<IMapper, Mapper>();
            #endregion

            #region Filter
            services.AddMvc(option =>
            {
                //模型验证
                option.Filters.Add(typeof(ModelStateFilterAttribute));
                //未捕获异常
                option.Filters.Add(typeof(CustomExceptionFilterAttribute));
            });
            #endregion

            #region Hangfire
            var connectionString = Configuration["AppSettings:HangfireContext"];
            services.AddHangfire(s => s.UseStorage(new MySqlStorage(connectionString)));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseStatusCodePages("text/plain", "Status Code: {0}");

            //Hangfire            
            HangfireJobs.Startup.Run(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
