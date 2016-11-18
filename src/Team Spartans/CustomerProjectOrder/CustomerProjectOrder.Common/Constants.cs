using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerProjectOrder.Common
{
    public static class Constants
    {
        #region Messages

        public const string NoDataFoundMessage = "Data not available";
        public const string NoCustomerDataFoundMessage = "Customer data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string CusotmerCodeIsRequiredMessage = "Please enter customer code";
        public const string CustomerNameLengthErrorMessage = "Customer name cannot be more than 35 character";
        public const string InvalidCompanyCodeMessage = "Please enter valid company code";
        public const string UnhandledExceptionMessage = "Unhandled exception occured!!!";
        public const string ProjectNumberRequired = "Please enter Project Number";
        public const string ProjectNumberLengthErrorMessage = "Project Number cannot be more than 12 characters";
        public const string ProjectNameRequired = "Please enter Project Name";
        public const string ProjectNameLengthErrorMessage = "Project Name cannot be more than 25 characters";
        public const string ProjectDurationRequired = "Project Start Date and End Date is required";
        public const string CustomerPoNoRequired = "Please enter CustomerPoNo";
        public const string CustomerPoNoLengthErrorMessage = "Cutomer PO No cannot be more than 20 characters";
        public const string AccountRequired = "Please enter Account";
        public const string AccountLengthErrorMessage = "Account cannot be more than 18 characters";
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

        public const string DefaultControllerName = "CustomerProjectOrder";
        public const string VersionConcator = "V";
        public const string MSSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";

        #endregion
    }
}
