using PurchaseLedger.API.Filters;
using PurchaseLedger.API.ModelBinders;
using PurchaseLedger.BusinessLayer.Interfaces;
using PurchaseLedger.Common.Enum;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PurchaseLedger.API.Controllers
{
    [RoutePrefix("api/purchaseledger")]
    [ValidationFilter]
    public class PurchaseLedgerController : ApiController
    {
        private readonly IPurchaseLedgerManager _purchaseLedgerManager;

        public PurchaseLedgerController(IPurchaseLedgerManager purchaseOrderManager)
        {
            _purchaseLedgerManager = purchaseOrderManager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Purchase Ledger Service....";
        }

        /// <summary>
        /// Get Purchase Ledger By Company Code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{companycode}")]
        [Route("companycode/{*companycode}")]
        public IHttpActionResult GetPurchaseLedgerByCompanyCode([ModelBinder(typeof(SlashInValueBinder))] string companyCode)
        {
            var response = _purchaseLedgerManager.GetPurchaseLedgerByCompanyCode(companyCode);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Suppliers);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get PurchaseLedger By SupplierCode
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="supplierCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/suppliercode/{*suppliercode}")]
        public IHttpActionResult GetPurchaseLedgerBySupplierCode(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string supplierCode)
        {
            var response = _purchaseLedgerManager.GetPurchaseLedgerBySupplierCode(companyCode, supplierCode);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Suppliers);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="supplierName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/suppliername/{*suppliername}")]
        public IHttpActionResult GetPurchaseLedgerBySupplierName(string companyCode, [ModelBinder(typeof(SlashInValueBinder))] string supplierName)
        {
            var response = _purchaseLedgerManager.GetPurchaseLedgerBySupplierName(companyCode, supplierName);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Suppliers);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Get PurchaseLedger By Order No
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/orderno/{*orderno}")]
        public IHttpActionResult GetPurchaseLedgerByOrderNo(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string orderNo)
        {
            var response = _purchaseLedgerManager.GetPurchaseLedgerByOrderNo(companyCode, orderNo);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Suppliers);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Gets the Purchase ledger details by company code and invoice start and end date range
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="invoiceStartDate">order start date as string</param>
        /// <param name="invoiceEndDate">order end date as string</param>
        /// <returns>return http action result with respnse or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/invoicestartdate/{invoicestartdate}/invoiceenddate/{*invoiceenddate}")]
        public IHttpActionResult GetPurchaseLedgerByInvoiceDateRange(string companyCode, string invoiceStartDate, [ModelBinder(typeof(SlashInValueBinder))]string invoiceEndDate)
        {
            var response = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceDateRange(companyCode, invoiceStartDate, invoiceEndDate);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Suppliers);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// Gets the Purchase ledger details by company code and due start and end date range
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="dueStartDate">order start date as string</param>
        /// <param name="dueEndDate">order end date as string</param>
        /// <returns>return http action result with respnse or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/duestartdate/{duestartdate}/dueenddate/{*dueenddate}")]
        public IHttpActionResult GetPurchaseLedgerByDueDateRange(string companyCode, string dueStartDate, [ModelBinder(typeof(SlashInValueBinder))]string dueEndDate)
        {
            var response = _purchaseLedgerManager.GetPurchaseLedgerByDueDateRange(companyCode, dueStartDate, dueEndDate);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Suppliers);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("companycode/{companycode}/invoiceno/{*invoiceno}")]
        public IHttpActionResult GetPurchaseLedgerByInvoiceNo(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string invoiceNo)
        {
            var response = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceNo(companyCode, invoiceNo);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.Suppliers);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }

    }
}
