using NLog;
using System;


namespace CustomerInformation.Common.Logger
{
    public class Logging
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public void DebugLog(string message, Category category, string stackTrace , string innerException)
        {
            var logInfo = new LogEventInfo
            {
                Level = LogLevel.Debug,
                Message = message,
                TimeStamp = DateTime.UtcNow
            };

            //logInfo.Properties.Add("TimeStamp", DateTime.UtcNow.ToString());
            //logInfo.Properties.Add("Category", category);
            //logInfo.Properties.Add("Level", "Debug");
            //logInfo.Properties.Add("stackTrace", stackTrace);

            logger.Log(logInfo);
        }
        public void TraceLog(string message, Category category, string stackTrace, string innerException)
        {
            var logInfo = new LogEventInfo
            {
                Level = LogLevel.Trace,
                Message = message,
                TimeStamp = DateTime.UtcNow
            };
            logger.Log(logInfo);
        }
        public void Errorlog(string message, Category category, string stackTrace, string innerException)
        {
            var logInfo = new LogEventInfo
            {
                Level = LogLevel.Error,
                Message = message,
                TimeStamp = DateTime.UtcNow
            };
            logger.Log(logInfo);

        }
    }
}
