using System;
using System.Collections.Generic;
using System.Web.Http;
using CustomerProjectOrder.Common.Logger;
using CustomerProjectOrder.DataLayer.Entities.Datalake;
using CustomerProjectOrder.DataLayer.Interfaces;
using System.Linq;

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

        //....added to direct connection
        private readonly IDatalakeEntities _datalakeEntities;

        public DataLayerContext(IDatalakeEntities datalakeEntities)
        {
            try
            {
                _configReader = new ConfigReader();
                _datalakeEntities = datalakeEntities;
                _datalakeEntities.ConnectionString = _configReader.DatalakeConnectionString;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                    exception.InnerException);
                throw;
            }
        }

        public IEnumerable<Pr01> GetProjectByAccount(string companyCode, string account)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProjectByAccount : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var lstOfPr01 = _datalakeEntities.Where<Pr01>(tableName, $"trim(lower({CustomerAccountNumber})){EqualOperator}'{account.ToLower().Trim()}'");
                ApplicationLogger.InfoLogger($"Orders count: {lstOfPr01.Count()}");
                return lstOfPr01;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Pr01 GetProjectByCustomerPONo(string companyCode, string customerPONo)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProjectByCustomerPONo : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var pr01 = _datalakeEntities.Where<Pr01>(tableName, $"trim(lower({CustomerPoNumber})){EqualOperator}'{customerPONo.ToLower().Trim()}'");
                return pr01.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public IEnumerable<Pr01> GetProjectByDuration(string companyCode, string startDate, string endDate)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProjectByDuration : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var lstOfPr01 = _datalakeEntities.Where<Pr01>(tableName, $"{ToDate}({ProjectstartField}){GreaterThanEqualOperator} '{startDate.ToLower().Trim()}' {AndOperator} {ToDate}({ProjectendField}){LessThanEqualOperator} '{endDate.ToLower().Trim()}'");
                ApplicationLogger.InfoLogger($"Orders count: {lstOfPr01.Count()}");
                return lstOfPr01;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public IEnumerable<Pr01> GetProjectByName(string companyCode, string projectName)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProjectByName : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var lstOfPr01 = _datalakeEntities.Where<Pr01>(tableName, $"trim(lower({ProjectnameField})){LikeOperator}'%{projectName.ToLower().Trim()}%'");
                ApplicationLogger.InfoLogger($"Orders count: {lstOfPr01.Count()}");
                return lstOfPr01;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public Pr01 GetProjectByNumber(string companyCode, string projectNumber)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProjectByCustomerPONo : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var pr01 = _datalakeEntities.Where<Pr01>(tableName, $"trim(lower({ProjectnumberField})){EqualOperator}'{projectNumber.ToLower().Trim()}'");
                return pr01.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public IEnumerable<Pr01> GetProjectByCompanyCode(string companyCode)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProjectByCompanyCode : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var lstOfPr01 = _datalakeEntities.Get<Pr01>(tableName);
                ApplicationLogger.InfoLogger($"Orders count: {lstOfPr01.Count()}");
                return lstOfPr01;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
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
