using DataCollector.Helpers;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataCollector.PS
{
    public class PSExecutor : IJob
    {
        private const string PowerShellExecutionPolicy = @"try{
Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process -Force
}
catch
{
    Write-Warning \""Unable to set ExecutionPolicy\"" 
}";

        private void RunPSScript()
        {
            try
            {
                using(PowerShell PSInstance = PowerShell.Create())
                {
                    Collection<PSObject> results = null;
                    Collection<ErrorRecord> errors = null;

                    var infoCommands = new[]
                    {
                        GetScript("LogDataCollect.ps")
                    };
                    var script = infoCommands.ToCSV(Environment.NewLine);
                    //Escape all code-related curly braces..
                    script = Regex.Replace(script, @"{\D", @"{$&");
                    script = Regex.Replace(script, @"\D}", @"$&}");
                    //"Xxxx cannot be loaded because running scripts is disabled on this system."
                    //Start the script by adjusting the ExecutionPolicy:
                    script = PowerShellExecutionPolicy + script;

                    Log.Debug("Running script: \r\n" + script);

                    results = PSInstance.Invoke<PSObject>();
                    errors = PSInstance.Streams.Error.ReadAll();

                    if(errors.Any())
                    {
                        foreach(var errorRecord in errors)
                        {
                            Log.Error("The Powershell script failed with the following error: " + errorRecord);
                        }
                    }
                }
            } catch (Exception e)
            {
                Log.Error(e, "Something went wrong when trying to run a PowerShell Script");
            }

        }

        public Task Execute(IJobExecutionContext context)
        {
            var lastRun = context.PreviousFireTimeUtc?.DateTime.ToString() ?? string.Empty;
            Log.Information("Executing PowerShell script!   Previous run: {lastRun}", lastRun);
            RunPSScript();
            return Task.CompletedTask;
        }

        private static string GetScript(string psFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"DataCollector.PS.{psFileName}";
            var result = "";

            using(var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if(stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            return result;
        }
    }
}
