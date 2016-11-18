using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Service.Logger
{
    public class Logging
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        //public void DebugLog(string message, Category category, string stackTrace , string innerException)
        //{
        //    var logInfo = new LogEventInfo
        //    {
        //        Level = LogLevel.Debug,
        //        Message = message,
        //        TimeStamp = DateTime.UtcNow
        //    };

        //    logInfo.Properties.Add("TimeStamp", DateTime.UtcNow);
        //    logInfo.Properties.Add("message", message);
        //    logInfo.Properties.Add("Category", category);
        //    logInfo.Properties.Add("Level", "Debug");
        //    logInfo.Properties.Add("stackTrace", stackTrace);
        //    logInfo.Properties.Add("innerException", innerException);

        //    logger.Log(logInfo);
        //}
        //public void TraceLog(string message, Category category, string stackTrace, string innerException)
        //{
        //    var logInfo = new LogEventInfo
        //    {
        //        Level = LogLevel.Trace,
        //        Message = message,
        //        TimeStamp = DateTime.UtcNow
        //    };


        //    logInfo.Properties.Add("TimeStamp", DateTime.UtcNow);
        //    logInfo.Properties.Add("message", message);
        //    logInfo.Properties.Add("Category", category);
        //    logInfo.Properties.Add("Level", "Trace");
        //    logInfo.Properties.Add("stackTrace", stackTrace);
        //    logInfo.Properties.Add("innerException", innerException);
        //    logger.Log(logInfo);
        //}
        public void Errorlog(string message, Category category, string stackTrace, Exception innerException)
        {
            var logInfo = new LogEventInfo
            {
                Level = LogLevel.Error,
                Message = message,
                TimeStamp = DateTime.UtcNow
            };
            logInfo.Properties.Add("TimeStamp", DateTime.UtcNow);
            logInfo.Properties.Add("Category", category);
            logInfo.Properties.Add("message", message);
            logInfo.Properties.Add("Level", "Error");
            logInfo.Properties.Add("stackTrace", stackTrace);
            logInfo.Properties.Add("innerException", innerException);
            logger.Log(logInfo);

        }

        //  logger.Log(LogLevel.Info, "Sample informational message, k={0}, l={1}", k, l);
        public  void InfoLogger(string input)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory.ToString();
            var dirName = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            string username = System.Environment.UserName.ToString();

            path = path + "\\" + dirName + "\\" + username + "\\" + "InfoLog.txt";

            if (!File.Exists(path))
            {
                string directoryName = Path.GetDirectoryName(path);
                if ((!Directory.Exists(directoryName)))
                {
                    Directory.CreateDirectory(directoryName);
                }

                TextWriterTraceListener listener = new TextWriterTraceListener(path);

                listener.WriteLine(input);
            }
            else if (File.Exists(path))
            {
                TextWriterTraceListener listener = new TextWriterTraceListener(path);

                listener.WriteLine(input);

                listener.Close();
            }
        }
    }
}
