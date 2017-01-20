using System;
using System.Globalization;
using CustomerInformation.BusinessLayer.Interface;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using CustomerInformation.Common.Enum;
using CustomerInformation.Common.Logger;
using CustomerInformation.API.Attributes;
using System.Linq;
using CustomerInformation.Common;
using CustomerInformation.Model;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using CustomerInformation.API.ModelBinders;
using CustomerInformation.Common.Error;

namespace CustomerInformation.API.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : BaseContoller
    {
        readonly ICustomerManager _customerManager;
        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Customer Information Service...";
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("{companyCode}")]
        [Route("companyCode/{companyCode}")]
        public HttpResponseMessage GetCustomers(string companyCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");
            var response = _customerManager.GetCustomers(companyCode);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.Customers, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/customerCode/{customerCode}")]
        public HttpResponseMessage GetCustomerById(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerById :: Custome Input: companyCode: [{companyCode}] And customerCode: [{customerCode}]");

            var response = _customerManager.GetCustomerById(companyCode, customerCode);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Success :: And ItemLegth: 1");
                return Request.CreateResponse(HttpStatusCode.OK, response.CustomerInformationModel);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/customerName/{*customerName}")]
        public HttpResponseMessage GetCustomerByName(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string customerName)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerByName :: Custome Input: companyCode: {companyCode} And customerName: {customerName}");
            var response = _customerManager.GetCustomerByName(companyCode, customerName);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.Customers, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="emailId"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/emailId/{emailId}")]
        public HttpResponseMessage GetCustomerByEmailId(string companyCode, string emailId)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerByEmailId :: Custome Input: companyCode: {companyCode} And EmailId: {emailId}");

            var response = _customerManager.GetCustomerByEmailId(companyCode, emailId);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.Customers, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="alternateName"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/alternateName/{*alternateName}")]
        public HttpResponseMessage GetCustomerByAlternateName(string companyCode,[ModelBinder(typeof(SlashInValueBinder))] string alternateName)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerByAlternateName :: Custome Input: companyCode: {companyCode} And alternateName: {alternateName}");

            var response = _customerManager.GetCustomerByCustomerAlternateName(companyCode, alternateName);

            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.Customers, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/phoneNumber/{*phoneNumber}")]
        public HttpResponseMessage GetCustomerByPhoneNumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string phoneNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerByPhoneNumber :: Customer Input: companyCode: {companyCode} And alternateName: {phoneNumber}");

            var response = _customerManager.GetCustomerByPhoneNumber(companyCode, phoneNumber);

            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.Customers, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/category/{category}")]
        public HttpResponseMessage GetCustomerByCategory(string companyCode, string category)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerByCategory :: Customer Input: companyCode: {companyCode} And category: {category}");
            var response = _customerManager.GetCustomerByCategory(companyCode, category);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.Customers, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/countryCode/{countryCode}")]
        public HttpResponseMessage GetCustomerByCountryCode(string companyCode, string countryCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerByCategory :: Customer Input: companyCode: [{companyCode}] And countryCode: [{countryCode}]");

            var response = _customerManager.GetCustomerByCountryCode(companyCode, countryCode);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.Customers, response.ErrorInfo);

        }

        #region Private Method

        private HttpResponseMessage ValidateResponseStatusAndReturnObjectWithValidMessage(ResponseStatus status, List<CustomerInformationModel> customers, List<ErrorInfo> errorInfo)
        {
            if (status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger($"Response Status: Success :: And ItemLegth: {customers.Count}");

                if (customers.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, customers);
                }

                errorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return Request.CreateResponse(HttpStatusCode.NotFound, errorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.NotFound, errorInfo);
        }

        #endregion
    }
}
