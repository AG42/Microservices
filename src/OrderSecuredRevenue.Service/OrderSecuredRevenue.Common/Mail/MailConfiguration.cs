using System.Configuration;

namespace OrderSecuredRevenue.Common.Mail
{
    public class MailConfiguration
    {
        public string Server { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }

        public MailConfiguration()
        {
            Server = ReadConfig(Constants.MailServerKey);
            From = ReadConfig(Constants.MailFromKey);
            To = ReadConfig(Constants.MailToKey);
            CC = ReadConfig(Constants.MailCcKey);
        }

        private string ReadConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
