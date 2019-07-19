namespace DataCollector
{
    using DataCollector.DB;
    using Serilog;
    using System;
    using System.IO;
    using System.Reflection;
    using Topshelf;

    internal static class Program
    {
        public static readonly string ExecutablePath = Assembly.GetExecutingAssembly().Location;

        static async System.Threading.Tasks.Task Main()
        {
            var currentPath = Path.GetDirectoryName(ExecutablePath);
            var logPath = Path.Combine(Path.Combine(currentPath, "Logs"), "DataCollector.log");

            var rc = HostFactory.Run(x =>
                        {
                            x.Service<PS.PSScheduleService>(s =>
                            {
                                s.ConstructUsing(name => new PS.PSScheduleService());
                                s.WhenStarted(tc => tc.Start());
                                s.WhenStopped(tc => tc.Stop());
                            });
                            x.Service<DB.DBScheduleService>(s =>
                            {
                                s.ConstructUsing(name => new DB.DBScheduleService());
                                s.WhenStarted(tc => tc.Start());
                                s.WhenStopped(tc => tc.Stop());
                            });
                            x.RunAsLocalSystem();

                            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .WriteTo.Console()
                            .WriteTo.File(logPath,
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            fileSizeLimitBytes: 5000_000)
                            .CreateLogger();

                            x.UseSerilog(Log.Logger);

                            x.SetDescription("Data collector for Snow Software Logs");
                            x.SetDisplayName("Snow Log Collector");
                            x.SetServiceName("SnowLogCollector");
                        });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;


        }
        
    }
}
