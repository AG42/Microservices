using CustomerSalesLedger.Model.Response;

namespace CustomerSalesLedger.BusinessLayer.Interfaces
{
    public interface ICustomerSalesLedgerManager
    {
        CustomerSalesLedgerByCustomerCodeResponse GetCustomerSalesLedgerByCustomerCode(string companyCode, string customerCode);
        CustomerSalesLedgerByCustomerNameResponse GetCustomerSalesLedgerByCustomerName(string companyCode, string customerName);
        CustomerSalesLedgerByEmailIdResponse GetCustomerSalesLedgerByEmailId(string companyCode, string emailId);
        CustomerSalesledgerByPhoneNoResponse GetCustomerSalesLedgerByPhoneNumber(string companyCode, string telephoneNumber);
        CustomerSalesLedgerByCategoryResponse GetCustomerSalesLedgerByCategory(string companyCode, string category);
        CustomerSalesLedgerByAlternateNameResponse GetCustomerSalesLedgerByAlternateName(string companyCode, string customerAlternateName);
    }
}
