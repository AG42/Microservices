using OrderSecuredRevenue.API.Attributes;
using OrderSecuredRevenue.BusinessLayer.Interface;
using OrderSecuredRevenue.Common;
using OrderSecuredRevenue.Common.Enum;
using OrderSecuredRevenue.Common.Error;
using OrderSecuredRevenue.Common.Logger;
using OrderSecuredRevenue.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderSecuredRevenue.API.Controllers
{
    [RoutePrefix("api/orderSecuredRevenue")]
    public class OrderSecuredRevenueController : BaseController
    {
        readonly IOrderSecuredRevenueManager _orderSecuredRevenueManager;
        public OrderSecuredRevenueController(IOrderSecuredRevenueManager orderSecuredRevenueManager)
        {
            _orderSecuredRevenueManager = orderSecuredRevenueManager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Order Secured Revenue Service is running...";
        }

        /// <summary>
        /// Get order secured revenue by order number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("{companyCode}")]
        [Route("companyCode/{companyCode}/orderNumber/{orderNumber}")]
        public HttpResponseMessage GetOrderSecuredRevenueByOrderNumber(string companyCode, string orderNumber)

        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: OrderSecuredRevenueControllerMethodName: GetOrderSecuredRevenueByOrderNumber :: Custome Input: companyCode: {companyCode} And orderNumber: [{orderNumber}");
            var response = _orderSecuredRevenueManager.GetOrderSecuredRevenueByOrderNumber(companyCode, orderNumber);
            //return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.OrderSecuredRevenueModels, response.ErrorInfo);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.OrderSecuredRevenueModel, response.ErrorInfo);
        }

        ///// <summary>
        ///// Get order secured revenue by invoice number
        ///// </summary>
        ///// <param name="companyCode"></param>
        ///// <param name="invoiceNumber"></param>
        ///// <returns></returns>
        //[InputValidator]
        //[Route("companyCode/{companyCode}/invoiceNumber/{invoiceNumber}")]
        //public HttpResponseMessage GetOrderSecuredRevenueByInvoiceNumber(string companyCode, string invoiceNumber)
        //{
        //    ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: OrderSecuredRevenueControllerMethodName: GetOrderSecuredRevenueByInvoiceNumber :: Custome Input: companyCode: {companyCode} And invoiceNumber: [{invoiceNumber}");

        //    var response = _orderSecuredRevenueManager.GetOrderSecuredRevenueByInvoiceNumber(companyCode, invoiceNumber);
        //    return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.OrderSecuredRevenueModels, response.ErrorInfo);
        //}

        [InputValidator]
        [Route("orderDetails/companyCode/{companyCode}/orderNumber/{orderNumber}")]
        public HttpResponseMessage GetOrderDetailsByOrderNumber(string companyCode, string orderNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: OrderSecuredRevenueControllerMethodName: GetOrderDetailsByOrderNumber :: Custome Input: companyCode: {companyCode} And orderNumber: [{orderNumber}");

            var response = _orderSecuredRevenueManager.GetOrderDetailsByOrderNumber(companyCode, orderNumber);
            if (response.Status != ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Failure");
                return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Success");

            if (response.SalesOrderDetails != null)
                return Request.CreateResponse(HttpStatusCode.OK, response.SalesOrderDetails);

            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }

        [InputValidator]
        [Route("orderType/companyCode/{companyCode}/orderNumber/{orderNumber}")]
        public HttpResponseMessage GetOrderTypeByOrderNumber(string companyCode, string orderNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: OrderSecuredRevenueControllerMethodName: GetOrderTypeByOrderNumber :: Custome Input: companyCode: {companyCode} And orderNumber: [{orderNumber}");

            var response = _orderSecuredRevenueManager.GetOrderTypeByOrderNumber(companyCode, orderNumber);

            if (response.Status != ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Failure");
                return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Success");

            if (response.OrderType != null)
                return Request.CreateResponse(HttpStatusCode.OK, response.OrderType);

            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }

        [InputValidator]
        [Route("orderDeliveryDate/companyCode/{companyCode}/orderNumber/{orderNumber}")]
        public HttpResponseMessage GetOrderDeliveryDateByOrderNumber(string companyCode, string orderNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: OrderSecuredRevenueControllerMethodName: GetOrderDeliveryDateByOrderNumber :: Custome Input: companyCode: {companyCode} And orderNumber: [{orderNumber}");

            var response = _orderSecuredRevenueManager.GetOrderDeliveryDateByOrderNumber(companyCode, orderNumber);

            if (response.Status != ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Failure");
                return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Success");

            if (response.DeliveryDate != null)
                return Request.CreateResponse(HttpStatusCode.OK, response.DeliveryDate);

            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            return Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo);
        }

        #region Private Method

        //private HttpResponseMessage ValidateResponseStatusAndReturnObjectWithValidMessage(ResponseStatus status, List<OrderSecuredRevenueModel> orderSecuredRevenueModel, List<ErrorInfo> errorInfo)
        private HttpResponseMessage ValidateResponseStatusAndReturnObjectWithValidMessage(ResponseStatus status, OrderSecuredRevenueModel orderSecuredRevenueDetails, List<ErrorInfo> errorInfo)
        {
            if (status != ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Failure");
                return Request.CreateResponse(HttpStatusCode.NotFound, errorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Success");

            if (orderSecuredRevenueDetails != null)
                return Request.CreateResponse(HttpStatusCode.OK, orderSecuredRevenueDetails);

            errorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            return Request.CreateResponse(HttpStatusCode.NotFound, errorInfo);
        }

        #endregion
    }
}
