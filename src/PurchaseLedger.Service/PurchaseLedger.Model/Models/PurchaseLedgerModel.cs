namespace PurchaseLedger.Model.Models
{
    public class PurchaseLedgerModel
    {
        public string InvoiceNo;
        public string OrderNo;
        public string InvoiceDate;
        public string DueDate;
        public string LatePaymDate;
        public decimal InvoiceAmt;
        public decimal SalesTaxAmt;
        public decimal PaidAmt;
        public string InvoicePaidFlag;
        public decimal Discount;
    }
}
