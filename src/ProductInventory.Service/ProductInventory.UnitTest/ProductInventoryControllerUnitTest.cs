using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductInventory.API.Controllers;
using ProductInventory.DataLayer.Entities;
using ProductInventory.BusinessLayer.Interfaces;
using ProductInventory.Model;
using ProductInventory.Common.Error;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using ProductInventory.DataLayer.Entities.Datalake;

namespace ProductInventory.UnitTest
{
    [TestClass]
    public class ProductInventoryControllerUnitTest
    {
        #region Declarations
        private IProductInventoryManager mockProductInventoryManager;
        private ProductInventoryController _controller;
        private const string COMPANY_CODE = "bh";

        readonly ProductInventoryModel _productInventoryModel = new ProductInventoryModel();
        readonly List<ProductWarehouseModel> _productWarehouseList = new List<ProductWarehouseModel>();
        readonly List<ItemWarehouse> _stockItemsEnttiesList = new List<ItemWarehouse>();
        readonly List<ProductInventoryModel> _productInventoryModelList = new List<ProductInventoryModel>();
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        #endregion

        #region UnitTests
        /// <summary/>
        /// Initializes
        ///// </summary>
        [TestInitialize]
        public void Initialize()
        {
            mockProductInventoryManager = MockRepository.GenerateMock<IProductInventoryManager>();
            _controller = new ProductInventoryController(mockProductInventoryManager) { Request = new HttpRequestMessage() };
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _controller.Configuration = new HttpConfiguration();
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:51083/?param1=someValue&param2=anotherValue");
        }

        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        public void GetProductByIdTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductSearchByIdResponse {ProductInventory = _productInventoryModel};
            mockProductInventoryManager.Stub(x => x.GetProductById(COMPANY_CODE, "000"))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetProductById(COMPANY_CODE, "123");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductByIdTestException()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductSearchByIdResponse {ProductInventory = _productInventoryModel};

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            //data.ErrorInfo = new List<Common.Error.ErrorInfo>({ });
            mockProductInventoryManager.Stub(x => x.GetProductById(COMPANY_CODE, "000"))
                            .IgnoreArguments()
                            .Return(data);
            var result = _controller.GetProductById(COMPANY_CODE, "123");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            result = _controller.GetProductById(COMPANY_CODE, "123");

            Assert.IsNotNull(result);

        }

        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        public void GetProductTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductResponse { ProductInventory = _productInventoryModel };
            mockProductInventoryManager.Stub(x => x.GetProduct(COMPANY_CODE, "000","70"))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetProduct(COMPANY_CODE, "123","70");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductTestException()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductResponse { ProductInventory = _productInventoryModel };

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            //data.ErrorInfo = new List<Common.Error.ErrorInfo>({ });
            mockProductInventoryManager.Stub(x => x.GetProduct(COMPANY_CODE, "000","70"))
                            .IgnoreArguments()
                            .Return(data);

             

            var result = _controller.GetProduct(COMPANY_CODE, "123","70");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            result = _controller.GetProduct(COMPANY_CODE, "123","70");

            Assert.IsNotNull(result);

        }

        /// <summary>
        /// Unit Test for GetCustomerByID Controller Method 
        /// </summary>
        [TestMethod]
        public void GetProductByDescriptionTest()
        {
            
            SetMockDataForProductModels();

            var data = new Model.Response.ProductSearchByDescriptionResponse();
            data.ProductList.AddRange(_productInventoryModelList);

            //errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            //data.ErrorInfo.AddRange(errorsList);


            mockProductInventoryManager.Stub(x => x.GetProductByDescription(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetProductByDescription(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<ProductInventoryModel>));
        }

        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductByDescriptionExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductSearchByDescriptionResponse();
            data.ProductList.AddRange(_productInventoryModelList);

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetProductByDescription(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);
            
            

            var result = _controller.GetProductByDescription(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            result = _controller.GetProductByDescription(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test  
        /// </summary>
        [TestMethod]
        public void GetProductByLocationIdTest()
        {            
            SetMockDataForProductModels();

            mockProductInventoryManager.Stub(x => x.GetProductByLocationId(COMPANY_CODE,  "1"))
                            .IgnoreArguments()
                            .Return(new Model.Response.ProductSearchByLocationIdResponse());
            var result = _controller.GetProductByLocationId(COMPANY_CODE,  "1");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductByLocationIdExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductSearchByLocationIdResponse();
            //data.ProductInventoryModel = new ProductInventoryModel();
            data.ProductInventoryList.AddRange(_productInventoryModelList);

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetProductByLocationId(COMPANY_CODE, "1"))
                            .IgnoreArguments()
                            .Return(data);
            
            

            var result = _controller.GetProductByLocationId(COMPANY_CODE, "1");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            result = _controller.GetProductByLocationId(COMPANY_CODE, "1");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductStockBalanceQuantityTest()
        {
            
            SetMockDataForProductModels();

            mockProductInventoryManager.Stub(x => x.GetProductStockBalanceQuantity(COMPANY_CODE, "TUBE", "1"))
                            .IgnoreArguments()
                            .Return(new Model.Response.ProductStockBalanceQuantityResponse());
            var result = _controller.GetProductStockBalanceQuantity(COMPANY_CODE, "TUBE", "1");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductStockBalanceQuantityExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductStockBalanceQuantityResponse {StockBalanceQuantity = 0};

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetProductStockBalanceQuantity(COMPANY_CODE, "TUBE", "1"))
                            .IgnoreArguments()
                            .Return(data);
            

            var result = _controller.GetProductStockBalanceQuantity(COMPANY_CODE, "TUBE", "1");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            result = _controller.GetProductStockBalanceQuantity(COMPANY_CODE, "TUBE", "1");

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetAvailableQuantityTest()
        {            
            SetMockDataForProductModels();
            var resp = new Model.Response.ProductAvailableQuantityResponse();
            mockProductInventoryManager.Stub(x => x.GetProductAvailableQuantity(COMPANY_CODE, "TUBE","1"))
                            .IgnoreArguments()
                            .Return(resp);

            var result = _controller.GetProductAvailableQuantity(COMPANY_CODE, "TUBE","1");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetAvailableQuantityExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductAvailableQuantityResponse {AvailableQuantity = 0};

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetProductAvailableQuantity(COMPANY_CODE, "TUBE","1"))
                            .IgnoreArguments()
                            .Return(data);
            

            var result = _controller.GetProductAvailableQuantity(COMPANY_CODE, "TUBE","1");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            result = _controller.GetProductAvailableQuantity(COMPANY_CODE, "TUBE","1");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductFamilyTypeTest()
        {
            
            SetMockDataForProductModels();

            mockProductInventoryManager.Stub(x => x.GetProductFamilyType(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(new Model.Response.ProductFamilyTypeResponse());

            var result = _controller.GetProductFamilyType(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductFamilyTypeExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductFamilyTypeResponse {FamilyType = "Spare Parts"};

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetProductFamilyType(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);
            

            var result = _controller.GetProductFamilyType(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            result = _controller.GetProductFamilyType(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductLineTypeTest()
        {
            
            SetMockDataForProductModels();

            mockProductInventoryManager.Stub(x => x.GetProductLineType(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(new Model.Response.ProductLineTypeResponse());
            var result = _controller.GetProductLineType(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductLineTypeExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductLineTypeResponse {LineType = "xyz"};

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetProductLineType(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);
            

            var result = _controller.GetProductLineType(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            result = _controller.GetProductLineType(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductStockStatusTest()
        {
            
            SetMockDataForProductModels();

            var data = new Model.Response.ProductStockStatusResponse {Stockable = 1};

            //errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            //data.ErrorInfo.AddRange(errorsList);

            mockProductInventoryManager.Stub(x => x.IsProductStockable(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetProductStockStatus(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductStockStatusExceptionTest()
        {
            
            SetMockDataForProductModels();

            var data = new Model.Response.ProductStockStatusResponse {Stockable = 1};

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.IsProductStockable(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);
            

            var result = _controller.GetProductStockStatus(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            result = _controller.GetProductStockStatus(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductActiveStatusTest()
        {
            
            SetMockDataForProductModels();

            mockProductInventoryManager.Stub(x => x.IsProductActive(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(new Model.Response.ProductStatusResponse());
            var result = _controller.GetProductActiveStatus(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductActiveStatusExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductStatusResponse {IsActive = true};

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.IsProductActive(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);
            

            var result = _controller.GetProductActiveStatus(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            result = _controller.GetProductActiveStatus(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductAvailableQuantityForAllLocationTest()
        {
            
            SetMockDataForProductModels();

            mockProductInventoryManager.Stub(x => x.GetProductAvailableQuantityForAllLocation(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(new Model.Response.ProductAvailableQuantityForAllLocationResponse());

            var result = _controller.GetProductAvailableQuantityForAllLocation(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductAvailableQuantityForAllLocationExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductAvailableQuantityForAllLocationResponse();
            data.ProductList.AddRange(new List<ProductAvailableQuantity> ());

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetProductAvailableQuantityForAllLocation(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetProductAvailableQuantityForAllLocation(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            result = _controller.GetProductAvailableQuantityForAllLocation(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductStockBalanceQuantityForAllLocationTest()
        {
            
            SetMockDataForProductModels();

            mockProductInventoryManager.Stub(x => x.GetProductStockBalanceQuantityForAllLocation(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(new Model.Response.ProductStockBalanceQuantityForAllLocationResponse());
            var result = _controller.GetProductStockBalanceQuantityForAllLocation(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetProductStockBalanceQuantityForAllLocationExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.ProductStockBalanceQuantityForAllLocationResponse();
            data.LocationList.AddRange(new List<ProductLocationModel>());

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetProductStockBalanceQuantityForAllLocation(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);
            var result = _controller.GetProductStockBalanceQuantityForAllLocation(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            result = _controller.GetProductStockBalanceQuantityForAllLocation(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationwiseProductStockBalanceQuantityTest()
        {
            
            SetMockDataForProductModels();

            mockProductInventoryManager.Stub(x => x.GetLocationwiseProductStockBalanceQuantity(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(new Model.Response.LocationwiseProductStockBalanceQuantityResponse());

            var result = _controller.GetLocationwiseProductStockBalanceQuantity(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetLocationwiseProductStockBalanceQuantityExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.LocationwiseProductStockBalanceQuantityResponse();
            data.ProductList.AddRange(new List<LocationWiseProductQuantityModel>());

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetLocationwiseProductStockBalanceQuantity(COMPANY_CODE, "TUBE"))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetLocationwiseProductStockBalanceQuantity(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);

            result = _controller.GetLocationwiseProductStockBalanceQuantity(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationwiseProductAvailableQuantityTest()
        {
            
            SetMockDataForProductModels();

            var data = new Model.Response.LocationwiseProductAvailableQuantityResponse();
            data.ProductList.AddRange(new List<LocationWiseProductAvailableQuantityModel>());

            mockProductInventoryManager.Stub(x => x.GetLocationwiseProductAvailableQuantity("bh", "01"))
                            .IgnoreArguments()
                            .Return(data);
            var result = _controller.GetLocationwiseProductAvailableQuantity(COMPANY_CODE, "1");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(System.Exception))]
        public void GetLocationwiseProductAvailableQuantityExceptionTest()
        {
            
            SetMockDataForProductModels();
            var data = new Model.Response.LocationwiseProductAvailableQuantityResponse();
            data.ProductList.AddRange(new List<LocationWiseProductAvailableQuantityModel>());

            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockProductInventoryManager.Stub(x => x.GetLocationwiseProductAvailableQuantity("bh", "01"))
                            .IgnoreArguments()
                            .Return(data);
            var result = _controller.GetLocationwiseProductAvailableQuantity(COMPANY_CODE, "TUBE");
            Assert.IsNotNull(result);

            var responses = new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            result = _controller.GetLocationwiseProductAvailableQuantity(COMPANY_CODE, "TUBE");

            Assert.IsNotNull(result);
        }

        #endregion

        #region MockData Methods

        public void MockControllerRequest()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/products");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForProductModels()
        {
            #region SampleCustomerInformationModelList

            // _productInventoryModel.ProductMasterDetails.add

            _productInventoryModel.ProductMasterDetails = new ProductMasterModel
            {
                ProductCode = "007-08831-000",
                Description = "TUBE-",
                //sc01003 = "",
                Family = "ESGS"
                //SVMXC_Stockable_c = 0,
                //sc01106 = 0.00000,
                //sc01120 = 0,
                //sc01135 = 0,
            };

            _productWarehouseList.Add(new ProductWarehouseModel()
            {
                ERP_Company_Code__c = "bh",
                //Name = "",
                ERP_Location_ID__c = "1"
            });
            _productInventoryModel.StockItemWarehouse.AddRange(_productWarehouseList);



            //_customerInformationModelList.Add(new CustomerInformationModel()
            //{
            //    ERP_Customer_Code_c = "C001",
            //    AccountName = "Customer 2",
            //    //AddressLine1 = "Commer Zone",
            //    //AddressLine2 = "Brad Pitt, Eric Bana, Orlando Bloom",
            //    //AddressLine3 = "Pune",
            //    //AddressLine4 = "AddressLine4",
            //    BillingCity = "CityPune Code",
            //    Phone = "9876543210",
            //    CurrencyIsoCode = "INR",
            //    BillingPostalCode = "428201"
            //});
            //_customerInformationModelList.Add(new CustomerInformationModel()
            //{
            //    ERP_Customer_Code_c = "C002",
            //    AccountName = "Customer 1",
            //    //AddressLine1 = "Commer Zone 21",
            //    //AddressLine2 = "qwerty Brad Pitt, Eric Bana, Orlando Bloom",
            //    //AddressLine3 = "Mumbai",
            //    //AddressLine4 = "AddressLine4 1",
            //    BillingCity = "Mumbai",
            //    Phone = "9876987210",
            //    CurrencyIsoCode = "USD",
            //    BillingPostalCode = "426201"
            //});
            #endregion
            #region SampleDataProduct
            _stockItemsEnttiesList.Add(new ItemWarehouse
            {
                sc03001 = "007-08831-000",
                sc03002 = "01",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });

            _stockItemsEnttiesList.Add(new ItemWarehouse
            {
                //sc01001 = "007-08831-000",
                //sc01002 = "TUBE-",
                //sc01003 = "",
                //sc01037 = "ESGS",
                //sc01066 = 0,
                //sc01106 = 0.00000,
                //sc01120 = 0,
                //sc01135 = 0,
                sc03001 = "007-08831-000",
                sc03002 = "70",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });

            new StockItemMaster
            {
                sc01001 = "007-08831-000",
                sc01002 = "TUBE-",
                sc01003 = "",
                sc01037 = "ESGS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0"
            };
            #endregion
        }
        #endregion
    }
}
