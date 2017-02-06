using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using ServiceContractManagement.Common;
using ServiceContractManagement.Common.Logger;
using ServiceContractManagement.DataLayer.Interfaces;
using Microservices.Common.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using ServiceContractManagement.DataLayer.Entities.Datalake;
using System.Linq;

namespace ServiceContractManagement.DataLayer
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly ConfigReader _configReader;
        readonly Stopwatch _stopwatch;

        [Import]
        public IDatabase Database { get; set; }

        public DatabaseContext()
        {
            _configReader = new ConfigReader();
            GetContainer();
            Database.ConnectionString = _configReader.DatabaseConnectionString;
            _stopwatch = new Stopwatch();
        }

        private void GetContainer()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(_configReader.DatasourceLibraryPath);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        #region Datalake
        public Task<SM11> GetServiceContractDetailsByContractCodeAsync(string companyCode, string contractCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceContractDetailsByContractCodeAsync : Reading datalake table name from config");
                var contractFileDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_CONTRACT_MASTER_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_CONTRACT_MASTER_COLUMN_NAME_KEY);
                var serviceContractMasterTableName = contractFileDbDetails[Constants.DATABASE_SERVICE_CONTRACT_MASTER_TABLE_NAME_KEY];
                var serviceContractColumnName = contractFileDbDetails[Constants.DATABASE_SERVICE_CONTRACT_MASTER_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"datalake table: [{serviceContractMasterTableName}]");
                var whereCondition = $"ltrim(rtrim(lower({Constants.SERVICE_CONTRACTMASTER_CODE_FIELD}))) {Constants.EQUAL_OPERATOR} '{contractCode.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();

                var serviceContractDetails = Database.Where<SM11>(serviceContractMasterTableName, serviceContractColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                //ApplicationLogger.InfoLogger($"Customer details count: {serviceContractDetails.Count()}");
                return serviceContractDetails.FirstOrDefault();
            });
        }
        public Task<IEnumerable<SM11>> GetServiceContractDetailsByContractDateRangeAsync(string companyCode, string contactStartDate, string contractEndDate)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceContractDetailsByContractDateRangeAsync : Reading datalake table name from config");
                var contractFileDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_CONTRACT_MASTER_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_CONTRACT_MASTER_COLUMN_NAME_KEY);
                var serviceContractMasterTableName = contractFileDbDetails[Constants.DATABASE_SERVICE_CONTRACT_MASTER_TABLE_NAME_KEY];
                var serviceContractColumnName = contractFileDbDetails[Constants.DATABASE_SERVICE_CONTRACT_MASTER_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"datalake table: [{serviceContractMasterTableName}]");


                string whereCondition = $"ltrim(rtrim(lower({Constants.SERVICE_CONTRACT_DATE_FIELD}))) {Constants.BETWEEN_OPERATOR} '{contactStartDate.Trim()}' {Constants.AND_OPERATOR} '{contractEndDate.Trim()}'";
                
                _stopwatch.Reset();
                _stopwatch.Start();

                var serviceContractLineDetails = Database.Where<SM11>(serviceContractMasterTableName, serviceContractColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {serviceContractLineDetails.Count()}");
                return serviceContractLineDetails;

            });
        }
        public Task<IEnumerable<SM13>> GetServiceContractLineDetailsByContractCodeAsync(string companyCode, string contractCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServiceContractLineDetailsByContractCodeAsync : Reading datalake table name from config");
                var contractlineDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_CONTRACT_LINE_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_CONTRACT_LINE_COLUMN_NAME_KEY);
                var serviceContractLineTableName = contractlineDbDetails[Constants.DATABASE_SERVICE_CONTRACT_LINE_TABLE_NAME_KEY];
                var serviceContractColumnName = contractlineDbDetails[Constants.DATABASE_SERVICE_CONTRACT_LINE_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"datalake table: [{serviceContractLineTableName}]");
                var whereCondition = $"ltrim(rtrim(lower({Constants.SERVICE_CONTRACT_LINE_CONTRACTCODE_FIELD}))) {Constants.EQUAL_OPERATOR} '{contractCode.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();

                var serviceContractDetails = Database.Where<SM13>(serviceContractLineTableName, serviceContractColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"Customer details count: {serviceContractDetails.Count()}");
                return serviceContractDetails;
            });
        }
        public Task<IEnumerable<SM13>> GetServieContractLineDetailsByContractCodeListAsync(string companyCode, List<SM11> contractList)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetServieContractLineDetailsByCustomerListAsync : Reading datalake table name from config");
                var contractLineDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SERVICE_CONTRACT_LINE_TABLE_NAME_KEY, Constants.DATABASE_SERVICE_CONTRACT_LINE_COLUMN_NAME_KEY);
                var serviceContractLineTableName = contractLineDbDetails[Constants.DATABASE_SERVICE_CONTRACT_LINE_TABLE_NAME_KEY];
                var serviceContractLineColumnName = contractLineDbDetails[Constants.DATABASE_SERVICE_CONTRACT_LINE_COLUMN_NAME_KEY];
                var whereCondition = $"trim(lower({Constants.SERVICE_CONTRACT_LINE_CONTRACTCODE_FIELD})) {Constants.IN_OPERATOR} ({GetFormattedCustomerCodes(contractList)})";
                ApplicationLogger.InfoLogger($"Datalake table: [{serviceContractLineTableName}]");
                _stopwatch.Reset();
                _stopwatch.Start();

                var salesLedgerInvoices = Database.Where<SM13>(serviceContractLineTableName, serviceContractLineColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"Customer details count: {salesLedgerInvoices.Count()}");
                return salesLedgerInvoices;
            });
        }

        private string GetFormattedCustomerCodes(List<SM11> contractList)
        {
            string customerCodes = string.Empty;
            for (int counter = 0; counter < contractList.Count; counter++)
            {
                customerCodes += $"'{contractList[counter].SM11001.ToLower().Trim()}'";
                if (counter != contractList.Count - 1)
                    customerCodes += ", ";
            }
            return customerCodes;
        }

        //private string GetFormattedCustomerCodes(List<SL03> salesLedgerInvoices)
        //{
        //    string customerCodes = string.Empty;
        //    for (int counter = 0; counter < salesLedgerInvoices.Count; counter++)
        //    {
        //        customerCodes += $"'{salesLedgerInvoices[counter].Sl03001.ToLower().Trim()}'";
        //        if (counter != salesLedgerInvoices.Count - 1)
        //            customerCodes += ", ";
        //    }
        //    return customerCodes;
        //}

        //private string GetFormattedCustomerCodes(List<SL01> customerList)
        //{
        //    string customerCodes = string.Empty;
        //    for (int counter = 0; counter < customerList.Count; counter++)
        //    {
        //        customerCodes += $"'{customerList[counter].Sl01001.ToLower().Trim()}'";
        //        if (counter != customerList.Count - 1)
        //            customerCodes += ", ";
        //    }
        //    return customerCodes;
        //}

        #endregion

    }
}
