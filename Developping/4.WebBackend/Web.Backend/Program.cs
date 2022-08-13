using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Web.Backend
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                .WriteTo.File(
                    Path.Combine(AppContext.BaseDirectory, "Logs/_server.txt"),
                    fileSizeLimitBytes: 1_000_000,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.Configure<KestrelServerOptions>(
                        context.Configuration.GetSection("Kestrel"));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        //serverOptions.Limits.MaxConcurrentConnections = 100;
                        //serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                        //serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                        //serverOptions.Limits.MinRequestBodyDataRate =
                        //    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                        //serverOptions.Limits.MinResponseDataRate =
                        //    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                        //serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                        //serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                    })
                    .UseStartup<Startup>();
                });
    }
}
