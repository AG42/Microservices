using CustomerInformation.Model.Response;

namespace CustomerInformation.BusinessLayer.Interface
{
    public interface ICustomerManager
    {
        CustomerSearchByCompanyCodeResponse GetCustomers(string companyCode);
        CustomerSearchByIdResponse GetCustomerById(string companyCode, string customerCode);
        CustomerSearchByNameResponse GetCustomerByName(string companyCode, string customerName);
        CustomerSearchByEmailIdResponse GetCustomerByEmailId(string companyCode, string emailId);
        CustomerSearchByPhoneNumberResponse GetCustomerByPhoneNumber(string companyCode, string phoneNumber);
        CustomerSearchByCategoryResponse GetCustomerByCategory(string companyCode, string category);
        CustomerSearchByCustomerAlternateNameResponse GetCustomerByCustomerAlternateName(string companyCode, string customerAlternateName);
        CustomerSearchByCountryCodeResponse GetCustomerByCountryCode(string companyCode, string countryCode);
    }
}
