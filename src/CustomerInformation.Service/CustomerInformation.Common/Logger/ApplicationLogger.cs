namespace CustomerInformation.Common.Logger
{
    public static class ApplicationLogger
    {
        private static Logging logger = new Logging();

        public static void Debug(string message, Category category, string stackTrace ,string innerException) {
            logger.DebugLog(message, category, stackTrace, innerException);
        }
        public static void Trace(string message, Category category, string stackTrace, string innerException) {
            logger.TraceLog(message, category,  stackTrace,  innerException);
        }
        public static void Error(string message, Category category, string stackTrace, string innerException) {
            logger.Errorlog(message, category, stackTrace, innerException);
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
