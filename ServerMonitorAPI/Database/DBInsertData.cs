using Dapper;
using HelperLibrary;
using System;
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

        internal static void InsertDujSet(DataTable dataTable)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                var records = dataTable;
                var p = new
                {
                    records = records.AsTableValuedParameter("DujUDT")
                };

                cnn.Execute("dbo.spDUjInsertLogs", p, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
