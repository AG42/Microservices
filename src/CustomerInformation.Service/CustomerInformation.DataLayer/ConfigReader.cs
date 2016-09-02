using System.Configuration;
using Configuration = CustomerInformation.Common.Configuration;

namespace CustomerInformation.DataLayer
{
    class ConfigReader
    {
        private const string BASEURI_KEY = "BaseUri";
        private const string DENODO_USERNAME_KEY = "DenodoUsername";
        private const string DENODO_PASSWORD_KEY = "DenodoPassword";
        private const string CUSTOMERINFORMATION_VIEWURI_KEY = "CustomerInformationViewUri";
        public string ServiceName { get; set; }
        public string Environment { get; set; }
        public string ConfigurationDbConnectionString { get; set; }
        public string BaseUri { get; set; }
        public string DenodoUsername { get; set; }
        public string DenodoPassword { get; set; }
        public string CustomerInformationViewUri { get; set; }
        public ConfigReader(bool readFromDatabase)
        {   
            ServiceName = ReadConfig("ServiceName");
            Environment = ReadConfig("Environment");
            if (readFromDatabase)
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
            BaseUri = ReadConfig(BASEURI_KEY);
            DenodoUsername = ReadConfig(DENODO_USERNAME_KEY);
            DenodoPassword = ReadConfig(DENODO_PASSWORD_KEY);
            CustomerInformationViewUri = ReadConfig(CUSTOMERINFORMATION_VIEWURI_KEY);
        }
        private void InitializeFromDatabase()
        {
            string configurationDbConnectionString = ReadConfig("ConfigurationDbConnectionString");
            var configuration = new Configuration(configurationDbConnectionString);
            var configurationDictionary = configuration.GetConfiguration(ServiceName, Environment);
            BaseUri = configurationDictionary[BASEURI_KEY];
            DenodoUsername = configurationDictionary[DENODO_USERNAME_KEY];
            DenodoPassword = configurationDictionary[DENODO_PASSWORD_KEY];
            CustomerInformationViewUri = configurationDictionary[CUSTOMERINFORMATION_VIEWURI_KEY];
        }

    }
}
