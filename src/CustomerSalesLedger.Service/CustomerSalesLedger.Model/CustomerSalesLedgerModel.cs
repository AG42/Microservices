using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSalesLedger.Model
{
    public class CustomerSalesLedgerModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string HighestCreditLimit { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string PaymentTerms { get; set; }
        public string Balance { get; set; }
        public string Budget { get; set; }
        public string TotalDaysDuePayment { get; set; }
        public string NumberOfinvoices { get; set; }
        public string LastInvoiceDate { get; set; }
        public string LastPaymentDate { get; set; }
        public string CreditCode { get; set; }
        public string Category { get; set; }
        public string WayOfPayment { get; set; }
        public string AlternateName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
