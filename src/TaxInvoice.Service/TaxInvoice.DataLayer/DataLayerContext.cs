using System;
using System.Collections.Generic;
using TaxInvoice.DataAccessLayer.Entities.Datalake;
using TaxInvoice.DataAccessLayer.Interface;
using TaxInvoice.Common;
using Microservices.Common.Interface;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using TaxInvoice.Common.Logger;

namespace TaxInvoice.DataAccessLayer
{
    public class DataLayerContext : IDataLayerContext
    {
        [Import]
        public IDatabase Database { get; set; }
        private readonly ConfigReader _configReader;


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
        /// This method gets Tax invoice details based on company code in Data layer
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <returns>Collection of SL17 objects</returns>
        public IEnumerable<SL17> GetTaxInvoiceByCompanyCode(string companyCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name: GetTaxInvoiceByCompanyCode :: Custome Input: companyCode: [{companyCode}] ,[{companyCode}]");
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, "");
            var lstOfOr03 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] 
                    ? Database.Get<SL17>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]) : null;
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow}] :: DataLayer Method Name :: GetTaxInvoiceByCompanyCode : Success");
            return lstOfOr03;
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and Invoice number in Data layer
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="invoiceNo">Invoice Number as string</param>
        /// <returns>Collection of SL17 objects</returns>
        public IEnumerable<SL17> GetTaxInvoiceByInvoiceNo(string companyCode, string invoiceNo)
        {
            companyCode = ReplaceSingleCode(companyCode);
            invoiceNo = ReplaceSingleCode(invoiceNo);
            ApplicationLogger.InfoLogger("DataLayer :: GetTaxInvoiceByInvoiceNo : Reading datalake table name from config");
            var dicTableName = _configReader.GetDatabaseTableName(companyCode, "");
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");
            string query = $"trim(lower({Constants.InvoiceNo})) like '{invoiceNo.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] 
                    ? Database.Where<SL17>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetTaxInvoiceByInvoiceNo : Success");
            return lstOfSl01;
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and Customer code in Data layer
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="customerCode">Customer Code as string</param>
        /// <returns>Collection of SL17 objects</returns>
        public IEnumerable<SL17> GetTaxInvoiceByCustomerCode(string companyCode, string customerCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            customerCode = ReplaceSingleCode(customerCode);

            ApplicationLogger.InfoLogger("DataLayer :: GetTaxInvoiceByCustomerCode : Reading datalake table name from config");
            var dicTableName = _configReader.GetDatabaseTableName(companyCode, "");
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");

            string query = $"trim(lower({Constants.CustomerCode})) = '{customerCode.ToLower().Trim()}'";
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]
                        ? Database.Where<SL17>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetTaxInvoiceByCustomerCode : Success");
            return lstOfSl01;
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and range of Tax amount in Data layer
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="minTaxInvoice">Minimum tax amount as decimal</param>
        /// <param name="maxTaxInvoice">Maximum tax amount as decimal</param>
        /// <returns>Collection of SL17 objects</returns>
        public IEnumerable<SL17> GetTaxInvoiceByTaxAmountRange(string companyCode, decimal minTaxAmount, decimal maxTaxAmount)
        {
            companyCode = ReplaceSingleCode(companyCode);

            ApplicationLogger.InfoLogger("DataLayer :: GetTaxInvoiceByTaxInvoiceRange : Reading datalake table name from config");
            var dicTableName = _configReader.GetDatabaseTableName(companyCode, "");
            ApplicationLogger.InfoLogger($"Datalake table: [{dicTableName[Constants.TableNameKey]}]");

            string query = $"{Constants.TaxAmount} >= {minTaxAmount} AND {Constants.TaxAmount} <= {maxTaxAmount}";
             
            var lstOfSl01 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]
                        ? Database.Where<SL17>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            ApplicationLogger.InfoLogger("DataLayer :: GetTaxInvoiceByTaxInvoiceRange : Success");
            return lstOfSl01;
        }


        private string ReplaceSingleCode(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
