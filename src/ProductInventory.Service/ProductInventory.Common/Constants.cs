namespace ProductInventory.Common
{
    public static class Constants
    {
        #region Messages

        public const string InternalServerError = "Internal server error occured";
        public const string NoDataFoundMessage = "Data not available";
        public const string NoCustomerDataFoundMessage = "Customer data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string CustomerCodeIsRequiredMessage = "Please enter customer code";
        public const string LocationIsRequiredMessage = "Please enter location code";
        public const string DescriptionIsRequiredMessage = "Please enter product description";
        public const string CustomerNameLengthErrorMessage = "Customer name cannot be more than 35 character";
        public const string InvalidCompanyCodeMessage = "Please enter valid company code";
        public const string UnhandledExceptionMessage = "Unhandled exception occured!!!";
        // Customer Name cannot be more than 32 character
        // Description: Desctription cannot be more than 200 character

        #endregion

        #region Logger Text/strings

        public const string InContollerText = "In Controller";
       

        public const string SvmxcStatus = "Available";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";


        #endregion

        #region Version

        public const string DefaultControllerName = "ProductInventory";
        public const string VersionConcator = "V";
        public const string MSSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";

        #endregion

        #region DataLayer

        public const string LIKE_OPERATOR = "like";
        public const string EQUAL_OPERATOR = "=";
        public const string AND_OPERATOR = "&";
        public const string STOCKITEM_PRODUCTCODE_FIELD = "sc01001";
        public const string STOCKITEM_DESCRIPTION1_FIELD = "sc01002";
        public const string STOCKITEM_DESCRIPTION2_FIELD = "sc01003";
        public const string ITEMWAREHOUSE_PRODUCTCODE_FIELD = "sc03001";
        public const string ITEMWAREHOUSE_LOCATIONID_FIELD = "sc03002";
        public const string DATALAKE_STOCKMASTER_TABLE_NAME_KEY = "DatalakeStockMasterTableName";
        public const string DATALAKE_STOCKMASTER_Column_NAME_KEY = "DatalakeStockMasterColumnName";
        public const string DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY = "DatalakeItemWarehouseTableName";
        public const string DATALAKE_ITEMWAREHOUSE_Column_NAME_KEY = "DatalakeItemWarehouseColumnName";

        public const string DataSourceLibraryPath = "DataSourceLibraryPath";
        public const string DatabaseTableNameKey = "DatabaseTableNameKey";
        public const string TableNameKey = "TableName";
        public const string ColumnNameKey = "ColumnName";

        #endregion
    }
}
