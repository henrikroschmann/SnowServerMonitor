using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelperLibrary.Models;
using HelperLibrary;
using Serilog;

namespace ServerMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : Controller
    {
        // POST: api/Import
        [HttpPost]
        public IActionResult Index([FromBody] List<ServerLog> serverLogs)
        {
            if (serverLogs == null)
            {
                Log.Error("Content was empty!");
                return NoContent();
            }
            try
            {
                var task = new[] {
                    Task.Run(() => ProcessingData(serverLogs))
                };
            } catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong!");
            }

            return Ok("ok");
        }

        private void ProcessingData(List<ServerLog> logs)
        {
            Database.DBInsertData.InsertDataSet(Tools.ConvertToDataTable(logs));
        }

        private void ProcessingData(List<DujRuns> logs)
        {
            Database.DBInsertData.InsertDujSet(Tools.ConvertToDataTable(logs));
        }

        [HttpPost("Duj")]
        public IActionResult Duj([FromBody] List<DujRuns> dujruns)
        {
            if (dujruns == null)
            {
                Log.Error("Content was empty!");
                return NoContent();
            }
            try
            {
                var task = new[] {
                    Task.Run(() => ProcessingData(dujruns))
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong!");
            }

            return Ok("ok");
        }

    }
}
