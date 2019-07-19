﻿using Microsoft.AspNetCore.Mvc;

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
            int[] array = new int[] { 405, 588, 321, 335, 335 };
            return Json(array);
        }
    }
}
