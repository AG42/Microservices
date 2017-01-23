using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseOrder.BusinessLayer;
using PurchaseOrder.BusinessLayer.Interfaces;
using PurchaseOrder.Common.Error;
using PurchaseOrder.Controllers;
using PurchaseOrder.Filters;
using PurchaseOrder.Model.Models;
using PurchaseOrder.Model.Response;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace PurchaseOrder.UnitTest
{
    [TestClass]
    public class PurchaseOrderControllerUnitTest
    {
        #region Declarations

        private IPurchaseOrderManager _purchaseorderManager, _mockRepository;
        private PurchaseOrderController _controller;
        
        readonly PurchaseOrderResponse _purchaseOrderResponse = new PurchaseOrderResponse();
        readonly PurchaseOrdersResponse _purchaseOrdersResponse = new PurchaseOrdersResponse() { PurchaseOrders = new List<PurchaseOrderModel>() };
        readonly PurchaseOrderCustomersResponse _purchaseCustOrdersResponse = new PurchaseOrderCustomersResponse()
                    { PurchaseOrderCustomers = new List<PurchaseOrderCustomerModel>() };

        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        private const string _companyCode = "na";
        private const string _purchaseOrderNo = "";
        private const string _orderType = "";
        private const string _productNumber = "";
        private const string _startDate = "";
        private const string _endDate = "";
        private const string _customerName = "";

        #endregion


        #region Initialization
        [TestInitialize]
        public void Initialization()
        {
            _purchaseorderManager = new PurchaseOrderManager(null);
            _controller = new PurchaseOrderController(_purchaseorderManager) { Request = new HttpRequestMessage() };
            _mockRepository = MockRepository.GenerateMock<IPurchaseOrderManager>();
        }
        #endregion


        #region Unit Tests methods

        [TestMethod]
        public void GetPurchaseOrderByCompanyCodeTest()
        {
            // Positive Scenario with success response
            SetMockDataPurchaseOrderModelList();
            var response = new PurchaseOrdersResponse() { PurchaseOrders = _purchaseOrdersResponse.PurchaseOrders };
            _mockRepository.Stub(x => x.GetPurchaseOrderByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseOrderByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
            
            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseOrderByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseOrderByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetPurchaseOrderByPurchaseOrderNumberTest()
        {
            // Positive Scenario with success response
            SetMockDataPurchaseOrderModel();
            var response = new PurchaseOrderResponse() { PurchaseOrder = _purchaseOrderResponse.PurchaseOrder };
            _mockRepository.Stub(x => x.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNo))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNo);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNo))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNo);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrderByOrderTypeTest()
        {
            // Positive Scenario with success response
            SetMockDataPurchaseOrderModelList();
            var response = new PurchaseOrdersResponse() { PurchaseOrders = _purchaseOrdersResponse.PurchaseOrders };
            _mockRepository.Stub(x => x.GetPurchaseOrderByOrderType(_companyCode, _orderType))
                            .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseOrderByOrderType(_companyCode, _orderType);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseOrderByOrderType(_companyCode, _orderType))
                            .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseOrderByOrderType(_companyCode, _orderType);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetPurchaseOrderByCustomerNameTest()
        {
            // Positive Scenario with success response
            SetMockDataPurchaseCustomerOrderModelList();
            var response = new PurchaseOrderCustomersResponse() { PurchaseOrderCustomers = _purchaseCustOrdersResponse.PurchaseOrderCustomers };
            _mockRepository.Stub(x => x.GetPurchaseOrdersByCustomerName(_companyCode, _customerName))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseOrderByCustomerName(_companyCode, _customerName);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseOrdersByCustomerName(_companyCode, _customerName))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseOrderByCustomerName(_companyCode, _customerName);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetPurchaseOrderByProjectNumberTest()
        {
            // Positive Scenario with success response
            SetMockDataPurchaseOrderModelList();
            var response = new PurchaseOrdersResponse() { PurchaseOrders = _purchaseOrdersResponse.PurchaseOrders };
            _mockRepository.Stub(x => x.GetPurchaseOrderByProjectNumber(_companyCode, _productNumber))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseOrderByProjectNumber(_companyCode, _productNumber);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseOrderByProjectNumber(_companyCode, _productNumber))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseOrderByProjectNumber(_companyCode, _productNumber);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetPurchaseOrderByOrderDateRangeTest()
        {
            // Positive Scenario with success response
            SetMockDataPurchaseOrderModelList();
            var response = new PurchaseOrdersResponse() { PurchaseOrders = _purchaseOrdersResponse.PurchaseOrders };
            _mockRepository.Stub(x => x.GetPurchaseOrderByOrderDateRange(_companyCode, _startDate, _endDate))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseOrderByOrderDateRange(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseOrderByOrderDateRange(_companyCode, _startDate, _endDate))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseOrderByOrderDateRange(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetPurchaseOrderByDeliveryDateRangeTest()
        {
            // Positive Scenario with success response
            SetMockDataPurchaseOrderModelList();
            var response = new PurchaseOrdersResponse() { PurchaseOrders = _purchaseOrdersResponse.PurchaseOrders };
            _mockRepository.Stub(x => x.GetPurchaseOrderByDeliveryDateRange(_companyCode, _startDate, _endDate))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetPurchaseOrderByDeliveryDateRange(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetPurchaseOrderByDeliveryDateRange(_companyCode, _startDate, _endDate))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetPurchaseOrderByDeliveryDateRange(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(PurchaseOrderController);
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
        public void GetPurchaseOrderServiceName()
        {
            var result = _controller.GetServiceName();
            Assert.IsNotNull(result);
        }
        #endregion


        #region Mock Methods

        /// <summary>
        /// 
        /// </summary>
        private void MockControllerRequestTestData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/purchaseorder");
            var route = config.Routes.MapHttpRoute("Default", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "purchase" } });

            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }


        /// <summary>
        /// 
        /// </summary>
        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/purchaseorder");
            var route = config.Routes.MapHttpRoute("Default", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null } });

            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPurchaseOrderManager"></param>
        private void MockController(IPurchaseOrderManager iPurchaseOrderManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/purchaseorder");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "purchaseorder" } });
            _controller = new PurchaseOrderController(iPurchaseOrderManager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }


        public void SetMockDataPurchaseOrderModelList()
        {
            _purchaseOrdersResponse.PurchaseOrders.ToList().Add(
                new PurchaseOrderModel()
                {
                    PurchaseOrderNo = "4930000009",
                    OrderType = "Back Order",
                    SupplierCode = "1395",
                    CustomerCodeDelivery = "+",
                    Remarks = "",
                    PrintStatusPurchaseOrder = "Document Sent",
                    PrintStatusRemindConfirm = "Document Not Ready For Printing",
                    PrintStatusRemindShipment = "Document Not Ready For Printing",
                    InvoiceEntered = "1",
                    PaymentTerms = "77",
                    DeliveryTerms = "0",
                    DeliveryMethod = "0",
                    OrderDate = "2014-07-10 00:00:00.0",
                    DeliveryDate = "2015-08-11 00:00:00.0",
                    OrderDiscount = "0.00",
                    OrderValue = "9849.91",
                    CurrncyCode = "0",
                    PurchaseCode = "CPS_HK",
                    ProjectNumber = "",
                    CustomerPurchaseOrderNo = "",
                    CustomerRequestNo = "",
                    LinkedToProject = "NotLinkedToProject"
                });
            _purchaseOrdersResponse.PurchaseOrders.ToList().Add(
                new PurchaseOrderModel()
                {
                    PurchaseOrderNo = "4930000010",
                    OrderType = "Back Order",
                    SupplierCode = "1395",
                    CustomerCodeDelivery = "+",
                    Remarks = "",
                    PrintStatusPurchaseOrder = "Document Sent",
                    PrintStatusRemindConfirm = "Document Not Ready For Printing",
                    PrintStatusRemindShipment = "Document Not Ready For Printing",
                    InvoiceEntered = "1",
                    PaymentTerms = "77",
                    DeliveryTerms = "0",
                    DeliveryMethod = "0",
                    OrderDate = "2014-07-10 00:00:00.0",
                    DeliveryDate = "2015-08-11 00:00:00.0",
                    OrderDiscount = "0.00",
                    OrderValue = "9849.91",
                    CurrncyCode = "0",
                    PurchaseCode = "CPS_HK",
                    ProjectNumber = "",
                    CustomerPurchaseOrderNo = "",
                    CustomerRequestNo = "",
                    LinkedToProject = "NotLinkedToProject"
                });
            _purchaseOrdersResponse.PurchaseOrders.ToList().Add(
                new PurchaseOrderModel()
                {
                    PurchaseOrderNo = "4930000012",
                    OrderType = "Back Order",
                    SupplierCode = "1398",
                    CustomerCodeDelivery = "+",
                    Remarks = "",
                    PrintStatusPurchaseOrder = "Document Sent",
                    PrintStatusRemindConfirm = "Document Not Ready For Printing",
                    PrintStatusRemindShipment = "Document Not Ready For Printing",
                    InvoiceEntered = "1",
                    PaymentTerms = "69",
                    DeliveryTerms = "0",
                    DeliveryMethod = "0",
                    OrderDate = "2015-01-15 00:00:00.0",
                    DeliveryDate = "2015-10-19 00:00:00.0",
                    OrderDiscount = "0.00",
                    OrderValue = "28716.36",
                    CurrncyCode = "10",
                    PurchaseCode = "CPS_HK",
                    ProjectNumber = "",
                    CustomerPurchaseOrderNo = "",
                    CustomerRequestNo = "",
                    LinkedToProject = "NotLinkedToProject"
                });
        }

        public void SetMockDataPurchaseOrderModel()
        {
            _purchaseOrderResponse.PurchaseOrder = new PurchaseOrderModel()
            {
                PurchaseOrderNo = "4930000012",
                OrderType = "Back Order",
                SupplierCode = "1398",
                CustomerCodeDelivery = "+",
                Remarks = "",
                PrintStatusPurchaseOrder = "Document Sent",
                PrintStatusRemindConfirm = "Document Not Ready For Printing",
                PrintStatusRemindShipment = "Document Not Ready For Printing",
                InvoiceEntered = "1",
                PaymentTerms = "69",
                DeliveryTerms = "0",
                DeliveryMethod = "0",
                OrderDate = "2015-01-15 00:00:00.0",
                DeliveryDate = "2015-10-19 00:00:00.0",
                OrderDiscount = "0.00",
                OrderValue = "28716.36",
                CurrncyCode = "10",
                PurchaseCode = "CPS_HK",
                ProjectNumber = "",
                CustomerPurchaseOrderNo = "",
                CustomerRequestNo = "",
                LinkedToProject = "NotLinkedToProject"
            };
        }

        public void SetMockDataPurchaseCustomerOrderModelList()
        {
            _purchaseCustOrdersResponse.PurchaseOrderCustomers.Add(new PurchaseOrderCustomerModel()
            {
                CustomerName = "Johnson",
                TelephoneNo = "123456789",
                PurchaseOrders = _purchaseOrdersResponse.PurchaseOrders
            });

            _purchaseCustOrdersResponse.PurchaseOrderCustomers.Add(new PurchaseOrderCustomerModel()
            {
                CustomerName = "Johnson1",
                TelephoneNo = "123456781",
                PurchaseOrders = _purchaseOrdersResponse.PurchaseOrders
            });
        }

        #endregion
    }
}