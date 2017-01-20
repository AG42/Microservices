using System.Collections.Generic;
using CustomerInformation.DataLayer.Entities.Datalake;

namespace CustomerInformation.DataLayer.Interfaces
{
    public interface IDatabaseContext
    {
        IEnumerable<Sl01> GetCustomers(string companyCode);
        Sl01 GetCustomerById(string companyCode, string customerCode);
        IEnumerable<Sl01> GetCustomerByName(string companyCode, string customerName);
        IEnumerable<Sl01> GetCustomerByAlternateName(string companyCode, string customerAlternateName);
        IEnumerable<Sl01> GetCustomerByEmailId(string companyCode, string customerEmailId);
        IEnumerable<Sl01> GetCustomerByCountryCode(string companyCode, string customerCountryCode);
        IEnumerable<Sl01> GetCustomerByPhoneNumber(string companyCode, string customerPhoneNumber);
        IEnumerable<Sl01> GetCustomerByCategory(string companyCode, string customerCategory);
    }
}
