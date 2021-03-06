﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BotDetect.Web;
using InternetShop.BLL.Interfaces;
using InternetShop.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternetShop.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InternetShop.DAL.Models;
using InternetShop.Middleware;
using InternetShop.WEB.Models;
using InternetShop.WEB.Requirements;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


namespace InternetShop
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


            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
             
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = AuthOptions.Issuer,
                        ValidAudience = AuthOptions.Audience,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ClockSkew = TimeSpan.Zero
                    };
                });
                    services.AddTransient<IAuthorizationHandler, AgeRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, EmailRequirementHandler>();
            services.AddTransient<UserService>();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Register");
            //        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Register");
            //    });
            //services.ConfigureApplicationCookie(options => {
            //    // Cookie settings
            //    //options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    // If the LoginPath isn't set, ASP.NET Core defaults 
            //    // the path to /Account/Login.
            //    options.LoginPath = "/Account/Login";
            //    // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
            //    // the path to /Account/AccessDenied.
            //    options.AccessDeniedPath = "/Account/Login";
            //    options.SlidingExpiration = true;
            //});
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AgeLimit", pol => pol.Requirements.Add(new AgeRequirement(30)));
                opt.AddPolicy("GmailOnly", pol => pol.Requirements.Add(new EmailRequirement("gmail.com")));
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
            services.AddMemoryCache();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(20));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseCaptcha(Configuration);

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseToken();

            app.UseRedirectUnautorize();
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
