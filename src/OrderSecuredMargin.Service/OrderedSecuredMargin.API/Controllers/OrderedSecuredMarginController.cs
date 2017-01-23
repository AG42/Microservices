using OrderedSecuredMargin.API.Filters;
using OrderedSecuredMargin.BusinessLayer.Interfaces;
using OrderedSecuredMargin.Common.Enum;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace OrderedSecuredMargin.API.Controllers
{
    [RoutePrefix("api/OrderSecureMargin")]
    [ValidationFilter]
    public class OrderedSecuredMarginController : ApiController
    {
        private readonly IOrderedSecuredMarginManager _orderSecureMarginManager;

        public OrderedSecuredMarginController(IOrderedSecuredMarginManager orderSecureMarginManager)
        {
            _orderSecureMarginManager = orderSecureMarginManager;
        }


        [Route("")]
        public IHttpActionResult GetOrderedSecuredMargin()
        {
            return Ok("Customer order secure margin");
        }


        [HttpGet]
        [Route("companycode/{companycode}")]
        public IHttpActionResult GetOrderSecuredMarginByCompanyCode(string companyCode)
        {
            var response = _orderSecureMarginManager.GetOrderSecuredMarginByCompanyCode(companyCode);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.orderedSecuredMargin);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }

        [HttpGet]
        [Route("companycode/{companycode}/orderno/{orderno}")]

        public IHttpActionResult GetOderSecuredMarginByOrderNo(string companyCode, string orderNo)
        {
            var response = _orderSecureMarginManager.GetOrderSecuredMarginByOrderNo(companyCode, orderNo);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.orderSecureMargin);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companycode/{companycode}/mincost/{mincost}/maxcost/{maxcost}")]

        public IHttpActionResult GetOrderSecuredMarginByCost(string companyCode, decimal minCost, decimal maxCost)
        {
            var response = _orderSecureMarginManager.GetOrderSecuredMarginByCost(companyCode, minCost, maxCost);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.orderedSecuredMargin);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

    }
}
