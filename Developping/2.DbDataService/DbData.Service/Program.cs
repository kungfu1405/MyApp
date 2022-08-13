using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using Serilog.Events;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DbData.Service
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
                Log.Information("Host Stopped!");
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

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureServices((context, services) =>
                {
                    services.Configure<KestrelServerOptions>(
                        context.Configuration.GetSection("Kestrel"));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        webBuilder.ConfigureKestrel(options =>
                        {
                            // Setup a HTTP/2 endpoint without TLS.
                            //options.ListenLocalhost(5101, o => o.Protocols =
                            //    HttpProtocols.Http2);
                        });
                    }
                    webBuilder.UseStartup<Startup>();
                })
                .UseWindowsService();
    }
}
