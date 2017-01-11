using System;
using System.Collections.Generic;
using CustomerInformation.DataLayer.Entities.Datalake;
using CustomerInformation.Model;
using CustomerInformation.Common;

namespace CustomerInformation.BusinessLayer
{
    class Converter
    {
        public static List<CustomerInformationModel> Convert(IEnumerable<Sl01> customerMasters, string companyCode)
        {
            var customerInformationModels = new List<CustomerInformationModel>();
            foreach (var customerMaster in customerMasters)
                customerInformationModels.Add(Convert(customerMaster, companyCode));

            return customerInformationModels;
        }
        public static CustomerInformationModel Convert(Sl01 sl01, string companyCode)
        {
            return new CustomerInformationModel()
            {
                Name = sl01.sl01002,
                Description = sl01.sl01109,
                Id = sl01.sl01198,
                BillingStreet = string.Format(sl01.sl01003 + "{0}" + sl01.sl01004 + "{0}" + sl01.sl01005 + "{0}" + sl01.sl01099 + "{0}" +
                                sl01.sl01194 + "{0}" + sl01.sl01195 + "{0}" + sl01.sl01196, Environment.NewLine),
                BillingCity = sl01.sl01152,
                County = sl01.sl01152,
                BillingState = sl01.sl01152,
                Region = sl01.sl01152,
                BillingCountry = sl01.sl01104,
                BillingPostalCode = string.IsNullOrEmpty(sl01.sl01083) ? sl01.sl01152 : sl01.sl01083,
                Fax = sl01.sl01013,
                CurrencyIsoCode = sl01.sl01022,
                ERP_Customer_Code_c = sl01.sl01001,
                Active__c = sl01.sl01060 == "0" ? Constants.Yes : sl01.sl01060 == "1" || sl01.sl01060 == "2" ? Constants.No : string.Empty,
                Credit_Limit__c = sl01.sl01037,
                Payment_Terms_Code__c = !string.IsNullOrEmpty(sl01.sl01024) ? (sl01.sl01024.Length > 2 ? sl01.sl01024.Substring(0, 2) : sl01.sl01024) : sl01.sl01024,
                ERP_Technician_Code__c = sl01.sl01084,
                Reference1__c = !string.IsNullOrEmpty(sl01.sl01006) ? sl01.sl01006 : null,
                Reference2__c = !string.IsNullOrEmpty(sl01.sl01007) ? sl01.sl01007 : null,
                Reference3__c = !string.IsNullOrEmpty(sl01.sl01008) ? sl01.sl01008 : null,
                Reference4__c = !string.IsNullOrEmpty(sl01.sl01009) ? sl01.sl01009 : null,
                Intercompany__c = sl01.sl01017,
                Organisational_Code__c = sl01.sl01017,
                Language_Code__c = sl01.sl01023,
                Terms_of_Delivery__c = sl01.sl01001 + " " + sl01.sl01026,
                TaxCode__c = sl01.sl01107,
                ERP_Key = "I" + companyCode + sl01.sl01001,
                Status__c = "Customer",
                SVMXC__Preferred_Technician__c = sl01.sl01084,
                ERP_Company_Code__c = companyCode,
                AlternateName = sl01.sl01054,
                EmailId = sl01.sl01052,
                Category = sl01.sl01010,
                StartDate = sl01.sl01220,
                EndDate = sl01.sl01221,
                PhoneNumber = sl01.sl01011
            };
        }
    }
}
