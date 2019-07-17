using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.ComponentModel;

namespace HelperLibrary
{
    public static class Tools
    {
        public static string GetConnectionString(string name = "DapperDB")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
