using System;
using System.Collections.Generic;
using System.Web.Http;
using CustomerProjectOrder.Common.Logger;
using CustomerProjectOrder.DataLayer.Entities.Datalake;
using CustomerProjectOrder.DataLayer.Interfaces;
using System.Linq;
using Microservices.Common.Interface;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CustomerProjectOrder.Common;

namespace CustomerProjectOrder.DataLayer
{

    public class DataLayerContext : IDataLayerContext
    {

        private const string EqualOperator = "=";
        private const string LessThanEqualOperator = "<=";
        private const string GreaterThanEqualOperator = ">=";
        private const string AndOperator = "AND";
        private const string LikeOperator = "like";
        private const string ProjectnumberField = "pr01001";
        private const string ProjectnameField = "pr01009";
        private const string ProjectstartField = "pr01067";
        private const string ProjectendField = "pr01069";
        private const string CustomerPoNumber = "pr01106";
        private const string CustomerAccountNumber = "pr01003";
        private const string ToDate = "TO_DATE";
        private readonly ConfigReader _configReader;
        private readonly string parentCompanyCode="";

        //....added to direct connection
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

        public IEnumerable<Pr01> GetProjectByAccount(string companyCode, string account)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetProjectByAccount : Reading datalake table name from config");

            var databaseDetails = _configReader.GetDatabaseDetails(companyCode,parentCompanyCode); ;
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
            var lstOfPr01 = Database.Where<Pr01>(tableName,columns, $"trim(lower({CustomerAccountNumber})){EqualOperator}'{account.ToLower().Trim()}'");
            ApplicationLogger.InfoLogger($"Orders count: {lstOfPr01.Count()}");
            return lstOfPr01;
        }

        public Pr01 GetProjectByCustomerPONo(string companyCode, string customerPONo)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetProjectByCustomerPONo : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, parentCompanyCode); ;
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
            var pr01 = Database.Where<Pr01>(tableName, columns, $"trim(lower({CustomerPoNumber})){EqualOperator}'{customerPONo.ToLower().Trim()}'");
            return pr01.FirstOrDefault();
        }

        public IEnumerable<Pr01> GetProjectByDuration(string companyCode, string startDate, string endDate)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetProjectByDuration : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, parentCompanyCode); ;
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
            var lstOfPr01 = Database.Where<Pr01>(tableName,columns, $"{ToDate}({ProjectstartField}){GreaterThanEqualOperator} '{startDate.ToLower().Trim()}' {AndOperator} {ToDate}({ProjectendField}){LessThanEqualOperator} '{endDate.ToLower().Trim()}'");
            ApplicationLogger.InfoLogger($"Orders count: {lstOfPr01.Count()}");
            return lstOfPr01;
        }

        public IEnumerable<Pr01> GetProjectByName(string companyCode, string projectName)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetProjectByName : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, parentCompanyCode); ;
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
            var lstOfPr01 = Database.Where<Pr01>(tableName,columns, $"trim(lower({ProjectnameField})){LikeOperator}'%{projectName.ToLower().Trim()}%'");
            ApplicationLogger.InfoLogger($"Orders count: {lstOfPr01.Count()}");
            return lstOfPr01;
        }

        public Pr01 GetProjectByNumber(string companyCode, string projectNumber)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetProjectByCustomerPONo : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, parentCompanyCode); ;
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
            var pr01 = Database.Where<Pr01>(tableName,columns, $"trim(lower({ProjectnumberField})){EqualOperator}'{projectNumber.ToLower().Trim()}'");
            return pr01.FirstOrDefault();
        }

        public IEnumerable<Pr01> GetProjectByCompanyCode(string companyCode)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetProjectByCompanyCode : Reading datalake table name from config");
            var databaseDetails = _configReader.GetDatabaseDetails(companyCode, parentCompanyCode); ;
            string tableName = databaseDetails[Constants.DATABASE_TABLE_NAME_KEY];
            string columns = databaseDetails[Constants.DATABASE_COLUMN_NAME_KEY];
            ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
            var lstOfPr01 = Database.Get<Pr01>(tableName,columns);
            ApplicationLogger.InfoLogger($"Orders count: {lstOfPr01.Count()}");
            return lstOfPr01;
        }

        private static void LogException(Exception exception)
        {
            if (exception.GetType() == typeof(HttpResponseException))
            {
                HttpResponseException responseException = (HttpResponseException)exception;
                ApplicationLogger.Errorlog(responseException.Response.ReasonPhrase, Category.Database,
                    responseException.Response.Content.ReadAsStringAsync().Result, responseException.InnerException);
                ApplicationLogger.InfoLogger(
                    $"Denodo Adapter exception :: {responseException.Response.ReasonPhrase}");
                throw responseException;
            }
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }
    }
}
