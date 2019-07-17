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
                throw new ArgumentNullException(nameof(serverLogs));
            }

            var result = Task.Run(() => ProcessingData(serverLogs));

            if(result.Status == TaskStatus.RanToCompletion)
            {
                return Ok("ok");
            } else
            {
                Log.Error("Something went wrong with the task");
                return StatusCode(500);
            }

            
        }       

        private void ProcessingData(List<ServerLog> logs)
        {
            Database.DBInsertData.InsertDataSet(Tools.ConvertToDataTable(logs));            
        }

    }
}
