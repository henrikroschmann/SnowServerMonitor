using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ServerMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerLogController : Controller
    {
        // GET: api/ServerLog
        [HttpGet]
        public JsonResult Get()
        {
            return Json(Database.DBGetData.MapMultipleObjects());
        }

        // GET: api/ServerLog/search?server=foot&date=bar
        [HttpGet("Search")]        
        public JsonResult Get(string server, string date)
        {
            return Json(Database.DBGetData.MapMultipleObjectsWithParam(server, date));
        }      

        [HttpGet("Servers")]
        public JsonResult ServerList()
        {
            return Json(Database.DBGetData.GetServerList());
        }

        [HttpGet("getChart")]
        public JsonResult ServerChart()
        {
            List<int> _result = new List<int>();
            var result = Database.DBGetData.GetDujRuns();            
            foreach (var item in result)
            {
                _result.Add(item.Duration);
            }

            return Json(_result);
        }
    }
}
