using CustomerInformation.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInformation.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        List<CustomerMaster> GetCustomers(string companyCode);
        CustomerMaster GetCustomerById(string companyCode, string customerCode);
        List<CustomerMaster> GetCustomerByName(string companyCode, string customerName);
    }
}
