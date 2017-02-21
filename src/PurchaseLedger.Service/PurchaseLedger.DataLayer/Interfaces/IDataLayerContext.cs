using PurchaseLedger.DataLayer.Entities.Datalake;
using System.Collections.Generic;

namespace PurchaseLedger.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        IEnumerable<Pl03> GetPurchaseLedgerByCompanyCode(string companyCode);
        IEnumerable<Pl03> GetPurchaseLedgerBySupplierCode(string companyCode,string supplierCode);
        IEnumerable<Pl03> GetPurchaseLedgerBySupplierName(string companyCode, string supplierName);
        IEnumerable<Pl03> GetPurchaseLedgerByInvoiceNo(string companyCode, string invoiceNo);
        IEnumerable<Pl03> GetPurchaseLedgerByInvoiceDateRange(string companyCode, string invoiceStartDate,string invoiceEndDate);
        IEnumerable<Pl03> GetPurchaseLedgerByOrderNo(string companyCode, string orderNo);
        IEnumerable<Pl03> GetPurchaseLedgerByDueDateRange(string companyCode, string dueStartDate,string dueEndDate);
    }
}
