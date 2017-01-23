namespace OrderedSecuredMargin.Common
{
    public static class Constants
    {
        #region Messages

        public const string InternalServerError = "Internal server error occured";
        public const string NoDataFoundMessage = "Data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string OrderNoRequiredMessage = "Please enter Order Name";
        public const string MinCostRequiredMessage = "Please enter minCost ";
        public const string MaxCostRequiredMessage = "Please enter maxCost";
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
        #endregion

        #region Version

        public const string DefaultControllerName = "CreditStatus";
        public const string VersionConcator = "V";
        public const string MsSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";


        public const string TableNameKey = "TableName";
        public const string ColumnNameKey = "ColumnName";

        public const string DatalakeTableNameKey = "DatalakeTableName";
        public const string DatalakeColumnNameKey = "DatalakeColumnName";

        public const string DatasourceLibraryPathKey = "DatasourceLibraryPath";


        #endregion

    }
}
