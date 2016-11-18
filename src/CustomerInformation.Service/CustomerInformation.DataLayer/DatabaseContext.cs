using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CustomerInformation.Common;
using CustomerInformation.Common.Logger;
using CustomerInformation.DataLayer.Entities.Datalake;
using DenodoAdapter;
using CustomerInformation.DataLayer.Interfaces;
using Microservices.Common.Interface;
using System.Diagnostics;

namespace CustomerInformation.DataLayer
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly ConfigReader _configReader;

        [Import]
        public IDatabase Database { get; set; }

        public DatabaseContext()
        {
            _configReader = new ConfigReader();
            GetContainer();
            Database.ConnectionString = _configReader.DatabaseConnectionString;
        }

        private void GetContainer()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(_configReader.DatasourceLibraryPath);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        public IEnumerable<Sl01> GetCustomerByName(string companyCode, string customerName)
        {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerByName : Reading datalake table name from config");
                var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY); ;
                string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
                string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var customers = Database.Where<Sl01>(tableName, columns, $"lower({Constants.CUSTOMERNAME_FIELD}) {Constants.LIKE_OPERATOR} '%{customerName.ToLower()}%'");
                ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
                return customers;
        }
        public IEnumerable<Sl01> GetCustomers(string companyCode)
        {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomers : Reading datalake table name from config");
                var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY); ;
                string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
                string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var customers = Database.Get<Sl01>(tableName, columns);
                ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
                return customers;
        }
        public Sl01 GetCustomerById(string companyCode, string customerCode)
        {
            //try
            //{
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerById : Reading datalake table name from config");
                var databaseDetails = _configReader.GetDatabaseDetails(companyCode, Constants.DATABASE_TABLE_NAME_KEY, Constants.DATABASE_COLUMN_NAME_KEY); ;
                string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
                string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var watch = Stopwatch.StartNew();
                var data = Database.Where<Sl01>(tableName, columns, $"trim(lower({Constants.CUSTOMERCODE_FIELD})){Constants.EQUAL_OPERATOR}'{customerCode.ToLower().Trim()}'");
                watch.Stop();
            ApplicationLogger.InfoLogger($"GetCustomerById Method Time::::=> ElapsedMilliseconds:: {watch.ElapsedMilliseconds}, Elapsed.Milliseconds:: {watch.Elapsed.Milliseconds}");
                return data.FirstOrDefault();
            //}
            //catch (Exception ex)
            //{
            //    ApplicationLogger.InfoLogger($"{ex.Message} StackTrace:: {ex.StackTrace}, innerexception:: {ex.InnerException}");
            //}
            //return null;
        }
    }
}
