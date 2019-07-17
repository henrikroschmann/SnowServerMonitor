using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using HelperLibrary;
using HelperLibrary.Models;

namespace ServerMonitorAPI.Database
{
    public static class DBGetData
    {
        public static List<ServerLog> MapMultipleObjects()
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                string sql = @"select * from dbo.ServerLog";
                List<ServerLog> records = cnn.Query<ServerLog>(sql).AsList();

                return records;
            }
        }

        // TODO: change type of date
        public static List<ServerLog> MapMultipleObjectsWithParam(string server, string date)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                var p = new
                {
                    server,
                    date
                };

                string sql = @"select * from dbo.ServerLog where ServerName = @server and date = @date ";
                List<ServerLog> records = cnn.Query<ServerLog>(sql, p).AsList();

                return records;
                
            }
        }
    }
}
