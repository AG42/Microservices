namespace CustomerSiteLocation.Common
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
        public const string CountryRequiredMessage = "Please enter country";
        public const string ZipRequiredMessage = "Please enter zip";
        public const string SiteNoRequiredMessage = "Please enter site number";
        public const string EmailIdRequiredMessage = "Please enter email";
        public const string ServiceEngineerCodeRequiredMessage = "Please enter service engineer code";
        public const string FaxNoRequiredMessage = "Please enter fax no";
        public const string TelephoneNoRequiredMessage = "Please enter telephone no";

        // Customer Name cannot be more than 32 character
        // Description: Desctription cannot be more than 200 character

        #endregion

        #region Logger Text/strings

        public const string InContollerText = "In Controller";
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "D:/Logs/InfoLog/";
        public const string TraceLoggerPath = "D:/Logs/Trace/";


        public const string SvmxcStatus = "Available";
        //public const string InMethodText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";

        #endregion

        #region Version

        public const string DefaultControllerName = "CustomerSiteLocation";
        public const string VersionConcator = "V";
        public const string MSSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";


        public const string TableNameKey = "TableName";
        public const string ColumnNameKey = "ColumnName";

        public const string DATALAKE_TABLE_NAME_KEY = "DatalakeTableName";
        public const string DATALAKE_COLUMN_NAME_KEY = "DatalakeColumnName";

        public const string DATASOURCE_LIBRARY_PATH_KEY = "DatasourceLibraryPath";


        #endregion
    }
}
