using System.Configuration;
using Configuration = CustomerProjectOrder.Common.Configuration;

namespace CustomerProjectOrder.DataLayer
{
    public class ConfigReader
    {
        private bool _readFromDatabase;
        private const string CustomerprojectorderViewuriKey = "CustomerProjectOrderViewUri";
        private const string DatalakeConnectionstringKey = "DatalakeConnectionString";
        private const string DatalakeTableNameKey = "DatalakeTableName";
        private string ServiceName { get; }
        private string Environment { get; }
        public string ConfigurationDbConnectionString { get; set; }
        public string DatalakeConnectionString { get; private set; }

        public bool ReadFromDatabase
        {
            get { return _readFromDatabase; }

            set { _readFromDatabase = value; }
        }

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
            DatalakeConnectionString = ReadConfig(DatalakeConnectionstringKey);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            DatalakeConnectionString = configurationDictionary[DatalakeConnectionstringKey];
        }
        public string GetDenodoViewUri(string companyCode)
        {
            if (!_readFromDatabase)
                return ReadConfig($"{CustomerprojectorderViewuriKey}_{companyCode.ToLower()}");

            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDenodoViewUri(ServiceName, Environment, companyCode, CustomerprojectorderViewuriKey);
        }

        public string GetDatalakeTableName(string companyCode)
        {
            if (!_readFromDatabase)
                return ReadConfig($"{DatalakeTableNameKey}_{companyCode.ToLower()}");

            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            return configuration.GetDatalakeTableName(ServiceName, Environment, companyCode);
        }
    }
}
