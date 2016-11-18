using ProductInventory.Common;
using System.Collections.Generic;
using System.Configuration;
using Configuration = ProductInventory.Common.Configuration;

namespace ProductInventory.DataLayer
{
    class ConfigReader
    {
        private readonly bool _readFromDatabase;
        private const string DATALAKE_CONNECTIONSTRING_KEY = "DatalakeConnectionString";
        private string ServiceName { get; }
        private string Environment { get; }
        public string ConfigurationDbConnectionString { get; set; }
        public string DatalakeConnectionString { get; private set; }
        public ConfigReader()
        {
            _readFromDatabase = bool.Parse(ReadConfig("ReadConfigFromDatabase"));
            ServiceName = ReadConfig("ServiceName");
            Environment = ReadConfig("Environment");

            if (_readFromDatabase)
                InitializeFromDatabase();
            else
                InitializeFromConfig();
        }
        public string ReadConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        private void InitializeFromConfig()
        {
            DatalakeConnectionString = ReadConfig(DATALAKE_CONNECTIONSTRING_KEY);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            DatalakeConnectionString = configurationDictionary[DATALAKE_CONNECTIONSTRING_KEY];
        }
        
        public Dictionary<string,string> GetDatabaseTableName(string companyCode, string databaseTableNameKey,string columnNameKey)
        {
            if (!_readFromDatabase)
            {
                var dicTableName = new Dictionary<string, string>();
                dicTableName.Add(Constants.TableNameKey, ReadConfig($"{databaseTableNameKey}_{companyCode.ToLower()}"));
                dicTableName.Add(Constants.ColumnNameKey, ReadConfig($"{columnNameKey}_{companyCode.ToLower()}"));
                return dicTableName;
            }
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDatabaseTableName(ServiceName, Environment, companyCode, databaseTableNameKey);
        }
    }
}
