using HelperLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ImportService
{
    public static class Import
    {
        public static void Process(List<ServerLog> data)
        {
            Database.InsertDataSet(HelperLibrary.Tools.ConvertToDataTable(data));
        }       
    }
}
