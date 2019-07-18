using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HelperLibrary.Models;
using Newtonsoft.Json;
using DataCollector.API;

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
        private string url;

        private List<ServerLog> RunPSScript()
        {
            try
            {
                List<ServerLog> serverLogs = new List<ServerLog>();
                using (PowerShell PSInstance = PowerShell.Create())
                {
                    Collection<PSObject> results = null;
                    Collection<ErrorRecord> errors = null;

                    var script = GetScript("LogDataCollect.ps1");
                    
                    //"Xxxx cannot be loaded because running scripts is disabled on this system."
                    
                    //Start the script by adjusting the ExecutionPolicy:
                    script = PowerShellExecutionPolicy + script;
                    PSInstance.AddScript(script);

                    Log.Debug("Running script: \r\n" + script);

                    results = PSInstance.Invoke();
                    errors = PSInstance.Streams.Error.ReadAll();

                    if(errors.Any())
                    {
                        foreach(var errorRecord in errors)
                        {
                            Log.Error("The Powershell script failed with the following error: " + errorRecord);
                        }
                    }

                    foreach(var outputItem in results)
                    {
                        var serverLog = new ServerLog()
                        {
                            Date = DateTime.Parse(GetPSValue(outputItem, "Date")),
                            ServerName = GetPSValue(outputItem, "ServerName"),
                            Service = GetPSValue(outputItem, "Service"),
                            LineNumber = int.Parse(GetPSValue(outputItem, "LineNumber")),
                            Line = GetPSValue(outputItem, "Line")
                        };
                        serverLogs.Add(serverLog);
                    }
                }
                return serverLogs;
            } catch (Exception e)
            {
                Log.Error(e, "Something went wrong when trying to run a PowerShell Script");
                return new List<ServerLog>();
            }

        }

        private string GetPSValue(PSObject outputItem, string value)
        {
            return outputItem.Properties[value].Value.ToString();
        }

        public Task Execute(IJobExecutionContext context)
        {
            url = "https://localhost:44321/api/Import";
            var lastRun = context.PreviousFireTimeUtc?.DateTime.ToString() ?? string.Empty;
            Log.Information("Executing PowerShell script!   Previous run: {lastRun}", lastRun);
            var serverLogs = RunPSScript();

            var jsonData = JsonConvert.SerializeObject(serverLogs);
            Log.Information("Posting data to API");
            var response = PostData.PostRequest(url, jsonData);
            if(response.Errors.Any())
            {
                Log.Error("The following errors were encountered during POST call:");
                foreach(var error in response.Errors)
                {
                    Log.Error($"ERROR: Message '{error}'");
                }
            } else
            {
                Log.Information("Successfully posted data to API with the following messages:");
                foreach(var result in response.Results)
                {
                    Log.Information($"SUCCESS: Response code '{result}'");
                }
            }            
            return Task.CompletedTask;
        }

        private static string GetScript(string psFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"DataCollector.PowerShellScript.{psFileName}";
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
