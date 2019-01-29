using System;
using System.IO;
using System.Text;

namespace Logger
{
    public sealed class Log:ILog
    {
        private static int Counter = 0;
        private Log() {
            Counter++;
            
        }
        private static readonly Lazy<Log> instance = new Lazy<Log>();

        public static Log GetInstance {
            get
            {
                return instance.Value;
            }
        }

        public void LogException(string message)
        {
            string fileName = string.Format("{0}_{1}.log", "Exception", DateTime.Now.ToShortDateString());
            string LogFilePath = string.Format(@"{0}\{1}", "Exception", AppDomain.CurrentDomain.BaseDirectory,fileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("_--------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            using (StreamWriter writer = new StreamWriter(LogFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
    }
}
