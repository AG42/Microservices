using Microservices.Common.Interface;
using PurchaseOrderBySupplier.Common;
using PurchaseOrderBySupplier.Common.Logger;
using PurchaseOrderBySupplier.DataLayer.Entities.Datalake;
using PurchaseOrderBySupplier.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;


namespace PurchaseOrderBySupplier.DataLayer
{
    public class DataLayerContext : IDataLayerContext
    {
        private const string SupplierInvoiceNumber = "Pc12008";
        private const string SupplierCode = "Pc12007";
        private const string VatCode = "Pc12013";
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
        public IEnumerable<Pc12> GetPurchaseOrdersByCompanyCode(string companyCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseOrdersByCompanyCode :: Custome Input: companyCode: [{companyCode}]");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Get<Pc12>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name :: GetPurchaseOrdersByCompanyCode : Success");
            return lstOfSl01;
        }
        public IEnumerable<Pc12> GetPurchaseOrdersBySupplierInvoiceNumber(string companyCode,string supplierInvoiceNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseOrderBySupplierBySupplierInvoiceNumber :: Custome Input: companyCode: [{companyCode}] ,[{supplierInvoiceNumber}]");
            companyCode = ReplaceSingleCode(companyCode);
            supplierInvoiceNumber = ReplaceSingleCode(supplierInvoiceNumber);
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrdersBySupplierInvoiceNumber : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({SupplierInvoiceNumber})) = '{supplierInvoiceNumber.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Pc12>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer :: GetPurchaseOrdersBySupplierInvoiceNumber : Success");
            return lstOfSl01;
        }

        public IEnumerable<Pc12> GetPurchaseOrdersBySupplierCode(string companyCode,string supplierCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseOrdersBySupplierCode :: Custome Input: companyCode: [{companyCode}] ,[{supplierCode}]");
            companyCode = ReplaceSingleCode(companyCode);
            supplierCode = ReplaceSingleCode(supplierCode);
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrdersBySupplierCode : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({SupplierCode})) = '{supplierCode.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Pc12>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer :: GetPurchaseOrdersBySupplierCode : Success");
            return lstOfSl01;
        }

        public IEnumerable<Pc12> GetPurchaseOrdersByVATCode(string companyCode,string vatCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetPurchaseOrderBySupplierByVATCode :: Custome Input: companyCode: [{companyCode}] ,[{vatCode}]");
            companyCode = ReplaceSingleCode(companyCode);
            vatCode = ReplaceSingleCode(vatCode);
            ApplicationLogger.InfoLogger("DataLayer :: GetPurchaseOrdersByVATCode : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({VatCode})) = '{vatCode.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Pc12>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer :: GetPurchaseOrdersByVATCode : Success");
            return lstOfSl01;
        }
        private string ReplaceSingleCode(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
