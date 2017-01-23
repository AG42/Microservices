using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesLedgerInvoicing.API.Attributes;
using SalesLedgerInvoicing.BusinessLayer.Interface;
using SalesLedgerInvoicing.Common;
using SalesLedgerInvoicing.Common.Enums;
using SalesLedgerInvoicing.Common.Error;
using SalesLedgerInvoicing.Common.Logger;
using SalesLedgerInvoicing.Model;

namespace SalesLedgerInvoicing.API.Controllers
{
    [RoutePrefix("api/salesLedgerInvoicing")]
    public class SalesLedgerInvoicingController : BaseContoller
    {
        readonly ISalesLedgerInvoicingManager _salesLedgerInvoicingManager;
        public SalesLedgerInvoicingController(ISalesLedgerInvoicingManager salesLedgerInvoicingManager)
        {
            _salesLedgerInvoicingManager = salesLedgerInvoicingManager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Sales Ledger Invoicing Service is running...";
        }

        /// <summary>
        /// Get sales ledger invoicing details by customer code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/customerCode/{customerCode}")]
        public HttpResponseMessage GetSalesLedgerInvoicingByCustomerCode(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: SalesLedgerInvoicingController: GetSalesLedgerInvoicingByCustomerCode :: Custome Input: companyCode: {companyCode} And customerCode: {customerCode}");
            var response = _salesLedgerInvoicingManager.GetSalesLedgerInvoicingByCustomerCode(companyCode, customerCode);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.SalesLedgerInvoicingModelList, response.ErrorInfo);
        }

        /// <summary>
        /// Get sales ledger invoicing details by customer name
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/customerName/{customerName}")]
        public HttpResponseMessage GetSalesLedgerInvoicingByCustomerName(string companyCode, string customerName)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: SalesLedgerInvoicingController: GetSalesLedgerInvoicingByCustomerName :: Custome Input: companyCode: {companyCode} And customerName: {customerName}");
            var response = _salesLedgerInvoicingManager.GetSalesLedgerInvoicingByCustomerName(companyCode, customerName);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.SalesLedgerInvoicingModelList, response.ErrorInfo);
        }

        /// <summary>
        /// Get sales ledger invoicing details by order number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/orderNumber/{orderNumber}")]
        public HttpResponseMessage GetSalesLedgerInvoicingByOrderNumber(string companyCode, string orderNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: SalesLedgerInvoicingController: GetSalesLedgerInvoicingByOrderNumber :: Custome Input: companyCode: {companyCode} And orderNumber: [{orderNumber}");
            var response = _salesLedgerInvoicingManager.GetSalesLedgerInvoicingByOrderNumber(companyCode, orderNumber);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.SalesLedgerInvoicingModelList, response.ErrorInfo);
        }

        /// <summary>
        /// Get sales ledger invoicing details by invoice Number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/invoiceNumber/{invoiceNumber}")]
        public HttpResponseMessage GetSalesLedgerInvoicingByInvoiceNumber(string companyCode, string invoiceNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: SalesLedgerInvoicingController: GetSalesLedgerInvoicingByCustomerName :: Custome Input: companyCode: {companyCode} And invoiceNumber: {invoiceNumber}");
            var response = _salesLedgerInvoicingManager.GetSalesLedgerInvoicingByInvoiceNumber(companyCode, invoiceNumber);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.SalesLedgerInvoicingModelList, response.ErrorInfo);
        }

        /// <summary>
        /// Get Sales ledger invoicing details by invoice from and to date
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="invoiceFromDate"></param>
        /// <param name="invoiceToDate"></param>
        /// <returns></returns>
        [InputValidator]
        [Route("companyCode/{companyCode}/invoiceFrom/{invoiceFromDate}/invoiceTo/{invoiceToDate}")]
        public HttpResponseMessage GetSalesLedgerInvoicingByInvoiceDate(string companyCode, string invoiceFromDate, string invoiceToDate)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: SalesLedgerInvoicingController: GetSalesLedgerInvoicingByCustomerName :: Custome Input: companyCode: {companyCode} And fromDate: {invoiceFromDate}, endDate: {invoiceToDate}");
            var response = _salesLedgerInvoicingManager.GetSalesLedgerInvoicingByInvoiceDate(companyCode, invoiceFromDate, invoiceToDate);
            return ValidateResponseStatusAndReturnObjectWithValidMessage(response.Status, response.SalesLedgerInvoicingModelList, response.ErrorInfo);
        }

        #region Private Method

        private HttpResponseMessage ValidateResponseStatusAndReturnObjectWithValidMessage(ResponseStatus status, List<SalesLedgerInvoicesModel> salesLedgerInvoicesModel, List<ErrorInfo> errorInfo)
        {
            if (status != ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger("Response Status: Failure");
                return Request.CreateResponse(HttpStatusCode.NotFound, errorInfo);
            }

            ApplicationLogger.InfoLogger("Response Status: Success");

            if (salesLedgerInvoicesModel != null)
                return Request.CreateResponse(HttpStatusCode.OK, salesLedgerInvoicesModel);

            errorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            return Request.CreateResponse(HttpStatusCode.NotFound, errorInfo);
        }

        #endregion
    }
}
