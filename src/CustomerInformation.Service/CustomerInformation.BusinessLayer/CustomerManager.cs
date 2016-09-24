using CustomerInformation.BusinessLayer.Interface;
using CustomerInformation.DataLayer.Interfaces;
using CustomerInformation.Model.Response;
using CustomerInformation.Common.Error;
using CustomerInformation.Common;
using CustomerInformation.Common.Logger;

namespace CustomerInformation.BusinessLayer
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IDataLayerContext _dataLayerContext;

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
        public CustomerSearchByCompanyCodeResponse GetCustomers(string companyCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomers :: Custome Input: companyCode: [{companyCode}]");
            var response = new CustomerSearchByCompanyCodeResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode Status: Success");
                var result = _dataLayerContext.GetCustomers(companyCode);
                if (result == null)
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }

                ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count}]");
                response.Customers.AddRange(Converter.Convert(result, companyCode));
                ApplicationLogger.InfoLogger($"Data to Business Model conversion successfull");
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode Status: Failed");
            return response;
        }

        /// <summary>
        /// Get customer by customer code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public CustomerSearchByIdResponse GetCustomerById(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerById :: Custome Input: companyCode: [{companyCode}] And customerCode: [{customerCode}]");
            var response = new CustomerSearchByIdResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateCustomerCode(customerCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and CustomerCode Status: Success");
                var result = _dataLayerContext.GetCustomerById(companyCode, customerCode);
                if (result == null)
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }
                                
                response.CustomerInformationModel = Converter.Convert(result,companyCode);
                ApplicationLogger.InfoLogger($"Data to Business Model conversion successfull");
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and CustomerCode Status: Failed");
            return response;
        }

        public CustomerSearchByNameResponse GetCustomerByName(string companyCode, string customerName)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByName :: Custome Input: companyCode: [{companyCode}] And customerName: [{customerName}]");
            var response = new CustomerSearchByNameResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateCustomerName(customerName, response))
            {
                var result = _dataLayerContext.GetCustomerByName(companyCode, customerName);
                if (result == null)
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }

                ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count}]");
                response.Customers.AddRange(Converter.Convert(result, companyCode));
                
                ApplicationLogger.InfoLogger($"Data to Business Model conversion successfull");
            }

            return response;
        }
    }
}
