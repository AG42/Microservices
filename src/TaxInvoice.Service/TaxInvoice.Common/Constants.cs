namespace TaxInvoice.Common
{
    public static class Constants
    {
        #region Messages
        public const string InternalServerError = "Internal server error occured";
        public const string NoDataFoundMessage = "Data not available";
        #endregion

        #region Logger Text/strings

        public const string InContollerText = "In Controller";
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";

        #endregion

        #region Version

        public const string DefaultControllerName = "TaxInvoice";
        public const string TableNameKey = "TableName";
        public const string ColumnNameKey = "ColumnName";

        public const string DatalakeTableNameKey = "DatalakeTableName";
        public const string DatalakeColumnNameKey = "DatalakeColumnName";

        public const string DatasourceLibraryPathKey = "DatasourceLibraryPath";

        #endregion

        #region DataLayer
        public const string InvoiceNo = "SL17001";
        public const string CustomerCode = "SL17002";
        public const string TaxAmount = "SL17008";
        #endregion
    }
}