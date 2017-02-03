namespace TaxInvoice.Model.Models
{
    public class TaxInvoiceModel
    {
        public string InvoiceNo;
        public string CustomerCode;
        public string TaxRateCode;
        public decimal TotalBaseAmount;
        public decimal TotalTaxAmount;
        public string TaxType;
        public decimal TotalSTBase;
        public string VATType;
        public decimal TotalSale;
    }
}