using CustomerSalesLedger.DataLayer.Entities.Datalake;
using System.Collections.Generic;

namespace CustomerSalesLedger.DataLayer.Interfaces
{
    public interface IDatabaseContext
    {
        Sl01 GetCustomerById(string companyCode, string customerCode);
        IEnumerable<Sl01> GetCustomerByName(string companyCode, string customerName);
        IEnumerable<Sl01> GetCustomerByCategory(string companyCode, string customerCategory);
        IEnumerable<Sl01> GetCustomerByPhoneNumber(string companyCode, string customerPhoneNumber);
        IEnumerable<Sl01> GetCustomerByAlternateName(string companyCode, string customerAlternateName);
        IEnumerable<Sl01> GetCustomerByEmailId(string companyCode, string customerEmailId);
    }
}   
