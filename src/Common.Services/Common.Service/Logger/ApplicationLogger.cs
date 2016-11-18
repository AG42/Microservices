using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Service.Logger
{
    public static class ApplicationLogger
    {
        private static Logging logger = new Logging();

        //public static void Debug(string message, Category category, string stackTrace ,string innerException) {
        //    logger.DebugLog(message, category, stackTrace, innerException);
        //}
        //public static void Trace(string message, Category category, string stackTrace, string innerException) {
        //    logger.TraceLog(message, category,  stackTrace,  innerException);
        //}
        public static void Errorlog(string message, Category category, string stackTrace, Exception innerException) {
            logger.Errorlog(message, category, stackTrace, innerException);
        }

        public static void InfoLogger(string input)
        {
            logger.InfoLogger(input);
        }


    }

    public enum Category
    {
        Unknown = 0,
        Business = 1,
        Service = 2,
        Database = 3
    }
}
