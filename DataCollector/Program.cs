namespace DataCollector
{
    using System;
    using System.Reflection;
    using Topshelf;

    internal static class Program
    {

        public static readonly string ExecutablePath = Assembly.GetExecutingAssembly().Location;

        static void Main()
        {
            var rc = HostFactory.Run(x =>
                        {
                            x.Service<PS.PSExecutor>(s =>
                            {
                                s.ConstructUsing(name => new PS.PSExecutor());
                                s.WhenStarted(tc => tc.Start());
                                s.WhenStopped(tc => tc.Stop());
                            });
                            x.RunAsLocalSystem();

                            x.SetDescription("Data collector for Snow Software Logs");
                            x.SetDisplayName("Snow Log Collector");
                            x.SetServiceName("SnowLogCollector");
                        });

                        var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
                        Environment.ExitCode = exitCode;
                        
        }
        
    }
}
