using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxInvoice.API.Filters;
using TaxInvoice.BusinessLayer.Interfaces;
using TaxInvoice.Common.Enum;

namespace TaxInvoice.API.Controllers
{
    [RoutePrefix("api/taxinvoice")]
    [ValidationFilter]
    public class TaxInvoiceController : ApiController
    {
        private readonly ITaxInvoiceManager _taxInvoiceManager;

        public TaxInvoiceController(ITaxInvoiceManager taxInvoiceManager)
        {
            _taxInvoiceManager = taxInvoiceManager;
        }


        [Route("")]
        public IHttpActionResult GetTaxInvoice()
        {
            return Ok("Tax Invoice");
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <returns>return HttpActionResult object with details</returns>
        [HttpGet]
        [Route("companycode/{companycode}")]
        public IHttpActionResult GetTaxInvoiceByCompanyCode(string companyCode)
        {
            var response = _taxInvoiceManager.GetTaxInvoiceByCompanyCode(companyCode);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.TaxInvoices);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and Invoice number
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="invoiceNo">Invoice number as string</param>
        /// <returns>return HttpActionResult object with details</returns>
        [HttpGet]
        [Route("companycode/{companycode}/invoiceno/{*invoiceno}")]
        public IHttpActionResult GetTaxInvoiceByInvoiceNo(string companyCode, string invoiceNo)
        {
            var response = _taxInvoiceManager.GetTaxInvoiceByInvoiceNo(companyCode, invoiceNo);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.TaxInvoices);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and Customer code
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="customerCode">Customer code as string</param>
        /// <returns>return HttpActionResult object with details</returns>
        [HttpGet]
        [Route("companycode/{companycode}/customercode/{*customercode}")]
        public IHttpActionResult GetTaxInvoiceByCustomerCode(string companyCode, string customerCode)
        {
            var response = _taxInvoiceManager.GetTaxInvoiceByCustomerCode(companyCode, customerCode);
            if(response.Status == ResponseStatus.Success)
            {
                return Ok(response.TaxInvoices);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }


        /// <summary>
        /// This method gets Tax invoice details based on company code and range of Tax amount
        /// </summary>
        /// <param name="companyCode">Company code as string</param>
        /// <param name="minTaxInvoice">Minimum tax amount as decimal</param>
        /// <param name="maxTaxInvoice">Maximum tax amount as decimal</param>
        /// <returns>return HttpActionResult object with details</returns>
        [HttpGet]
        [Route("companycode/{companycode}/mintaxamount/{mintaxamount}/maxtaxamount/{maxtaxamount}")]
        public IHttpActionResult GetTaxInvoiceByTaxInvoiceRange(string companyCode, decimal minTaxAmount, decimal maxTaxAmount)
        {
            var response = _taxInvoiceManager.GetTaxInvoiceByTaxAmountRange(companyCode, minTaxAmount, maxTaxAmount);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.TaxInvoices);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }
    }
}