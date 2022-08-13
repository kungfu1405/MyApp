using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using Web.Backend.Commons;
using System.IO;
using Mic.Core.Website;

namespace Web.Backend
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            UrlList.Configs = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    SameSiteCookie.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    SameSiteCookie.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            services.AddControllersWithViews();
            IMvcBuilder builder = services.AddRazorPages();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
#if DEBUG
            if (environment == Environments.Development)
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = UrlList.IdServer;
                //options.RequireHttpsMetadata = true;

                options.ClientId = UrlList.IdsClientId; 
                options.ClientSecret = UrlList.IdsClientSecret;
                options.ResponseType = "code";// "code id_token";
                options.SaveTokens = true;

                options.Scope.Add("openid");
                options.Scope.Add("profile");

                options.Scope.Add("srv.websocket");
                options.Scope.Add("srv.file");
                options.Scope.Add("svc.dbdata");
                options.Scope.Add("svc.ddata");
                options.Scope.Add("svc.cache");
                options.Scope.Add("svc.email");
                options.Scope.Add("svc.report");
                options.Scope.Add("api.notify");
                options.Scope.Add("api.web");

            });
            IdentityModelEventSource.ShowPII = true;

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DynamicData", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "svc.ddata");
                });
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            loggerFactory.AddFile(Path.Combine(AppContext.BaseDirectory, "Logs/log-{Date}.txt"));
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "dynamicForm",
                    pattern: "dform/{action=Index}/{fid}",
                    defaults: new { controller = "DForm", action = "Index" });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
