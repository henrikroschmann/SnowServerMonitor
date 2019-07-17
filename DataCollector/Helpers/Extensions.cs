using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Helpers
{
    public static class EnumerableExtensions
    {
        public static string ToCSV<T>(this IEnumerable<T> list, string separator = ",")
        {
            if (list == null) { return null; }

            return string.Join(separator, list);
        }
    }
}
