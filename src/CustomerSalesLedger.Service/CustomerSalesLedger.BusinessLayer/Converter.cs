using System;
using System.Collections.Generic;
using CustomerSalesLedger.DataLayer.Entities.Datalake;
using CustomerSalesLedger.Model;
using CustomerSalesLedger.Common;
using CustomerSalesLedger.Common.Enums;

namespace CustomerSalesLedger.BusinessLayer
{
    class Converter
    {
        public static List<CustomerSalesLedgerModel> Convert(IEnumerable<Sl01> customerMasters, string companyCode)
        {
            var customerSalesLedgerModels = new List<CustomerSalesLedgerModel>();
            foreach (var customerMaster in customerMasters)
                customerSalesLedgerModels.Add(Convert(customerMaster, companyCode));

            return customerSalesLedgerModels;
        }
        public static CustomerSalesLedgerModel Convert(Sl01 sl01, string companyCode)
        {
            return new CustomerSalesLedgerModel()
            {
                CustomerCode = sl01.Sl01001,
                CustomerName = sl01.Sl01002,
                HighestCreditLimit = sl01.Sl01018,
                TaxRegistrationNumber = sl01.Sl01021,
                PaymentTerms = sl01.Sl01024,
                Balance = sl01.Sl01038,
                Budget = sl01.Sl01045,
                TotalDaysDuePayment = sl01.Sl01048,
                NumberOfinvoices = sl01.Sl01049,
                LastInvoiceDate = sl01.Sl01050,
                LastPaymentDate = sl01.Sl01051,
                CreditCode = sl01.Sl01068,
                Category = sl01.Sl01010,
                WayOfPayment = !string.IsNullOrWhiteSpace(sl01.Sl01112) ? Utility.GetEnumDescription((WayOfPayment)(System.Convert.ToInt32(sl01.Sl01112))) : null,
                AlternateName = sl01.Sl01054,
                EmailId = sl01.Sl01193,
                PhoneNumber = sl01.Sl01011
            };
        }
    }
}
