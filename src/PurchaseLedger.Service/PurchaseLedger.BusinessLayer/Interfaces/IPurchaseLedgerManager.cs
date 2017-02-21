using PurchaseLedger.Model.Response;

namespace PurchaseLedger.BusinessLayer.Interfaces
{
    public interface IPurchaseLedgerManager
    {
        PurchaseLedgersResponse GetPurchaseLedgerByCompanyCode(string companyCode);
        PurchaseLedgersResponse GetPurchaseLedgerBySupplierCode(string companyCode, string supplierCode);
        PurchaseLedgersResponse GetPurchaseLedgerBySupplierName(string companyCode, string supplierName);
        PurchaseLedgersResponse GetPurchaseLedgerByInvoiceNo(string companyCode, string invoiceNo);
        PurchaseLedgersResponse GetPurchaseLedgerByInvoiceDateRange(string companyCode, string invoiceStartDate, string invoiceEndDate);
        PurchaseLedgersResponse GetPurchaseLedgerByOrderNo(string companyCode, string orderNo);
        PurchaseLedgersResponse GetPurchaseLedgerByDueDateRange(string companyCode, string dueStartDate, string dueEndDate);
    }
}
