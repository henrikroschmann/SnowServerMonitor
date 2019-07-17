using System;

namespace HelperLibrary.Models
{
    public class ServerLog
    {
        public string ServerName { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; }
        public int LineNumber { get; set; }
        public string Line { get; set; }
    }
}
