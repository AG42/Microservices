using PurchaseOrderBySupplier.API.Filters;
using PurchaseOrderBySupplier.API.ModelBinders;
using PurchaseOrderBySupplier.BusinessLayer.Interfaces;
using PurchaseOrderBySupplier.Common.Enum;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PurchaseOrderBySupplier.API.Controllers
{
    [RoutePrefix("api/purchaseorderbysupplier")]
    [ValidationFilter]
    public class PurchaseOrderBySupplierController : ApiController
    {
        private readonly IPurchaseOrderBySupplierManager _purchaseOrderManager;

       

        public PurchaseOrderBySupplierController(IPurchaseOrderBySupplierManager purchaseOrderManager)
        {
            _purchaseOrderManager = purchaseOrderManager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Purchase Order By Supplier Service....";
        }

        /// <summary>
        /// Get Purchase Order by Supplier by Company Code
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <returns>return http action result with response or error info if any</returns>
        [HttpGet]
        [Route("{companycode}")]
        [Route("companycode/{*companycode}")]
        public IHttpActionResult GetPurchaseOrdersByCompanyCode([ModelBinder(typeof(SlashInValueBinder))]string companyCode)
        {
           
            var response = _purchaseOrderManager.GetPurchaseOrdersByCompanyCode(companyCode);

            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrdersBySupplier);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
        }
     
        /// <summary>
        /// Get Purchase Order by Supplier By Company Code and supplier Invoice Number
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="supplierInvoiceNumber">Supplier Invoice number as string</param>
        /// <returns>return http action result with response or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/supplierinvoicenumber/{*supplierinvoicenumber}")]
        public IHttpActionResult GetPurchaseOrdersBySupplierInvoiceNumber(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string supplierInvoiceNumber)
        {

            var response = _purchaseOrderManager.GetPurchaseOrdersBySupplierInvoiceNumber(companyCode, supplierInvoiceNumber);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrdersBySupplier);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }

        /// <summary>
        /// Get Purchase Order by Supplier by Company Code and Supplier Code
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="supplierCode">purchase order number as string</param>
        /// <returns>return http action result with response or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/suppliercode/{*suppliercode}")]
        public IHttpActionResult GetPurchaseOrdersBySupplierCode(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string supplierCode)
        {
            var response = _purchaseOrderManager.GetPurchaseOrdersBySupplierCode(companyCode, supplierCode);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrdersBySupplier);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }

        /// <summary>
        /// Get Purchase Order by Supplier by Company Code and VAT Code
        /// </summary>
        /// <param name="companyCode">company code as string</param>
        /// <param name="vatCode">purchase order number as string</param>
        /// <returns>return http action result with response or error info if any</returns>
        [HttpGet]
        [Route("companycode/{companycode}/vatcode/{*vatcode}")]
        public IHttpActionResult GetPurchaseOrdersByVATCode(string companyCode, [ModelBinder(typeof(SlashInValueBinder))]string vatCode)
        {          
            var response = _purchaseOrderManager.GetPurchaseOrdersByVATCode(companyCode, vatCode);
            if (response.Status == ResponseStatus.Success)
            {
                return Ok(response.PurchaseOrdersBySupplier);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));

        }

    }
}
