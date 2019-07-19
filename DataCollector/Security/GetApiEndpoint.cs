using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Security
{
    public static class GetApiEndpoint
    {        
        public static string EndPoint()
        {
            return ConfigurationManager.AppSettings["endpoint"];
        }
        
    }
}
