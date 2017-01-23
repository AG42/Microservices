using CustomerSalesLedger.Common;
using CustomerSalesLedger.Common.Logger;
using CustomerSalesLedger.DataLayer.Entities.Datalake;
using CustomerSalesLedger.DataLayer.Interfaces;
using Microservices.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;

namespace CustomerSalesLedger.DataLayer
{
    public class DatabaseContext: IDatabaseContext
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

        public Sl01 GetCustomerById(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerById : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY);
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");

            _stopwatch.Reset();
            _stopwatch.Start();
            var customers = Database.Where<Sl01>(tableName, columns, $"trim(lower({Constants.CUSTOMERCODE_FIELD})){Constants.EQUAL_OPERATOR}'{customerCode.ToLower().Trim()}'");
            _stopwatch.Stop();
            ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");

            return customers.FirstOrDefault();

        }

        public IEnumerable<Sl01> GetCustomerByName(string companyCode, string customerName)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerByName : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY);
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");

            _stopwatch.Reset();
            _stopwatch.Start();
            var customers = Database.Where<Sl01>(tableName, columns, $"lower({Constants.CUSTOMERNAME_FIELD}) {Constants.LIKE_OPERATOR} '%{customerName.ToLower().Trim()}%'");
            _stopwatch.Stop();
            ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
            ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
            return customers;
        }

        public IEnumerable<Sl01> GetCustomerByCategory(string companyCode, string customerCategory)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerByCategory : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY);
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");

            _stopwatch.Reset();
            _stopwatch.Start();
            var customers = Database.Where<Sl01>(tableName, columns, $"trim(lower({Constants.CATEGORY_FIELD})) {Constants.EQUAL_OPERATOR} '{customerCategory.ToLower().Trim()}'");
            _stopwatch.Stop();
            ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
            ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
            return customers;
        }

        public IEnumerable<Sl01> GetCustomerByPhoneNumber(string companyCode, string customerPhoneNumber)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerByPhoneNumber : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY);
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");

            _stopwatch.Reset();
            _stopwatch.Start();
            var customers = Database.Where<Sl01>(tableName, columns, $"lower({Constants.PHONENUMBER_FIELD}) {Constants.LIKE_OPERATOR} '%{customerPhoneNumber.ToLower()}%'");
            _stopwatch.Stop();
            ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
            ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
            return customers;
        }

        public IEnumerable<Sl01> GetCustomerByAlternateName(string companyCode, string customerAlternateName)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerByAlternateName : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY);
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");

            _stopwatch.Reset();
            _stopwatch.Start();
            var customers = Database.Where<Sl01>(tableName, columns, $"lower({Constants.ALPHASEARCHKEY_FIELD}) {Constants.LIKE_OPERATOR} '%{customerAlternateName.ToLower()}%' OR lower({Constants.COMPLETECUSTOMERNAME_FIELD}) {Constants.LIKE_OPERATOR} '%{customerAlternateName.ToLower()}%'");
            _stopwatch.Stop();
            ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
            ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
            return customers;
        }

        public IEnumerable<Sl01> GetCustomerByEmailId(string companyCode, string customerEmailId)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetCustomerByEmailId : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY);
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");

            _stopwatch.Reset();
            _stopwatch.Start();
            var customers = Database.Where<Sl01>(tableName, columns, $"lower({Constants.EMAILID_FIELD}) {Constants.LIKE_OPERATOR} '%{customerEmailId.ToLower()}%'");
            _stopwatch.Stop();
            ApplicationLogger.InfoLogger($"Query Time: {_stopwatch.ElapsedMilliseconds}");
            ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
            return customers;
        }
    }
}
