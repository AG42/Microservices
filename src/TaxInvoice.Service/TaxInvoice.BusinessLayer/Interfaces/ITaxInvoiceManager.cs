using TaxInvoice.Model.Response;

namespace TaxInvoice.BusinessLayer.Interfaces
{
    public interface ITaxInvoiceManager
    {
        TaxInvoiceResponses GetTaxInvoiceByCompanyCode(string companyCode);
        TaxInvoiceResponses GetTaxInvoiceByInvoiceNo(string companyCode, string invoiceNo);
        TaxInvoiceResponses GetTaxInvoiceByCustomerCode(string companyCode, string customerCode);
        TaxInvoiceResponses GetTaxInvoiceByTaxAmountRange(string companyCode, decimal minTaxAmount, decimal maxTaxAmount);
    }
}