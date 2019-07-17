using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using HelperLibrary;

namespace ImportService
{
    public static class Database
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
