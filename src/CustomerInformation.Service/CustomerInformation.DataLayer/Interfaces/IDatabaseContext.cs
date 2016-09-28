using System.Collections.Generic;
using CustomerInformation.DataLayer.Entities.Datalake;

namespace CustomerInformation.DataLayer.Interfaces
{
    public interface IDatabaseContext
    {
        IEnumerable<Sl01> GetCustomers(string companyCode);
        Sl01 GetCustomerById(string companyCode, string customerCode);
        IEnumerable<Sl01> GetCustomerByName(string companyCode, string customerName);
    }
}
