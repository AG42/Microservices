namespace ServiceOrder.Common
{
    public static class Constants
    {
        #region Messages

        public const string NoDataFoundMessage = "Data not available";
        public const string NoServiceOrderDataFoundMessage = "Service order data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string ServiceOrderIdIsRequiredMessage = "Please enter service order id";
        public const string InvoiceNumberIsRequiredMessage = "Please enter invoice number";
        public const string InvoiceCustomerCodeIsRequiredMessage = "Please enter invoice customer code";
        //public const string CustomerNameLengthErrorMessage = "Customer name cannot be more than 35 character";
        public const string InvalidCompanyCodeMessage = "Please enter valid company code";
        public const string UnhandledExceptionMessage = "Unhandled exception occured!!!";
        // Customer Name cannot be more than 32 character
        // Description: Desctription cannot be more than 200 character

        #endregion

        #region Logger Text/strings

        public const string InContollerText = "In Controller";

        //public const string InMethodText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";

        #endregion

        public const string CompanyCode = "companyCode";
        public const string ServiceOrderNo = "serviceOrderNo";
        public const string InvoiceNumber = "invoiceNumber";
        public const string InvoiceCustomerCode = "invoiceCustomerCode";

        public const string Parts = "PARTS";
        public const string Expenses = "EXPENSES";
        public const string Labor = "LABOR";
        public const string Confirmed = "Confirmed";

        #region Version

        public const string DefaultControllerName = "ServiceOrder";
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

        public const string DATABASE_SERVICE_ORDER_MASTER_TABLE_NAME_KEY = "DatabaseServiceOrderMasterTableName";
        public const string DATABASE_SERVICE_ORDER_MASTER_COLUMN_NAME_KEY = "DatabaseServiceOrderMasterColumnName";

        public const string DATABASE_CUSTOMERFILE_TABLE_NAME_KEY = "DatabaseCustomerFileTableName";
        public const string DATABASE_CUSTOMERFILE_COLUMN_NAME_KEY = "DatabaseCustomerFileColumnName";
        public const string DATABASE_SERVICE_ORDER_ACTIVITYLINES_TABLE_NAME_KEY = "DatabaseServiceOrderActivityLinesTableName";
        public const string DATABASE_SERVICE_ORDER_ACTIVITYLINES_COLUMN_NAME_KEY = "DatabaseServiceOrderActivityLinesColumnName";
        public const string DATABASE_SERVICE_ORDER_COSTLINES_TABLE_NAME_KEY = "DatabaseServiceOrderCostLinesTableName";
        public const string DATABASE_SERVICE_ORDER_COSTLINES_COLUMN_NAME_KEY = "DatabaseServiceOrderCostLinesColumnName";
        public const string DATABASE_SERVICE_ORDER_MATERIALLINES_TABLE_NAME_KEY = "DatabaseServiceOrderMaterialLinesTableName";
        public const string DATABASE_SERVICE_ORDER_MATERIALLINES_COLUMN_NAME_KEY = "DatabaseServiceOrderMaterialLinesColumnName";

        public const string SERVICEORDER_MASTER_SERVICEORDERNO_FIELD = "SM01001";
        public const string SERVICEORDER_ACTIVITYLINES_SERVICEORDERNO_FIELD = "SM03001";
        public const string SERVICEORDER_COSTLINES_SERVICEORDERNO_FIELD = "SM05001";
        public const string SERVICEORDER_MATERIALLINES_SERVICEORDERNO_FIELD = "SM07001";
        public const string SERVICEORDER_MASTER_CUSTOMERCODE_FIELD = "SM01002";
        public const string CUSTOMERFILE_CUSTOMERCODE_FIELD = "SL01001";
        public const string SERVICEORDER_ACTIVITYLINES_CUSTOMERCODE_FIELD = "SM03022";
        public const string SERVICEORDER_COSTLINES_CUSTOMERCODE_FIELD = "SM05018";
        public const string SERVICEORDER_MATERIALLINES_CUSTOMERCODE_FIELD = "SM07041";
        public const string SERVICEORDER_MASTER_INVOICENUMBER_FIELD = "SM01038";
        public const string SERVICEORDER_ACTIVITYLINES_INVOICENUMBER_FIELD = "SM03024";
        public const string SERVICEORDER_COSTLINES_INVOICENUMBER_FIELD = "SM05020";
        public const string SERVICEORDER_MATERIALLINES_INVOICENUMBER_FIELD = "SM07043";

        #endregion
    }
}
