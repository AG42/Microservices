using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderSecuredCost.API.Controllers;
using OrderSecuredCost.BusinessLayer.Interface;
using OrderSecuredCost.API.Filters;
using OrderSecuredCost.Common.Error;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Linq;
using OrderSecuredCost.API;
using OrderSecuredCost.BusinessLayer;
using OrderSecuredCost.Model.Response;
using System.Net.Http;
using System.Web.Http.Hosting;
using OrderSecuredCost.Model;
using System;

namespace OrderSecuredCost.UnitTest
{
    [TestClass]
    public class OrderSecuredCostControllerTest
    {
        #region Declarations

        private IOrderSecuredCostManager _orderSecuredCostManager;
        private OrderSecuredCostController _controller;

        private string _companyCode = "na";      
        private string _purchOrderNo = "4930000007";
        private string _orderType = "1";
        private string _orderStartDate = "01-01-2014";
        private string _orderEndDate = "01-01-2017";
        private string _deliveryStartDate = "01-01-2014";
        private string _deliveryEndDate = "01-01-2017";       
        private string _userID = "CKHOURM";
        private string _minOrderCost = "0";
        private string _maxOrderCost = "0";


        readonly OrderSecuredCostResponse _orderSecuredCostResponse = new OrderSecuredCostResponse();
        readonly OrderSecuredCostsResponse _orderSecuredCostsResponse = new OrderSecuredCostsResponse() { OrderSecuredCosts = new List<OrderSecuredCostModel>() };
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        #endregion

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _orderSecuredCostManager = new OrderSecuredCostManager(null);
            _controller = new OrderSecuredCostController(_orderSecuredCostManager) { Request = new HttpRequestMessage() };
        }

        #endregion

        #region Unit Test Methods

        [TestMethod]
        //Unit Testing Get OrderSecuredCost By CompanyCode 
        public void GetOrderSecuredCostByCompanyCodeTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            SetMockDataForOrderSecuredCostsModelList();
            var response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            mockRepository.Stub(x => x.GetOrderSecuredCostByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredCostByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetOrderSecuredCostByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredCostByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get OrderSecuredCost By PurchaseOrderNumber 
        public void GetOrderSecuredCostByPurchaseOrderNumberTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            SetMockDataForOrderSecuredCostModelList();
            var response = new OrderSecuredCostResponse { OrderSecuredCost = _orderSecuredCostResponse.OrderSecuredCost };
            mockRepository.Stub(x => x.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchOrderNo)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchOrderNo);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            response = new OrderSecuredCostResponse { OrderSecuredCost = _orderSecuredCostResponse.OrderSecuredCost };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchOrderNo)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchOrderNo);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get OrderSecuredCost By OrderType 
        public void GetOrderSecuredCostByOrderTypeTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            SetMockDataForOrderSecuredCostsModelList();
            var response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderType(_companyCode,_orderType)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredCostByOrderType(_companyCode,_orderType);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderType(_companyCode,_orderType)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredCostByOrderType(_companyCode,_orderType);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get OrderSecuredCost By Order Date Range 
        public void GetOrderSecuredCostByOrderDateRangeTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            SetMockDataForOrderSecuredCostsModelList();
            var response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderDateRange(_companyCode,_orderStartDate,_orderEndDate)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get OrderSecuredCost By Delivery Date Range  
        public void GetOrderSecuredCostByDeliveryDateRangeTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            SetMockDataForOrderSecuredCostsModelList();
            var response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            mockRepository.Stub(x => x.GetOrderSecuredCostByDeliveryDateRange(_companyCode,_deliveryStartDate, _deliveryEndDate)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get OrderSecuredCost By UserId 
        public void GetOrderSecuredCostByUserIDTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            SetMockDataForOrderSecuredCostsModelList();
            var response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            mockRepository.Stub(x => x.GetOrderSecuredCostByUserID(_companyCode,_userID)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredCostByUserID(_companyCode,_userID);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetOrderSecuredCostByUserID(_companyCode, _userID)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredCostByUserID(_companyCode, _userID);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get OrderSecuredCost By CompanyCode 
        public void GetOrderSecuredCostByOrderCostRangeTest()
        {
            //// Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            SetMockDataForOrderSecuredCostsModelList();
            var response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderCostRange(_companyCode,_minOrderCost,_maxOrderCost)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderSecuredCostManager>();
            response = new OrderSecuredCostsResponse { OrderSecuredCosts = _orderSecuredCostsResponse.OrderSecuredCosts };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(OrderSecuredCostController);
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
        public void GetOrderSecuredCostServiceName()
        {
            var result = _controller.GetServiceName();
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void UnityConfigUnitTest()
        {
            UnityConfig.RegisterComponents(new HttpConfiguration());
           
        }

        #endregion

        #region Mock Methods

        public void MockControllerRequestTestData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/creditstatus");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "contract" } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/creditstatus");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private void MockController(IOrderSecuredCostManager iCreditStatusManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/creditstatus");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "creditstatus" } });
            _controller = new OrderSecuredCostController(iCreditStatusManager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        #endregion

        #region SampleOrderSecuredCostModelList
        public void SetMockDataForOrderSecuredCostsModelList()
        {

            _orderSecuredCostsResponse.OrderSecuredCosts.ToList().Add(new OrderSecuredCostModel()
            {
                PurchOrderNo = "4930000007",
                OrderType = "2",
                OrderDate = "07-07-2014",
                DeliveryDate = "11-18-2015",
                Remark = "",
                UserID = "CKHOURM",
                OrderSecuredCost = "0"
            });

            _orderSecuredCostsResponse.OrderSecuredCosts.Add(new OrderSecuredCostModel()
            {
                PurchOrderNo = "4930000008",
                OrderType = "1",
                OrderDate = "08-07-2014",
                DeliveryDate = "12-21-2015",
                Remark = "",
                UserID = "CKHOURMA",
                OrderSecuredCost = "0"
            });
        }
        public void SetMockDataForOrderSecuredCostModelList()
        {
            _orderSecuredCostResponse.OrderSecuredCost = new OrderSecuredCostModel()
            {
                PurchOrderNo = "4930000007",
                OrderType = "2",
                OrderDate = "07-07-2014",
                DeliveryDate = "11-18-2015",
                Remark = "",
                UserID = "CKHOURM",
                OrderSecuredCost = "0"
            };
        }
        #endregion

        #region Filter Code Coverage

        #endregion

        #region Startup Code Coverage
        [TestMethod]
        public void StartUpMethodTest()
        {
           // var mockRepository = MockRepository.GenerateMock<>();           
         
        }
        #endregion

    }
}
