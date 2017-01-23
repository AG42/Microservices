using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microservices.Common.Interface;
using PurchaseOrder.DataLayer.Interfaces;
using PurchaseOrder.Common;
using System;
using PurchaseOrder.DataLayer.Entities.Datalake;
using System.Collections.Generic;
using PurchaseOrder.Common.Logger;
using System.Linq;

namespace PurchaseOrder.DataLayer
{
    public class DataLayerContext :IDataLayerContext
    {
        private const string ParentCompanyCode = "";
        private const string ColPurchaseOrderNumber = "Pc01001";
        private const string ColOrderType = "Pc01002";
        private const string OrderDate = "Pc01015";
        private const string DeliveryDate = "Pc01016";
        private const string ColProjectNumber = "Pc01056";
        private readonly ConfigReader _configReader;

        [Import]
        public IDatabase Database { get; set; }

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
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public IEnumerable<Pc01> GetPurchaseOrderByCompanyCode(string companyCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseOrderByCompanyCode :: Custome Input: companyCode: [{companyCode}] ,[{companyCode}]");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode,Constants.DatalakePc01TableNameKey,Constants.DatalakePc01ColumnNameKey);
            var lstOfPc01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Get<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name :: GetPurchaseOrderByCompanyCode : Success");
            return lstOfPc01;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="purchaseOrderNumber"></param>
        /// <returns></returns>
        public Pc01 GetPurchaseOrderByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseOrderByPurchaseOrderNumber :: Custome Input: companyCode: [{companyCode}] ,[{purchaseOrderNumber}]");
            companyCode = ReplaceSingleCode(companyCode);
            purchaseOrderNumber = ReplaceSingleCode(purchaseOrderNumber);
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByPurchaseOrderNumber : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode, Constants.DatalakePc01TableNameKey, Constants.DatalakePc01ColumnNameKey);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({ColPurchaseOrderNumber})) = '{purchaseOrderNumber.ToLower().Trim()}'";
            var lstOfPc01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer :: GetPurchaseOrderByPurchaseOrderNumber : Success");
            return lstOfPc01?.FirstOrDefault();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public IEnumerable<Pc01> GetPurchaseOrderByOrderType(string companyCode, string orderType)
        {
            companyCode = ReplaceSingleCode(companyCode);
            orderType = ReplaceSingleCode(orderType);
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByOrderType : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode, Constants.DatalakePc01TableNameKey, Constants.DatalakePc01ColumnNameKey);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({ColOrderType})) = '{orderType.ToLower().Trim()}'";
            var lstOfPc01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? 
                Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByOrderType : Success");
            return lstOfPc01;
        }
        /// <summary>
        /// Get Purchase Order Details of 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public IEnumerable<Pc04> GetPurchaseOrderByCustomerName(string companyCode, string customerName)
        {
            companyCode = ReplaceSingleCode(companyCode);
            customerName = ReplaceSingleCode(customerName);
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByCustomerName : Reading datalake table name from config");
            var dicPc01TableDetails = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode, Constants.DatalakePc01TableNameKey, Constants.DatalakePc01ColumnNameKey);
            string purchaseOrderTableName = dicPc01TableDetails[Constants.TableNameKey];
            var dicPc04TableDetails = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode, Constants.DatalakePc04TableNameKey, Constants.DatalakePc04ColumnNameKey);
            string deliveryAddressTableName = dicPc04TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{purchaseOrderTableName}], [{deliveryAddressTableName}]");
            string joinCondition = $" pc01 JOIN {deliveryAddressTableName} pc04 ON pc01.{Constants.Pc01IdColumn} = pc04.{Constants.Pc04IdColumn}";
            string whereCondition = $"lower(pc04.{Constants.ColCustomerName}) like '%{customerName.ToLower().Trim()}%'";
            string joincolumn = $"{dicPc01TableDetails[Constants.ColumnNameKey]}, {dicPc04TableDetails[Constants.ColumnNameKey]}";
            var lstOfpc04 = dicPc01TableDetails[Constants.TableNameKey] != dicPc01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Pc04>(purchaseOrderTableName,joincolumn,joinCondition,whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByCustomerName : Success");
            return lstOfpc04;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="projectNumber"></param>
        /// <returns></returns>
        public IEnumerable<Pc01> GetPurchaseOrderByProjectNumber(string companyCode, string projectNumber)
        {
            companyCode = ReplaceSingleCode(companyCode);
            projectNumber = ReplaceSingleCode(projectNumber);
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByProjectNumber : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode, Constants.DatalakePc01TableNameKey, Constants.DatalakePc01ColumnNameKey);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({ColProjectNumber})) = '{projectNumber.ToLower().Trim()}'";
            var lstOfPc01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ?
                Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByProjectNumber : Success");
            return lstOfPc01;
        }


        /// <summary>
        /// Gets the Purchase order details by company code and order start and end date range at DAL
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="orderStartDate">order start date as string</param>
        /// <param name="orderEndDate">order end date as string</param>
        /// <returns>return collection of PC01 tale records</returns>
        public IEnumerable<Pc01> GetPurchaseOrderByOrderDateRange(string companyCode, string orderStartDate, string orderEndDate)
        {
            companyCode = ReplaceSingleCode(companyCode);
            orderStartDate = ReplaceSingleCode(orderStartDate);
            orderEndDate = ReplaceSingleCode(orderEndDate);

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByOrderDateRange : Reading datalake table name from config");
            var dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode, 
                                        Constants.DatalakePc01TableNameKey, Constants.DatalakePc01ColumnNameKey);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");

            string query = $"TO_DATE({OrderDate})>='{orderStartDate.ToLower().Trim()}' AND TO_DATE({OrderDate})<='{orderEndDate.ToLower().Trim()}'";
            var lstOfPc01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ?
                Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("Datalayer :: GetPurchaseOrderByOrderDateRange : Success");

            return lstOfPc01;
        }


        /// <summary>
        /// Gets the Purchase order details by company code and delivery start and end date range at DAL
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="deliveryStartDate">Deliery start date as string</param>
        /// <param name="deliveryEndDate">Delivery end date as string</param>
        /// <returns>return collection of PC01 tale records</returns>
        public IEnumerable<Pc01> GetPurchaseOrderByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate)
        {
            companyCode = ReplaceSingleCode(companyCode);
            deliveryStartDate = ReplaceSingleCode(deliveryStartDate);
            deliveryEndDate = ReplaceSingleCode(deliveryEndDate);

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrderByDeliveryDateRange : Reading datalake table name from config");
            var dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode, 
                                            Constants.DatalakePc01TableNameKey, Constants.DatalakePc01ColumnNameKey);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");

            string query = $"TO_DATE({DeliveryDate})>='{deliveryStartDate.ToLower().Trim()}' AND TO_DATE({DeliveryDate})<='{deliveryEndDate.ToLower().Trim()}'";
            var lstOfPc01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ?
                Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("Datalayer :: GetPurchaseOrderByDeliveryDateRange : Success");

            return lstOfPc01;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ReplaceSingleCode(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
