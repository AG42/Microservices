using System;
using System.Diagnostics;

namespace CustomerInformation.Common.Logger
{
    public static class ApplicationLogger
    {
        private static readonly Logging _logger = new Logging();
        private static BooleanSwitch boolSwitch = new BooleanSwitch("BoolSwitch",
     "Switch in config file");

        //public static void Debug(string message, Category category, string stackTrace ,string innerException) {
        //    logger.DebugLog(message, category, stackTrace, innerException);
        //}
        //public static void Trace(string message, Category category, string stackTrace, string innerException) {
        //    logger.TraceLog(message, category,  stackTrace,  innerException);
        //}
        public static void Errorlog(string message, Category category, string stackTrace, Exception innerException) {
            _logger.Errorlog(message, category, stackTrace, innerException);
        }

        /// <summary>
        /// method used for information logging for method name ,input parameters etc
        /// input is string .
        /// </summary>
        /// <param name="input">The input.</param>
        public static void InfoLogger(string input)
        {
            if (boolSwitch.Enabled)
            {
                _logger.InfoLogger(input);
            }
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
