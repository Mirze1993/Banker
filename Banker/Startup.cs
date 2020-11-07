using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banker.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Banker
{
    public class Startup
    {
        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services )
        {
            services.AddControllersWithViews();


           
            MicroORM.ORMConfig.ConnectionString = "Server=.\\SQLExpress;Database=Banker;Trusted_Connection=True;";
            MicroORM.ORMConfig.DbType = MicroORM.DbType.MSSQL;

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option=> {
                option.LoginPath = "/Home/Login";
                option.LogoutPath = "/Home/Login";
                option.Cookie.Name = "Banker";
            });


            services.AddTransient<IAppUserRepositoty, AppUsersRepository>();
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
