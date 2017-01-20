using CustomerSiteLocation.Model.Response;

namespace CustomerSiteLocation.BusinessLayer.Interface
{
    public interface ICustomerSiteLocationManager
    {
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByCompanyCode(string companyCode, string parentCompanyCode = "");
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByCountry(string companyCode, string country);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByZip(string companyCode, string zipCode);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsBySiteNo(string companyCode, string siteNo);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByAddress(string companyCode, string address);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByTelephoneNo(string companyCode, string telePhone);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByFaxNo(string companyCode, string faxNo);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByCustomerCode(string companyCode, string customerCode);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByCustomerName(string companyCode, string customerName);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByEmailId(string companyCode, string emaild);
        CustomerSiteLocationsResponse GetCustomerSiteLocationsByServiceEngineerCode(string companyCode, string serviceEngineerCode);

    }
}
