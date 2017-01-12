using System.Web.Http;
using CustomerSiteLocation.BusinessLayer.Interface;
using CustomerSiteLocation.Common.Logger;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using CustomerSiteLocation.Common.Enum;
using CustomerSiteLocation.API.Filters;

namespace CustomerSiteLocation.API.Controllers
{
    [RoutePrefix("api/customersitelocation")]
    [ValidationFilter]
    public class CustomerSiteLocationController : BaseContoller
    {
        private readonly ICustomerSiteLocationManager _customerSiteLocationManager;

        public CustomerSiteLocationController(ICustomerSiteLocationManager customerSiteLocationManager)
        {
            _customerSiteLocationManager = customerSiteLocationManager;
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>List of customer site locations</returns>
        [HttpGet]
        [Route("{companyCode}")]
        [Route("companyCode/{companyCode}/parentcompanycode/{parentcompanycode}")]
        public IHttpActionResult GetCutomerSiteLocationsByCompanyCode(string companyCode, string parentCompanyCode = "")
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerControllerMethodName: GetCutomerSiteLocationsByCompanyCode :: Custome Input: [{companyCode}]");
            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByCompanyCode(companyCode);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerName"></param>
        /// <returns>List of customer site locations</returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/customerName/{*customerName}")]
        public IHttpActionResult GetCustomerSiteLocationsByCustomerName(string companyCode, string customerName)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSiteLocationsByCustomerName :: Custome Input: [{companyCode} +,+[{customerName}]");
            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerName(companyCode, customerName);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="customerCode"></param>
        /// <returns>List of customer site locations</returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/customerCode/{*customerCode}")]
        public IHttpActionResult GetCustomerSiteLocationsByCustomerCode(string companyCode, string customerCode)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSiteLocationsByCustomerCode :: Custome Input: [{companyCode}][{","}][{customerCode}]");

            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerCode(companyCode, customerCode);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Customer Site Locations By Country
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/country/{*country}")]
        public IHttpActionResult GetCustomerSiteLocationsByCountry(string companyCode, string country)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerSiteLocationControllerMethodName: GetCustomerSiteLocationsByCountry :: Custome Input: [{companyCode}],[{country}]");

            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByCountry(companyCode, country);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Customer Site Location By Telephone No
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="telePhone"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("companyCode/{companyCode}/telePhone")]
        public IHttpActionResult GetCustomerSiteLocationsByTelephoneNo(string companyCode, [FromBody] string telePhone)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSite :: Custome Input: [{companyCode}],[{telePhone}]");
            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByTelephoneNo(companyCode, telePhone);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth:[{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Customer site location by Address
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/address/{*address}")]
        public IHttpActionResult GetCustomerSiteLocationsByAddress(string companyCode, string address)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");

            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByAddress(companyCode, address);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Customer site location by faxno
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="faxNo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("companyCode/{companyCode}/faxno")]
        public IHttpActionResult GetCustomerSiteLocationsByFaxNo(string companyCode, [FromBody] string faxNo)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSite :: Custome Input: [{companyCode}],[{faxNo}]");
            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByFaxNo(companyCode, faxNo);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth:[{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Customer Site Locations By Zip
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="emailId"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/emailid/{*emailid}")]
        public IHttpActionResult GetCustomerSiteLocationsByEmailId(string companyCode, string emailId)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerSiteLocationControllerMethodName: GetCustomerSiteLocationsByZip :: Custome Input: [{companyCode}],[{emailId}]");

            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByEmailId(companyCode, emailId);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }

        /// <summary>
        /// Get Customer site location by Site No.
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="siteNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/siteno/{*siteno}")]
        public IHttpActionResult GetCustomerSiteLocationsBySiteNo(string companyCode, string siteNo)
        {

            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerControllerMethodName: GetCustomers :: Custome Input: [{companyCode}]");

            var response = _customerSiteLocationManager.GetCustomerSiteLocationsBySiteNo(companyCode, siteNo);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }

        /// <summary>
        /// Get Customer Site Locations By Zip
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/zip/{*zipCode}")]
        public IHttpActionResult GetCustomerSiteLocationsByZip(string companyCode, string zipCode)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerSiteLocationControllerMethodName: GetCustomerSiteLocationsByZip :: Custome Input: [{companyCode}],[{zipCode}]");
            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByZip(companyCode, zipCode);

            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get Customer site locations base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="serviceEngineerCode"></param>
        /// <returns>List of customer site locations</returns>
        [HttpGet]
        [Route("companyCode/{companyCode}/serviceEngineerCode/{*serviceEngineerCode}")]
        public IHttpActionResult GetCustomerSiteLocationsByServiceEngineerCode(string companyCode,
            string serviceEngineerCode)
        {
            ApplicationLogger.InfoLogger(
                $"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{Request.RequestUri}] :: CustomerControllerMethodName: GetCustomerSiteLocationsByServiceEngineerCode :: Custome Input: [{companyCode}][{","}][{serviceEngineerCode}]");

            var response = _customerSiteLocationManager.GetCustomerSiteLocationsByServiceEngineerCode(companyCode,
                serviceEngineerCode);
            if (response.Status == ResponseStatus.Success)
            {
                ApplicationLogger.InfoLogger(
                    $"Response Status: Success :: ItemLegth: [{response.CustomerSiteLocations.Count}]");
                return Ok(response.CustomerSiteLocations);
            }

            ApplicationLogger.InfoLogger("Response Status: Failuer");
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }
    }
}
