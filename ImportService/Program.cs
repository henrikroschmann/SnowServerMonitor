namespace ImportService
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal static class Program
    {
        public static readonly string ExecutablePath = Assembly.GetExecutingAssembly().Location;

        static void Main()
        {
            List<Mapper.ServerLogMapper> Logs = new List<Mapper.ServerLogMapper>();

            // TODO: Filename should not be hardcoded
            using (TextReader reader = File.OpenText("SUPMAINT03.csv"))
            {
                CsvReader csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ",";
                csv.Configuration.MissingFieldFound = null;
                while (csv.Read())
                {
                    Mapper.ServerLogMapper Record = csv.GetRecord<Mapper.ServerLogMapper>();
                    Logs.Add(Record);
                }
            }
            Database.InsertDataSet(HelperLibrary.Tools.ConvertToDataTable(Logs));                                  
        }
    }
}
