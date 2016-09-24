using System;
using System.Collections.Generic;
using CustomerInformation.DataLayer.Entities;
using CustomerInformation.Model;

namespace CustomerInformation.BusinessLayer
{
    class Converter
    {
        public static List<CustomerInformationModel> Convert(List<CustomerMaster> customerMasters, string companyCode)
        {
            var customerInformationModels = new List<CustomerInformationModel>();
            foreach (var customerMaster in customerMasters)
                customerInformationModels.Add(Convert(customerMaster, companyCode));

            return customerInformationModels;
        }
        public static CustomerInformationModel Convert(CustomerMaster customerMaster, string companyCode)
        {

            return new CustomerInformationModel()
            {
                Name = customerMaster.sl01002,
                Description = customerMaster.sl01109,
                Id = customerMaster.sl01198,
                BillingStreet = string.Format(customerMaster.sl01003 + "{0}" + customerMaster.sl01004 + "{0}" + customerMaster.sl01005 + "{0}" + customerMaster.sl01099 + "{0}" +
                                customerMaster.sl0194 + "{0}" + customerMaster.sl01195 + "{0}" + customerMaster.sl01196, Environment.NewLine),
                BillingCity = customerMaster.sl01152,
                County = customerMaster.sl01152,
                BillingState = customerMaster.sl01152,
                Region = customerMaster.sl01152,
                BillingCountry = customerMaster.sl01104,
                BillingPostalCode = string.IsNullOrEmpty(customerMaster.sl01083) ? customerMaster.sl01152 : customerMaster.sl01083,
                Fax = customerMaster.sl01013,
                Phone = customerMaster.sl01011,
                CurrencyIsoCode = customerMaster.sl01022,
                ERP_Customer_Code_c = customerMaster.sl01001,
                Active__c = customerMaster.sl01060 == "0" ? "Yes" : customerMaster.sl01060 == "1" || customerMaster.sl01060 == "2" ? "No" : string.Empty,
                Credit_Limit__c = customerMaster.sl01037,
                Payment_Terms_Code__c = !string.IsNullOrEmpty(customerMaster.sl01024) ?customerMaster.sl01024.Substring(0, 2): customerMaster.sl01024,
                ERP_Technician_Code__c = customerMaster.sl01084,
                Reference1__c = !string.IsNullOrEmpty(customerMaster.sl01006) ? customerMaster.sl01006 : null,
                Reference2__c = !string.IsNullOrEmpty(customerMaster.sl01007) ? customerMaster.sl01007 : null,
                Reference3__c = !string.IsNullOrEmpty(customerMaster.sl01008) ? customerMaster.sl01008 : null,
                Reference4__c = !string.IsNullOrEmpty(customerMaster.sl01009) ? customerMaster.sl01009 : null,
                Intercompany__c = customerMaster.sl01017,
                Organisational_Code__c = customerMaster.sl01017,
                Language_Code__c = customerMaster.sl01023,
                Terms_of_Delivery__c = customerMaster.sl01001 + " " + customerMaster.sl01026,
                TaxCode__c = customerMaster.sl01107,
                ERP_Key = "I" + companyCode + customerMaster.sl01001,
                Status__c = "Customer",
                SVMXC__Preferred_Technician__c = customerMaster.sl01084,
                ERP_Company_Code__c = companyCode
            };
        }
    }
}
