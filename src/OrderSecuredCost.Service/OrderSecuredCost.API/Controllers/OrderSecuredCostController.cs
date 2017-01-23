using OrderSecuredCost.API.Filters;
using OrderSecuredCost.BusinessLayer.Interface;
using OrderSecuredCost.Common.Enum;
using OrderSecuredCost.Common.Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrderSecuredCost.API.Controllers
{
    [RoutePrefix("api/ordersecuredcost")]
    [ValidationFilter]
    public class OrderSecuredCostController : ApiController
    {
        private readonly IOrderSecuredCostManager _orderSecuredCostManager;
        public OrderSecuredCostController(IOrderSecuredCostManager orderSecuredCostManager)
        {
            _orderSecuredCostManager = orderSecuredCostManager;
        }

        /// <summary>
        ///  Test Service
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public string GetServiceName()
        {
            return "Order Secured Cost Service...";
        }

        /// <summary>
        /// Get Order Secured Cost By Company Code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{companycode}")]
        [Route("companycode/{*companycode}")]
        public IHttpActionResult GetOrderSecuredCostByCompanyCode(string companyCode)
        {
            var response = _orderSecuredCostManager.GetOrderSecuredCostByCompanyCode(companyCode);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.OrderSecuredCosts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }


        /// <summary>
        /// Get Order Secured Cost By Purchase Order Number
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="purchaseOrderNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/purchaseordernumber/{*purchaseordernumber}")]
        public IHttpActionResult GetOrderSecuredCostByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber)
        {
            var response = _orderSecuredCostManager.GetOrderSecuredCostByPurchaseOrderNumber(companyCode,purchaseOrderNumber);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.OrderSecuredCost);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Order Secured Cost By Order Type
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/ordertype/{*ordertype}")]
        public IHttpActionResult GetOrderSecuredCostByOrderType(string companyCode, string orderType)
        {
            var response = _orderSecuredCostManager.GetOrderSecuredCostByOrderType(companyCode,orderType);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.OrderSecuredCosts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Order Secured Cost By Order Date Range
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="orderStartDate"></param>
        /// <param name="orderEndDate"></param>       
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/orderstartdate/{orderstartdate}/orderenddate/{*orderenddate}")]
        public IHttpActionResult GetOrderSecuredCostByOrderDateRange(string companyCode, string orderStartDate, string orderEndDate)
        {
            var response = _orderSecuredCostManager.GetOrderSecuredCostByOrderDateRange(companyCode, orderStartDate, orderEndDate);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.OrderSecuredCosts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Order Secured Cost By Delivery Date Range
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="deliveryStartDate"></param>
        /// <param name="deliveryEndDate"></param>       
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/deliverystartdate/{deliverystartdate}/deliveryenddate/{*deliveryenddate}")]
        public IHttpActionResult GetOrderSecuredCostByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate)
        {
            var response = _orderSecuredCostManager.GetOrderSecuredCostByDeliveryDateRange(companyCode,deliveryStartDate,deliveryEndDate);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.OrderSecuredCosts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Order Secured Cost By User ID
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="userId"></param>           
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/userid/{*userid}")]
        public IHttpActionResult GetOrderSecuredCostByUserID(string companyCode, string userId)
        {
            var response = _orderSecuredCostManager.GetOrderSecuredCostByUserID(companyCode,userId);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.OrderSecuredCosts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// Get Order Secured Cost By Order Cost Range
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="ordercostrange"></param>             
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/minordersecuredcost/{minordercost}/maxordersecuredcost/{*maxordercost}")]
        public IHttpActionResult GetOrderSecuredCostByOrderCostRange(string companyCode, string minOrderCost, string maxOrderCost)
        {
            var response = _orderSecuredCostManager.GetOrderSecuredCostByOrderCostRange(companyCode, minOrderCost,maxOrderCost);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.OrderSecuredCosts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response));
        }

    }
}
