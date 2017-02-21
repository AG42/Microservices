using ServiceContractManagement.API.Attributes;
using ServiceContractManagement.BusinessLayer.Interface;
using ServiceContractManagement.Common;
using ServiceContractManagement.Common.Enums;
using ServiceContractManagement.Common.Error;
using ServiceContractManagement.Common.Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceContractManagement.API.Controllers
{    
    [RoutePrefix("api/serviceContract")]
    public class ServiceContractController : BaseController
    {
        readonly IServiceContractManager _serviceContractManager;
        public ServiceContractController(IServiceContractManager serviceContractManager)
        {
            _serviceContractManager = serviceContractManager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Service Contract Management service is running....";
        }

        /// <summary>
        /// Get service contract details by contract code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="contractNumber"></param>        
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/contractNumber/{contractNumber}")]
        public HttpResponseMessage GetServiceContractDetailsByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: ServiceContractControllerMethodName: GetServiceContractDetailsBycontractNumber:: Customer Input: companyCode: {companyCode} And contractNumber: {contractNumber} ");

            var response = _serviceContractManager.GetServiceContractDetailsByContractNumber(companyCode, contractNumber);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Success");

                if (response.ServiceContractDetails != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response.ServiceContractDetails);
                }

                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }

        /// <summary>
        /// Get service contract details by start date and end date
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/startDate/{startDate}/endDate/{endDate}")]
        public HttpResponseMessage GetServiceContractDetailsByContractDate(string companyCode, string startDate, string endDate)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: ServiceContractControllerMethodName: GetServiceContractDetailsByContractDate:: Customer Input: companyCode: {companyCode} And startDate: {startDate} And endDate: {endDate}");

            var response = _serviceContractManager.GetServiceContractDetailsByContractDate(companyCode, startDate, endDate);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Success");

                if (response.ServiceContractDetails.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response.ServiceContractDetails);
                }

                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }

        [InputValidator]
        [Route("type/companyCode/{companyCode}/contractNumber/{contractNumber}")]
        public HttpResponseMessage GetServiceContractTypeByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: ServiceContractControllerMethodName: GetServiceContractTypeBycontractNumber:: Customer Input: companyCode: {companyCode} And contractNumber: {contractNumber} ");

            var response = _serviceContractManager.GetServiceContractTypeByContractNumber(companyCode, contractNumber);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.ContractType, response.ErrorInfo);
        }
               
        [InputValidator]
        [Route("unitPrice/companyCode/{companyCode}/contractNumber/{contractNumber}")]
        public HttpResponseMessage GetServiceContractUnitPriceByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: ServiceContractControllerMethodName: GetServiceContractUnitPriceBycontractNumber:: Customer Input: companyCode: {companyCode} And contractNumber: {contractNumber} ");

            var response = _serviceContractManager.GetServiceContractUnitPriceByContractNumber(companyCode, contractNumber);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Success");

                if (response.ServiceContractLineItemUnitPrice.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response.ServiceContractLineItemUnitPrice);
                }

                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }

        [InputValidator]
        [Route("quantity/companyCode/{companyCode}/contractNumber/{contractNumber}")]
        public HttpResponseMessage GetServiceContractInvoiceQuantityByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: ServiceContractControllerMethodName: GetServiceContractInvoiceQuantityBycontractNumber:: Customer Input: companyCode: {companyCode} And contractNumber: {contractNumber} ");

            var response = _serviceContractManager.GetServiceContractInvoiceQuantityByContractNumber(companyCode, contractNumber);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Success");

                if (response.ServiceContractLineItemQuantity.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response.ServiceContractLineItemQuantity);
                }

                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }


        [InputValidator]
        [Route("debitCreditValue/companyCode/{companyCode}/contractNumber/{contractNumber}")]
        public HttpResponseMessage GetServiceContractDebitCreditValueByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: ServiceContractControllerMethodName: GetServiceContractDebitCreditValueBycontractNumber:: Customer Input: companyCode: {companyCode} And contractNumber: {contractNumber} ");

            var response = _serviceContractManager.GetServiceContractDebitCreditValueByContractNumber(companyCode, contractNumber);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Success");

                if (response.ServiceContractLineItemDebitCreditValue.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response.ServiceContractLineItemDebitCreditValue);
                }

                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Failure");
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }

        [InputValidator]
        [Route("paymentTerms/companyCode/{companyCode}/contractNumber/{contractNumber}")]
        public HttpResponseMessage GetServiceContractPaymentTermsByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: ServiceContractControllerMethodName: GetServiceContractPaymentTermsBycontractNumber:: Customer Input: companyCode: {companyCode} And contractNumber: {contractNumber} ");

            var response = _serviceContractManager.GetServiceContractPaymentTermsByContractNumber(companyCode, contractNumber);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.PaymentTerms, response.ErrorInfo);
        }

        [InputValidator]
        [Route("contractValue/companyCode/{companyCode}/contractNumber/{contractNumber}")]
        public HttpResponseMessage GetServiceContractValueByContractNumber(string companyCode, string contractNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: [{ Request.RequestUri}] :: ServiceContractControllerMethodName: GetServiceContractValueBycontractNumber:: Customer Input: companyCode: {companyCode} And contractNumber: {contractNumber} ");

            var response = _serviceContractManager.GetServiceContractValueByContractNumber(companyCode, contractNumber);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.ContractValue, response.ErrorInfo);
        }

        #region Private Method

        private HttpResponseMessage ValidateResponseStatusAndReturnObjectWithValidMessage(ResponseStatus status, string result, List<ErrorInfo> errorInfo)
        {
            if (status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger($"Response Status: Success :: result: {result}");

                if (!string.IsNullOrWhiteSpace(result))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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
