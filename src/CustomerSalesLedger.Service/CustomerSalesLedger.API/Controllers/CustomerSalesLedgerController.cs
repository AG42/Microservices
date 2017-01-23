using CustomerSalesLedger.API.Attributes;
using CustomerSalesLedger.API.ModelBinders;
using CustomerSalesLedger.BusinessLayer.Interfaces;
using CustomerSalesLedger.Common;
using CustomerSalesLedger.Common.Enums;
using CustomerSalesLedger.Common.Error;
using CustomerSalesLedger.Common.Logger;
using CustomerSalesLedger.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CustomerSalesLedger.API.Controllers
{    
    [RoutePrefix("api/customerSalesLedger")]
    public class CustomerSalesLedgerController : BaseController
    {
        readonly ICustomerSalesLedgerManager _customerSalesLedgerManager;
        public CustomerSalesLedgerController(ICustomerSalesLedgerManager customerSalesLedgerManager)
        {
            _customerSalesLedgerManager = customerSalesLedgerManager;
        }
        [Route("")]
        public string GetServiceName()
        {
            return "Customer Sales Ledger service is running....";
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/customerCode/{customerCode}")]
        public HttpResponseMessage GetCustomerSalesLedgerByCustomerCode(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerSalesLedgerControllerMethodName: GetCustomerSalesLedgerByCustomerCode :: Customer Input: companyCode: {companyCode} And customerCode: {customerCode}");

            var response = _customerSalesLedgerManager.GetCustomerSalesLedgerByCustomerCode(companyCode, customerCode);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Success :: And ItemLegth: 1");
                return Request.CreateResponse(HttpStatusCode.OK, response.CustomerSalesLedger);
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
        public HttpResponseMessage GetCustomerSalesLedgerByCustomerName(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string customerName)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerSalesLedgerControllerMethodName: GetCustomerSalesLedgerByCustomerName :: Customer Input: companyCode: {companyCode} And customerName: {customerName}");
            var response = _customerSalesLedgerManager.GetCustomerSalesLedgerByCustomerName(companyCode, customerName);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.CustomerSalesLedger, response.ErrorInfo);
        }/// <summary>
         /// Get all the customer base on filter criteria
         /// </summary>
         /// <param name="companyCode"></param>
         /// <param name="emailId"></param>
         /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/emailId/{emailId}")]
        public HttpResponseMessage GetCustomerSalesLedgerByEmailId(string companyCode, string emailId)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSalesLedgerByEmailId :: Custome Input: companyCode: {companyCode} And EmailId: {emailId}");

            var response = _customerSalesLedgerManager.GetCustomerSalesLedgerByEmailId(companyCode, emailId);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.CustomerSalesLedger, response.ErrorInfo);
        }
                
        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/phoneNumber/{*phoneNumber}")]
        public HttpResponseMessage GetCustomerSalesLedgerByPhoneNumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string phoneNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSalesLedgerByPhoneNumber :: Customer Input: companyCode: {companyCode} And alternateName: {phoneNumber}");

            var response = _customerSalesLedgerManager.GetCustomerSalesLedgerByPhoneNumber(companyCode, phoneNumber);

            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.CustomerSalesLedger, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/category/{category}")]
        public HttpResponseMessage GetCustomerSalesLedgerByCategory(string companyCode, string category)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSalesLedgerByCategory :: Customer Input: companyCode: {companyCode} And category: {category}");
            var response = _customerSalesLedgerManager.GetCustomerSalesLedgerByCategory(companyCode, category);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.CustomerSalesLedger, response.ErrorInfo);
        }

        /// <summary>
        /// Get all the customer base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="alternateName"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/alternateName/{*alternateName}")]
        public HttpResponseMessage GetCustomerSalesLedgerByAlternateName(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string alternateName)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSalesLedgerByAlternateName :: Custome Input: companyCode: {companyCode} And alternateName: {alternateName}");

            var response = _customerSalesLedgerManager.GetCustomerSalesLedgerByAlternateName(companyCode, alternateName);

            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.CustomerSalesLedger, response.ErrorInfo);
        }

        #region Private Method

        private HttpResponseMessage ValidateResponseStatusAndReturnObjectWithValidMessage(ResponseStatus status, List<CustomerSalesLedgerModel> customers, List<ErrorInfo> errorInfo)
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
