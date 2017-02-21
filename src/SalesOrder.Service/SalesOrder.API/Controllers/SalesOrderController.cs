using SalesOrder.API.Filters;
using SalesOrder.API.ModelBinders;
using SalesOrder.BusinessLayer.Interfaces;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace SalesOrder.API.Controllers
{
    [RoutePrefix("api/salesorder")]
    [ValidationFilter]
    public class SalesOrderController : ApiController
    {
        private readonly ISalesOrderManager _salesOrderManager;

        public SalesOrderController(ISalesOrderManager salesOrderManager)
        {
            _salesOrderManager = salesOrderManager;
        }


        /// <summary>
        /// This method is default methode for checking service is up and running
        /// </summary>
        /// <returns>it simply return name of the service</returns>
        [Route("")]
        public IHttpActionResult GetSalesOrder()
        {
            return Ok("Sales Order Service");
        }


        /// <summary>
        /// This methode provides sales order details based on company code
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <returns>return object of IHttpActionResult data or error message</returns>
        [HttpGet]
        [Route("companycode/{companycode}")]
        public IHttpActionResult GetSalesOrderByCompanyCode([ModelBinder(typeof(SlashInValueBinder))]string companyCode)
        {
            var response = _salesOrderManager.GetSalesOrderByCompanyCode(companyCode);

            if(response.Status == Common.Enum.ResponseStatus.Success)
            {
                return Ok(response.SalesOrders);
            }
            return ResponseMessage(Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This methode provides sales order detail based on company code and Sales order number
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="salesOrderNumber">Sales order number as string</param>
        /// <returns>return object of IHttpActionResult data or error message</returns> 
        [HttpGet]
        [Route("companycode/{companycode}/salesordernumber/{salesordernumber}")]
        public IHttpActionResult GetSalesOrderBySalesOrderNumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string salesOrderNumber)
        {
            var response = _salesOrderManager.GetSalesOrderBySalesOrderNumber(companyCode, salesOrderNumber);

            if (response.Status == Common.Enum.ResponseStatus.Success)
            {
                return Ok(response.SalesOrders);
            }
            return ResponseMessage(Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This methode provides sales order details based on company code and type of order
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="orderType">type of order as string</param>
        /// <returns>return object of IHttpActionResult data or error message</returns>
        [HttpGet]
        [Route("companycode/{companycode}/ordertype/{ordertype}")]
        public IHttpActionResult GetSalesOrderByOrderType(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string orderType)
        {
            var response = _salesOrderManager.GetSalesOrderByOrderType(companyCode, orderType);

            if (response.Status == Common.Enum.ResponseStatus.Success)
            {
                return Ok(response.SalesOrders);
            }
            return ResponseMessage(Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This methode provides sales order details based on company code and customer invoice code
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="customerInvoiceCode">customer invoice code as string</param>
        /// <returns>return object of IHttpActionResult data or error message</returns>
        [HttpGet]
        [Route("companycode/{companycode}/customerinvoicecode/{customerinvoicecode}")]
        public IHttpActionResult GetSalesOrderByCustomerInvoiceCode(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string customerInvoiceCode)
        {
            var response = _salesOrderManager.GetSalesOrderByCustomerInvoiceCode(companyCode, customerInvoiceCode);

            if (response.Status == Common.Enum.ResponseStatus.Success)
            {
                return Ok(response.SalesOrders);
            }
            return ResponseMessage(Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This methode provides sales order details based on company code and flag pick list
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="flagPickList">flag pick list as string</param>
        /// <returns>return object of IHttpActionResult data or error message</returns>
        [HttpGet]
        [Route("companycode/{companycode}/flagpicklist/{flagpicklist}")]
        public IHttpActionResult GetSalesOrderByFlagPickList(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string flagPickList)
        {
            var response = _salesOrderManager.GetSalesOrderByFlagPickList(companyCode, flagPickList);

            if (response.Status == Common.Enum.ResponseStatus.Success)
            {
                return Ok(response.SalesOrders);
            }
            return ResponseMessage(Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This methode provides sales order details based on company code and order date range
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="minOrderDate">minimum order date as string</param>
        /// <param name="maxOrderDate">maximum order date as string</param>
        /// <returns>return object of IHttpActionResult data or error message</returns>
        [HttpGet]
        [Route("companycode/{companycode}/minorderdate/{minorderdate}/maxorderdate/{maxorderdate}")]
        public IHttpActionResult GetSalesOrderByOrderDateRange(string companyCode, string minOrderDate, [ModelBinder(typeof(SlashInValueBinder))]string maxOrderDate)
        {
            var response = _salesOrderManager.GetSalesOrderByOrderDateRange(companyCode, minOrderDate, maxOrderDate);

            if (response.Status == Common.Enum.ResponseStatus.Success)
            {
                return Ok(response.SalesOrders);
            }
            return ResponseMessage(Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This methode provides sales order details based on company code and delivery date range
        /// </summary>
        /// <param name="companyCode">Company Code as string</param>
        /// <param name="minDeliveryDate">minimum delivery date as string</param>
        /// <param name="maxDeliveryDate">maximum delivery date as string</param>
        /// <returns>return object of IHttpActionResult data or error message</returns>
        [HttpGet]
        [Route("companycode/{companycode}/mindeliverydate/{mindeliverydate}/maxdeliverydate/{maxdeliverydate}")]
        public IHttpActionResult GetSalesOrderByDeliveryDateRange(string companyCode, string minDeliveryDate, [ModelBinder(typeof(SlashInValueBinder))]string maxDeliveryDate)
        {
            var response = _salesOrderManager.GetSalesOrderByDeliveryDateRange(companyCode, minDeliveryDate, maxDeliveryDate);

            if (response.Status == Common.Enum.ResponseStatus.Success)
            {
                return Ok(response.SalesOrders);
            }
            return ResponseMessage(Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, response.ErrorInfo));
        }
    }
}