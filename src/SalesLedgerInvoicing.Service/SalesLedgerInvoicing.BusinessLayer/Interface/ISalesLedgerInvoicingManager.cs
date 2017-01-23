using SalesLedgerInvoicing.Model.Responses;

namespace SalesLedgerInvoicing.BusinessLayer.Interface
{
    public interface ISalesLedgerInvoicingManager
    {
        SalesLedgerInvoicingByCustomerCodeResponse GetSalesLedgerInvoicingByCustomerCode(string companyCode, string customerCode);
        SalesLedgerInvoicingByCustomerNameResponse GetSalesLedgerInvoicingByCustomerName(string companyCode, string customerName);
        SalesLedgerInvoicingByOrderNumberResponse GetSalesLedgerInvoicingByOrderNumber(string companyCode, string ordernumber);
        SalesLedgerInvoicingByInvoiceNumberResponse GetSalesLedgerInvoicingByInvoiceNumber(string companyCode, string inviceNumber);
        SalesLedgerInvoicingByInvoiceDateResponse GetSalesLedgerInvoicingByInvoiceDate(string companyCode, string fromDate, string toDate);
    }
}
