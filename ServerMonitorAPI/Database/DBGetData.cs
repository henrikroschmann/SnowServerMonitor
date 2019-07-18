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

        public static List<Servers> GetServerList()
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                string sql = @"select distinct servername from ServerLog";
                // TODO: Create a query that will return list of servers and 
                // if they have issues report it and so on 
                // Example: Server1 larms and top three warnings ??

                List<Servers> records = cnn.Query<Servers>(sql).AsList();

                return records;
            }
        }
    }
}
