using System.Collections.Generic;
using TaxInvoice.DataAccessLayer.Entities.Datalake;

namespace TaxInvoice.DataAccessLayer.Interface
{
    public interface IDataLayerContext
    {
        IEnumerable<SL17> GetTaxInvoiceByCompanyCode(string companyCode);
        IEnumerable<SL17> GetTaxInvoiceByInvoiceNo(string companyCode, string invoiceNo);
        IEnumerable<SL17> GetTaxInvoiceByCustomerCode(string companyCode, string customerCode);
        IEnumerable<SL17> GetTaxInvoiceByTaxAmountRange(string companyCode, decimal minTaxAmount, decimal maxTaxAmount);
    }
}