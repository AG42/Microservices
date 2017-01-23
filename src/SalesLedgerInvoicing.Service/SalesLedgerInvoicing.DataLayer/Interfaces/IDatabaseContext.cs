using System.Collections.Generic;
using System.Threading.Tasks;
using SalesLedgerInvoicing.DataLayer.Entities.Datalake;

namespace SalesLedgerInvoicing.DataLayer.Interfaces
{
    public interface IDatabaseContext
    {
        Task<SL01> GetCustomerDetailsByCustomerCodeAsync(string companyCode, string customerCode);
        Task<IEnumerable<SL01>> GetCustomerDetailsBySalesLedgerCustomerCodeAsync(string companyCode, List<SL03> salesLedgerInvoices);
        Task<IEnumerable<SL01>> GetCustomerDetailsByNameAsync(string companyCode, string customerName);
        Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByCustomerCodeAsync(string companyCode, string customerCode);
        Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByCustomerListAsync(string companyCode, List<SL01> customerList);
        Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByOrderNumberAsync(string companyCode, string orderNumber);
        Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByInvoiceNumberAsync(string companyCode, string invoiceNumber);
        Task<IEnumerable<SL03>> GetSalesLedgerInvoicesByInvoiceDateRangeAsync(string companyCode, string invoiceStartDate, string invoiceEndDate);
    }
}
