using System;
using System.Globalization;
using CustomerInformation.BusinessLayer.Interface;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using CustomerInformation.Common.Enum;
using CustomerInformation.Common.Logger;

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
        [Route("{companyCode}")]
        [Route("companyCode/{companyCode}")]
        public HttpResponseMessage GetCustomers(string companyCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");
            var response = _customerManager.GetCustomers(companyCode);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger($"Response Status: Success :: ItemLegth: [{response.Customers.Count}]");
                return Request.CreateResponse(HttpStatusCode.OK, response.Customers);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
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
        [Route("companyCode/{companyCode}/customerName/{customerName}")]
        public HttpResponseMessage GetCustomerByName(string companyCode, string customerName)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerByName :: Custome Input: companyCode: [{companyCode}] And customerName: [{customerName}]");

            var response = _customerManager.GetCustomerByName(companyCode, customerName);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger($"Response Status: Success :: And ItemLegth: [{response.Customers.Count}]");
                return Request.CreateResponse(HttpStatusCode.OK, response.Customers);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }
    }
}
