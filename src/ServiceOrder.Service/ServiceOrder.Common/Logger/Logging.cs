using NLog;
using ServiceOrder.Common.Enum;
using System;
using System.IO;

namespace ServiceOrder.Common.Logger
{
    public class Logging
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();
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
            var datetime = $"{DateTime.Now:yyyy-MM-dd}";

            path = $"{path}\\{datetime}";

            if ((!Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists($"{path}\\{Constants.InfoLoggerFileName}"))
            {

                File.AppendAllText($"{path}\\{Constants.InfoLoggerFileName}", Environment.NewLine);

                File.AppendAllText($"{path}\\{Constants.InfoLoggerFileName}", input);

            }

            else
            {
                File.AppendAllText($"{path}\\{Constants.InfoLoggerFileName}", Environment.NewLine);

                File.AppendAllText($"{path}\\{Constants.InfoLoggerFileName}", input);
                
            }

        }
    }
}
