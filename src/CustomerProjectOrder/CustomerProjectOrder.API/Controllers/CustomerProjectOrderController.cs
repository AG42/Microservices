using System;
using System.Globalization;
using CustomerProjectOrder.BusinessLayer.Interface;
using System.Net;
using System.Web.Http;
using CustomerProjectOrder.Common;
using System.Net.Http;
using CustomerProjectOrder.API.Filters;
using CustomerProjectOrder.Common.Enum;
using CustomerProjectOrder.Common.Logger;


namespace CustomerProjectOrder.API.Controllers
{
    [RoutePrefix("api/customerprojectorder")]
    [ValidationFilter]
    public class CustomerProjectOrderController : ApiController
    {
        readonly ICustomerProjectOrderManager _customerProjectOrderManager;
        public CustomerProjectOrderController(ICustomerProjectOrderManager customerProjectOrderManager)
        {
            _customerProjectOrderManager = customerProjectOrderManager;
        }

        [HttpGet]
        [Route("{companyCode}")]
        [Route("companyCode/{companyCode}")]
        public IHttpActionResult GetProjectByCompanyCode(string companyCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");
           
                var response = _customerProjectOrderManager.GetProjectByCompanyCode(companyCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :: ItemLegth: 1.");
                    return Ok(response.CustomerProjectOrders);
                }

                ApplicationLogger.InfoLogger("Response Status: Failuer");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// Get project base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="projectNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/projectNumber/{*projectNumber}")]
        public IHttpActionResult GetProjectByNumber(string companyCode, string projectNumber)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");
           
                var response = _customerProjectOrderManager.GetProjectByNumber(companyCode, projectNumber);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :: ItemLegth: 1.");
                    return Ok(response.CustomerProjectOrder);
                }

                ApplicationLogger.InfoLogger("Response Status: Failuer");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get project base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/projectName/{*projectName}")]
        public IHttpActionResult GetProjectByName(string companyCode, string projectName)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");

                var response = _customerProjectOrderManager.GetProjectByName(companyCode, projectName);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :: ItemLegth: [{response.CustomerProjectOrders.Count}]");
                    return Ok(response.CustomerProjectOrders);
                }

                ApplicationLogger.InfoLogger("Response Status: Failuer");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get project base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/startDate/{startDate}/endDate/{endDate}")]
        public IHttpActionResult GetProjectByDuration(string companyCode, string startDate, string endDate)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");

                var response = _customerProjectOrderManager.GetProjectByDuration(companyCode, startDate, endDate);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :: ItemLegth: [{response.CustomerProjectOrders.Count}]");
                    return Ok(response.CustomerProjectOrders);
                }

                ApplicationLogger.InfoLogger("Response Status: Failuer");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }

        /// <summary>
        /// Get project base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerPONo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/customerPONo/{*customerPONo}")]
        public IHttpActionResult GetProjectByCustomerPONo(string companyCode, string customerPONo)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");
           
                var response = _customerProjectOrderManager.GetProjectByCustomerPONo(companyCode, customerPONo);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :: ItemLegth: 1.");
                    return Ok(response.CustomerProjectOrder);
                }

                ApplicationLogger.InfoLogger("Response Status: Failuer");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            
        }

        /// <summary>
        /// Get project base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/account/{*account}")]
        public IHttpActionResult GetProjectByAccount(string companyCode, string account)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");
          
                var response = _customerProjectOrderManager.GetProjectByAccount(companyCode, account);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :: ItemLegth: 1.");
                    return Ok(response.CustomerProjectOrders);
                }
                ApplicationLogger.InfoLogger("Response Status: Failuer");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }
    }
}
