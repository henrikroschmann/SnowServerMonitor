using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportService
{
    public class Import
    {
        public static void Process(HelperLibrary.Models.ServerLog[] data)
        {
            Database.InsertDataSet(HelperLibrary.Tools.ConvertToDataTable(data.ToList()));          
                
        }       
    }
}
