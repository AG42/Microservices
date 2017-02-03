namespace PurchaseOrderBySupplier.Model.Models
{
    public class Invoice
    {
        public string SupplierInvoiceNumber { get; set; }
        public decimal Total { get; set; }
        public decimal AmountLCU { get; set; }
        public decimal SalesTaxAmount { get; set; }
        public string VatCode { get; set; }
    }
}
