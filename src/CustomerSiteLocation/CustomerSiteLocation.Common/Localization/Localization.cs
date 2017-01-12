using CustomerSiteLocation.Common.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSiteLocation.Common
{
    public class MessageLocalization
    {
        private static string _connectionString= ConfigurationManager.AppSettings["ConfigurationDbConnectionString"];
        public static string GetLocaleMessage(string companyCode, string messageKey, string localeCode)
        {
            //localeCode = "ar";
            string localMessage = string.Empty;
            Configuration configuration = new Configuration(_connectionString);
            try
            {
                var parameterCollection = new List<SqlParameter>
               {
                   new SqlParameter("@CompanyCode", companyCode),
                   new SqlParameter("@MessageKey", messageKey),
                   new SqlParameter("@LocaleCode", localeCode)
               };
                var dataSet = configuration.GetDataFromStoredProcedure("GetLocaleMessage", parameterCollection);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    localMessage = dataSet.Tables[0].Rows[0]["LocaleMessage"].ToString();
                    return localMessage;
                }
                throw new Exception(
                    $"Record not found for CompanyCode:[{companyCode}] MessageKey:[{messageKey}] LanguageCode:[{localeCode}]");
            }
            catch (Exception ex)
            {
                Exception innerException = ex;
                while (innerException != null && innerException.InnerException != null)
                    innerException = innerException.InnerException;

                ApplicationLogger.Errorlog(innerException.Message, Category.Database, innerException.StackTrace, innerException);
                throw;
            }
        }
    }
}
