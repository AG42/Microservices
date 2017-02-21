namespace ServiceContractManagement.Common
{
    public static class Constants
    {
        #region Messages

        public const string NoDataFoundMessage = "Data not available";
        public const string UnhandledExceptionMessage = "Unhandled exception occured!!!";

        #endregion

        #region Logger Text/strings

        public const string InControllerText = "In Controller";
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";

        #endregion

        #region Version

        public const string DefaultControllerName = "ServiceContractManagement";
        public const string VersionConcator = "V";
        public const string MsSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";

        #endregion

        #region Config Reader
        public const string DATABASE_CONNECTIONSTRING_KEY = "DatabaseConnectionString";
        public const string DATABASE_TABLE_NAME_KEY = "DatabaseTableName";
        public const string DATABASE_COLUMN_NAME_KEY = "DatabaseColumnName";
        public const string DATASOURCE_LIBRARY_PATH_KEY = "DatasourceLibraryPath";
        //public const string ServiceNameKey = "ServiceName";
        //public const string EnvironmentKey = "Environment";
        public const string CONFIGURATION_DB_CONNECTIONSTRING_KEY = "ConfigurationDbConnectionString";


        //public const string DATABASE_CONNECTIONSTRING_KEY = "DatabaseConnectionString";
        public const string DATASOURCE_LIBRARYPATH_KEY = "DatasourceLibraryPath";
        public const string SERVICE_NAME_KEY = "ServiceName";
        public const string ENVIRONMENT_KEY = "Environment";
        public const string READ_CONFIG_FROM_DATABASE_KEY = "ReadConfigFromDatabase";
        public const string DATABASE_SERVICE_CONTRACT_MASTER_TABLE_NAME_KEY = "DatabaseServiceContractMasterTableName";
        public const string DATABASE_SERVICE_CONTRACT_LINE_TABLE_NAME_KEY = "DatabaseServiceContractLineTableName";
        public const string DATABASE_SERVICE_CONTRACT_MASTER_COLUMN_NAME_KEY = "DatabaseServiceContractMasterColumnName";
        public const string DATABASE_SERVICE_CONTRACT_LINE_COLUMN_NAME_KEY = "DatabaseServiceContractLineColumnName";


        #endregion

        #region DatabaseContext

        public const string EQUAL_OPERATOR = "=";
        public const string LIKE_OPERATOR = "like";
        public const string BETWEEN_OPERATOR = "between";
        public const string AND_OPERATOR = "and";
        public const string IN_OPERATOR = "in";
        public const string SERVICE_CONTRACTMASTER_CODE_FIELD = "SM11001";
        public const string SERVICE_CONTRACT_LINE_CONTRACTCODE_FIELD = "SM13001";
        //public const string SALES_LEDGER_INVOICES_CUSTOMER_CODE_FIELD = "SL03001";
        //public const string SALES_LEDGER_INVOICES_ORDER_NUMBER_FIELD = "SL03036";
        //public const string SALES_LEDGER_INVOICES_INVOICE_NUMBER_FIELD = "SL03002";
        public const string SERVICE_CONTRACT_DATE_FIELD = "SM11012";

        #endregion

        #region Mail

        public const string MailServerKey = "Server";
        public const string MailFromKey = "From";
        public const string MailToKey = "To";
        public const string MailCcKey = "CC";
        public const string MailBodyStart = "<html><body>Hi,<br/><br/>";
        public const string MailBodyEnd = "<br/><br/>Thanks,<br/>Microservices development team<br/><br/>You are receiving this mail because you are part of Microservices team.<br/>Please do not reply to this mail.</body></html>";

        #endregion

    }
}
