using ContractInformation.API.Filters;
using ContractInformation.BusinessLayer.Interfaces;
using ContractInformation.Common.Enum;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using ContractInformation.API.ModelBinders;

namespace ContractInformation.API.Controllers
{
    [RoutePrefix("api/contract")]
    [ValidationFilter]
    public class ContractInformationController : ApiController
    {
        readonly IContractInformationManager _contractInformationManager;
        public ContractInformationController(IContractInformationManager contractInformationManager)
        {
            _contractInformationManager = contractInformationManager;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetContractsByCompanyCode()
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Contract Information Service"));
        }

        [HttpGet]
        [Route("{companyCode}")]
        [Route("companyCode/{companyCode}")]
        public IHttpActionResult GetContractsByCompanyCode(string companyCode)
        {
            var response = _contractInformationManager.GetContractsByCompanyCode(companyCode);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/contractNumber/{contractNumber}")]
        public IHttpActionResult GetContractsByServiceContractNumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string contractNumber)
        {
            var response = _contractInformationManager.GetContractsByServiceContractNumber(companyCode, contractNumber);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/requestNumber/{*requestNumber}")]
        public IHttpActionResult GetContractsByRequestNumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string requestNumber)
        {
            var response = _contractInformationManager.GetContractsByRequestNumber(companyCode, requestNumber);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/poNumber/{*poNumber}")]
        public IHttpActionResult GetContractsByCustomerPONumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string poNumber)
        {
            var response = _contractInformationManager.GetContractsByCustomerPONumber(companyCode, poNumber);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/customerName/{*customerName}")]
        public IHttpActionResult GetContractsByCustomerName(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string customerName)
        {
            var response = _contractInformationManager.GetContractsByCustomerName(companyCode, customerName);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/reference/{*reference}")]
        public IHttpActionResult GetContractsByCustomerReference(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string reference)
        {
            var response = _contractInformationManager.GetContractsByCustomerReference(companyCode, reference);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/searchKey/{*searchKey}")]
        public IHttpActionResult GetContractsByCustomerSearchKey(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string searchKey)
        {
            var response = _contractInformationManager.GetContractsByCustomerSearchKey(companyCode, searchKey);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/name/{name}/status/{status}")]
        public IHttpActionResult GetContractsByCustomerNameandStatus(string companyCode, string name, [ModelBinder(typeof(SlashInValueBinder))] string status)
        {
            var response = _contractInformationManager.GetContractsByCustomerNameandStatus(companyCode, name, status);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/reference/{reference}/status/{status}")]
        public IHttpActionResult GetContractsByCustomerReferenceandStatus(string companyCode, string reference, [ModelBinder(typeof(SlashInValueBinder))] string status)
        {
            var response = _contractInformationManager.GetContractsByCustomerReferenceandStatus(companyCode, reference, status);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/searchKey/{searchKey}/status/{status}")]
        public IHttpActionResult GetContractsByCustomerSearchKeyandStatus(string companyCode, string searchKey, [ModelBinder(typeof(SlashInValueBinder))] string status)
        {
            var response = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(companyCode, searchKey, status);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/status/{status}")]
        public IHttpActionResult GetContractsByStatus(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string status)
        {
            var response = _contractInformationManager.GetContractsByStatus(companyCode, status);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companyCode/{companyCode}/startDate/{startDate}/endDate/{endDate}")]
        public IHttpActionResult GetContractsByDateRange(string companyCode,  string startDate, [ModelBinder(typeof(SlashInValueBinder))] string endDate)
        {
            var response = _contractInformationManager.GetContractsByDateRange(companyCode, startDate, endDate);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Contracts);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }
    }
}
