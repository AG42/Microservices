using System;
using System.Linq;
using CustomerSiteLocation.BusinessLayer.Interface;
using CustomerSiteLocation.Common;
using CustomerSiteLocation.Common.Error;
using CustomerSiteLocation.DataLayer.Interfaces;
using CustomerSiteLocation.Model.Response;
using CustomerSiteLocation.Common.Logger;

namespace CustomerSiteLocation.BusinessLayer
{
    public class CustomerSiteLocationManager : ICustomerSiteLocationManager
    {
        private readonly IDataLayerContext _dataLayerContext;
        public CustomerSiteLocationManager(IDataLayerContext dataLayerContext)
        {
            _dataLayerContext = dataLayerContext;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="faxNo"></param>
        /// <returns></returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByFaxNo(string companyCode, string faxNo)
        {

            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByFaxNo :: Custome Input: CompanyCode: [{companyCode}] And FaxNo: [{faxNo}]");
            var response = new CustomerSiteLocationsResponse();

            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateFaxNo(faxNo, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and FaxNo Status: Success");

                // Get Item from Master

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByFaxNo(companyCode, faxNo);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations = Converter.ConvertToCustomerSiteLocationsModel(
                        customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and FaxNo Status: Failed");
            return response;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="telePhone"></param>
        /// <returns>List of customer site location</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByTelephoneNo(string companyCode, string telePhone)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByTelephoneNo :: Custome Input: CompanyCode: [{companyCode}] And TelephoneNo: [{telePhone}]");
            var response = new CustomerSiteLocationsResponse();

            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateTelephoneNo(telePhone, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and TelephoneNo Status: Success");

                // Get Item from Master

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByTelephoneNo(companyCode,
                    telePhone);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations = Converter.ConvertToCustomerSiteLocationsModel(
                        customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }


                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and telePhone Status: Failed");
            return response;

        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="address"></param>
        /// <returns>List of customer site location</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByAddress(string companyCode, string address)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByAddress :: Custome Input: CompanyCode: [{companyCode}] And address: [{address}]");
            var response = new CustomerSiteLocationsResponse();

            if (!InputValidation.ValidateCompanyCode(companyCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and address Status: Success");

                // Get Item from Master

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByAddress(companyCode, address);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations = Converter.ConvertToCustomerSiteLocationsModel(
                        customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;
            }
            return response;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>List of customer site location</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByCompanyCode(string companyCode,
            string parentCompanyCode = "")
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByCompanyCode :: Custome Input: companyCode: [{companyCode}]");
            var response = new CustomerSiteLocationsResponse();

            if (!InputValidation.ValidateCompanyCode(companyCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode Status: Success");

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByCompanyCode(companyCode);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations = Converter.ConvertToCustomerSiteLocationsModel(
                        customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode Status: Failed");
            return response;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerName"></param>
        /// <returns>List of customer site locations</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByCustomerName(string companyCode,
            string customerName)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByCustomerName :: Custome Input: companyCode: [{companyCode}] And customerName: [{customerName}]");
            var response = new CustomerSiteLocationsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateCustomerName(customerName, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and CustomerName Status: Success");

                // Get Item from Master

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByCustomerName(companyCode,
                    customerName);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations =
                        Converter.ConvertToCustomerSiteLocationsModel(customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;
            }
            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and CustomerName Status: Failed");
            return response;
        }

        /// <summary>
        /// Get Customer Site Locations By Country
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByCountry(string companyCode, string country)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByCountry :: Custome Input: companyCode: [{companyCode}] And country: [{country}]");
            var response = new CustomerSiteLocationsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateCountry(country, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and Country Status: Success");

                // Get Item from Master
                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByCountry(companyCode, country);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations =
                        Converter.ConvertToCustomerSiteLocationsModel(customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;
            }
            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and Country Status: Failed");
            return response;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="siteNo"></param>
        /// <returns>List of customer site location</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsBySiteNo(string companyCode, string siteNo)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsBySiteNo :: Custome Input: companyCode: [{companyCode}] And SiteNo: [{siteNo}]");
            var response = new CustomerSiteLocationsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateSiteNo(siteNo, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and SiteNo Status: Success");

                // Get Item from Master
                    var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsBySiteNo(companyCode, siteNo);
                    if (customerSiteLocations != null && customerSiteLocations.Any())
                    {
                        //response.CustomerSiteLocations = new List<CustomerSiteLocationModel>();
                        response.CustomerSiteLocations =Converter.ConvertToCustomerSiteLocationsModel(customerSiteLocations, companyCode);
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }
                    return response;
            }
            return response;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="zipCode"></param>
        /// <returns>List of customer site location</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByZip(string companyCode, string zipCode)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByZip :: Custome Input: companyCode: [{companyCode}] And zip: [{zipCode}]");
            var response = new CustomerSiteLocationsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateZip(zipCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and Zip Status: Success");

                // Get Item from Master

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByZip(companyCode, zipCode);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations =
                        Converter.ConvertToCustomerSiteLocationsModel(customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;
            }
            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and Zip Status: Failed");
            return response;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns>List of customer site locations</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByCustomerCode(string companyCode,
            string customerCode)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByCustomerCode :: Custome Input: companyCode: [{companyCode}] And customerCode: [{customerCode}]");
            var response = new CustomerSiteLocationsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateCustomerCode(customerCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and customerCode Status: Success");

                // Get Item from Master

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByCustomerCode(companyCode,
                    customerCode);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations =
                        Converter.ConvertToCustomerSiteLocationsModel(customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;

            }
            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and CustomerCode Status: Failed");
            return response;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="emailId"></param>
        /// <returns>List of customer site location</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByEmailId(string companyCode, string emailId)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByEmailId :: Custome Input: CompanyCode: [{companyCode}] And EmailId: [{emailId}]");
            var response = new CustomerSiteLocationsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateEmailId(emailId, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and EmailId Status: Success");

                // Get Item from Master

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByEmailId(companyCode, emailId);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations =
                        Converter.ConvertToCustomerSiteLocationsModel(customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;
            }
            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and EmailId Status: Failed");
            return response;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="serviceEngineerCode"></param>
        /// <returns>List of customer site locations</returns>
        public CustomerSiteLocationsResponse GetCustomerSiteLocationsByServiceEngineerCode(string companyCode,
            string serviceEngineerCode)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetCustomerSiteLocationsByServiceEngineerCode :: Custome Input: CompanyCode: [{companyCode}] And ServiceEngineerCode: [{serviceEngineerCode}]");
            var response = new CustomerSiteLocationsResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateServiceEngineerCode(serviceEngineerCode, response))
            {
                ApplicationLogger.InfoLogger(
                    "InputValidation.Validate CompanyCode and ServiceEngineerCode Status: Success");

                // Get Item from Master

                var customerSiteLocations = _dataLayerContext.GetCustomerSiteLocationsByServiceEngineerCode(
                    companyCode, serviceEngineerCode);
                if (customerSiteLocations != null && customerSiteLocations.Any())
                {
                    response.CustomerSiteLocations =
                        Converter.ConvertToCustomerSiteLocationsModel(customerSiteLocations, companyCode);
                }
                else
                {
                    ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                }

                return response;
            }
            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ServiceEngineerCode Status: Failed");
            return response;
        }
    }
}
