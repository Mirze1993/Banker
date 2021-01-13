using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Banker
{
    public class Startup
    {
        public IWebHostEnvironment  Environment { get; }
        public IConfiguration Config { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration config)
        {
            Environment = environment;
            Config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services )
        {
            services.AddControllersWithViews();


            //sdf
            MicroORM.ORMConfig.ConnectionString = Config.GetConnectionString("DefaultConnection");
            MicroORM.ORMConfig.DbType = MicroORM.DbType.MSSQL;
            MicroORM.Logging.FileLoggerOptions.FolderPath = System.IO.Path.Combine(this.Environment.WebRootPath,"Log");


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option=> {
                option.LoginPath = "/Home/Login";
                option.LogoutPath = "/Home/Index";
                option.Cookie.Name = "Banker";                
            });


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStatusCodePagesWithRedirects("/Error?statusCode={0}");
            app.UseStatusCodePagesWithReExecute("/Error","?statusCode={0}");
            app.UseExceptionHandler("/Exception");

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
