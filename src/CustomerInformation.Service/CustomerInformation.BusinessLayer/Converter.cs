using System;
using System.Collections.Generic;
using CustomerInformation.DataLayer.Entities;
using CustomerInformation.Model;
using CustomerInformation.Common.Error;
using CustomerInformation.Common;
using System.Net;

namespace CustomerInformation.BusinessLayer
{
    class Converter
    {
        public static List<CustomerInformationModel> Convert(List<CustomerMaster> customerMasters)
        {

            if (customerMasters == null)
            {
                Error.AddError(Constants.NoDataFound, HttpStatusCode.NotFound);
                return null;
            }

            var customerInformationModels = new List<CustomerInformationModel>();
            foreach (var customerMaster in customerMasters)
                customerInformationModels.Add(Convert(customerMaster));

            return customerInformationModels;
        }
        public static CustomerInformationModel Convert(CustomerMaster customerMaster)
        {
            if (customerMaster == null)
            {
                Error.AddError(Constants.NoDataFound, HttpStatusCode.NotFound);
                return null;
            }

            return new CustomerInformationModel()
            {
                AccountName = customerMaster.sl01002,
                Description = customerMaster.sl01109,
                Id = customerMaster.sl01198,
                AddressLine1 = customerMaster.sl01003,
                AddressLine2 = customerMaster.sl01004,
                AddressLine3 = customerMaster.sl01005,
                AddressLine4 = customerMaster.sl01099,
                AddressLine5 = customerMaster.sl0194,
                AddressLine6 = customerMaster.sl01195,
                AddressLine7 = customerMaster.sl01196,
                BillingStreet = String.Format(customerMaster.sl01003 + "{0}" + customerMaster.sl01004 + "{0}" + customerMaster.sl01005 + "{0}" + customerMaster.sl01099 + "{0}" + 
                                customerMaster.sl0194 + "{0}" + customerMaster.sl01195 + "{0}" + customerMaster.sl01196, Environment.NewLine),
                BillingCity = customerMaster.sl01152,
                County = customerMaster.sl01152,
                BillingState = customerMaster.sl01152,
                Region = customerMaster.sl01152,
                BillingCountry = customerMaster.sl01104,
                BillingPostalCode = customerMaster.sl01083,
                Fax = customerMaster.sl01013,
                Phone = customerMaster.sl01011,
                CurrencyIsoCode = customerMaster.sl01022,
                ERP_Customer_Code_c = customerMaster.sl01001,
                Active__c = customerMaster.sl01060,
                Credit_Limit__c = customerMaster.sl01037,
                Payment_Terms_Code__c = customerMaster.sl01024,
                ERP_Technician_Code__c = customerMaster.sl01084,
                Reference1__c = customerMaster.sl01006,
                Reference2__c = customerMaster.sl01007,
                Reference3__c = customerMaster.sl01008,
                Reference4__c = customerMaster.sl01009,
                Intercompany__c = customerMaster.sl01017,
                Organisational_Code__c = customerMaster.sl01017,
                Language_Code__c = customerMaster.sl01023,
                Terms_of_Delivery__c = customerMaster.sl01026,
                TaxCode__c = customerMaster.sl01107,
                ERP_Key = customerMaster.sl01001
            };
        }
    }
}
