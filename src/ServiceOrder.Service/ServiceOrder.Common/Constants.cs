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
        public const string InfoLoggerFileName = "InfoLog.txt";
        //public const string InMethodText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";
        //public const string InContollerText = "";

        #endregion

        public const string CompanyCode = "companyCode";
        public const string ServiceOrderNo = "serviceOrderNo";
        public const string InvoiceNumber = "invoiceNumber";
        public const string InvoiceCustomerCode = "invoiceCustomerCode";

        public const string Parts = "PARTS";
        public const string Expenses = "EXPENSES";
        public const string Labor = "LABOR";
        public const string Confirmed = "Confirmed";
    }
}
