using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProductInventory.Common.Logger;

namespace ProductInventory.Common
{
    public class Configuration
    {
        private readonly string _connectionString;
        public Configuration(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Dictionary<string, string> GetConfiguration(string serviceName, string environment)
        {
            var configDictionary = new Dictionary<string, string>();
            try
            {
                var parameterCollection = new List<SqlParameter>
               {
                   new SqlParameter("@ServiceName", serviceName),
                   new SqlParameter("@Environment", environment)
               };
                var dataSet = GetDataFromStoredProcedure("GetConfigDetails", parameterCollection);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        configDictionary.Add(row["APIKey"].ToString(), row["APIValue"].ToString());
                    }
                    return configDictionary;
                }
                throw new Exception(
                    $"Record not found for Service:[{serviceName}] Environment:[{environment}]");
            }
            catch (Exception ex)
            {
                Exception innerException = ex;
                while (innerException != null && innerException.InnerException != null)
                    innerException = innerException.InnerException;

                ApplicationLogger.Errorlog(innerException.Message, Category.Database, innerException.StackTrace, innerException);
                throw;
            }
        }

        public string GetDenodoViewUri(string serviceName, string environment, string companyCode, string denodoViewName)
        {
            try
            {
                var parameterCollection = new List<SqlParameter>
               {
                   new SqlParameter("@ServiceName", serviceName),
                   new SqlParameter("@Environment", environment),
                   new SqlParameter("@CompanyCode", companyCode),
                   new SqlParameter("@DenodoViewName", denodoViewName)
               };
                var dataSet = GetDataFromStoredProcedure("GetCompanyCodeDenodoViewMapping", parameterCollection);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return dataSet.Tables[0].Rows[0]["DenodoViewUri"].ToString();
                throw new Exception(
                    $"Record not found for Service:[{serviceName}] Environment:[{environment}] CompanyCode:[{companyCode}] DenodoViewName:[{denodoViewName}]");
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace, exception.InnerException);
                throw;
            }
        }
        public string GetDatalakeTableName(string serviceName, string environment, string companyCode)
        {
            try
            {
                var parameterCollection = new List<SqlParameter>
               {
                   new SqlParameter("@ServiceName", serviceName),
                   new SqlParameter("@Environment", environment),
                   new SqlParameter("@CompanyCode", companyCode)
               };
                var dataSet = GetDataFromStoredProcedure("GetDatalakeTableMapping", parameterCollection);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    return $"{dataSet.Tables[0].Rows[0]["DatabaseName"]}.{dataSet.Tables[0].Rows[1]["TableName"]}";
                throw new Exception(
                    $"Record not found for Service:[{serviceName}] Environment:[{environment}] CompanyCode:[{companyCode}]");
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace, exception.InnerException);
                throw;
            }
        }
        private DataSet GetDataFromStoredProcedure(string storedProcedureName, List<SqlParameter> parameterCollection)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;
                command.Parameters.AddRange(parameterCollection.ToArray());

                var dataAdapter = new SqlDataAdapter(command);
                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }
    }
}
