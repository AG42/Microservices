using CustomerProjectOrder.Common;
using System.Collections.Generic;
using System.Configuration;
using Configuration = CustomerProjectOrder.Common.Configuration;

namespace CustomerProjectOrder.DataLayer
{
    public class ConfigReader
    {
        private bool _readFromDatabase;
        private string ServiceName { get; }
        private string Environment { get; }
        public string ConfigurationDbConnectionString { get; set; }
        public string DatalakeConnectionString { get; private set; }
        public string DatasourceLibraryPath { get; private set; }

        public bool ReadFromDatabase
        {
            get { return _readFromDatabase; }

            set { _readFromDatabase = value; }
        }

        public ConfigReader()
        {
            _readFromDatabase = bool.Parse(ReadConfig(Constants.READ_CONFIG_FROM_DATABASE));
            ServiceName = ReadConfig(Constants.ServiceNameKey);
            Environment = ReadConfig(Constants.EnvironmentKey);
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
            DatalakeConnectionString = ReadConfig(Constants.DATABASE_CONNECTIONSTRING_KEY);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig(Constants.CONFIGURATION_DB_CONNECTIONSTRING_KEY);
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            DatalakeConnectionString = configurationDictionary[Constants.DATABASE_CONNECTIONSTRING_KEY];
        }

        public Dictionary<string, string> GetDatabaseDetails(string companyCode,string parentCompanyCode)
        {
            if (!_readFromDatabase)
            {
                var dicTableName = new Dictionary<string, string>();
                dicTableName.Add(Constants.DATABASE_TABLE_NAME_KEY, ReadConfig($"{Constants.DATABASE_TABLE_NAME_KEY}_{companyCode.ToLower()}"));
                dicTableName.Add(Constants.DATABASE_COLUMN_NAME_KEY, ReadConfig($"{Constants.DATABASE_COLUMN_NAME_KEY}_{companyCode.ToLower()}"));
                return dicTableName;
            }
            string configurationDbConnectionString = ReadConfig(Constants.CONFIGURATION_DB_CONNECTIONSTRING_KEY);
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDatabaseTableName(ServiceName, Environment, companyCode,parentCompanyCode);
        }


    }
}
