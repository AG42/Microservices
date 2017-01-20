using System.Collections.Generic;
using System.Linq;
using SalesLedgerInvoicing.Model;
using SalesLedgerInvoicing.DataLayer.Entities.Datalake;
using SalesLedgerInvoicing.Common;
using SalesLedgerInvoicing.Common.Enums;

namespace SalesLedgerInvoicing.BusinessLayer
{
    class Converter
    {
        public static List<SalesLedgerInvoicesModel> Convert(IEnumerable<SL03> salesLegderInvoicing, string companyCode, string customerName)
        {
            var salesLedgerDetailsLineModels = new List<SalesLedgerInvoicesModel>();
            foreach (var salesLedger in salesLegderInvoicing)
                salesLedgerDetailsLineModels.Add(ConvertSalesLedgerDetails(salesLedger, companyCode, customerName));

            return salesLedgerDetailsLineModels;
        }

        public static List<SalesLedgerInvoicesModel> Convert(IEnumerable<SL03> salesLegderInvoicing, string companyCode, IEnumerable<SL01> customerDetails)
        {
            var salesLedgerDetailsLineModels = new List<SalesLedgerInvoicesModel>();
            foreach (var salesLedger in salesLegderInvoicing)
            {
                var customerName = customerDetails.Where(cust => cust.Sl01001 == salesLedger.Sl03001).Select(cust => cust.Sl01002).FirstOrDefault();
                salesLedgerDetailsLineModels.Add(ConvertSalesLedgerDetails(salesLedger, companyCode, customerName));
            }
            return salesLedgerDetailsLineModels;
        }

        public static SalesLedgerInvoicesModel ConvertSalesLedgerDetails(SL03 sl03, string companyCode, string customerName)
        {
            var salesLedgerInvoicesModel = new SalesLedgerInvoicesModel
            {
                CustomerCode = sl03.Sl03001,
                CustomerName = customerName,
                OrderNumber = sl03.Sl03036,
                OrderType =
                    !string.IsNullOrWhiteSpace(sl03.Sl03035)
                        ? Utility.GetEnumDescription((OrderType) (System.Convert.ToInt32(sl03.Sl03035)))
                        : null,
                InvoiceNumber = sl03.Sl03002,
                InvoiceDate = sl03.Sl03004,
                InvoiceType =
                    !string.IsNullOrWhiteSpace(sl03.Sl03027)
                        ? Utility.GetEnumDescription((InvoiceType) (System.Convert.ToInt32(sl03.Sl03027)))
                        : null,
                InvoiceStatus =
                    !string.IsNullOrWhiteSpace(sl03.Sl03042)
                        ? Utility.GetEnumDescription((InvoiceStatusType) (System.Convert.ToInt32(sl03.Sl03042)))
                        : null,
                TransactionType =
                    !string.IsNullOrWhiteSpace(sl03.Sl03040)
                        ? Utility.GetEnumDescription((TransactionType) (System.Convert.ToInt32(sl03.Sl03040)))
                        : null,
                StornoInvoice =
                    !string.IsNullOrWhiteSpace(sl03.Sl03025)
                        ? Utility.GetEnumDescription((Storno) (System.Convert.ToInt32(sl03.Sl03025)))
                        : null,
                Amount = sl03.Sl03013,
                DueDate = sl03.Sl03006
            };

            return salesLedgerInvoicesModel;
        }
    }
}
