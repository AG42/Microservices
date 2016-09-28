using System.Configuration;
using Configuration = CustomerInformation.Common.Configuration;

namespace CustomerInformation.DataLayer
{
    class ConfigReader
    {
        private readonly bool _readFromDatabase;
        private const string CUSTOMERINFORMATION_VIEWURI_KEY = "CustomerInformationViewUri";
        private const string DATALAKE_CONNECTIONSTRING_KEY = "DatalakeConnectionString";
        private const string DATALAKE_TABLE_NAME_KEY = "DatalakeTableName";
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
        private string ReadConfig(string key)
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
        public string GetDenodoViewUri(string companyCode)
        {
            if (!_readFromDatabase)
                return ReadConfig($"{CUSTOMERINFORMATION_VIEWURI_KEY}_{companyCode.ToLower()}");

            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDenodoViewUri(ServiceName, Environment, companyCode, CUSTOMERINFORMATION_VIEWURI_KEY);
        }

        public string GetDatalakeTableName(string companyCode)
        {
            if (!_readFromDatabase)
                return ReadConfig($"{DATALAKE_TABLE_NAME_KEY}_{companyCode.ToLower()}");

            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDatalakeTableName(ServiceName, Environment, companyCode);
        }
    }
}
