using System;
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
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo("C:\\P9ysCookieShare"));

            //设置身份验证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                {
                    option.Cookie.Domain = Configuration.GetSection("CookieDomain").Value;
                    option.LoginPath = "/Member/Login";
                    option.Cookie.HttpOnly = true;
                });

            //Session
            services.AddSession();

            #endregion

            //配置文件
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddHttpContextAccessor();

            #region 配置DI

            services.AddScoped<EntityFramework.MovieResourceContext>();
            services.AddSingleton<BaseService>();
            //批量注入Service
            var dics = BaseHelper.GetClassName("P9YS.Services", t => t.Name.EndsWith("Service") && !t.IsInterface);
            foreach (var item in dics)
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            #endregion

            //AutoMapper
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfile(appSettings)));

            //Json输出时间格式
            services.AddMvc().AddJsonOptions(option =>
            {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            
            //Filter
            services.AddMvc(option =>
            {
                //模型验证
                option.Filters.Add(typeof(ModelStateFilterAttribute));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
