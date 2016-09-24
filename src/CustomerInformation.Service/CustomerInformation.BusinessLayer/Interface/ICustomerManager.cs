using CustomerInformation.Model;
using CustomerInformation.Model.Requests;
using CustomerInformation.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInformation.BusinessLayer.Interface
{
    public interface ICustomerManager
    {
        CustomerSearchByCompanyCodeResponse GetCustomers(string companyCode);
        CustomerSearchByIdResponse GetCustomerById(string companyCode, string customerCode);
        CustomerSearchByNameResponse GetCustomerByName(string companyCode, string customerName);
    }
}
