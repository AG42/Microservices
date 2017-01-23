namespace SalesLedgerInvoicing.Model
{
    public class SalesLedgerInvoicesModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string OrderNumber{ get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceStatus { get; set; }
        public string OrderType { get; set; }
        public string TransactionType { get; set; }
        public string StornoInvoice { get; set; }
        public string InvoiceNumber { get; set; }
        public string Amount { get; set; }
        public string DueDate { get; set; }
    }
}
