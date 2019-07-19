using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataCollector.API;
using DataCollector.Security;
using HelperLibrary.Models;
using Newtonsoft.Json;
using Quartz;
using Serilog;

namespace DataCollector.DB
{
    public class DbGetDujExecutor : IJob
    {
        public List<DujRuns> GetFiveLastDujRuns()
        {
            using (IDbConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                string sql = @"SELECT  @@ServerName as ServerName, StartTime, CAST(SUBSTRING(CONVERT(VARCHAR(20), Duration, 108), 0, 3) AS INT) * 60 +
                            (
                                SELECT CAST(SUBSTRING(CONVERT(VARCHAR(10), Duration, 108), 4, 2) AS INT)
                            ) +
                            (
                                SELECT CASE
                                           WHEN(CAST(SUBSTRING(CONVERT(VARCHAR(10), Duration, 108), 7, 2) AS INT) < 55)
                                           THEN
                                (
                                    SELECT CAST('0' AS INT)
                                )
                                           ELSE
                                (
                                    SELECT CAST('1' AS INT)
                                )
                                       END
                            ) as Duration
                            FROM
                            (
                                SELECT TOP 4 id, 
                                             StartTime, 
                                             Duration
                                FROM tblJobLogMetrics
                                ORDER BY id DESC
                            ) result
                            ORDER BY 1 ASC";

                List<DujRuns> records = cnn.Query<DujRuns>(sql).AsList();

                return records;
            }

        }

        public Task Execute(IJobExecutionContext context)
        {
            var url = GetApiEndpoint.EndPoint()+ "/Duj";
            var lastRun = context.PreviousFireTimeUtc?.DateTime.ToString() ?? string.Empty;
            Log.Information("Executing DB script!   Previous run: {lastRun}", lastRun);


            var serverLogs = GetFiveLastDujRuns();

            var jsonData = JsonConvert.SerializeObject(serverLogs);
            Log.Information("Posting data to API");
            var response = PostData.PostRequest(url, jsonData);
            if (response.Errors.Any())
            {
                Log.Error("The following errors were encountered during POST call:");
                foreach (var error in response.Errors)
                {
                    Log.Error($"ERROR: Message '{error}'");
                }
            }
            else
            {
                Log.Information("Successfully posted data to API with the following messages:");
                foreach (var result in response.Results)
                {
                    Log.Information($"SUCCESS: Response code '{result}'");
                }
            }
            return Task.CompletedTask;
        }
    }
}
