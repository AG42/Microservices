using System;
using System.Linq;
using System.Collections.Generic;
using CustomerInformation.Common.Logger;
using CustomerInformation.DataLayer.Entities.Datalake;
using DenodoAdapter;
using CustomerInformation.DataLayer.Interfaces;

namespace CustomerInformation.DataLayer
{
    public class DatabaseContext : IDatabaseContext
    {
        private const string LIKE_OPERATOR = "like";
        private const string EQUAL_OPERATOR = "=";
        private const string CUSTOMERNAME_FIELD = "sl01002";
        private const string CUSTOMERCODE_FIELD = "sl01001";
        public IDenodoContext DenodoContext { get; set; }
        private readonly ConfigReader _configReader;
        private readonly IDatalakeEntities _datalakeEntities;
        public DatabaseContext(IDatalakeEntities _DatalakeEntities)
        {
            try
            {
                _configReader = new ConfigReader();
                _datalakeEntities = _DatalakeEntities;
                _datalakeEntities.ConnectionString = _configReader.DatalakeConnectionString;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                    exception.InnerException);
                throw;
            }
        }

        public IEnumerable<Sl01> GetCustomerByName(string companyCode, string customerName)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerByName : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var customers = _datalakeEntities.Where<Sl01>(tableName, $"lower({CUSTOMERNAME_FIELD}) {LIKE_OPERATOR} '%{customerName.ToLower()}%'");
                ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
                return customers;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public IEnumerable<Sl01> GetCustomers(string companyCode)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomers : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var customers = _datalakeEntities.Get<Sl01>(tableName);
                ApplicationLogger.InfoLogger($"Customers count: {customers.Count()}");
                return customers;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public Sl01 GetCustomerById(string companyCode, string customerCode)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetCustomerById : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var data = _datalakeEntities.Where<Sl01>(tableName, $"trim(lower({CUSTOMERCODE_FIELD})){EQUAL_OPERATOR}'{customerCode.ToLower().Trim()}'");
                return data.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        private static void LogException(Exception exception)
        {
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }
    }
}
