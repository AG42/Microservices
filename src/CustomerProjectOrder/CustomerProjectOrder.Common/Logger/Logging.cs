using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;



namespace CustomerProjectOrder.Common.Logger
{
    public class Logging
    {
       
        //Error logging with Enterprise library
        public void Errorlog(string message, Category category, string stackTrace, Exception innerException)
        {
            SetTraceLogPath();
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Reset();

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.SetLogWriter(new LogWriterFactory().Create());
            LogEntry logEntry = new LogEntry();
            

            string strMessage = string.Empty;
            strMessage += message + "\r\n" + "StackTrace :";
            strMessage += stackTrace + "\r\n" + "Inner Exception:";
            strMessage += Convert.ToString(innerException)+ "\r\n";

            logEntry.Message = strMessage;
            logEntry.Categories.Add(category.ToString() + Environment.NewLine);
           // logEntry.Message=message + Environment.NewLine;
          //  logEntry.AddErrorMessage(stackTrace + Environment.NewLine);
          //  logEntry.Categories.Add(Convert.ToString(innerException));
         
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntry);
        }

        public void SetTraceLogPath()
        {
            // Log file path.
            string logFilePath = Constants.TraceLoggerPath + "/" + Constants.DefaultControllerName + "/"+ Constants.TraceLoggerFileName + DateTime.Now.ToString("yyyy -MM-dd") + ".log";// Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            ConfigurationFileMap objConfigPath = new ConfigurationFileMap();

            // App config file path.
            string appPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            objConfigPath.MachineConfigFilename = appPath;

            System.Configuration.Configuration entLibConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            LoggingSettings loggingSettings = (LoggingSettings)entLibConfig.GetSection(LoggingSettings.SectionName);

            FlatFileTraceListenerData objFlatFileTraceListenerData =
            loggingSettings.TraceListeners.Get("Flat File Trace Listener") as
            FlatFileTraceListenerData;

            objFlatFileTraceListenerData.FileName = logFilePath;

            entLibConfig.Save();
        }
        
        public void InfoLogger(string input)
        {
            string path = Constants.InfoLoggerPath + "/" + Constants.DefaultControllerName; 
          //  "C://Logs/InfoLog/CustomerProjectOrder/";// AppDomain.CurrentDomain.BaseDirectory;
         //   var datetime = $"{DateTime.Now:yyyy-MM-dd}";

           // path = $"{path}\\{datetime}";

            if ((!Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists($"{path}\\{Constants.InfoLoggerFileName + DateTime.Now.ToString("yyyy -MM-dd") + ".txt"}"))
            {

                File.AppendAllText($"{path}\\{Constants.InfoLoggerFileName + DateTime.Now.ToString("yyyy -MM-dd") + ".txt"}", Environment.NewLine);

                File.AppendAllText($"{path}\\{Constants.InfoLoggerFileName + DateTime.Now.ToString("yyyy -MM-dd") + ".txt"}", input);

            }

            else
            {
                File.AppendAllText($"{path}\\{Constants.InfoLoggerFileName + DateTime.Now.ToString("yyyy -MM-dd") + ".txt"}", Environment.NewLine);

                File.AppendAllText($"{path}\\{Constants.InfoLoggerFileName + DateTime.Now.ToString("yyyy -MM-dd") + ".txt"}", input);
            }

        }
    }
}
