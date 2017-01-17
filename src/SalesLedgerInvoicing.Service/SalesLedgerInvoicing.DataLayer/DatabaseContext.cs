using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Common.Interface;
using SalesLedgerInvoicing.Common;
using SalesLedgerInvoicing.Common.Logger;
using SalesLedgerInvoicing.DataLayer.Entities.Datalake;
using SalesLedgerInvoicing.DataLayer.Interfaces;

namespace SalesLedgerInvoicing.DataLayer
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
        public Task<SL01> GetCustomerDetailsByCustomerCodeAsync(string companyCode, string customerCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerDetailsByCodeAsync : Reading datalake table name from config");
                var customerFileDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_CUSTOMER_FILE_TABLE_NAME_KEY, Constants.DATABASE_CUSTOMER_FILE_COLUMN_NAME_KEY);
                var customerFileTableName = customerFileDbDetails[Constants.DATABASE_CUSTOMER_FILE_TABLE_NAME_KEY];
                var customerFileColumnName = customerFileDbDetails[Constants.DATABASE_CUSTOMER_FILE_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"Datalake table: [{customerFileTableName}]");
                var whereCondition = $"trim(lower({Constants.CUSTOMERFILE_CUSTOMER_CODE_FIELD})) {Constants.EQUAL_OPERATOR} '{customerCode.ToLower().Trim()}'";
                _stopwatch.Reset();
                _stopwatch.Start();

                var customerDetails = Database.Where<SL01>(customerFileTableName, customerFileColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"Customer details count: {customerDetails.Count()}");
                return customerDetails.FirstOrDefault();
            });
        }
        public Task<IEnumerable<SL01>> GetCustomerDetailsBySalesLedgerCustomerCodeAsync(string companyCode, List<SL03> salesLedgerInvoices)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerDetailsByCodeAsync : Reading datalake table name from config");
                var customerFileDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_CUSTOMER_FILE_TABLE_NAME_KEY, Constants.DATABASE_CUSTOMER_FILE_COLUMN_NAME_KEY);
                var customerFileTableName = customerFileDbDetails[Constants.DATABASE_CUSTOMER_FILE_TABLE_NAME_KEY];
                var customerFileColumnName = customerFileDbDetails[Constants.DATABASE_CUSTOMER_FILE_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"Datalake table: [{customerFileTableName}]");
                var whereCondition = $"trim(lower({Constants.CUSTOMERFILE_CUSTOMER_CODE_FIELD})) {Constants.IN_OPERATOR} ({GetFormattedCustomerCodes(salesLedgerInvoices)})";
                _stopwatch.Reset();
                _stopwatch.Start();

                var customerDetails = Database.Where<SL01>(customerFileTableName, customerFileColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"Customer details count: {customerDetails.Count()}");
                return customerDetails;
            });
        }
        private string GetFormattedCustomerCodes(List<SL03> salesLedgerInvoices)
        {
            string customerCodes = string.Empty;
            for (int counter = 0; counter < salesLedgerInvoices.Count; counter++)
            {
                customerCodes += $"'{salesLedgerInvoices[counter].Sl03001.ToLower().Trim()}'";
                if (counter != salesLedgerInvoices.Count - 1)
                    customerCodes += ", ";
            }
            return customerCodes;
        }
        public Task<IEnumerable<SL01>> GetCustomerDetailsByNameAsync(string companyCode, string customerName)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerDetailsByNameAsync : Reading datalake table name from config");
                var customerFileDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_CUSTOMER_FILE_TABLE_NAME_KEY, Constants.DATABASE_CUSTOMER_FILE_COLUMN_NAME_KEY);
                var customerFileTableName = customerFileDbDetails[Constants.DATABASE_CUSTOMER_FILE_TABLE_NAME_KEY];
                var customerFileColumnName = customerFileDbDetails[Constants.DATABASE_CUSTOMER_FILE_COLUMN_NAME_KEY];
                var whereCondition = $"trim(lower({Constants.CUSTOMERFILE_CUSTOMER_NAME_FIELD})) {Constants.LIKE_OPERATOR} '%{customerName.ToLower().Trim()}%'";
                ApplicationLogger.InfoLogger($"Datalake table: [{customerFileTableName}]");
                _stopwatch.Reset();
                _stopwatch.Start();

                var customerDetails = Database.Where<SL01>(customerFileTableName, customerFileColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"Customer details count: {customerDetails.Count()}");
                return customerDetails;
            });
        }
        public Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByCustomerCodeAsync(string companyCode, string customerCode)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetSalesLedgerInvoicesByCustomerCodeAsync : Reading datalake table name from config");
                var salesLedgerInvoicesDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY, Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY);
                var salesLedgerInvoicesTableName = salesLedgerInvoicesDbDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY];
                var salesLedgerInvoicesColumnName = salesLedgerInvoicesDbDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY];
                var whereCondition = $"trim(lower({Constants.SALES_LEDGER_INVOICES_CUSTOMER_CODE_FIELD})) {Constants.EQUAL_OPERATOR} '{customerCode.ToLower().Trim()}'";
                ApplicationLogger.InfoLogger($"Datalake table: [{salesLedgerInvoicesTableName}]");
                _stopwatch.Reset();
                _stopwatch.Start();

                var salesLedgerInvoices = Database.Where<SL03>(salesLedgerInvoicesTableName, salesLedgerInvoicesColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"Customer details count: {salesLedgerInvoices.Count()}");
                return salesLedgerInvoices;
            });
        }
        public Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByCustomerListAsync(string companyCode, List<SL01> customerList)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetSalesLedgerInvoicesByCustomerCodeAsync : Reading datalake table name from config");
                var salesLedgerInvoicesDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY, Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY);
                var salesLedgerInvoicesTableName = salesLedgerInvoicesDbDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY];
                var salesLedgerInvoicesColumnName = salesLedgerInvoicesDbDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY];
                var whereCondition = $"trim(lower({Constants.SALES_LEDGER_INVOICES_CUSTOMER_CODE_FIELD})) {Constants.IN_OPERATOR} ({GetFormattedCustomerCodes(customerList)})";
                ApplicationLogger.InfoLogger($"Datalake table: [{salesLedgerInvoicesTableName}]");
                _stopwatch.Reset();
                _stopwatch.Start();

                var salesLedgerInvoices = Database.Where<SL03>(salesLedgerInvoicesTableName, salesLedgerInvoicesColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"Customer details count: {salesLedgerInvoices.Count()}");
                return salesLedgerInvoices;
            });
        }
        private string GetFormattedCustomerCodes(List<SL01> customerList)
        {
            string customerCodes = string.Empty;
            for (int counter = 0; counter < customerList.Count; counter++)
            {
                customerCodes += $"'{customerList[counter].Sl01001.ToLower().Trim()}'";
                if (counter != customerList.Count - 1)
                    customerCodes += ", ";
            }
            return customerCodes;
        }
        public Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByOrderNumberAsync(string companyCode, string orderNumber)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger(
                    "DataLayer :: GetSalesLedgerInvoicesByOrderNumberAsync : Reading datalake table name from config");
                var databaseDetails = _configReader.GetDatabaseDetails(companyCode,
                    Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY,
                    Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY);
                string tableName = databaseDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY];
                string columns = databaseDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY];
                var whereCondition = $"trim(lower({Constants.SALES_LEDGER_INVOICES_ORDER_NUMBER_FIELD})) {Constants.EQUAL_OPERATOR} '{orderNumber.ToLower().Trim()}'";
                ApplicationLogger.InfoLogger($"Datalake table: {tableName}");
                _stopwatch.Reset();
                _stopwatch.Start();

                var salesLedgerInvoices = Database.Where<SL03>(tableName, columns, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"Sales order line history count: {salesLedgerInvoices.Count()}");
                return salesLedgerInvoices;
            });
        }
        public Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByInvoiceNumberAsync(string companyCode, string invoiceNumber)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetSalesLedgerInvoicesByInvoiceNumberAsync : Reading datalake table name from config");
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY, Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY);
                var serviceOrderMasterTableName = serviceOrderDbDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY];
                var serviceOrderMasterColumnName = serviceOrderDbDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY];
                string whereCondition = $"trim(lower({Constants.SALES_LEDGER_INVOICES_INVOICE_NUMBER_FIELD})) {Constants.EQUAL_OPERATOR} '{invoiceNumber.ToLower().Trim()}'";
                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderMasterTableName}]");
                _stopwatch.Reset();
                _stopwatch.Start();

                var salesLedgerInvoices = Database.Where<SL03>(serviceOrderMasterTableName, serviceOrderMasterColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {salesLedgerInvoices.Count()}");
                return salesLedgerInvoices;

            });
        }
        public Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByInvoiceDateRangeAsync(string companyCode, string invoiceStartDate, string invoiceEndDate)
        {
            return Task.Run(() =>
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetSalesLedgerInvoicesByInvoiceDateRangeAsync : Reading datalake table name from config");
                var serviceOrderDbDetails = _configReader.GetDatabaseDetails(companyCode.Trim(), Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY, Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY);
                var serviceOrderMasterTableName = serviceOrderDbDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_TABLE_NAME_KEY];
                var serviceOrderMasterColumnName = serviceOrderDbDetails[Constants.DATABASE_SALES_LEDGER_INVOICES_COLUMN_NAME_KEY];
                string whereCondition = $"trim(lower({Constants.SALES_LEDGER_INVOICES_INVOICE_DATE_FIELD})) {Constants.BETWEEN_OPERATOR} '{invoiceStartDate.Trim()}' {Constants.AND_OPERATOR} '{invoiceEndDate.Trim()}'";
                ApplicationLogger.InfoLogger($"Datalake table: [{serviceOrderMasterTableName}]");
                _stopwatch.Reset();
                _stopwatch.Start();

                var salesLedgerInvoices = Database.Where<SL03>(serviceOrderMasterTableName, serviceOrderMasterColumnName, whereCondition);

                _stopwatch.Stop();
                ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
                ApplicationLogger.InfoLogger($"ServiceOrder count: {salesLedgerInvoices.Count()}");
                return salesLedgerInvoices;

            });
        }
      }
}
