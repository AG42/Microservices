using CustomerSalesLedger.BusinessLayer.Interfaces;
using CustomerSalesLedger.Common;
using CustomerSalesLedger.Common.Error;
using CustomerSalesLedger.Common.Logger;
using CustomerSalesLedger.DataLayer.Interfaces;
using CustomerSalesLedger.Model.Response;
using System.Linq;

namespace CustomerSalesLedger.BusinessLayer
{
    public class CustomerSalesLedgerManager: ICustomerSalesLedgerManager
    {
        private readonly IDatabaseContext _databaseContext;

        public CustomerSalesLedgerManager(IDatabaseContext databaseContext)
        {
            // create ICustomer instance -Data Layer
            _databaseContext = databaseContext;
        }

        public CustomerSalesLedgerByCustomerCodeResponse GetCustomerSalesLedgerByCustomerCode(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerById :: Custome Input: companyCode: [{companyCode}] And customerCode: [{customerCode}]");
            var response = new CustomerSalesLedgerByCustomerCodeResponse();
            var result = _databaseContext.GetCustomerById(companyCode, customerCode);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            response.CustomerSalesLedger = Converter.Convert(result, companyCode);
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");
            return response;
        }

        public CustomerSalesLedgerByCustomerNameResponse GetCustomerSalesLedgerByCustomerName(string companyCode, string customerName)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByName :: Custome Input: companyCode: [{companyCode}] And customerName: [{customerName}]");
            var response = new CustomerSalesLedgerByCustomerNameResponse();
            var result = _databaseContext.GetCustomerByName(companyCode, customerName);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count()}]");
            response.CustomerSalesLedger.AddRange(Converter.Convert(result, companyCode));

            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSalesLedgerByEmailIdResponse GetCustomerSalesLedgerByEmailId(string companyCode, string emailId)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByEmailId :: Custome Input: companyCode: [{companyCode}] And emailId: [{emailId}]");
            var response = new CustomerSalesLedgerByEmailIdResponse();
            var result = _databaseContext.GetCustomerByEmailId(companyCode, emailId);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count()}]");
            response.CustomerSalesLedger.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSalesledgerByPhoneNoResponse GetCustomerSalesLedgerByPhoneNumber(string companyCode, string telephoneNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerSalesLedgerByTelephoneNumber :: Custome Input: companyCode: [{companyCode}] And telephone number: [{telephoneNumber}]");
            var response = new CustomerSalesledgerByPhoneNoResponse();
            var result = _databaseContext.GetCustomerByPhoneNumber(companyCode, telephoneNumber);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count()}]");
            response.CustomerSalesLedger.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSalesLedgerByCategoryResponse GetCustomerSalesLedgerByCategory(string companyCode, string category)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByCategory :: Customer Input: companyCode: {companyCode} And Category: {category}");
            var response = new CustomerSalesLedgerByCategoryResponse();
            var result = _databaseContext.GetCustomerByCategory(companyCode, category);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: {result.Count()}");
            response.CustomerSalesLedger.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }

        public CustomerSalesLedgerByAlternateNameResponse GetCustomerSalesLedgerByAlternateName(string companyCode, string customerAlternateName)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetCustomerByCustomerAlternateName :: Customer Input: companyCode: {companyCode} And CustomerAlternateName: {customerAlternateName}");
            var response = new CustomerSalesLedgerByAlternateNameResponse();
            var result = _databaseContext.GetCustomerByAlternateName(companyCode, customerAlternateName);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: {result.Count()}");
            response.CustomerSalesLedger.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger("Data to Business Model conversion successful");

            return response;
        }
    }
}
