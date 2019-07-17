using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportService
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Mapper.ServerLogMapper> Logs = new List<Mapper.ServerLogMapper>();

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

            foreach (var item in Logs)
            {
                Console.WriteLine(item);
            }
        }
    }
}
