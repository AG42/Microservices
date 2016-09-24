using ProductInventory.BusinessLayer.Interfaces;
using ProductInventory.Common;
using ProductInventory.Common.Enum;
using ProductInventory.Common.Logger;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductInventory.API.Controllers
{
    
    [RoutePrefix("api/productInventory")]
    public class ProductInventoryController : ApiController
    {
        readonly IProductInventoryManager _manager;
        public ProductInventoryController(IProductInventoryManager manager)
        {
            _manager = manager;
        }

        [Route("")]
        public string GetServiceName()
        {
            return "Product Inventory Service...";
        }

        /// <summary>
        /// Get product inventory by company code and by product code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/productCode/{productCode}")]
        public IHttpActionResult GetProductById(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProductById :: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}]");
                var response = _manager.GetProductById(companyCode, productCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger("Response Status: Success");
                    return Ok(response.ProductInventory);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");                
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                ApplicationLogger.Errorlog(ex.Message, Category.Database, ex.StackTrace, ex.InnerException);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.InternalServerError));
            }
        }

        /// <summary>
        /// Get product inventory by company code and by description
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/description/{description}")]
        public IHttpActionResult GetProductByDescription(string companyCode, string description)
        {
            try
            {
                var response = _manager.GetProductByDescription(companyCode, description);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :Item Length:[{response.ProductList.Count}]");
                    return Ok(response.ProductList);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                ApplicationLogger.Errorlog(ex.Message, Category.Database, ex.StackTrace, ex.InnerException);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.InternalServerError));
            }
        }

        /// <summary>
        /// Get product inventory by company code and locationId and by product code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/locationId/{locationId}")]
        public IHttpActionResult GetProductByLocationId(string companyCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProduct :: Custome Input: ComanyCode: [{companyCode}], locationId: [{ locationId}]");
                var response = _manager.GetProductByLocationId(companyCode, locationId);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger("Response Status: Success");
                    return Ok(response.ProductInventoryList);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                ApplicationLogger.Errorlog(ex.Message, Category.Database, ex.StackTrace, ex.InnerException);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.InternalServerError));
            }
        }

        [Route("companyCode/{companyCode}/productCode/{productCode}/locationId/{locationId}")]
        public IHttpActionResult GetProduct(string companyCode, string productCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProduct :: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}], locationId: [{ locationId}]");
                var response = _manager.GetProduct(companyCode, productCode, locationId);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger("Response Status: Success");
                    return Ok(response.ProductInventory);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                ApplicationLogger.Errorlog(ex.Message, Category.Database, ex.StackTrace, ex.InnerException);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.InternalServerError));
            }
        }              

        /// <summary>
        /// Get product family type
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [Route("productFamily/companyCode/{companyCode}/productCode/{productCode}")]
        public IHttpActionResult GetProductFamilyType(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProductFamilyType :: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}]");

                var response = _manager.GetProductFamilyType(companyCode, productCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :Family Type :[{ response.FamilyType }]");
                    return Ok(response.FamilyType);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        /// <summary>
        /// Get product line type
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [Route("productLine/companyCode/{companyCode}/productCode/{productCode}")]
        public IHttpActionResult GetProductLineType (string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProductLineType :: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}]");

                var response = _manager.GetProductLineType(companyCode, productCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :Line Type :[{ response.LineType }]");                    
                    return Ok(response.LineType);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        /// <summary>
        /// Get lcoationwiseproduct stock status
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [Route("productStockStatus/companyCode/{companyCode}/productCode/{productCode}")]
        public IHttpActionResult GetProductStockStatus(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProductLineType :: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}]");

                var response = _manager.IsProductStockable(companyCode, productCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :Line Type :[{ response.Stockable }]");
                    return Ok(response.Stockable);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        [Route("productStatus/companyCode/{companyCode}/productCode/{productCode}")]
        public IHttpActionResult GetProductActiveStatus(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: IsProductActive:: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}]");

                var response = _manager.IsProductActive(companyCode, productCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :IsProductActive :[{ response.IsActive }]");
                    return Ok(response.IsActive);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        /// <summary>
        /// Get given product available qantity for all location
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [Route("locationwiseProductAvailableQuantity/companyCode/{companyCode}/productCode/{productCode}")]        
        public IHttpActionResult GetProductAvailableQuantityForAllLocation(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProductAvailableQuantityForAllLocation :: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}]");

                var response = _manager.GetProductAvailableQuantityForAllLocation(companyCode, productCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :Product Length :[{ response.ProductList.Count }]");
                    return Ok(response.ProductList);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        [Route("productAvailableQuantityForLocation/companyCode/{companyCode}/locationId/{locationId}")]
        public IHttpActionResult GetLocationwiseProductAvailableQuantity(string companyCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetLocationwiseProductAvailableQuantity :: Custome Input: ComanyCode: [{companyCode}], locationId: [{ locationId}]");

                var response = _manager.GetLocationwiseProductAvailableQuantity(companyCode, locationId);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :Product length :[{ response.ProductList.Count }]");
                    return Ok(response.ProductList);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        [Route("productAvailableQuantity/companyCode/{companyCode}/productCode/{productCode}/locationId/{locationId}")]
        public IHttpActionResult GetProductAvailableQuantity(string companyCode, string productCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetAvailableQuantity :: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}]");

                var response = _manager.GetProductAvailableQuantity(companyCode, productCode, locationId);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :Available Quantity :[{ response.AvailableQuantity }]");
                    return Ok(response.AvailableQuantity);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        /// <summary>
        /// Get product stock balance qantity for given location
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="productCode"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [Route("productStockBalance/companyCode/{companyCode}/productCode/{productCode}/locationId/{locationId}")]
        public IHttpActionResult GetProductStockBalanceQuantity(string companyCode, string productCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProductStockBalanceQuantity :: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}], locationId: [{ locationId}]");

                var response = _manager.GetProductStockBalanceQuantity(companyCode, productCode, locationId);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :Stock Balance Quantity :[{ response.StockBalanceQuantity }]");
                    return Ok(response.StockBalanceQuantity);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        [Route("locationwiseProductStockStatus/companyCode/{companyCode}/productCode/{productCode}")]
        public IHttpActionResult GetProductStockBalanceQuantityForAllLocation(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetProductStockBalanceQuantityForAllLocation:: Custome Input: ComanyCode: [{companyCode}], productCode: [{ productCode}]");

                
                var response = _manager.GetProductStockBalanceQuantityForAllLocation(companyCode, productCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :ProductStockBalanceQuantity :[{ response.LocationList }]");
                    return Ok(response.LocationList);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }

        [Route("productStockStatusForLocation/companyCode/{companyCode}/locationId/{locationId}")]
        public IHttpActionResult GetLocationwiseProductStockBalanceQuantity(string companyCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: ProductInventoryControllerMethodName: GetLocationwiseProductStockBalanceQuantity:: Custome Input: ComanyCode: [{companyCode}], locationId: [{ locationId}]");


                var response = _manager.GetLocationwiseProductStockBalanceQuantity(companyCode, locationId);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :ProductStockBalanceQuantity :[{response.ProductList}]");
                    return Ok(response.ProductList);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger($"Exception: HttpResponseException: [{exception.Message}]");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception: BaseException: [{ex.Message}]");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.NoDataFoundMessage));
            }
        }
    }
}
