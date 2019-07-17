using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLibrary.Models
{
    public class ServerLog
    {
        public string ServerName { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; }
        public string Path { get; set; }
        public int LineNumber { get; set; }
        public string Line { get; set; }
    }
}
