using System.Configuration;
using Configuration = ServiceOrder.Common.Configuration;

namespace ServiceOrder.DataLayer
{
    class ConfigReader
    {
        private readonly bool _readFromDatabase;
        private const string DATALAKE_CONNECTIONSTRING_KEY = "DatalakeConnectionString";
        private const string ISCALA_CONNECTIONSTRING_KEY = "iScalaConnectionString";
        private string ServiceName { get; }
        private string Environment { get; }
        public string ConfigurationDbConnectionString { get; set; }
        public string DatalakeConnectionString { get; private set; }
        public string IScalaConnectionString { get; private set; }
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
            IScalaConnectionString = ReadConfig(ISCALA_CONNECTIONSTRING_KEY);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            DatalakeConnectionString = configurationDictionary[DATALAKE_CONNECTIONSTRING_KEY];
            IScalaConnectionString = configurationDictionary[ISCALA_CONNECTIONSTRING_KEY];
        }
        
        public string GetDatalakeTableName(string companyCode, string datalakeTableNameKey)
        {
            if (!_readFromDatabase)
                return ReadConfig($"{datalakeTableNameKey}_{companyCode.ToLower()}");

            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDatalakeTableName(ServiceName, Environment, companyCode,datalakeTableNameKey);
        }
    }
}
