using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Repositories;
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.Services;

namespace PhotoHub.WEB
{
    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region .ctors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Logic

        public void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ApplicationDbContextSeeder>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<ILikesService, LikesService>();
            services.AddScoped<IPhotosService, PhotosService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITagsService, TagsService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "898819496715-g0dk8mimqmmsl93c6o3hlm6j65qhahqc.apps.googleusercontent.com";
                googleOptions.ClientSecret = "JTdwBEP7c0FN5r9Xk1WcMdVm";
            }).AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "1458237504267258";
                facebookOptions.AppSecret = "cb13b1b64b2735b45f3837ecc5f79ad0";
            }).AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = "oieC1IDbdXx9dJeWeADGBnYJY";
                twitterOptions.ConsumerSecret = "tQG8nqAOzlUJqwcZix1yEecKjRAhrAM9jeUZLxFhfI6nvuS59z";
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 4;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
            
            AddApplicationServices(services);

            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider services, ApplicationDbContextSeeder seeder) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else if(env.IsProduction())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Cover}/{id?}");
            });

            //seeder.Seed().Wait();
            //seeder.CreateUserRoles(services).Wait();
        }

        #endregion
    }
}
