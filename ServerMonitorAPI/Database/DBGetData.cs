using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using HelperLibrary;
using ServerMonitorAPI.Models;

namespace ServerMonitorAPI.Database
{
    public static class DBGetData
    {
        public static List<Serverlog> MapMultipleObjects()
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                string sql = @"select * from dbo.ServerLog";
                List<Serverlog> records = cnn.Query<Serverlog>(sql).AsList();

                return records;
            }
        }
        
        public static List<Serverlog> MapMultipleObjectsWithParam(string server, string date)
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {
                var p = new
                {
                    server,
                    date
                };

                string sql = @"select * from dbo.ServerLog where ServerName = @server and date = @date ";
                List<Serverlog> records = cnn.Query<Serverlog>(sql, p).AsList();

                return records;
                
            }
        }

        public static List<Servers> GetServerList()
        {
            using (IDbConnection cnn = new SqlConnection(Tools.GetConnectionString()))
            {                
                List<Servers> records = cnn.Query<Servers>("dbo.spServerOverview").AsList();

                return records;
            }
        }
    }
}
