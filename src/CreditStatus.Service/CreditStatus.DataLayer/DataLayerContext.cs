using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using CreditStatus.DataLayer.Entities.Datalake;
using CreditStatus.DataLayer.Interfaces;
using Microservices.Common.Interface;
using CreditStatus.Common;
using CreditStatus.Common.Logger;
using System.Linq;

namespace CreditStatus.DataLayer
{
    public class DataLayerContext : IDataLayerContext
    {
        private readonly ConfigReader _configReader;

        private const string CustomerName = "sl01002";
        private const string CustomerCode = "Sl01001";
        private const string ParentCompanyCode = "";

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
        public IEnumerable<Sl01> GetCreditStatusByCompanyCode(string companyCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetCreditStatusByCompanyCode :: Custome Input: companyCode: [{companyCode}] ,[{companyCode}]");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            var lstOfSl01 = dicTableName[Constants.TableNameKey]!=dicTableName[Constants.ColumnNameKey]? Database.Get<Sl01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]):null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name :: GetCreditStatusByCompanyCode : Success");
            return lstOfSl01;
        }

        public Sl01 GetCreditStatusByCustomerCode(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetCreditStatusByCustomerCode :: Custome Input: companyCode: [{companyCode}] ,[{customerCode}]");
            companyCode = ReplaceSingleCode(companyCode);
            customerCode = ReplaceSingleCode(customerCode);
            ApplicationLogger.InfoLogger("DataLayer :: GetCreditStatusByCustomerCode : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({CustomerCode})) = '{customerCode.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Sl01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer :: GetCreditStatusByCustomerCode : Success");
            return lstOfSl01.FirstOrDefault();
        }

        public IEnumerable<Sl01> GetCreditStatusByCustomerName(string companyCode, string customerName)
        {
            companyCode = ReplaceSingleCode(companyCode);
            customerName = ReplaceSingleCode(customerName);
            ApplicationLogger.InfoLogger("DataLayer :: GetCreditStatusByCustomerName : Reading datalake table name from config");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({CustomerName})) like '%{customerName.ToLower().Trim()}%'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Sl01>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetCreditStatusByCustomerName : Success");
            return lstOfSl01;
        }

        private string ReplaceSingleCode(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
