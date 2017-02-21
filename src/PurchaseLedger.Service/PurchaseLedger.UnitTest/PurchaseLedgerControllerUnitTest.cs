using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseLedger.API;
using PurchaseLedger.API.Controllers;
using PurchaseLedger.API.Filters;
using PurchaseLedger.BusinessLayer;
using PurchaseLedger.BusinessLayer.Interfaces;
using PurchaseLedger.Common.Error;
using PurchaseLedger.Model.Models;
using PurchaseLedger.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Linq;
using Rhino.Mocks;

namespace PurchaseLedger.UnitTest
{
    [TestClass]
    public class PurchaseLedgerControllerUnitTest
    {
        #region Declarations

        private IPurchaseLedgerManager _purchaseLedgerManager,_mockRepository;
        private PurchaseLedgerController _controller;

        private string _companyCode = "na";
        private string _supplierCode = "1020";
        private string _supplierName = "AL MOAYYED AIRCONDITIONING";
        private string _orderno = "4931000056";
        private string _invoiceNo = "1234";
        private string _invoicestartdate = "2015-06-10";
        private string _invoiceenddate = "2015-10-10";
        private string _duestartdate = "2014-07-30";
        private string _dueenddate = "2014-07-30";

        readonly PurchaseLedgersResponse _purchaseLedgersResponse = new PurchaseLedgersResponse() { Suppliers = new List<Supplier>() };
        readonly PurchaseLedgerResponse _purchaseLedgerResponse = new PurchaseLedgerResponse();
        readonly List<PurchaseLedgerModel> _purchaseLedgerModelList = new List<PurchaseLedgerModel>();

        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        #endregion

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _purchaseLedgerManager = new PurchaseLedgerManager(null);
            _controller = new PurchaseLedgerController(_purchaseLedgerManager) { Request = new HttpRequestMessage() };
            _mockRepository = MockRepository.GenerateMock<IPurchaseLedgerManager>();
            SetMockDataForPurchaseLedgerModelList();
            SetMockDataForSuppliers();
        }

        #endregion

        #region Unit Test Methods..........
        [TestMethod]
        //Unit Testing Get CreditStatus By CompanyCode, CustomerCode  and LedgerFlag
        public void GetPurchaseLedgerTest()
        {
            var result = _controller.GetServiceName();
            Assert.IsNotNull(result);
        }


        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(PurchaseLedgerController);
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
        public void UnityConfigUnitTest()
        {
            UnityConfig.RegisterComponents(new HttpConfiguration());
        }

        [TestMethod]
        public void GetPurchaseLedgerByCompanyCodeTest()
        {
           
            // Positive Scenario with success response
            var response = new PurchaseLedgersResponse() { Suppliers = _purchaseLedgersResponse.Suppliers };
            _mockRepository.Stub(x => x.GetPurchaseLedgerByCompanyCode(_companyCode))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseLedgerByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseLedgerByCompanyCode(_companyCode))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseLedgerByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetPurchaseLedgerBySupplierCodeTest()
        {

            // Positive Scenario with success response
            var response = new PurchaseLedgersResponse() { Suppliers = _purchaseLedgersResponse.Suppliers };
            _mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetPurchaseLedgerBySupplierNameTest()
        {

            // Positive Scenario with success response
            var response = new PurchaseLedgersResponse() { Suppliers = _purchaseLedgersResponse.Suppliers };
            _mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetPurchaseLedgerByInvoiceNoTest()
        {

            // Positive Scenario with success response
            var response = new PurchaseLedgersResponse() { Suppliers = _purchaseLedgersResponse.Suppliers };
            _mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNo))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNo);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNo))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNo);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetPurchaseLedgerByOrderNo()
        {

            // Positive Scenario with success response
            var response = new PurchaseLedgersResponse() { Suppliers = _purchaseLedgersResponse.Suppliers };
            _mockRepository.Stub(x => x.GetPurchaseLedgerByOrderNo(_companyCode, _orderno))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseLedgerByOrderNo(_companyCode, _orderno);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseLedgerByOrderNo(_companyCode, _orderno))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseLedgerByOrderNo(_companyCode, _orderno);
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void GetPurchaseLedgerByInvoiceDateRange()
        {

            // Positive Scenario with success response
            var response = new PurchaseLedgersResponse() { Suppliers = _purchaseLedgersResponse.Suppliers };
            _mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoicestartdate, _invoiceenddate))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoicestartdate, _invoiceenddate);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoicestartdate, _invoiceenddate))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoicestartdate, _invoiceenddate);
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void GetPurchaseLedgerByDueDateRange()
        {

            // Positive Scenario with success response
            var response = new PurchaseLedgersResponse() { Suppliers = _purchaseLedgersResponse.Suppliers };
            _mockRepository.Stub(x => x.GetPurchaseLedgerByDueDateRange(_companyCode, _duestartdate,_dueenddate))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseLedgerByDueDateRange(_companyCode, _duestartdate, _dueenddate);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseLedgerByDueDateRange(_companyCode, _duestartdate, _dueenddate))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseLedgerByDueDateRange(_companyCode, _duestartdate, _dueenddate);
            Assert.IsNotNull(result);

        }

        #endregion.........................

        #region Mock data creation.........
        public void MockControllerRequestTestData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/purchaseledger");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "taxinvoice" } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/purchaseledger");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        private void MockController(IPurchaseLedgerManager iPurchaseLedgerManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/purchaseledger");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "taxinvoice" } });
            _controller = new PurchaseLedgerController(iPurchaseLedgerManager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        #region SamplePurchaseLedgerModelList
        public void SetMockDataForSuppliers()
        {
            _purchaseLedgersResponse.Suppliers.ToList().Add(new Supplier()
            {
                SupplierCode = "1001",
                SupplierName = "AL MOAYYED AIRCONDITIONING",
                PurchaseLedgers = new List<PurchaseLedgerModel>() { _purchaseLedgerResponse.Supplier }
            });
        }
        #endregion

        #region SamplePurchaseLedgerModel..
        public void SetMockDataForPurchaseLedgerModelList()
        {
            _purchaseLedgerResponse.Supplier = new PurchaseLedgerModel()
                {
                    InvoiceNo = "1028288",
                    OrderNo = "4930000042",
                    InvoiceDate = "2015-10-06",
                    DueDate = "2015-11-30",
                    LatePaymDate = "2016-05-16",
                    InvoiceAmt = 400.0M,
                    SalesTaxAmt = 0.0M,
                    PaidAmt = 400.0M,
                    InvoicePaidFlag = "",
                    Discount = 0.0M
                };
            _purchaseLedgerResponse.Supplier = new PurchaseLedgerModel()
            {
                InvoiceNo = "1028289",
                OrderNo = "4930000042",
                InvoiceDate = "2015-10-07",
                DueDate = "2015-11-20",
                LatePaymDate = "2016-05-16",
                InvoiceAmt = 600.0M,
                SalesTaxAmt = 0.0M,
                PaidAmt = 600.0M,
                InvoicePaidFlag = "",
                Discount = 0.0M
            };
        }

        #endregion.........................


        #endregion.........................
    }
}
