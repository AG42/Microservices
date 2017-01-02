using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreditStatus.BusinessLayer.Interfaces;
using CreditStatus.API.Filters;
using CreditStatus.Common.Enum;
using CreditStatus.Common.Logger;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net;

namespace CreditStatus.API.Controllers
{
    [RoutePrefix("api/creditstatus")]
    [ValidationFilter]
    public class CreditStatusController :ApiController
    {
        private readonly ICreditStatusManager _creditStatusManager;
        public CreditStatusController(ICreditStatusManager creditStatusManager)
        {
            _creditStatusManager = creditStatusManager;
        }

        [Route("")]
        public IHttpActionResult GetCreditStatus()
        {
            return Ok("Customer Credit Status");
        }

        [HttpGet]
        [Route("companycode/{companycode}/ledgerflag/{ledgerflag}")]
        public IHttpActionResult GetCreditStatusByCompanyCode(string companyCode, bool ledgerFlag)
        {
            var response = _creditStatusManager.GetCreditStatusByCompanyCode(companyCode, ledgerFlag);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Credits);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companycode/{companycode}/customercode/{customercode}/ledgerflag/{ledgerflag}")]
        public IHttpActionResult GetCreditStatusByCustomerCode(string companyCode, string customerCode, bool ledgerFlag)
        {
            var response = _creditStatusManager.GetCreditStatusByCustomerCode(companyCode,customerCode, ledgerFlag);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Credit);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        [HttpGet]
        [Route("companycode/{companycode}/customername/{customername}/ledgerflag/{ledgerflag}")]
        public IHttpActionResult GetCreditStatusByCustomerName(string companyCode, string customerName, bool ledgerFlag)
        {
            var response = _creditStatusManager.GetCreditStatusByCustomerName(companyCode, customerName, ledgerFlag);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Credits);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }
    }
}
