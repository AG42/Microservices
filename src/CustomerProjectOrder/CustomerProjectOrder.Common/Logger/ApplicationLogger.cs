using System;
using System.Diagnostics;

namespace CustomerProjectOrder.Common.Logger
{
    public static class ApplicationLogger
    {
        private static readonly Logging Logger = new Logging();
        private static readonly BooleanSwitch BoolSwitch = new BooleanSwitch("BoolSwitch",
      "Switch in config file");
       
        public static void Errorlog(string message, Category category, string stackTrace, Exception innerException)
        {
            Logger.Errorlog(message, category, stackTrace, innerException);
        }

        /// <summary>
        /// method used for information logging for method name ,input parameters etc
        /// input is string .
        /// </summary>
        /// <param name="input">The input.</param>
        public static void InfoLogger(string input)
        {
            if (BoolSwitch.Enabled)
            {
                Logger.InfoLogger(input);
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
