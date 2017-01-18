using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSecuredCost.Common
{
    public static class Constants
    {
        #region Messages

        public const string InternalServerError = "Internal server error occured";
        public const string NoDataFoundMessage = "Data not available";
        public const string NoCustomerDataFoundMessage = "Customer data not available";
        public const string CompanyCodeRequiredMessage = "Please enter company code";
        public const string PurchaseOrderNoRequiredMessage = "Please enter purchase order number";
        public const string OrderTypeRequiredMessage = "Please enter order type";
        public const string UserIdRequiredMessage = "Please enter user id";
        public const string DateRequiredMessage = "Please enter date";
        public const string OrderCostRequiredMessage = "Please enter order cost";

        #endregion

        #region Logger Text/strings

        public const string InContollerText = "In Controller";
        public const string SvmxcStatus = "Available";      
        public const string InfoLoggerFileName = "InfoLog_";
        public const string TraceLoggerFileName = "Trace_";
        public const string InfoLoggerPath = "C:/Logs/InfoLog/";
        public const string TraceLoggerPath = "C:/Logs/Trace/";


        #endregion

        #region Version

        public const string DefaultControllerName = "OrderSecuredCost";
        public const string VersionConcator = "V";
        public const string MSSubRoutes = "MS_SubRoutes";
        public const string JsonMediaType = "application/json";
        public const string VersionParamName = "version";

        #endregion

        #region DataLayer

        public const string LIKE_OPERATOR = "like";
        public const string EQUAL_OPERATOR = "=";
        public const string AND_OPERATOR = "&";
        public const string SUBTRACT = "-";
        public const string LessThanEqualOperator = "<=";
        public const string GreaterThanEqualOperator = ">=";
        public const string TableNameKey = "TableName";
        public const string ColumnNameKey = "ColumnName";

        public const string DatalakeTableNameKey = "DatalakeTableName";
        public const string DatalakeColumnNameKey = "DatalakeColumnName";

        public const string DatasourceLibraryPathKey = "DatasourceLibraryPath";

        #endregion
    }
}
