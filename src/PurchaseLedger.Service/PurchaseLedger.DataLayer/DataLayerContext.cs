using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microservices.Common.Interface;
using PurchaseLedger.Common;
using PurchaseLedger.DataLayer.Interfaces;
using System.Collections.Generic;
using PurchaseLedger.DataLayer.Entities.Datalake;
using PurchaseLedger.Common.Logger;
using System;

namespace PurchaseLedger.DataLayer
{
    public class DataLayerContext : IDataLayerContext
    {
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

       public IEnumerable<Pl03> GetPurchaseLedgerByCompanyCode(string companyCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseLedgerByCompanyCode :: Custome Input: companyCode: [{companyCode}]");
            companyCode = ReplaceSingleCode(companyCode);
          

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByCompanyCode : Reading datalake table name from config");
            var dicPl01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl01TableNameKey, Constants.DatalakePl01ColumnNameKey);
            string purchaseLedgerTableName = dicPl01TableDetails[Constants.TableNameKey];
            var dicPl03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl03TableNameKey, Constants.DatalakePl03ColumnNameKey);
            string invoiceTableName = dicPl03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{purchaseLedgerTableName}], [{invoiceTableName}]");

            string joinCondition = $" pl01 JOIN {invoiceTableName} pl03 ON trim(pl01.{Constants.ColSupplierCode}) = trim(pl03.{Constants.ColInvoiceSupplierCode})";
           
            string joincolumn = $"{dicPl01TableDetails[Constants.ColumnNameKey]}, {dicPl03TableDetails[Constants.ColumnNameKey]}";

            var lstOfpl03 = dicPl01TableDetails[Constants.TableNameKey] != dicPl01TableDetails[Constants.ColumnNameKey] ? Database.GetJoinData<Pl03>(purchaseLedgerTableName, joincolumn, joinCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByOrderNo : Success");
            return lstOfpl03;
        }
        public IEnumerable<Pl03> GetPurchaseLedgerBySupplierCode(string companyCode, string supplierCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseLedgerBySupplierCode :: Custome Input: companyCode: [{companyCode}] ,[{supplierCode}]");
            companyCode = ReplaceSingleCode(companyCode);
            supplierCode = ReplaceSingleCode(supplierCode);

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerBySupplierCode : Reading datalake table name from config");
            var dicPl01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl01TableNameKey, Constants.DatalakePl01ColumnNameKey);
            string purchaseLedgerTableName = dicPl01TableDetails[Constants.TableNameKey];
            var dicPl03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl03TableNameKey, Constants.DatalakePl03ColumnNameKey);
            string invoiceTableName = dicPl03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{purchaseLedgerTableName}], [{invoiceTableName}]");

            string joinCondition = $" pl01 JOIN {invoiceTableName} pl03 ON trim(pl01.{Constants.ColSupplierCode}) = trim(pl03.{Constants.ColInvoiceSupplierCode})";
            string whereCondition = $"trim(lower({Constants.ColSupplierCode})) = '{supplierCode.ToLower().Trim()}'";
            string joincolumn = $"{dicPl01TableDetails[Constants.ColumnNameKey]}, {dicPl03TableDetails[Constants.ColumnNameKey]}";

            var lstOfpl03 = dicPl01TableDetails[Constants.TableNameKey] != dicPl01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Pl03>(purchaseLedgerTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerBySupplierCode : Success");
            return lstOfpl03;
        }
        public IEnumerable<Pl03> GetPurchaseLedgerBySupplierName(string companyCode, string supplierName)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseLedgerBySupplierName :: Custome Input: companyCode: [{companyCode}] ,[{supplierName}]");
            companyCode = ReplaceSingleCode(companyCode);
            supplierName = ReplaceSingleCode(supplierName);

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerBySupplierCode : Reading datalake table name from config");
            var dicPl01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl01TableNameKey, Constants.DatalakePl01ColumnNameKey);
            string purchaseLedgerTableName = dicPl01TableDetails[Constants.TableNameKey];
            var dicPl03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl03TableNameKey, Constants.DatalakePl03ColumnNameKey);
            string invoiceTableName = dicPl03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{purchaseLedgerTableName}], [{invoiceTableName}]");

            string joinCondition = $" pl01 JOIN {invoiceTableName} pl03 ON trim(pl01.{Constants.ColSupplierCode}) = trim(pl03.{Constants.ColInvoiceSupplierCode})";
            string whereCondition = $"trim(lower({Constants.ColSupplierName})) like '%{supplierName.ToLower().Trim()}%'";
            string joincolumn = $"{dicPl01TableDetails[Constants.ColumnNameKey]}, {dicPl03TableDetails[Constants.ColumnNameKey]}";

            var lstOfpl03 = dicPl01TableDetails[Constants.TableNameKey] != dicPl01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Pl03>(purchaseLedgerTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerBySupplierName : Success");
            return lstOfpl03;
        }
        public IEnumerable<Pl03> GetPurchaseLedgerByInvoiceNo(string companyCode, string invoiceNo)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseLedgerByInvoiceNo :: Custome Input: companyCode: [{companyCode}] ,[{invoiceNo}]");
            companyCode = ReplaceSingleCode(companyCode);
            invoiceNo = ReplaceSingleCode(invoiceNo);

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerBySupplierCode : Reading datalake table name from config");
            var dicPl01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl01TableNameKey, Constants.DatalakePl01ColumnNameKey);
            string purchaseLedgerTableName = dicPl01TableDetails[Constants.TableNameKey];
            var dicPl03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl03TableNameKey, Constants.DatalakePl03ColumnNameKey);
            string invoiceTableName = dicPl03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{purchaseLedgerTableName}], [{invoiceTableName}]");

            string joinCondition = $" pl01 JOIN {invoiceTableName} pl03 ON trim(pl01.{Constants.ColSupplierCode}) = trim(pl03.{Constants.ColInvoiceSupplierCode})";
            string whereCondition = $"trim(lower({Constants.ColInvoiceNo})) = '{invoiceNo.ToLower().Trim()}'";
            string joincolumn = $"{dicPl01TableDetails[Constants.ColumnNameKey]}, {dicPl03TableDetails[Constants.ColumnNameKey]}";

            var lstOfpl03 = dicPl01TableDetails[Constants.TableNameKey] != dicPl01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Pl03>(purchaseLedgerTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByInvoiceNo : Success");
            return lstOfpl03;
        }

        /// <summary>
        /// Fetch data from Datalake by Order no and Company code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="orderNo"></param>
        /// <returns>returns list of pl03 and pl01  records</returns>
        public IEnumerable<Pl03> GetPurchaseLedgerByOrderNo(string companyCode, string orderNo)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseLedgerByOrderNo :: Custome Input: companyCode: [{companyCode}] ,[{orderNo}]");
            companyCode = ReplaceSingleCode(companyCode);
            orderNo = ReplaceSingleCode(orderNo);

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByOrderNo : Reading datalake table name from config");
            var dicPl01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl01TableNameKey, Constants.DatalakePl01ColumnNameKey);
            string purchaseLedgerTableName = dicPl01TableDetails[Constants.TableNameKey];
            var dicPl03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl03TableNameKey, Constants.DatalakePl03ColumnNameKey);
            string invoiceTableName = dicPl03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{purchaseLedgerTableName}], [{invoiceTableName}]");

            string joinCondition = $" pl01 JOIN {invoiceTableName} pl03 ON trim(pl01.{Constants.ColSupplierCode}) = trim(pl03.{Constants.ColInvoiceSupplierCode})";
            string whereCondition = $"trim(lower({Constants.ColOrderNo})) = '{orderNo.ToLower().Trim()}'";
            string joincolumn = $"{dicPl01TableDetails[Constants.ColumnNameKey]}, {dicPl03TableDetails[Constants.ColumnNameKey]}";

            var lstOfpl03 = dicPl01TableDetails[Constants.TableNameKey] != dicPl01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Pl03>(purchaseLedgerTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByOrderNo : Success");
            return lstOfpl03;
        }
        /// <summary>
        /// Fetch data from Datalake by Invoice date range and Company code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="invoiceStartDate"></param>
        /// <param name="invoiceEndDate"></param>
        /// <returns>returns list of pl03 and pl01  records</returns>
        public IEnumerable<Pl03> GetPurchaseLedgerByInvoiceDateRange(string companyCode, string invoiceStartDate, string invoiceEndDate)
        {
            companyCode = ReplaceSingleCode(companyCode);
            invoiceStartDate = ReplaceSingleCode(invoiceStartDate);
            invoiceEndDate = ReplaceSingleCode(invoiceEndDate);

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByInvoiceDate : Reading datalake table name from config");
            var dicPl01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl01TableNameKey, Constants.DatalakePl01ColumnNameKey);
            string purchaseLedgerTableName = dicPl01TableDetails[Constants.TableNameKey];
            var dicPl04TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl03TableNameKey, Constants.DatalakePl03ColumnNameKey);
            string invoiceTableName = dicPl04TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{purchaseLedgerTableName}], [{invoiceTableName}]");

            string joinCondition = $" pl01 JOIN {invoiceTableName} pl03 ON trim(pl01.{Constants.ColSupplierCode}) = trim(pl03.{Constants.ColInvoiceSupplierCode})";
            string whereCondition = $"TO_DATE({Constants.ColInvoiceDate}) >='{invoiceStartDate.ToLower().Trim()}' AND TO_DATE({Constants.ColInvoiceDate}) <='{invoiceEndDate.ToLower().Trim()}'";
            string joincolumn = $"{dicPl01TableDetails[Constants.ColumnNameKey]}, {dicPl04TableDetails[Constants.ColumnNameKey]}";

            var lstOfpl03 = dicPl01TableDetails[Constants.TableNameKey] != dicPl01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Pl03>(purchaseLedgerTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByInvoiceDate : Success");
            return lstOfpl03;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="dueStartDate"></param>
        /// <param name="dueEndDate"></param>
        /// <returns></returns>
        public IEnumerable<Pl03> GetPurchaseLedgerByDueDateRange(string companyCode, string dueStartDate, string dueEndDate)
        {
            companyCode = ReplaceSingleCode(companyCode);
            dueStartDate = ReplaceSingleCode(dueStartDate);
            dueEndDate = ReplaceSingleCode(dueEndDate);

            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByDueDate : Reading datalake table name from config");
            var dicPl01TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl01TableNameKey, Constants.DatalakePl01ColumnNameKey);
            string suppliersTableName = dicPl01TableDetails[Constants.TableNameKey];
            var dicPl03TableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.ParentCompanyCode,
                                        Constants.DatalakePl03TableNameKey, Constants.DatalakePl03ColumnNameKey);
            string purchaseLedgerInvoicesTableName = dicPl03TableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{suppliersTableName}], [{purchaseLedgerInvoicesTableName}]");

            string joinCondition = $" pl01 JOIN {purchaseLedgerInvoicesTableName} pl03 ON trim(pl01.{Constants.ColSupplierCode}) = trim(pl03.{Constants.ColInvoiceSupplierCode})";
            string whereCondition = $"TO_DATE({Constants.ColDueDate}) >='{dueStartDate.ToLower().Trim()}' AND TO_DATE({Constants.ColDueDate}) <='{dueEndDate.ToLower().Trim()}'";
            string joincolumn = $"{dicPl01TableDetails[Constants.ColumnNameKey]}, {dicPl03TableDetails[Constants.ColumnNameKey]}";

            var lstOfpl03 = dicPl01TableDetails[Constants.TableNameKey] != dicPl01TableDetails[Constants.ColumnNameKey] ? Database.WhereJoin<Pl03>(suppliersTableName, joincolumn, joinCondition, whereCondition) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseLedgerByDueDate : Success");
            return lstOfpl03;
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
