using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImportService;
using Newtonsoft.Json;

namespace ServerMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : Controller
    {        
        // POST: api/Import
        [HttpPost]
        public IActionResult Index([FromBody] HelperLibrary.Models.ServerLog[] serverlog)
        {
            if (serverlog == null)
            {
                throw new ArgumentNullException(nameof(serverlog));
            }

            FileImport(serverlog);

            return Json(serverlog);
        }

        public void FileImport(HelperLibrary.Models.ServerLog[] logrows)
        {
            ImportService.Import.Process(logrows);
        }
        

    }
}
