using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System.IO;
using System.Reflection;

namespace ServerMonitorAPI
{
    public static class Program
    {
        public static readonly string ExecutablePath = Assembly.GetExecutingAssembly().Location;

        public static int Main(string[] args)
        {
            var currentPath = Path.GetDirectoryName(ExecutablePath);
            var logPath = Path.Combine(Path.Combine(currentPath, "Logs"), "DataCollector");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(logPath,
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true)
                            .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (System.Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .UseSerilog();
    }
}
