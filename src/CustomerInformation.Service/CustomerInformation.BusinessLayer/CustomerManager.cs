using System.Linq;
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
        private readonly IDatabaseContext _databaseContext;

        public CustomerManager(IDatabaseContext databaseContext)
        {
            // create ICustomer instance -Data Layer
            _databaseContext = databaseContext;
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
            var result = _databaseContext.GetCustomers(companyCode);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count()}]");
            response.Customers.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successfull");

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
            var result = _databaseContext.GetCustomerById(companyCode, customerCode);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            response.CustomerInformationModel = Converter.Convert(result, companyCode);
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");
            return response;
        }

        public CustomerSearchByNameResponse GetCustomerByName(string companyCode, string customerName)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByName :: Custome Input: companyCode: [{companyCode}] And customerName: [{customerName}]");
            var response = new CustomerSearchByNameResponse();
            var result = _databaseContext.GetCustomerByName(companyCode, customerName);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count()}]");
            response.Customers.AddRange(Converter.Convert(result, companyCode));

            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSearchByEmailIdResponse GetCustomerByEmailId(string companyCode, string emailId)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByEmailId :: Custome Input: companyCode: [{companyCode}] And emailId: [{emailId}]");
            var response = new CustomerSearchByEmailIdResponse();
            var result = _databaseContext.GetCustomerByEmailId(companyCode, emailId);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count()}]");
            response.Customers.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSearchByPhoneNumberResponse GetCustomerByPhoneNumber(string companyCode, string phoneNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByPhoneNumber :: Custome Input: companyCode: {companyCode} And phone number: [{phoneNumber}]");
            var response = new CustomerSearchByPhoneNumberResponse();
            var result = _databaseContext.GetCustomerByPhoneNumber(companyCode, phoneNumber);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count()}]");
            response.Customers.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSearchByCategoryResponse GetCustomerByCategory(string companyCode, string category)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByCategory :: Customer Input: companyCode: {companyCode} And Category: {category}");
            var response = new CustomerSearchByCategoryResponse();
            var result = _databaseContext.GetCustomerByCategory(companyCode, category);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: {result.Count()}");
            response.Customers.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSearchByCustomerAlternateNameResponse GetCustomerByCustomerAlternateName(string companyCode, string customerAlternateName)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByCustomerAlternateName :: Customer Input: companyCode: {companyCode} And CustomerAlternateName: {customerAlternateName}");
            var response = new CustomerSearchByCustomerAlternateNameResponse();
            var result = _databaseContext.GetCustomerByAlternateName(companyCode, customerAlternateName);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: {result.Count()}");
            response.Customers.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSearchByCountryCodeResponse GetCustomerByCountryCode(string companyCode, string countryCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByCountryCode :: Customer Input: companyCode: {companyCode} And CountryCode: {countryCode}");
            var response = new CustomerSearchByCountryCodeResponse();
            var result = _databaseContext.GetCustomerByCountryCode(companyCode, countryCode);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: {result.Count()}");
            response.Customers.AddRange(Converter.Convert(result, companyCode));

            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }
    }
}
