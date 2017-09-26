using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchApp
{
    public class Logger : ILogger
    {
        public void WriteLog(LogEntry logEntry)
        {
            File.AppendAllText("D:\\esapp.log", logEntry.ToString());
        }
    }
}
