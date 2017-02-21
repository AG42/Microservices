namespace PurchaseLedger.Common
{
    public static class Constants
    {
        #region Messages

        public const string InternalServerError = "Internal server error occured";
        public const string NoDataFoundMessage = "Data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string SupplierCodeRequiredMessage = "Please enter Supplier code";
        #endregion

        #region Logger Text/strings

        public const string InContollerText = "In Controller";
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";
        public const string SvmxcStatus = "Available";
        #endregion

        #region Version

        public const string DefaultControllerName = "PurchaseLedger";
        public const string VersionConcator = "V";
        public const string MsSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";


        public const string TableNameKey = "TableName";
        public const string ColumnNameKey = "ColumnName";

        public const string DatalakeTableNameKey = "DatalakeTableName";
        public const string DatalakeColumnNameKey = "DatalakeColumnName";

        public const string DatasourceLibraryPathKey = "DatasourceLibraryPath";

        public const string DatalakePl01TableNameKey = "Datalakepl01TableName";
        public const string DatalakePl01ColumnNameKey = "Datalakepl01ColumnName";
        public const string DatalakePl03TableNameKey = "Datalakepl03TableName";
        public const string DatalakePl03ColumnNameKey = "Datalakepl03ColumnName";
       // public const string Pc04IdColumn = "PC04001";
       // public const string Pc01IdColumn = "PC01001";

       

        #endregion

        #region Data Layer
        public const string ParentCompanyCode = "";
        public const string ColSupplierCode = "Pl01001";
        public const string ColSupplierName = "Pl01002";
        public const string ColInvoiceNo = "Pl03002";
        public const string ColInvoiceDate = "Pl03004";
        public const string ColOrderNo = "Pl03033";
        public const string ColDueDate = "Pl03006";
        public const string ColInvoiceSupplierCode = "Pl03001";

        #endregion
    }
}
