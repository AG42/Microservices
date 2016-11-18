namespace CustomerInformation.Common
{
    public static class Constants
    {
        #region Messages

        public const string NoDataFoundMessage = "Data not available";
        public const string NoCustomerDataFoundMessage = "Customer data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string CusotmerCodeIsRequiredMessage = "Please enter customer code";
        public const string CustomerNameLengthErrorMessage = "Customer name cannot be more than 35 character";
        public const string InvalidCompanyCodeMessage = "Please enter valid company code";
        public const string UnhandledExceptionMessage = "Unhandled exception occured!!!";
        // Customer Name cannot be more than 32 character
        // Description: Desctription cannot be more than 200 character

        #endregion

        #region Logger Text/strings

        public const string InContollerText = "In Controller";
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";

        #endregion

        #region Config Reader
        public const string DATABASE_CONNECTIONSTRING_KEY = "DatabaseConnectionString";
        public const string DATABASE_TABLE_NAME_KEY = "DatabaseTableName";
        public const string DATABASE_COLUMN_NAME_KEY = "DatabaseColumnName";
        public const string DATASOURCE_LIBRARY_PATH_KEY = "DatasourceLibraryPath";
        public const string ServiceNameKey = "ServiceName";
        public const string EnvironmentKey = "Environment";
        public const string CONFIGURATION_DB_CONNECTIONSTRING_KEY = "ConfigurationDbConnectionString";
        public const string READ_CONFIG_FROM_DATABASE = "ReadConfigFromDatabase";

        #endregion

        #region DatabaseContext
        public const string LIKE_OPERATOR = "like";
        public const string EQUAL_OPERATOR = "=";
        public const string CUSTOMERNAME_FIELD = "sl01002";
        public const string CUSTOMERCODE_FIELD = "sl01001";
        #endregion

        #region Version

        public const string DefaultControllerName = "Customer";
        public const string VersionConcator = "V";
        public const string MSSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";

        #endregion
    }
}
