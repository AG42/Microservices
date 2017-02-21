using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using SalesOrder.DataLayer.Interfaces;
using SalesOrder.Common;
using SalesOrder.DataLayer.Entities.Datalake;
using System.Collections.Generic;
using SalesOrder.Common.Logger;
using Microservices.Common.Interface;

namespace SalesOrder.DataLayer
{
    public class DataLayerContext : IDataLayerContext
    {
        private readonly ConfigReader _configReader;

        [Import]
        public IDatabase Database { get; set; } 
        /// <summary>
        /// Defualt Construcor DataLayerContext 
        /// For Initialise Configreader Object and and MEF Container
        /// </summary>
        public DataLayerContext()
        {
            _configReader = new ConfigReader();
            GetContainer();
            Database.ConnectionString = _configReader.DatalakeConnectionString;
        }

        /// <summary>
        /// This method initialise the Import interface object with matchs of export with same type.
        /// here it initialise to IDatabase object.
        /// </summary>
        private void GetContainer()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(_configReader.DatasourceLibraryPath);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        /// <summary>
        /// Get Sales Order by Company Code
        /// </summary>
        /// <param name="companyCode"> companycode as string</param>
        /// <returns></returns>
        public IEnumerable<Or01> GetSalesOrderByCompanyCode(string companyCode)
        {
            companyCode = ReplaceSingleCode(companyCode);

            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByCompanyCode : Reading datalake table name from config");
            var dicOr01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr01TableNameKey, Constants.DatalakeOr01ColumnNameKey);
            string salesOrderHeadTableName = dicOr01TableDetails[Constants.TableNameKey];
            var dicOr03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr03TableNameKey, Constants.DatalakeOr03ColumnNameKey);
            string salesOrderLineTableName = dicOr03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{salesOrderLineTableName}], [{salesOrderLineTableName}]");

            string joinCondition = $" or01 JOIN {salesOrderLineTableName} or03 ON or01.{Constants.ColOr01SaleOrderNumber} = or03.{Constants.ColOr03SaleOrderNumber}";
            string joincolumn = $"{dicOr01TableDetails[Constants.ColumnNameKey]}, {dicOr03TableDetails[Constants.ColumnNameKey]}";
            var lstOfOr01 = dicOr01TableDetails[Constants.TableNameKey] != dicOr01TableDetails[Constants.ColumnNameKey] ? Database.GetJoinData<Or01>(salesOrderHeadTableName, joincolumn, joinCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByCompanyCode : Success");
            return lstOfOr01;
        }
        /// <summary>
        /// Get Sales Order by Sales Order Number
        /// </summary>
        /// <param name="companyCode"> company Code as string</param>
        /// <param name="salesOrderNumber">sales order number as string</param>
        /// <returns></returns>
        public IEnumerable<Or01> GetSalesOrderBySalesOrderNumber(string companyCode, string salesOrderNumber)
        {
            companyCode = ReplaceSingleCode(companyCode);
            salesOrderNumber = ReplaceSingleCode(salesOrderNumber);

            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderBySalesOrderNumber : Reading datalake table name from config");
            var dicOr01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr01TableNameKey, Constants.DatalakeOr01ColumnNameKey);
            string salesOrderHeadTableName = dicOr01TableDetails[Constants.TableNameKey];
            var dicOr03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr03TableNameKey, Constants.DatalakeOr03ColumnNameKey);
            string salesOrderLineTableName = dicOr03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{salesOrderLineTableName}], [{salesOrderLineTableName}]");

            string joinCondition = $" or01 JOIN {salesOrderLineTableName} or03 ON or01.{Constants.ColOr01SaleOrderNumber} = or03.{Constants.ColOr03SaleOrderNumber}";
            string whereCondition = $"trim(lower(or01.{Constants.ColOr01SaleOrderNumber})) = '{salesOrderNumber.ToLower().Trim()}'";
            string joincolumn = $"{dicOr01TableDetails[Constants.ColumnNameKey]}, {dicOr03TableDetails[Constants.ColumnNameKey]}";
            var lstOfOr01 = dicOr01TableDetails[Constants.TableNameKey] != dicOr01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Or01>(salesOrderHeadTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderBySalesOrderNumber : Success");
            return lstOfOr01;
        }
        /// <summary>
        /// Get Salse Order by Order Type
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="orderType">ordertype as string</param>
        /// <returns></returns>
        public IEnumerable<Or01> GetSalesOrderByOrderType(string companyCode, string orderType)
        {
            companyCode = ReplaceSingleCode(companyCode);
            orderType = ReplaceSingleCode(orderType);

            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByOrderType : Reading datalake table name from config");
            var dicOr01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr01TableNameKey, Constants.DatalakeOr01ColumnNameKey);
            string salesOrderHeadTableName = dicOr01TableDetails[Constants.TableNameKey];
            var dicOr03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr03TableNameKey, Constants.DatalakeOr03ColumnNameKey);
            string salesOrderLineTableName = dicOr03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{salesOrderLineTableName}], [{salesOrderLineTableName}]");

            string joinCondition = $" or01 JOIN {salesOrderLineTableName} or03 ON or01.{Constants.ColOr01SaleOrderNumber} = or03.{Constants.ColOr03SaleOrderNumber}";
            string whereCondition = $"trim(lower(or01.{Constants.ColOrderType}))  = '{orderType.ToLower().Trim()}'";
            string joincolumn = $"{dicOr01TableDetails[Constants.ColumnNameKey]}, {dicOr03TableDetails[Constants.ColumnNameKey]}";
            var lstOfOr01 = dicOr01TableDetails[Constants.TableNameKey] != dicOr01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Or01>(salesOrderHeadTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByOrderType : Success");
            return lstOfOr01;
        }
        /// <summary>
        /// Get Sales Order by Customer Invoie Code
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="customerInvoiceCode">customer Invoice number as string</param>
        /// <returns></returns>
        public IEnumerable<Or01> GetSalesOrderByCustomerInvoiceCode(string companyCode, string customerInvoiceCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            customerInvoiceCode = ReplaceSingleCode(customerInvoiceCode);

            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByCustomerInvoiceCode : Reading datalake table name from config");
            var dicOr01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr01TableNameKey, Constants.DatalakeOr01ColumnNameKey);
            string salesOrderHeadTableName = dicOr01TableDetails[Constants.TableNameKey];
            var dicOr03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr03TableNameKey, Constants.DatalakeOr03ColumnNameKey);
            string salesOrderLineTableName = dicOr03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{salesOrderLineTableName}], [{salesOrderLineTableName}]");

            string joinCondition = $" or01 JOIN {salesOrderLineTableName} or03 ON or01.{Constants.ColOr01SaleOrderNumber} = or03.{Constants.ColOr03SaleOrderNumber}";
            string whereCondition = $"trim(lower(or01.{Constants.ColCutomerInvoiceCode})) = '{customerInvoiceCode.ToLower().Trim()}'";
            string joincolumn = $"{dicOr01TableDetails[Constants.ColumnNameKey]}, {dicOr03TableDetails[Constants.ColumnNameKey]}";
            var lstOfOr01 = dicOr01TableDetails[Constants.TableNameKey] != dicOr01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Or01>(salesOrderHeadTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByCustomerInvoiceCode : Success");
            return lstOfOr01;
        }

        /// <summary>
        /// Get Sales Order by Flag Pick List
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="flagPickList">flagpicklist as string</param>
        /// <returns></returns>
        public IEnumerable<Or01> GetSalesOrderByFlagPickList(string companyCode, string flagPickList)
        {
            companyCode = ReplaceSingleCode(companyCode);
            flagPickList = ReplaceSingleCode(flagPickList);

            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByFlagPickList : Reading datalake table name from config");
            var dicOr01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr01TableNameKey, Constants.DatalakeOr01ColumnNameKey);
            string salesOrderHeadTableName = dicOr01TableDetails[Constants.TableNameKey];
            var dicOr03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr03TableNameKey, Constants.DatalakeOr03ColumnNameKey);
            string salesOrderLineTableName = dicOr03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{salesOrderLineTableName}], [{salesOrderLineTableName}]");

            string joinCondition = $" or01 JOIN {salesOrderLineTableName} or03 ON or01.{Constants.ColOr01SaleOrderNumber} = or03.{Constants.ColOr03SaleOrderNumber}";
            string whereCondition = $"trim(lower(or01.{Constants.ColFlagPickListNumber})) = '{flagPickList.ToLower().Trim()}'";
            string joincolumn = $"{dicOr01TableDetails[Constants.ColumnNameKey]}, {dicOr03TableDetails[Constants.ColumnNameKey]}";
            var lstOfOr01 = dicOr01TableDetails[Constants.TableNameKey] != dicOr01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Or01>(salesOrderHeadTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByFlagPickList : Success");
            return lstOfOr01;
        }

        /// <summary>
        /// Get Sales order by order date range
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="minOrderDate">min order date as string</param>
        /// <param name="maxOrderDate">max order date as string</param>
        /// <returns></returns>
        public IEnumerable<Or01> GetSalesOrderByOrderDateRange(string companyCode, string minOrderDate, string maxOrderDate)
        {
            companyCode = ReplaceSingleCode(companyCode);
            minOrderDate = ReplaceSingleCode(minOrderDate);
            maxOrderDate = ReplaceSingleCode(maxOrderDate);

            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByOrderDateRange : Reading datalake table name from config");
            var dicOr01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr01TableNameKey, Constants.DatalakeOr01ColumnNameKey);
            string salesOrderHeadTableName = dicOr01TableDetails[Constants.TableNameKey];
            var dicOr03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr03TableNameKey, Constants.DatalakeOr03ColumnNameKey);
            string salesOrderLineTableName = dicOr03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{salesOrderLineTableName}], [{salesOrderLineTableName}]");

            string joinCondition = $" or01 JOIN {salesOrderLineTableName} or03 ON or01.{Constants.ColOr01SaleOrderNumber} = or03.{Constants.ColOr03SaleOrderNumber}";
            string whereCondition = $"TO_DATE({Constants.ColOrderDate})>='{minOrderDate.ToLower().Trim()}' AND TO_DATE({Constants.ColOrderDate})<='{maxOrderDate.ToLower().Trim()}'"; 
            string joincolumn = $"{dicOr01TableDetails[Constants.ColumnNameKey]}, {dicOr03TableDetails[Constants.ColumnNameKey]}";
            var lstOfOr01 = dicOr01TableDetails[Constants.TableNameKey] != dicOr01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Or01>(salesOrderHeadTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByOrderDateRange : Success");
            return lstOfOr01;
        }
        /// <summary>
        /// Get Sales Order by Delivery Date Range
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="minDeliveryDate">min Delivery Date as string </param>
        /// <param name="maxDeliveryDate">max Delivary Date as string</param>
        /// <returns></returns>
        public IEnumerable<Or01> GetSalesOrderByDeliveryDateRange(string companyCode, string minDeliveryDate, string maxDeliveryDate)
        {
            companyCode = ReplaceSingleCode(companyCode);
            minDeliveryDate = ReplaceSingleCode(minDeliveryDate);
            maxDeliveryDate = ReplaceSingleCode(maxDeliveryDate);

            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByDeliveryDateRange : Reading datalake table name from config");
            var dicOr01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr01TableNameKey, Constants.DatalakeOr01ColumnNameKey);
            string salesOrderHeadTableName = dicOr01TableDetails[Constants.TableNameKey];
            var dicOr03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakeOr03TableNameKey, Constants.DatalakeOr03ColumnNameKey);
            string salesOrderLineTableName = dicOr03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{salesOrderLineTableName}], [{salesOrderLineTableName}]");

            string joinCondition = $" or01 JOIN {salesOrderLineTableName} or03 ON or01.{Constants.ColOr01SaleOrderNumber} = or03.{Constants.ColOr03SaleOrderNumber}";
            string whereCondition = $"TO_DATE({Constants.ColDeliveryDate})>='{minDeliveryDate.ToLower().Trim()}' AND TO_DATE({Constants.ColDeliveryDate})<='{maxDeliveryDate.ToLower().Trim()}'";
            string joincolumn = $"{dicOr01TableDetails[Constants.ColumnNameKey]}, {dicOr03TableDetails[Constants.ColumnNameKey]}";
            var lstOfOr01 = dicOr01TableDetails[Constants.TableNameKey] != dicOr01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Or01>(salesOrderHeadTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetSalesOrderByDeliveryDateRange : Success");
            return lstOfOr01;
        }
        /// <summary>
        /// Replace Single Code
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ReplaceSingleCode(string value)
        {
            return value.Trim().Replace("'", "''");
        }
    }
}
