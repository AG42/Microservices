using System.Collections.Generic;
using ContractInformation.DataLayer.Entities.Datalake;
using ContractInformation.DataLayer.Interfaces;
using ContractInformation.Common;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using Microservices.Common.Interface;
using System.Linq;

namespace ContractInformation.DataLayer
{
    public class DataLayerContext : IDataLayerContext
    {
        private const string ServiceContractNumber = "cmh1001";
        private const string CustomerReference = "cmh1004";
        private const string ContractStartDate = "cmh1015";
        private const string ContractEndDate = "cmh1016";
        private const string CustomerPurchaseOrderNumber = "cmh1040";
        private const string ContractStatus = "cmh1091";
        private const string CustomerSearchKey = "cmh1093";
        private const string CustomerName = "cmh1093";
        private const string CustomerRequestNumber = "cmh1132";



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

        public IEnumerable<Cmh1Na00> GetContractsByCompanyCode(string companyCode)
        {
            companyCode = ReplaceSingleCode(companyCode);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey]!=dicTableName[Constants.ColumnNameKey]? Database.Get<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByCustomerName(string companyCode, string customerName)
        {
            companyCode = ReplaceSingleCode(companyCode);
            customerName = ReplaceSingleCode(customerName);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({CustomerName})) like '%{customerName.ToLower().Trim()}%'";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey] ? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query) : null;
            return lstOfCmh1Na00;
        }


        public IEnumerable<Cmh1Na00> GetContractsByCustomerPONumber(string companyCode, string poNumber)
        {
            companyCode = ReplaceSingleCode(companyCode);
            poNumber = ReplaceSingleCode(poNumber);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({CustomerPurchaseOrderNumber}))='{poNumber.ToLower().Trim()}'";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByRequestNumber(string companyCode, string requestNumber)
        {
            companyCode = ReplaceSingleCode(companyCode);
            requestNumber = ReplaceSingleCode(requestNumber);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({CustomerRequestNumber}))='{requestNumber.ToLower().Trim()}'";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByServiceContractNumber(string companyCode, string contractNumber)
        {
            companyCode = ReplaceSingleCode(companyCode);
            contractNumber = ReplaceSingleCode(contractNumber);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({ServiceContractNumber}))='{contractNumber.ToLower().Trim()}'";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByCustomerNameandStatus(string companyCode, string name, string status)
        {
            companyCode = ReplaceSingleCode(companyCode);
            name = ReplaceSingleCode(name);
            status = ReplaceSingleCode(status);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({CustomerName}))like '%{name.ToLower().Trim()}%' AND trim(lower({ContractStatus}))= '{status.ToLower().Trim()}' ";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByCustomerReference(string companyCode, string reference)
        {
            companyCode = ReplaceSingleCode(companyCode);
            reference = ReplaceSingleCode(reference);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({CustomerReference})) like '%{reference.ToLower().Trim()}%'";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByCustomerReferenceandStatus(string companyCode, string reference, string status)
        {
            reference = ReplaceSingleCode(reference);
            status = ReplaceSingleCode(status);
            companyCode = ReplaceSingleCode(companyCode);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({CustomerReference}))like '%{reference.ToLower().Trim()}%' AND trim(lower({ContractStatus}))= '{status.ToLower().Trim()}' ";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByCustomerSearchKey(string companyCode, string searchKey)
        {
            companyCode = ReplaceSingleCode(companyCode);
            searchKey = ReplaceSingleCode(searchKey);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({CustomerSearchKey}))like '%{searchKey.ToLower().Trim()}%'";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByCustomerSearchKeyandStatus(string companyCode, string searchKey, string status)
        {
            companyCode = ReplaceSingleCode(companyCode);
            searchKey = ReplaceSingleCode(searchKey);
            status = ReplaceSingleCode(status);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({CustomerSearchKey})) like '%{searchKey.ToLower().Trim()}%' AND trim(lower({ContractStatus}))= '{status.ToLower().Trim()}' ";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByDateRange(string companyCode, string startDate, string endDate)
        {
            startDate = ReplaceSingleCode(startDate);
            endDate = ReplaceSingleCode(endDate);
            companyCode = ReplaceSingleCode(companyCode);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"TO_DATE({ContractStartDate})>='{startDate.ToLower().Trim()}' AND TO_DATE({ContractEndDate})<= '{endDate.ToLower().Trim()}'";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }

        public IEnumerable<Cmh1Na00> GetContractsByStatus(string companyCode, string status)
        {
            companyCode = ReplaceSingleCode(companyCode);
            status = ReplaceSingleCode(status);
            Dictionary<string, string> dicTableName = _configReader.GetDatabaseTableName(companyCode, ParentCompanyCode);
            string query = $"trim(lower({ContractStatus}))='{status.ToLower().Trim()}'";
            var lstOfCmh1Na00 = dicTableName[Constants.TableNameKey] != dicTableName[Constants.ColumnNameKey]? Database.Where<Cmh1Na00>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], query):null;
            return lstOfCmh1Na00;
        }
        private string ReplaceSingleCode(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
