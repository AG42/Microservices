using System.Collections.Generic;
using CustomerSiteLocation.Model;
using CustomerSiteLocation.DataLayer.Entities.Datalake;
using System.Text;

namespace CustomerSiteLocation.BusinessLayer
{
    class Converter
    {
        #region "Members" 

        private const bool DefaultStocking = false;

        #endregion

        #region "Public Methods"
        /// <summary>
        /// This method takes CustomerProjectOrderHeaders denodo entities and Company code as a input and converts it to 
        /// CustomerProjectOrderModel business entities
        /// </summary>
        /// <param name="customerSitesSy80"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public static List<CustomerSiteLocationModel> ConvertToCustomerSiteLocationsModel(
            IEnumerable<Sy80> customerSitesSy80, string companyCode)
        {
            var customerSiteLocationsModels = new List<CustomerSiteLocationModel>();
            foreach (var customerSite in customerSitesSy80)
            {
                customerSiteLocationsModels.Add(ConvertToCustomerSiteLocationModel(customerSite, companyCode));
            }
            return customerSiteLocationsModels;
        }
        /// <summary>
        /// This Method converts the Sy80 datalake entity and company code as input and converts it to
        /// CustomerSiteLocationModel business entity 
        /// </summary>
        /// <param name="customerSiteSy80"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public static CustomerSiteLocationModel ConvertToCustomerSiteLocationModel(Sy80 customerSiteSy80,
            string companyCode)
        {
            string erpLocationId = "I" + companyCode?.ToUpperInvariant() + customerSiteSy80.Sy80001?.Trim() + customerSiteSy80.Sy80002?.Trim();
            return new CustomerSiteLocationModel()
            {
                Name = customerSiteSy80.Sy80003?.Trim(),
                ERP_Customer_Code__c = customerSiteSy80.Sy80001?.Trim(),
                ERP_Site_Code__c = customerSiteSy80.Sy80002?.Trim(),
                SVMXC__Street__c = ExtractAddress(customerSiteSy80),
                SVMXC__City__c = string.Empty,
                County__c = string.Empty,
                SVMXC__State__c = string.Empty,
                Region__c = string.Empty,
                SVMXC__Country__c = customerSiteSy80.Sy80048?.Trim(),
                SVMXC__Zip__c = ExtractZip(customerSiteSy80.Sy80010?.Trim(), customerSiteSy80.Sy80045?.Trim()),
                SVMXC__Site_Fax__c = customerSiteSy80.Sy80012?.Trim(),
                SVMXC__Site_Phone__c = customerSiteSy80.Sy80011?.Trim(),
                SVMXC__Email__c = customerSiteSy80.Sy80049?.Trim(),
                SVMXC__Latitude__c = customerSiteSy80.Sy80054?.Trim(),
                SVMXC__Longitude__c = customerSiteSy80.Sy80053?.Trim(),
                Altitude__c = customerSiteSy80.Sy80055?.Trim(),
                SVMXC__Service_Engineer__c = string.Empty,
                Service_Engineer_Code__c = customerSiteSy80.Sy80046?.Trim(),
                SVMXC__Stocking_Location__c = DefaultStocking,
                ERP_Location_ID__c = erpLocationId
            };
        }

        #endregion

        #region "Private Methods"
        /// <summary>
        /// Extract Address
        /// </summary>
        /// <param name="customerSiteSy80"></param>
        /// <returns></returns>
        private static string ExtractAddress(Sy80 customerSiteSy80)
        {
            string newLine = "\n";
            StringBuilder addressBuilder = new StringBuilder();
            addressBuilder.Append(customerSiteSy80.Sy80004 + newLine);
            addressBuilder.Append(customerSiteSy80.Sy80005 + newLine);
            addressBuilder.Append(customerSiteSy80.Sy80006 + newLine);
            addressBuilder.Append(customerSiteSy80.Sy80007 + newLine);
            addressBuilder.Append(customerSiteSy80.Sy80050 + newLine);
            addressBuilder.Append(customerSiteSy80.Sy80051 + newLine);
            addressBuilder.Append(customerSiteSy80.Sy80052);
            return addressBuilder.ToString();
        }
        /// <summary>
        /// Extract zip code
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="county"></param>
        /// <returns></returns>
        private static string ExtractZip(string zip, string county)
        {
            //To do once the county data is extracted properly
            if (!string.IsNullOrEmpty(zip))
                return zip;
            county =
                county.Length > 10
                    ? county.Substring(0, 10)
                    : county;
            return county;
        }

        #endregion
    }
}
