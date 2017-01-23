using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using PurchaseOrder.BusinessLayer.Interfaces;
using PurchaseOrder.Common.Enum;
using PurchaseOrder.Filters;
using PurchaseOrder.ModelBinders;

namespace PurchaseOrder.Controllers
{
    [RoutePrefix("api/purchaseorder")]
    [ValidationFilter]
    public class PurchaseOrderController : ApiController
    {
        private readonly IPurchaseOrderManager _purchaseOrderManager;
        public PurchaseOrderController(IPurchaseOrderManager purchaseOrderManager)
        {
            _purchaseOrderManager = purchaseOrderManager;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public string GetServiceName()
        {
            return "Purchase Order Service....";
        }


        /// <summary>
        /// Get Purchase Order By Company Code
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{companycode}")]
        [Route("companycode/{*companycode}")]
        public IHttpActionResult GetPurchaseOrderByCompanyCode([ModelBinder(typeof(SlashInValueBinder))]string companyCode)
        {

            var response = _purchaseOrderManager.GetPurchaseOrderByCompanyCode(companyCode);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrders);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }


        /// <summary>
        /// Get Purchase Order by Company Code and Purchase Order Number
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="purchaseOrderNumber">purchase order number as string</param>
        /// <returns>return http action result with respnse or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/purchaseordernumber/{*purchaseordernumber}")]
        public IHttpActionResult GetPurchaseOrderByPurchaseOrderNumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string purchaseOrderNumber)
        {
            var response = _purchaseOrderManager.GetPurchaseOrderByPurchaseOrderNumber(companyCode, purchaseOrderNumber);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrder);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }


        /// <summary>
        /// Get Purchase Order by Company Code and Order Type
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="orderType">order type as string</param>
        /// <returns>return http action result with respnse or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/ordertype/{*ordertype}")]
        public IHttpActionResult GetPurchaseOrderByOrderType(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string orderType)
        {
            var response = _purchaseOrderManager.GetPurchaseOrderByOrderType(companyCode, orderType);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrders);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// Get Puchase order by Company Code and Customer name
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="customerName">customer name as string</param>
        /// <returns>return http action result with respnse or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/customername/{*customername}")]
        public IHttpActionResult GetPurchaseOrderByCustomerName(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string customerName)
        {
            var response = _purchaseOrderManager.GetPurchaseOrdersByCustomerName(companyCode, customerName);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrderCustomers);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// Get Purchase Order by Company Code and Project Number
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="projectNumber">project number as string</param>
        /// <returns>return http action result with respnse or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/projectnumber/{*projectnumber}")]
        public IHttpActionResult GetPurchaseOrderByProjectNumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string projectNumber)
        {
            var response = _purchaseOrderManager.GetPurchaseOrderByProjectNumber(companyCode, projectNumber);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrders);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// Gets the Purchase order details by company code and order start and end date range
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="orderStartDate">order start date as string</param>
        /// <param name="orderEndDate">order end date as string</param>
        /// <returns>return http action result with respnse or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/orderstartdate/{orderstartdate}/orderenddate/{*orderenddate}")]
        public IHttpActionResult GetPurchaseOrderByOrderDateRange(string companyCode, string orderStartDate, [ModelBinder(typeof(SlashInValueBinder))]string orderEndDate)
        {
            var response = _purchaseOrderManager.GetPurchaseOrderByOrderDateRange(companyCode, orderStartDate, orderEndDate);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrders);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// Gets the Purchase order details by company code and delivery start and end date range
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="deliveryStartDate">Deliery start date as string</param>
        /// <param name="deliveryEndDate">Delivery end date as string</param>
        /// <returns>return http action result with respnse or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/deliverystartdate/{deliverystartdate}/deliveryenddate/{*deliveryenddate}")]
        public IHttpActionResult GetPurchaseOrderByDeliveryDateRange(string companyCode, string deliveryStartDate, [ModelBinder(typeof(SlashInValueBinder))]string deliveryEndDate)
        {
            var response = _purchaseOrderManager.GetPurchaseOrderByDeliveryDateRange(companyCode, deliveryStartDate, deliveryEndDate);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrders);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }
    }
}
