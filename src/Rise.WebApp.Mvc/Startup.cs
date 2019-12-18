using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rise.Core.Constants;
using Rise.Core.WebApp.Data;
using Rise.Core.WebApp.Extensions;
using Rise.Email.AntiCorruption;
using Rise.Email.Api.Setup;
using Rise.Students.Data;
using Rise.Users.Data;
using Rise.Users.Domain;
using Rise.WebApp.Mvc.Extensions;
using Syncfusion.Licensing;
using System;

namespace Rise.WebApp.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UsersContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<StudentsContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = ConstSizes.UserPasswordMin;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddRoles<Role>()
            .AddEntityFrameworkStores<UsersContext>();

            services.AddAuthorizationWithPolicies();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.LoginPath = "/Auth/Login";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });

            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(Startup));
            services.InjectDependencies();
            SyncfusionLicenseProvider.RegisterLicense("MTc4NjMyQDMxMzcyZTMzMmUzMElidVVLdGNQQjgxNi95UGNjQVl4MEtMNUFObVNBVzFyV3Z2OStLTHpMRUU9");
            services.AddMediatR(typeof(Startup));
            services.AddEmailProvider()
                .AddSendGrid(options =>
                {
                    options.ApiKey = "SG.39yr7I34S8eQeqAda9i_BA.I_X-L4c_DbBzuPrLZwg9IUIy4_r4dJGWrJTLfPIdD_U";
                    options.SenderEmail = "victor.m.fernandes@outlook.com";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
            });

            seeder.Seed();
        }
    }
}
