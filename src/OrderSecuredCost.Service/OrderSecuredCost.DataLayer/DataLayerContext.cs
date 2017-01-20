using System;
using System.Collections.Generic;
using OrderSecuredCost.Common.Logger;
using OrderSecuredCost.DataLayer.Interfaces;
using System.Linq;
using OrderSecuredCost.Common;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microservices.Common.Interface;
using OrderSecuredCost.DataLayer.Entities.Datalake;

namespace OrderSecuredCost.DataLayer
{
    public class DataLayerContext: IDataLayerContext
    {
        private const string PurchaseOrderNumber = "Pc01001";
        private const string OrderType = "Pc01002";
        private const string OrderValue = "Pc01020";
        private const string OrderDate = "Pc01015";
        private const string DeliveryDate = "Pc01016";
        private const string UserID = "Pc01073";
        private const string OrderDiscount = "Pc01019";
        private const string InvoicingFee = "Pc01091";
        private const string InsuranceAmount = "Pc01090";
        private const string PackingAmount = "Pc01089";
        private const string FreightAmount = "Pc01088";
        private const string ParentCompanyCode = "";
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
        public IEnumerable<Pc01> GetOrderSecuredCostByCompanyCode(string companyCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetOrderSecuredCostByCompanyCode :: Custome Input: companyCode: [{companyCode}] ,[{companyCode}]");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Get<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name :: GetOrderSecuredCostByCompanyCode : Success");
            return lstOfSl01;
        }
        public Pc01 GetOrderSecuredCostByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetOrderSecuredCostByPurchaseOrderNumber :: Custome Input: companyCode: [{companyCode}] ,[{purchaseOrderNumber}]");
            companyCode = ReplaceSingleCode(companyCode);
            purchaseOrderNumber = ReplaceSingleCode(purchaseOrderNumber);
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByPurchaseOrderNumber : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({PurchaseOrderNumber})) = '{purchaseOrderNumber.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer :: GetOrderSecuredCostByPurchaseOrderNumber : Success");
            return lstOfSl01?.FirstOrDefault();
        }

        public IEnumerable<Pc01> GetOrderSecuredCostByOrderType(string companyCode, string orderType)
        {
            companyCode = ReplaceSingleCode(companyCode);
            orderType = ReplaceSingleCode(orderType);
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByOrderType : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({OrderType})) = '{orderType.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByOrderType : Success");
            return lstOfSl01;
        }
        public IEnumerable<Pc01> GetOrderSecuredCostByOrderDateRange(string companyCode, string orderStartDate, string orderEndDate)
        {
            companyCode = ReplaceSingleCode(companyCode);
            orderStartDate = ReplaceSingleCode(orderStartDate);
            orderEndDate = ReplaceSingleCode(orderEndDate);
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByOrderDateRange : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"TO_DATE({OrderDate})>='{orderStartDate.ToLower().Trim()}'AND TO_DATE({OrderDate})<= '{orderEndDate.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ?
                Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByOrderDateRange : Success");
            return lstOfSl01;
        }
        public IEnumerable<Pc01> GetOrderSecuredCostByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate)
        {
            companyCode = ReplaceSingleCode(companyCode);
            deliveryStartDate = ReplaceSingleCode(deliveryStartDate);
            deliveryEndDate = ReplaceSingleCode(deliveryEndDate);
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByDeliveryDateRange : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"TO_DATE({DeliveryDate})>='{deliveryStartDate.ToLower().Trim()}'AND TO_DATE({DeliveryDate})<= '{deliveryEndDate.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ?
                Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByDeliveryDateRange : Success");
            return lstOfSl01;
        }
        public IEnumerable<Pc01> GetOrderSecuredCostByUserID(string companyCode, string userId)
        {
            companyCode = ReplaceSingleCode(companyCode);
            userId = ReplaceSingleCode(userId);
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByUserID : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({UserID})) like '%{userId.ToLower().Trim()}%'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ?
                Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByUserID : Success");
            return lstOfSl01;
        }
        public IEnumerable<Pc01> GetOrderSecuredCostByOrderCostRange(string companyCode, string minOrderCost, string maxOrderCost)
        {
            companyCode = ReplaceSingleCode(companyCode);
            //double MinOrderCost = Convert.ToDouble(ReplaceSingleCode(minOrderCost));
            //double MaxOrderCost = Convert.ToDouble(ReplaceSingleCode(maxOrderCost));

            decimal MinOrderCost = Convert.ToDecimal(ReplaceSingleCode(minOrderCost));
            decimal MaxOrderCost = Convert.ToDecimal(ReplaceSingleCode(maxOrderCost));

            // Calculate OrderSecured Cost
            ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByOrderCostRange : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"({OrderValue} + {OrderDiscount} + {InvoicingFee} + {InsuranceAmount}+ {PackingAmount} + {FreightAmount})>= {MinOrderCost} AND ({OrderValue} + {OrderDiscount} + {InvoicingFee} + {InsuranceAmount}+ {PackingAmount} + {FreightAmount})<= {MaxOrderCost}";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ?
                Database.Where<Pc01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
             ApplicationLogger.InfoLogger("DataLayer :: GetOrderSecuredCostByOrderCostRange : Success");
            return lstOfSl01;
        }
        private string ReplaceSingleCode(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
