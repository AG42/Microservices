using System.Collections.Generic;
using System.Configuration;
using SalesLedgerInvoicing.Common;
using Configuration = SalesLedgerInvoicing.Common.Configuration;

namespace SalesLedgerInvoicing.DataLayer
{
    class ConfigReader
    {
        private readonly bool _readFromDatabase;
        private string ServiceName { get; }
        private string Environment { get; }
        public string ConfigurationDbConnectionString { get; set; }
        public string DatabaseConnectionString { get; private set; }
        public string DatasourceLibraryPath { get; private set; }
        public ConfigReader()
        {
            _readFromDatabase = bool.Parse(ReadConfig(Constants.READ_CONFIG_FROM_DATABASE_KEY));
            ServiceName = ReadConfig(Constants.SERVICE_NAME_KEY);
            Environment = ReadConfig(Constants.ENVIRONMENT_KEY);
            DatasourceLibraryPath = ReadConfig(Constants.DATASOURCE_LIBRARYPATH_KEY);
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
            DatabaseConnectionString = ReadConfig(Constants.DATABASE_CONNECTIONSTRING_KEY);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig(Constants.CONFIGURATION_DB_CONNECTIONSTRING_KEY);
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            DatabaseConnectionString = configurationDictionary[Constants.DATABASE_CONNECTIONSTRING_KEY];
        }

        public Dictionary<string, string> GetDatabaseDetails(string companyCode, string databaseTableNameKey, string databaseColumnNameKey)
        {
            if (!_readFromDatabase)
            {
                var dicTableName = new Dictionary<string, string>
                {
                    {databaseTableNameKey, ReadConfig($"{databaseTableNameKey}_{companyCode.ToLower()}")},
                    {databaseColumnNameKey, ReadConfig($"{databaseColumnNameKey}_{companyCode.ToLower()}")}
                };
                return dicTableName;
            }
            string configurationDbConnectionString = ReadConfig(Constants.CONFIGURATION_DB_CONNECTIONSTRING_KEY);
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDatabaseTableName(ServiceName, Environment, companyCode, databaseTableNameKey, databaseColumnNameKey);
        }

    }
}
