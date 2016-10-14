using ServiceOrder.API.Attributes;
using ServiceOrder.BusinessLayer.Interfaces;
using ServiceOrder.Common;
using ServiceOrder.Common.Enum;
using ServiceOrder.Common.Logger;
using ServiceOrder.Model.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceOrder.API.Controllers
{
    [RoutePrefix("api/serviceOrder")]
    public class ServiceOrderController : BaseController
    {
        readonly IServiceOrderManager serviceOrderManager;
        public ServiceOrderController(IServiceOrderManager iserviceOrderManager)
        {
            serviceOrderManager = iserviceOrderManager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Service Order Service...";
        }

        //[InputValidator]
        //[Route("companyCode/{companyCode}")]
        //public HttpResponseMessage GetServiceOrderByCompanyCode(string companyCode)
        //{
        //    ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ServiceOrderController: GetServiceOrderByCompanyId :: Custom Input: companyCode: {companyCode}");
        //    try
        //    {
        //        var response = serviceOrderManager.GetServiceOrderByCompanyCode(companyCode);

        //        if (response.Status == ResponseStatus.Success)
        //        {
        //            ApplicationLogger.InfoLogger($"Response Status: Success");
        //            return Request.CreateResponse(HttpStatusCode.OK, new { result = response });
        //        }

        //        ApplicationLogger.InfoLogger("Response Status: Failure");
        //        return GetErrorJsonResponse(response, Category.Business);
        //    }
        //    catch (HttpResponseException exception)
        //    {
        //        ApplicationLogger.InfoLogger("Exception: HttpResponseException");
        //        return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Business, exception);
        //    }
        //    catch (Exception ex)
        //    {
        //        ApplicationLogger.InfoLogger("Exception: BaseException");
        //        return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Database, ex);
        //    }
        //}

        [InputValidator]
        [Route("companyCode/{companyCode}/serviceOrderNo/{serviceOrderNo}")]
        public HttpResponseMessage GetServiceOrderByServiceOrderNo(string companyCode, string serviceOrderNo)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ServiceOrderController: GetServiceOrderById :: Custom Input: companyCode: {companyCode}, serviceOrderId: {serviceOrderNo}");
            try
            {
                var response = serviceOrderManager.GetServiceOrderByServiceOrderNo(companyCode, serviceOrderNo);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success");
                    return Request.CreateResponse(HttpStatusCode.OK, new { result = response });
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return GetErrorJsonResponse(response, Category.Business);
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Business, exception);
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Database, ex);
            }
        }

        [InputValidator]
        [Route("OrderStatus/companyCode/{companyCode}/serviceOrderNo/{serviceOrderNo}")]
        public HttpResponseMessage GetServiceOrderStatusByServiceOrderNo(string companyCode, string serviceOrderNo)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ServiceOrderController: GetServiceOrderStatus  :: Custom Input: companyCode: {companyCode}, serviceOrderId: {serviceOrderNo}");
            try
            {
                var response = serviceOrderManager.GetServiceOrderStatusByServiceOrderNo(companyCode, serviceOrderNo);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success");
                    return Request.CreateResponse(HttpStatusCode.OK, new { result = response });
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return GetErrorJsonResponse(response, Category.Business);
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Business, exception);
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Database, ex);
            }
        }

        [InputValidator]
        [Route("OrderType/companyCode/{companyCode}/serviceOrderNo/{serviceOrderNo}")]
        public HttpResponseMessage GetServiceOrderTypeByServiceOrderNo(string companyCode, string serviceOrderNo)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ServiceOrderController: GetServiceOrderType  :: Custom Input: companyCode: {companyCode}, serviceOrderId: {serviceOrderNo}");
            try
            {
                var response = serviceOrderManager.GetServiceOrderTypeByServiceOrderNo(companyCode, serviceOrderNo);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success");
                    return Request.CreateResponse(HttpStatusCode.OK, new { result = response });
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return GetErrorJsonResponse(response, Category.Business);
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Business, exception);
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Database, ex);
            }
        }

        [InputValidator]
        [Route("companyCode/{companyCode}/InvoiceCustomerCode/{invoiceCustomerCode}")]
        public HttpResponseMessage GetServiceOrderByInvoiceCustomerCode(string companyCode, string invoiceCustomerCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ServiceOrderController: GetServiceOrderByInvoiceCustomerCode  :: Custom Input: companyCode: {companyCode}, invoiceCustomerCode: {invoiceCustomerCode}");
            try
            {
                var response = serviceOrderManager.GetServiceOrderByInvoiceCustomerCode(companyCode, invoiceCustomerCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success");
                    return Request.CreateResponse(HttpStatusCode.OK, new { result = response });
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return GetErrorJsonResponse(response, Category.Business);
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Business, exception);
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Database, ex);
            }
        }

        [InputValidator]
        [Route("companyCode/{companyCode}/InvoiceNumber/{invoiceNumber}")]
        public HttpResponseMessage GetServiceOrderByInvoiceNumber(string companyCode, string invoiceNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: {DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)} :: Request Uri: { Request.RequestUri} :: ServiceOrderController: GetServiceOrderByInvoiceNumber :: Custom Input: companyCode: {companyCode}, invoiceNumber: {invoiceNumber}");
            try
            {
                var response = serviceOrderManager.GetServiceOrderByInvoiceNumber(companyCode, invoiceNumber);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success");
                    return Request.CreateResponse(HttpStatusCode.OK, new { result = response });
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return GetErrorJsonResponse(response, Category.Business);
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Business, exception);
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                return GetErrorJsonResponse(Constants.NoDataFoundMessage, Category.Database, ex);
            }
        }
    }
}
