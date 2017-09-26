using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchApp
{
    public class LogEntry
    {
        public string RequestType { get; set; }
        public string Response { get; set; }
        public string RequestTime { get; set; }
        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            var logEntry = new StringBuilder();
            logEntry.Append($"{RequestTime}\t{RequestType}\t{IpAddress}\t{Status}\t{SessionId}\t{Response}\n\n");
            logEntry.Append("-------------------------------------------------------------------\n\n");
            return logEntry.ToString();
        }
    }
}
