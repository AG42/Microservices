using CustomerSiteLocation.Common;
using System.Collections.Generic;
using System.Configuration;
using Configuration = CustomerSiteLocation.Common.Configuration;

namespace CustomerSiteLocation.DataLayer
{
    public class ConfigReader
    {
        private readonly bool _readFromDatabase;
        private const string DatabaseConnectionstringKey = "DatabaseConnectionString";
        private string ServiceName { get; }
        private string Environment { get; }
        public string ConfigurationDbConnectionString { get; set; }
        public string DatalakeConnectionString { get; private set; }
        public string DatasourceLibraryPath { get; private set; }
        public ConfigReader()
        {
            _readFromDatabase = bool.Parse(ReadConfig("ReadConfigFromDatabase"));
            ServiceName = ReadConfig("ServiceName");
            Environment = ReadConfig("Environment");
            DatasourceLibraryPath = ReadConfig(Constants.DATASOURCE_LIBRARY_PATH_KEY);

            if (_readFromDatabase)
                InitializeFromDatabase();
            else
                InitializeFromConfig();
        }
        private string ReadConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        private void InitializeFromConfig()
        {
            DatalakeConnectionString = ReadConfig(DatabaseConnectionstringKey);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            DatalakeConnectionString = configurationDictionary[DatabaseConnectionstringKey];
        }

        public Dictionary<string, string> GetDatabaseTableName(string companyCode, string parentTableNameKey)
        {
            if (!_readFromDatabase)
            {
                var dicTableName = new Dictionary<string, string>();
                dicTableName.Add(Constants.TableNameKey, ReadConfig($"{Constants.DATALAKE_TABLE_NAME_KEY}_{companyCode.ToLower()}"));
                dicTableName.Add(Constants.ColumnNameKey, ReadConfig($"{Constants.DATALAKE_COLUMN_NAME_KEY}_{companyCode.ToLower()}"));
                return dicTableName;
            }
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDataBaseTableName(ServiceName, Environment, companyCode, parentTableNameKey);
        }
    }
}


