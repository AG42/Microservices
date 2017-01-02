namespace OrderSecuredRevenue.Common
{
    public static class Constants
    {
        #region Messages

        public const string NoDataFoundMessage = "Data not available";
        public const string OrderNumberRequiredMessage = "Please enter order no.";
        public const string InvoiceNumberRequiredMessage = "Please enter invoice no.";
        public const string UnhandledExceptionMessage = "Unhandled exception occured!!!";

        #endregion

        public const string OrderNumber = "orderNumber";
        public const string InvoiceNumber = "invoiceNumber";
        public const string NonStockItemLine = "Non Stock Item Line";
        public const string NormalOrderLine = "Normal Order Line";


        #region Logger Text/strings

        public const string InContollerText = "In Controller";

        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";

        #endregion

        #region Version

        public const string DefaultControllerName = "OrderSecuredRevenue";
        public const string VersionConcator = "V";
        public const string MSSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";

        #endregion

        #region Config reader

        public const string DATABASE_CONNECTIONSTRING_KEY = "DatabaseConnectionString";
        public const string DATASOURCE_LIBRARYPATH_KEY = "DatasourceLibraryPath";
        public const string SERVICE_NAME_KEY = "ServiceName";
        public const string ENVIRONMENT_KEY = "Environment";
        public const string READ_CONFIG_FROM_DATABASE_KEY = "ReadConfigFromDatabase";
        public const string DATABASE_TABLE_NAME_KEY = "DatabaseTableName";
        public const string DATABASE_COLUMN_NAME_KEY = "DatabaseColumnName";
        public const string CONFIGURATION_DB_CONNECTIONSTRING_KEY = "ConfigurationDbConnectionString";

        #endregion

        #region DatabaseContext

        public const string EQUAL_OPERATOR = "=";
        public const string ORDER_NUMBER_FIELD = "or03001";
        //public const string INVOICE_NUMBER_FIELD = "or21065";


        #endregion
    }
}
