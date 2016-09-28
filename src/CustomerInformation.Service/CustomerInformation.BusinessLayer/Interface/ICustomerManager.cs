using CustomerInformation.Model.Response;

namespace CustomerInformation.BusinessLayer.Interface
{
    public interface ICustomerManager
    {
        CustomerSearchByCompanyCodeResponse GetCustomers(string companyCode);
        CustomerSearchByIdResponse GetCustomerById(string companyCode, string customerCode);
        CustomerSearchByNameResponse GetCustomerByName(string companyCode, string customerName);
    }
}
