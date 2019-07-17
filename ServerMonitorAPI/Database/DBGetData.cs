using System.Data;
using System.Data.SqlClient;
using Dapper;
using HelperLibrary;

namespace ServerMonitor.Database
{
    public static class DBGetData
    {
        //public static void MapMultipleObjects()
        //{
        //    using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
        //    {
        //        string sql = @"select * from dbo.ServerLog";

        //        var logs = cnn.Query<HelperLibrary.Models.ServerLog>(sql);

        //        // TODO: Fix return type, Collection of logs?
        //        System.Console.WriteLine(logs);
        //    }
        //}

        //// TODO: change type of date
        //public static void MapMultipleObjectsWithParam(string server,string date)
        //{
        //    using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
        //    {
        //        var p = new
        //        {
        //            server = server,
        //            date = date
        //        };

        //        string sql = @"select * from dbo.ServerLog where ServerName = @server and date = @date ";

        //        var records = cnn.Query<HelperLibrary.Models.ServerLog>(sql, p);

        //        // TODO: fix return type, COllection of records?
        //        System.Console.WriteLine(records);
        //    }
        //}
    }
}
