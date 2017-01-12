using CustomerSiteLocation.DataLayer.Entities.Datalake;
using System.Collections.Generic;


//using ProductInventory.DataLayer.Entities;

namespace CustomerSiteLocation.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        IEnumerable<Sy80> GetCustomerSiteLocationsByCompanyCode(string companyCode, string parentCompanyCode = "");
        IEnumerable<Sy80> GetCustomerSiteLocationsByCountry(string companyCode, string country);
        IEnumerable<Sy80> GetCustomerSiteLocationsByZip(string companyCode, string zipCode);
        IEnumerable<Sy80> GetCustomerSiteLocationsByCustomerCode(string companyCode, string customerCode);
        IEnumerable<Sy80> GetCustomerSiteLocationsByCustomerName(string companyCode, string customerName);
        IEnumerable<Sy80> GetCustomerSiteLocationsBySiteNo(string companyCode, string siteNo);
        IEnumerable<Sy80> GetCustomerSiteLocationsByAddress(string companyCode, string address);

        IEnumerable<Sy80> GetCustomerSiteLocationsByTelephoneNo(string companyCode, string telePhone);
        IEnumerable<Sy80> GetCustomerSiteLocationsByFaxNo(string companyCode, string faxNo);
        IEnumerable<Sy80> GetCustomerSiteLocationsByEmailId(string companyCode, string emailId);

        IEnumerable<Sy80> GetCustomerSiteLocationsByServiceEngineerCode(string companyCode, string serviceEngineerCode);
    }
}
