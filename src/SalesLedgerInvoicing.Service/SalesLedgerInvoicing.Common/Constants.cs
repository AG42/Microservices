namespace SalesLedgerInvoicing.Common
{
    public static class Constants
    {
        #region Messages

        public const string NoDataFoundMessage = "Data not available";
        public const string CustomerCodeRequiredMessage = "Please enter customer code";
        public const string CustomerNameRequiredMessage = "Please enter customer name";
        public const string OrderNumberRequiredMessage = "Please enter order no.";
        public const string InvoiceNumberRequiredMessage = "Please enter invoice no.";
        public const string InvoiceFromDateRequiredMessage = "Please enter invoice fromDate";
        public const string InvoiceToDateRequiredMessage = "Please enter invoice toDate";
        public const string InvoiceFromDateInvalidFormatMessage = "Please enter invoice fromDate in mm-dd-yyyy";
        public const string InvoiceToDateInvalidFormatMessage = "Please enter invoice toDate in mm-dd-yyyy";        
        public const string InvoiceFromDateGreaterMessage = "Invoice toDate should be greater than fromDate";        
        public const string UnhandledExceptionMessage = "Unhandled exception occured!!!";

        #endregion

        public const string CustomerCode = "customerCode";
        public const string CustomerName = "customerName";        
        public const string OrderNumber = "orderNumber";
        public const string InvoiceNumber = "invoiceNumber";
        public const string InvoiceFromDate = "invoiceFromDate";
        public const string InvoiceToDate = "invoiceToDate";
        public const string NonStockItemLine = "Non Stock Item Line";
        public const string NormalOrderLine = "Normal Order Line";

        #region SalesOrderType
        public const string Quotation = "Quotation";
        public const string NormalOrder = "Normal Order";
        public const string InvoiceOrder = "Invoice Order";
        public const string DirectOrder = "Direct Order";
        public const string BackOrder = "Back Order";
        public const string RepeatOrder = "Repeat Order";
        public const string WorkOrder = "Work Order";
        public const string DirectCreditOrder = "Direct Credit Order";
        public const string CreditOrder = "Credit Order";
        public const string ReinvoiceOrder = "Re-invoice Order";
        #endregion

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
        public const string MsSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";

        #endregion

        #region Config reader

        public const string DATABASE_CONNECTIONSTRING_KEY = "DatabaseConnectionString";
        public const string DATASOURCE_LIBRARYPATH_KEY = "DatasourceLibraryPath";
        public const string SERVICE_NAME_KEY = "ServiceName";
        public const string ENVIRONMENT_KEY = "Environment";
        public const string READ_CONFIG_FROM_DATABASE_KEY = "ReadConfigFromDatabase";
        public const string DATABASE_CUSTOMER_FILE_TABLE_NAME_KEY = "DatabaseCustomerFileTableName";
        public const string DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY = "DatabaseSalesLedgerInvoicesTableName";
        public const string DATABASE_CUSTOMER_FILE_COLUMN_NAME_KEY = "DatabaseCustomerFileColumnName";
        public const string DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY = "DatabaseSalesLedgerInvoicesColumnName";
        public const string CONFIGURATION_DB_CONNECTIONSTRING_KEY = "ConfigurationDbConnectionString";

        #endregion

        #region DatabaseContext

        public const string EQUAL_OPERATOR = "=";
        public const string LIKE_OPERATOR = "like";
        public const string BETWEEN_OPERATOR = "between";
        public const string AND_OPERATOR = "and";
        public const string IN_OPERATOR = "in";
        public const string CUSTOMERFILE_CUSTOMER_CODE_FIELD = "Sl01001";
        public const string CUSTOMERFILE_CUSTOMER_NAME_FIELD = "Sl01002";
        public const string SALES_LEDGER_INVOICES_CUSTOMER_CODE_FIELD = "Sl03001";
        public const string SALES_LEDGER_INVOICES_ORDER_NUMBER_FIELD = "Sl03036";
        public const string SALES_LEDGER_INVOICES_INVOICE_NUMBER_FIELD = "Sl03002";
        public const string SALES_LEDGER_INVOICES_INVOICE_DATE_FIELD = "Sl03004";

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
