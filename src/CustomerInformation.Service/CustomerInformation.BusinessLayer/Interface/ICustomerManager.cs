using CustomerInformation.Model;
using CustomerInformation.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInformation.BusinessLayer.Interface
{
    public interface ICustomerManager
    {
        List<CustomerInformationModel> GetCustomers(string companyCode);
        CustomerInformationModel GetCustomerById(string companyCode, string customerCode);
        List<CustomerInformationModel> GetCustomerByName(string companyCode, string customerName);
    }
}
