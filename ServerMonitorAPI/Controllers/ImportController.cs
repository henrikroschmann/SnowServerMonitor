using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImportService;
using HelperLibrary.Models;
using HelperLibrary;

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

            var tasks = new[]
            {
                Task.Run(() => ProcessingData(serverLogs))
            };            

            return Ok("ok");            
        }       

        private void ProcessingData(List<ServerLog> logs)
        {
            Database.DBInsertData.InsertDataSet(Tools.ConvertToDataTable(logs));            
        }

    }
}
