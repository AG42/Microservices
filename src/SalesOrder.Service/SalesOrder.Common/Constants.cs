namespace SalesOrder.Common
{
    public static class Constants
    {
        #region Messages

        public const string InternalServerError = "Internal server error occured";
        public const string NoDataFoundMessage = "Data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string CustomerCodeRequiredMessage = "Please enter customer code";
        public const string CustomerNameIsRequiredMessage = "Please enter customer Name";

        // Customer Name cannot be more than 32 character
        // Description: Desctription cannot be more than 200 character

        #endregion

        #region Logger Text/strings

        public const string InContollerText = "In Controller";
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";
        public const string SvmxcStatus = "Available";
        public const string Delivered = "Delivered";
        public const string PartiallyDelivered = "Partially Delivered";
        public const string Undelivered = "Undelivered";
        #endregion

        #region Version

        public const string DefaultControllerName = "PurchaseOrder";
        public const string VersionConcator = "V";
        public const string MsSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";


        public const string TableNameKey = "TableName";
        public const string ColumnNameKey = "ColumnName";

        public const string DatalakeTableNameKey = "DatalakeTableName";
        public const string DatalakeColumnNameKey = "DatalakeColumnName";

        public const string DatasourceLibraryPathKey = "DatasourceLibraryPath";

        public const string DatalakeOr01TableNameKey = "Datalakeor01TableName";
        public const string DatalakeOr01ColumnNameKey = "Datalakeor01ColumnName";
        public const string DatalakeOr03TableNameKey = "Datalakeor03TableName";
        public const string DatalakeOr03ColumnNameKey = "Datalakeor03ColumnName";
  

        #endregion

        #region Data Layer
        public const string ParentCompanyCode = "";
        public const string ColOr01SaleOrderNumber = "OR01001";
        public const string ColOr03SaleOrderNumber = "OR03001";      
        public const string ColOrderType = "OR01002";
        public const string ColCutomerInvoiceCode = "OR01003";
        public const string ColFlagPickListNumber = "OR01008";
        public const string ColOrderDate = "OR01015";
        public const string ColDeliveryDate = "OR01016";




        #endregion
    }
}
