using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using DbData.Service.Commons;
using DbData.Dal;
using DbData.Dal.MySql;
using Microsoft.IdentityModel.Tokens;
using Mic.Core.Security;
using Mic.UserDb.Dal;
using Mic.UserDb.Dal.MsSql;
using Mic.UserDb.Dal.MySql;
using DbData.Dal.MsSql;
using DbData.Service.Services;
using System.IO;
using Mic.UserDb.Entities;


namespace DbData.Service
{
    public class Startup
    {
        private string connectionString = "";

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            UrlList.Configs = Configuration;
            initConnections();
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            setDbConfigurations(services);

            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.Authority = UrlList.IdServer;
                //options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DbDataScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "svc.dbdata");
                });
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // DB migrations
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
            using (var dbContext = serviceProvider.GetService<IDbDataContext>())
            {
                dbContext.Database.Migrate();
            }
            using (var usrContext = serviceProvider.GetService<IUserDbContext>())
            {
                usrContext.Database.Migrate();
            }

            loggerFactory.AddFile(Path.Combine(AppContext.BaseDirectory, "Logs/log-{Date}.txt"));
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<UserAuthenticateService>().AllowAnonymous();
                endpoints.MapGrpcService<UserService>();
                endpoints.MapGrpcService<RoleService>();
                endpoints.MapGrpcService<UsergroupService>();
                endpoints.MapGrpcService<LanguageService>();
                endpoints.MapGrpcService<LanguageDataService>();
                endpoints.MapGrpcService<LanguageDataLocalService>();
                endpoints.MapGrpcService<WebPageService>();

                endpoints.MapGrpcService<ContinentService>();
                endpoints.MapGrpcService<CountryService>();
                endpoints.MapGrpcService<StateService>();
                endpoints.MapGrpcService<TagService>();
                endpoints.MapGrpcService<CityService>();

                endpoints.MapGrpcService<AttractionService>();
                endpoints.MapGrpcService<DestinationService>();
                endpoints.MapGrpcService<ExperienceService>();
                endpoints.MapGrpcService<ExperienceSessionService>();
                endpoints.MapGrpcService<CommentService>();
                endpoints.MapGrpcService<UserProfileService>();
                endpoints.MapGrpcService<AttPropertyService>();
                endpoints.MapGrpcService<VehicleService>();
                endpoints.MapGrpcService<AttractionLanguageService>();
                endpoints.MapGrpcService<DestinationLanguageService>();
              
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Invalid request");
                });
            });
        }

        #region init configs
        private void initConnections()
        {
            var aescrypt = new AES();
            var user = aescrypt.Decode(Configuration.GetConnectionString("User"));
            var pass = aescrypt.Decode(Configuration.GetConnectionString("Password"));

            switch (Configuration.GetConnectionString("DbType").ToUpper())
            {
                case "MSSQL":
                    connectionString = $"Data Source={Configuration.GetConnectionString("Server")};Initial Catalog={Configuration.GetConnectionString("Database")};user id={user};password={pass};MultipleActiveResultSets=True;persist security info=True";
                    break;
                case "MYSQL":
                    connectionString = $"server={Configuration.GetConnectionString("Server")}; database={Configuration.GetConnectionString("Database")}; user={user}; password={pass}";
                    break;
                default:
                    throw new NotSupportedException($"Database Type \"{Configuration.GetConnectionString("DbType")}\" is not supported!");
            }
        }

        private void setDbConfigurations(IServiceCollection services)
        {
            switch (Configuration.GetConnectionString("DbType").ToUpper())
            {
                case "MSSQL":
                    services.AddDbContext<MsSqlDbDataContext>(options
                        => options.UseSqlServer(connectionString
                        , o => o.MigrationsAssembly("DbData.Dal.MsSql"))
                        , ServiceLifetime.Transient);
                    services.AddScoped<IDbDataContext, MsSqlDbDataContext>();

                    services.AddDbContext<MsSqlUserDbContext>(options
                        => options.UseSqlServer(connectionString
                        , o => o.MigrationsAssembly("Mic.UserDb.Dal.MsSql"))
                        , ServiceLifetime.Transient);
                    services.AddScoped<IUserDbContext, MsSqlUserDbContext>();
                    break;
                case "MYSQL":
                    services.AddDbContext<MySqlDbDataContext>(options
                        => options.UseMySql(connectionString
                        , ServerVersion.AutoDetect(connectionString)
                        , o => o.MigrationsAssembly("DbData.Dal.MySql"))
                        , ServiceLifetime.Transient);
                    services.AddScoped<IDbDataContext, MySqlDbDataContext>();

                    services.AddDbContext<MySqlUserDbContext>(options
                        => options.UseMySql(connectionString
                        , ServerVersion.AutoDetect(connectionString)
                        , sql => sql.MigrationsAssembly("Mic.UserDb.Dal.MySql"))
                        , ServiceLifetime.Transient);
                    services.AddScoped<IUserDbContext, MySqlUserDbContext>();
                    break;
                default:
                    throw new NotSupportedException($"Database Type \"{Configuration.GetConnectionString("DbType")}\" is not supported!");
            }
        }
        #endregion init configs
    }
}
