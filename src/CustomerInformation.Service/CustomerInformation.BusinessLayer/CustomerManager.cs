using CustomerInformation.Model;
using System.Collections.Generic;
using CustomerInformation.BusinessLayer.Interface;
using CustomerInformation.DataLayer.Interfaces;

namespace CustomerInformation.BusinessLayer
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IDataLayerContext _dataLayerContext = null;//= new DataLayerContext();

        public CustomerManager(IDataLayerContext dataLayerContext)
        {
            // create ICustomer instance -Data Layer
            _dataLayerContext = dataLayerContext;
        }

        /// <summary>
        /// Get all customer by company code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public List<CustomerInformationModel> GetCustomers(string companyCode)
        {
            if (!InputValidation.ValidateCompanyCode(companyCode))
            {
                var result = _dataLayerContext.GetCustomers(companyCode);
                return Converter.Convert(result);
            }

            return null;
        }

        /// <summary>
        /// Get customer by customer code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public CustomerInformationModel GetCustomerById(string companyCode, string customerCode)
        {
            if (!InputValidation.ValidateCompanyCode(companyCode) && !InputValidation.ValidateCustomerCode(customerCode))
            {
                var result = _dataLayerContext.GetCustomerById(companyCode, customerCode);
                return Converter.Convert(result);
            }

            return null;
        }

        public List<CustomerInformationModel> GetCustomerByName(string companyCode, string customerName)
        {
            if (!string.IsNullOrEmpty(companyCode) && !string.IsNullOrEmpty(customerName))
            {
                var result = _dataLayerContext.GetCustomerByName(companyCode, customerName);
                return Converter.Convert(result);
            }

            return null;
        }
    }
}
