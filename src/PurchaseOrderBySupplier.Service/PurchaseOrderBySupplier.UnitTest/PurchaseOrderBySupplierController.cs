using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseOrderBySupplier.API;
using PurchaseOrderBySupplier.API.Controllers;
using PurchaseOrderBySupplier.BusinessLayer;
using PurchaseOrderBySupplier.BusinessLayer.Interfaces;
using PurchaseOrderBySupplier.Common.Error;
using PurchaseOrderBySupplier.Model.Response;
using System;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PurchaseOrderBySupplier.API.Filters;
using PurchaseOrderBySupplier.Model.Models;
using System.Web.Http;
using System.Web.Http.Hosting;
using Rhino.Mocks;
using PurchaseOrderBySupplier.DataLayer.Entities.Datalake;
using System.Web.Http.ExceptionHandling;
using System.Threading;

namespace PurchaseOrderBySupplier.UnitTest
{
    [TestClass]
    public class PurchaseOrderBySupplierControllerTest
    {
        #region Declarations
        private IPurchaseOrderBySupplierManager _purchaseOrderBySupplierManager;
        private PurchaseOrderBySupplierController _controller;

        private string _companyCode = "na";
        private const string _supplierInvoiceNumber = "01984289";
        private const string _supplierCode = "J0017";
        private const string _vatCode = "0";
        readonly PurchaseOrderBySupplierResponse _purchaseOrderBySupplierResponse = new PurchaseOrderBySupplierResponse();
        readonly PurchaseOrdersBySupplierResponse _purchaseOrdersBySupplierResponse = new PurchaseOrdersBySupplierResponse();
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();
        private readonly List<Pc12> _pc012 = new List<Pc12>();
        #endregion

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(null);
            _controller = new PurchaseOrderBySupplierController(_purchaseOrderBySupplierManager) { Request = new HttpRequestMessage() };
        }

        #endregion
        #region Unit Test Methods
        [TestMethod]
        //Unit Testing Get PurchaseOrders By CompanyCode 
        public void GetPurchaseOrdersByCompanyCodeTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IPurchaseOrderBySupplierManager>();
            SetPurchaseOrdersBySupplier(); 
            var response = new PurchaseOrdersBySupplierResponse { PurchaseOrdersBySupplier = _purchaseOrdersBySupplierResponse.PurchaseOrdersBySupplier };
            mockRepository.Stub(x => x.GetPurchaseOrdersByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetPurchaseOrdersByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IPurchaseOrderBySupplierManager>();
            response = new PurchaseOrdersBySupplierResponse { PurchaseOrdersBySupplier = _purchaseOrdersBySupplierResponse.PurchaseOrdersBySupplier };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetPurchaseOrdersByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetPurchaseOrdersByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        //Unit Testing Get PurchaseOrders By SupplierInvoiceNumber
        public void GetPurchaseOrdersBySupplierInvoiceNumberTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IPurchaseOrderBySupplierManager>();
            SetPurchaseOrdersBySupplier(); 
            var response = new PurchaseOrdersBySupplierResponse { PurchaseOrdersBySupplier = _purchaseOrdersBySupplierResponse.PurchaseOrdersBySupplier };
            mockRepository.Stub(x => x.GetPurchaseOrdersBySupplierInvoiceNumber(_companyCode, _supplierInvoiceNumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetPurchaseOrdersBySupplierInvoiceNumber(_companyCode, _supplierInvoiceNumber);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IPurchaseOrderBySupplierManager>();
            response = new PurchaseOrdersBySupplierResponse { PurchaseOrdersBySupplier = _purchaseOrdersBySupplierResponse.PurchaseOrdersBySupplier };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetPurchaseOrdersBySupplierInvoiceNumber(_companyCode, _supplierInvoiceNumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetPurchaseOrdersBySupplierInvoiceNumber(_companyCode, _supplierInvoiceNumber);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        //Unit Testing Get PurchaseOrders By SupplierCode
        public void GetPurchaseOrdersBySupplierCodeTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IPurchaseOrderBySupplierManager>();
            SetPurchaseOrdersBySupplier();
            var response = new PurchaseOrdersBySupplierResponse { PurchaseOrdersBySupplier = _purchaseOrdersBySupplierResponse.PurchaseOrdersBySupplier };
            mockRepository.Stub(x => x.GetPurchaseOrdersBySupplierCode(_companyCode, _supplierCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetPurchaseOrdersBySupplierCode(_companyCode, _supplierCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IPurchaseOrderBySupplierManager>();
            response = new PurchaseOrdersBySupplierResponse { PurchaseOrdersBySupplier = _purchaseOrdersBySupplierResponse.PurchaseOrdersBySupplier };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetPurchaseOrdersBySupplierCode(_companyCode, _supplierCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetPurchaseOrdersBySupplierCode(_companyCode, _supplierCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get PurchaseOrders By VATCode
        public void GetPurchaseOrdersByVATCodeTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IPurchaseOrderBySupplierManager>();
            SetPurchaseOrdersBySupplier();
            var response = new PurchaseOrdersBySupplierResponse { PurchaseOrdersBySupplier = _purchaseOrdersBySupplierResponse.PurchaseOrdersBySupplier };
            mockRepository.Stub(x => x.GetPurchaseOrdersByVATCode(_companyCode, _vatCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetPurchaseOrdersByVATCode(_companyCode, _vatCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IPurchaseOrderBySupplierManager>();
            response = new PurchaseOrdersBySupplierResponse { PurchaseOrdersBySupplier = _purchaseOrdersBySupplierResponse.PurchaseOrdersBySupplier };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetPurchaseOrdersByVATCode(_companyCode, _vatCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetPurchaseOrdersByVATCode(_companyCode, _vatCode);
            Assert.IsNotNull(result);
        }

       
        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(PurchaseOrderBySupplierController);
            ValidationFilter obj = new ValidationFilter();
            MockControllerRequestTestData();
            obj.OnActionExecuting(_controller.ActionContext);
            Assert.IsTrue(t.GetCustomAttributes(typeof(ValidationFilter), true).Length > 0);
            // Negative Scenario
            MockControllerRequestFilterNegativeData();
            obj.OnActionExecuting(_controller.ActionContext);
            Assert.IsNotNull(_controller.ActionContext.Response);
        }

        [TestMethod]
        //Unit Testing Get CreditStatus By CompanyCode, CustomerCode  and LedgerFlag
        public void GetPurchaseOrdersBySupplierServiceName()
        {
            var result = _controller.GetServiceName();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UnityConfigUnitTest()
        {
            UnityConfig.RegisterComponents(new System.Web.Http.HttpConfiguration());

        }
        #endregion
        #region Mock Methods

        public void MockControllerRequestTestData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/purchaseorderbysupplier");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "purchaseorderbysupplier" } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/purchaseorderbysupplier");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private void MockController(IPurchaseOrderBySupplierManager ipurchaseordermanager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/ordersecuredcost");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "ordersecuredcost" } });
            _controller = new PurchaseOrderBySupplierController(ipurchaseordermanager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        #endregion
        #region SamplePurchaseOrderBySupplierModelList
        public void SetPurchaseOrdersBySupplier()
        {
            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0017",
                //Purchase Order Number               
                Pc12001 = "*********3",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });

            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0017",
                //Purchase Order Number               
                Pc12001 = "*********2",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });

            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0018",
                //Purchase Order Number               
                Pc12001 = "*********2",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });

            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0018",
                //Purchase Order Number               
                Pc12001 = "*********4",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });
        }
        #endregion
    }
}
