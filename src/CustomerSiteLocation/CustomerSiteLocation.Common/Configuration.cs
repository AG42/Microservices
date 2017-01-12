using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomerSiteLocation.Common.Logger;
using CustomerSiteLocation.Common.Enum;

namespace CustomerSiteLocation.Common
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

        public string GetDenodoViewUri(string serviceName, string environment, string companyCode, string denodoViewName)
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
        public Dictionary<string, string> GetDataBaseTableName(string serviceName, string environment, string companyCode, string parentCompanyCode)
        {
           
                var returnCollection = new Dictionary<string, string>();
                var parameterCollection = new List<SqlParameter>
               {
                   new SqlParameter("@ServiceName", serviceName),
                   new SqlParameter("@Environment", environment),
                   new SqlParameter("@CompanyCode", companyCode),
                   new SqlParameter("@TableNameKey", ""),
                   new SqlParameter("@ParentCompanyCode", parentCompanyCode)
               };
                var dataSet = GetDataFromStoredProcedure("GetDatalakeTableMapping", parameterCollection);

                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    returnCollection.Add(Constants.TableNameKey, $"{dataSet.Tables[0].Rows[0]["DatabaseName"]}.{dataSet.Tables[0].Rows[0]["TableName"]}");
                    returnCollection.Add(Constants.ColumnNameKey, $"{dataSet.Tables[0].Rows[0]["ColumnName"]}");
                   
                    return returnCollection;
                }
                throw new Exception(
                    $"Record not found for Service:[{serviceName}] Environment:[{environment}] CompanyCode:[{companyCode}]");
           
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
