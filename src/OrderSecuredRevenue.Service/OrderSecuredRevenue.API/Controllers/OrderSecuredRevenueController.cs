using OrderSecuredRevenue.API.Attributes;
using OrderSecuredRevenue.BusinessLayer;
using OrderSecuredRevenue.BusinessLayer.Interface;
using OrderSecuredRevenue.Common;
using OrderSecuredRevenue.Common.Enum;
using OrderSecuredRevenue.Common.Error;
using OrderSecuredRevenue.Common.Logger;
using OrderSecuredRevenue.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace OrderSecuredRevenue.API.Controllers
{
    [RoutePrefix("api/orderSecuredRevenue")]
    public class OrderSecuredRevenueController : BaseContoller
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
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.OrderSecuredRevenueModels, response.ErrorInfo);
        }

        /// <summary>
        /// Get order secured revenue by invoice number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        //[InputValidator]
        //[Route("companyCode/{companyCode}/invoiceNumber/{invoiceNumber}")]
        //public HttpResponseMessage GetOrderSecuredRevenueByInvoiceNumber(string companyCode, string invoiceNumber)
        //{
        //    ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: OrderSecuredRevenueControllerMethodName: GetOrderSecuredRevenueByInvoiceNumber :: Custome Input: companyCode: {companyCode} And invoiceNumber: [{invoiceNumber}");

        //    var response = _orderSecuredRevenueManager.GetOrderSecuredRevenueByInvoiceNumber(companyCode, invoiceNumber);
        //    return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.OrderSecuredRevenueModels, response.ErrorInfo);
        //}

        #region Private Method

        private HttpResponseMessage ValidateResponseStatusAndReturnObjectWithValidMessage(ResponseStatus status, List<OrderSecuredRevenueModel> orderSecuredRevenueModel, List<ErrorInfo> errorInfo)
        {
            if (status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger($"Response Status: Success");

                if (orderSecuredRevenueModel.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, orderSecuredRevenueModel);
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
