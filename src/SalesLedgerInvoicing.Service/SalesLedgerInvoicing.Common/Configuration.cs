using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace SalesLedgerInvoicing.Common
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
            throw new Exception($"Record not found for Service:[{serviceName}] Environment:[{environment}]");
        }

        public Dictionary<string, string> GetDatabaseTableName(string serviceName, string environment, string companyCode, string tableNameKey, string columnNameKey)
        {
            var returnCollection = new Dictionary<string, string>();
            var parameterCollection = new List<SqlParameter>
                {
                    new SqlParameter("@ServiceName", serviceName),
                    new SqlParameter("@Environment", environment),
                    new SqlParameter("@CompanyCode", companyCode),
                    new SqlParameter("@TableNameKey", tableNameKey)
                };
            var dataSet = GetDataFromStoredProcedure("GetDatalakeTableMapping", parameterCollection);
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                returnCollection.Add(tableNameKey, $"{dataSet.Tables[0].Rows[0]["DatabaseName"]}.{dataSet.Tables[0].Rows[0]["TableName"]}");
                returnCollection.Add(columnNameKey, $"{dataSet.Tables[0].Rows[0]["ColumnName"]}");
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
