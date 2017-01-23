namespace CustomerSalesLedger.Common
{
    public static class Constants
    {
        #region Messages

        public const string NoDataFoundMessage = "Data not available";
        public const string NoCustomerDataFoundMessage = "Customer data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string CustomerCodeIsRequiredMessage = "Please enter customer code";
        public const string CustomerNameIsRequiredMessage = "Please enter customer name";
        public const string CustomerNameLengthErrorMessage = "Customer name cannot be more than 35 character";
        public const string InvalidCompanyCodeMessage = "Please enter valid company code";
        public const string UnhandledExceptionMessage = "Unhandled exception occured!!!";
        public const string CustomerEmailIsRequiredMessage = "Please enter customer email";
        public const string CustomerCategoryIsRequiredMessage = "Please enter customer category";
        public const string CustomerAlternateNameIsRequiredMessage = "Please enter customer alternate name";
        public const string CustomerPhoneNumberIsRequiredMessage = "Please enter phone number";
        public const string CustomerCountryCodeIsRequiredMessage = "Please enter customer country code";
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
        public const string SERVICE_NAME_KEY = "ServiceName";
        public const string ENVIRONMENT_KEY = "Environment";
        public const string CONFIGURATION_DB_CONNECTIONSTRING_KEY = "ConfigurationDbConnectionString";
        public const string READ_CONFIG_FROM_DATABASE_KEY = "ReadConfigFromDatabase";

        #endregion

        #region DatabaseContext
        public const string LIKE_OPERATOR = "like";
        public const string EQUAL_OPERATOR = "=";
        public const string CUSTOMERCODE_FIELD = "sl01001";
        public const string CUSTOMERNAME_FIELD = "sl01002";
        public const string CATEGORY_FIELD = "sl01010";
        public const string PHONENUMBER_FIELD = "sl01011";
        public const string ALPHASEARCHKEY_FIELD = "sl01054";
        public const string COMPLETECUSTOMERNAME_FIELD = "sl01109";
        public const string COUNTRYCODE_FIELD = "sl01104";
        public const string EMAILID_FIELD = "sl01052";
        #endregion

        #region Version

        public const string DefaultControllerName = "CustomerSalesLedger";
        public const string VersionConcator = "V";
        public const string MSSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";

        public const string Yes = "Yes";
        public const string No = "No";

        #endregion

        #region Validators

        public const string CompanyCode = "companyCode";
        public const string CustomerCode = "customerCode";
        public const string CustomerName = "customerName";
        public const string AlternateCustomerName = "alternateName";
        public const string EmailId = "emailId";
        public const string PhoneNumber = "phoneNumber";
        public const string Category = "category";
        public const string CountryCode = "countryCode";

        #endregion
    }
}
