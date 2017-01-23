using System;
using System.Net.Http;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedSecuredMargin.BusinessLayer.Interfaces;
using OrderedSecuredMargin.API.Controllers;
using OrderedSecuredMargin.Model.Response;
using System.Collections.Generic;
using OrderedSecuredMargin.Common.Error;
using OrderedSecuredMargin.BusinessLayer;
using OrderedSecuredMargin.Model.Models;
using OrderedSecuredMargin.DataAccessLayer.Entities.Datalake;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using Rhino.Mocks;
using OrderedSecuredMargin.API.Filters;
using OrderedSecuredMargin.API;

namespace OrderedSecuredMargin.UnitTest
{
    [TestClass]
    public class OrdereSecureMarginControllerUnitTest
    {

        #region  Declartions

        private IOrderedSecuredMarginManager _orderSecureMarginManager;

        private OrderedSecuredMarginController _controller;

        private string _companyCode = "na";
        private string _orderNumber = "";
        

        readonly OrderSecureMarginResponse _orderSecureMarginResponse = new OrderSecureMarginResponse();
        OrderedSecuredMarginResponse _orderedSecuredMarginResponse = new OrderedSecuredMarginResponse() { orderedSecuredMargin = new List<OrderSecureMarginModel>() };
        readonly List<ErrorInfo> _errorList = new List<ErrorInfo>();

        #endregion

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _orderSecureMarginManager = new OrderedSecuredMarginManager(null);
            _controller = new OrderedSecuredMarginController(_orderSecureMarginManager) { Request = new HttpRequestMessage() };
        }

        #endregion

        #region UnitTestMethods
        [TestMethod]
        public void GetOrderSecureMarginByCompanyCodeTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderedSecuredMarginManager>();
            SetMockDataForOrderSecureMarginList();
            var response = new OrderedSecuredMarginResponse { orderedSecuredMargin = _orderedSecuredMarginResponse.orderedSecuredMargin };
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredMarginByCompanyCode(_companyCode);
            var controller = _controller.GetOrderedSecuredMargin();
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderedSecuredMarginManager>();
            response = new OrderedSecuredMarginResponse { orderedSecuredMargin = _orderedSecuredMarginResponse.orderedSecuredMargin };
            _errorList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorList);
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredMarginByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }
        [TestMethod]

        public void GetOrderSecureMarginByOrderNoTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderedSecuredMarginManager>();
            SetMockDataForOrderSecureMarginList();
            var response = new OrderSecureMarginResponse { orderSecureMargin = _orderSecureMarginResponse.orderSecureMargin };
            mockRepository.Stub(x => x.GetOrderSecuredMarginByOrderNo(_companyCode, _orderNumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOderSecuredMarginByOrderNo(_companyCode, _orderNumber);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IOrderedSecuredMarginManager>();
            response = new OrderSecureMarginResponse { orderSecureMargin = _orderSecureMarginResponse.orderSecureMargin };
            _errorList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorList);
            mockRepository.Stub(x => x.GetOrderSecuredMarginByOrderNo(_companyCode, _orderNumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOderSecuredMarginByOrderNo(_companyCode, _orderNumber);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetOrderSecureMarginByCostTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IOrderedSecuredMarginManager>();
            SetMockDataForOrderSecureMarginList();
            var response = new OrderedSecuredMarginResponse { orderedSecuredMargin = _orderedSecuredMarginResponse.orderedSecuredMargin };
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCost(_companyCode, 123.1M, 345.4M)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetOrderSecuredMarginByCost(_companyCode, 123.1M, 345.4M);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response

            mockRepository = MockRepository.GenerateMock<IOrderedSecuredMarginManager>();
            response = new OrderedSecuredMarginResponse { orderedSecuredMargin = _orderedSecuredMarginResponse.orderedSecuredMargin };
            _errorList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorList);
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCost(_companyCode, 123.1M, 345.4M)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetOrderSecuredMarginByCost(_companyCode, 123.1M, 345.4M);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(OrderedSecuredMarginController);
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





        #endregion

        #region Mock Methods

        public void MockControllerRequestTestData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/OrderSecureMargin");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "contract" } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
           
        }
        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/OrderSecureMargin");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private void MockController(IOrderedSecuredMarginManager iOrderSecureManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/OrderSecureMargin");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "creditstatus" } });
            _controller = new OrderedSecuredMarginController(iOrderSecureManager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        #endregion



        #region MockData
        public void SetMockDataForOrderSecureMarginList()
        {
            _orderedSecuredMarginResponse.orderedSecuredMargin.Add(new OrderSecureMarginModel()
            {
                OrderNo = "4823937000007",
                MarginPercentage = 170.470M
            });




        }
        #endregion



    }
}
