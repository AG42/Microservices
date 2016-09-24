using NLog;
using System;
using System.Diagnostics; 
using System.IO;
using CustomerInformation.Common;


namespace CustomerInformation.Common.Logger
{
    public class Logging
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();
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
            _logger.Log(logInfo);

        }

        //  logger.Log(LogLevel.Info, "Sample informational message, k={0}, l={1}", k, l);
        public void InfoLogger(string input)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory;
            var datetime = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            path = $"{path}\\[{datetime}";



            if ((!Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);

                Trace.Listeners.Add(new TextWriterTraceListener($"{path}\\{Constants.InfoLoggerFileName}"));
                Trace.AutoFlush = true;
                Trace.WriteLine("\n");
                Trace.WriteLine(input);

                Trace.Flush();

            }
            else
            {
                Trace.Listeners.Add(new TextWriterTraceListener($"{path}\\{Constants.InfoLoggerFileName}"));
                Trace.AutoFlush = true;
                Trace.WriteLine("\n");
                Trace.WriteLine(input);
                Trace.Flush();
            }
        }
    }
}
