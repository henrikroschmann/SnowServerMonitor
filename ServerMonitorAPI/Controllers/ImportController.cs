using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImportService;
using Newtonsoft.Json;
using HelperLibrary.Models;

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

            Import.Process(serverLogs);

            return Json(serverLogs);
        }
    }
}
