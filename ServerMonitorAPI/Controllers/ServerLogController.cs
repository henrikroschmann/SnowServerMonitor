using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerLogController : ControllerBase
    {
        // GET: api/ServerLog
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Database.DBGetData.MapMultipleObjects();
            return new string[] { "value1", "value2" };
        }

        // GET: api/ServerLog/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ServerLog
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ServerLog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
