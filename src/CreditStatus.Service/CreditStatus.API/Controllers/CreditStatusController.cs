﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using CreditStatus.BusinessLayer.Interfaces;
using CreditStatus.API.Filters;
using CreditStatus.API.ModelBinders;
using CreditStatus.Common.Enum;

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
        public IHttpActionResult GetCreditStatusByCompanyCode(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] bool ledgerFlag)
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
        public IHttpActionResult GetCreditStatusByCustomerCode(string companyCode, string customerCode, [ModelBinder(typeof(SlashInValueBinder))] bool ledgerFlag)
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
        public IHttpActionResult GetCreditStatusByCustomerName(string companyCode, string customerName, [ModelBinder(typeof(SlashInValueBinder))] bool ledgerFlag)
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
