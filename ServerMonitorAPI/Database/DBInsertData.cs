using Dapper;
using HelperLibrary;
using System.Data;
using System.Data.SqlClient;

namespace ServerMonitorAPI.Database
{
    public static class DBInsertData
    {
        public static void InsertDataSet(DataTable data)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                var records = data;
                var p = new
                {
                    records = records.AsTableValuedParameter("BasicUDT")
                };

                cnn.Execute("dbo.spInsertLogs", p, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
