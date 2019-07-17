using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Timers;
using System;
using Serilog;
using System.Reflection;
using System.IO;

namespace DataCollector.PS
{
    public class PSExecutor
    {
        public static readonly string ExecutablePath = Assembly.GetExecutingAssembly().Location;

        readonly Timer _timer;

        public PSExecutor()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => RunPSScript();
        }

        private void RunPSScript()
        {
            var currentPath = Path.GetDirectoryName(ExecutablePath);
            var logPath = Path.Combine(Path.Combine(currentPath, "Logs"), "DataCollector_");
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(logPath,
            rollingInterval: RollingInterval.Day,
            rollOnFileSizeLimit: true)
            .CreateLogger();


        }

        public void Start() {  _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
