using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesOrder.BusinessLayer.Interfaces;
using SalesOrder.API.Controllers;
using SalesOrder.Model.Response;
using SalesOrder.Common.Error;
using System.Collections.Generic;
using SalesOrder.BusinessLayer;
using System.Net.Http;
using Rhino.Mocks;
using SalesOrder.Model.Models;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Linq;

namespace SalesOrder.UnitTest
{
    [TestClass]
    public class SalesOrderControllerUnitTest
    {
        #region Declarations

        private ISalesOrderManager _salesOrderManager, _mockRepository;
        private SalesOrderController _controller;

        readonly SalesOrdersResponse _salesOrdersResponse = new SalesOrdersResponse() { SalesOrders = new List<SalesOrderHeadModel>() };
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        private const string CompanyCode = "m1";
        private const string OrderNumber = "3526001783";
        private const string OrderType = "Back Order";
        private const string CustomerInvoiceCode = "RB047";
        private const string FlagPickList = "3";
        private const string MinOrderDate = "2012-03-09 00:00:00.0";
        private const string MaxOrderDate = "2014-11-13 00:00:00.0";
        private const string MinDeliveryDate = "2012-03-16 00:00:00.0";
        private const string MaxDeliveryDate = "2014-12-01 00:00:00.0";

        #endregion

        #region Initialization
        [TestInitialize]
        public void Initialization()
        {
            _salesOrderManager = new SalesOrderManager(null);
            _controller = new SalesOrderController(_salesOrderManager) { Request = new HttpRequestMessage() };
            _mockRepository = MockRepository.GenerateMock<ISalesOrderManager>();
        }
        #endregion

        #region Unit Tests methods

        [TestMethod]
        public void GetSalesOrderByCompanyCodeTest()
        {
            // Positive Scenario with success response
            SetMockDataSalesOrderModelList();
            var response = new SalesOrdersResponse();
            _mockRepository.Stub(x => x.GetSalesOrderByCompanyCode(CompanyCode)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetSalesOrderByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetSalesOrderByCompanyCode(CompanyCode)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetSalesOrderByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetSalesOrderBySalesOrderNumberTest()
        {
            // Positive Scenario with success response
            SetMockDataSalesOrderModel();
            var response = new SalesOrdersResponse() { SalesOrders = _salesOrdersResponse.SalesOrders };
            _mockRepository.Stub(x => x.GetSalesOrderBySalesOrderNumber(CompanyCode,OrderNumber))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetSalesOrderBySalesOrderNumber(CompanyCode, OrderNumber);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetSalesOrderBySalesOrderNumber(CompanyCode, OrderNumber))
                        .IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetSalesOrderBySalesOrderNumber(CompanyCode, OrderNumber);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByOrderTypeTest()
        {
            // Positive Scenario with success response
            SetMockDataSalesOrderModelList();
            var response = new SalesOrdersResponse();
            _mockRepository.Stub(x => x.GetSalesOrderByOrderType(CompanyCode, OrderType)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetSalesOrderByOrderType(CompanyCode, OrderType);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetSalesOrderByOrderType(CompanyCode, OrderType)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetSalesOrderByOrderType(CompanyCode, OrderType);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByCustomerInvoiceCodeTest()
        {
            // Positive Scenario with success response
            SetMockDataSalesOrderModel();
            var response = new SalesOrdersResponse();
            _mockRepository.Stub(x => x.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByFlagPickListTest()
        {
            // Positive Scenario with success response
            SetMockDataSalesOrderModelList();
            var response = new SalesOrdersResponse();
            _mockRepository.Stub(x => x.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByOrderDateRangeTest()
        {
            // Positive Scenario with success response
            SetMockDataSalesOrderModelList();
            var response = new SalesOrdersResponse();
            _mockRepository.Stub(x => x.GetSalesOrderByOrderDateRange(CompanyCode, MinOrderDate, MaxOrderDate)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetSalesOrderByOrderDateRange(CompanyCode, MinOrderDate, MaxOrderDate);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetSalesOrderByOrderDateRange(CompanyCode, MinOrderDate, MaxOrderDate)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetSalesOrderByOrderDateRange(CompanyCode, MinOrderDate, MaxOrderDate);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByDeliveryDateRangeTest()
        {
            // Positive Scenario with success response
            SetMockDataSalesOrderModelList();
            var response = new SalesOrdersResponse();
            _mockRepository.Stub(x => x.GetSalesOrderByDeliveryDateRange(CompanyCode, MinDeliveryDate, MaxDeliveryDate)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            var result = _controller.GetSalesOrderByDeliveryDateRange(CompanyCode, MinDeliveryDate, MaxDeliveryDate);
            Assert.IsNotNull(result);

            // Positive Scenario without sucess response
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            _mockRepository.Stub(x => x.GetSalesOrderByDeliveryDateRange(CompanyCode, MinDeliveryDate, MaxDeliveryDate)).IgnoreArguments().Return(response);
            MockController(_mockRepository);
            result = _controller.GetSalesOrderByDeliveryDateRange(CompanyCode, MinDeliveryDate, MaxDeliveryDate);
            Assert.IsNotNull(result);
        }

        #endregion

        #region Mock Methods

        /// <summary>
        /// 
        /// </summary>
        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/salesorder");
            var route = config.Routes.MapHttpRoute("Default", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null } });

            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSalesOrderManager"></param>
        private void MockController(ISalesOrderManager iSalesOrderManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/salesorder");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "purchaseorder" } });
            _controller = new SalesOrderController(iSalesOrderManager)
            {
                Request = new HttpRequestMessage(),
                ControllerContext = new HttpControllerContext(config, routeData, request)
            };
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        public void SetMockDataSalesOrderModelList()
        {
            _salesOrdersResponse.SalesOrders.ToList().Add(
                new SalesOrderHeadModel()
                {
                    OrderNumber= "3521001512",
                    OrderType= "Normal Order",
                    CustomerCodeInvoice= "RK042",
                    FlagPickList="3",
                    OrderDate = "2012-03-09 00:00:00.0",
                    DelivaryDate = "2012-03-16 00:00:00.0",
                    OrderDeliveryStatus = "Partially Delivered",
                    SalesOrderLines = new List<SalesOrderLineModel>() { new SalesOrderLineModel {
                        Description="DIGITAL THERMOSTAT C/W",
                        QuantityOrdered="5",
                        QuantityShipped = "0",
                        QuantityUniOutForDelivary = "1",
                        DeliveryStatus = "Partially Delivered",
                    }
                    }
                });
            _salesOrdersResponse.SalesOrders.ToList().Add(
                new SalesOrderHeadModel()
                {
                    OrderNumber = "3526001050",
                    OrderType = "Back Order",
                    CustomerCodeInvoice = "RD032",
                    FlagPickList = "3",
                    OrderDate = "2013-02-26 00:00:00.0",
                    DelivaryDate = "2013-06-15 00:00:00.0",
                    OrderDeliveryStatus = "Delivered",
                    SalesOrderLines = new List<SalesOrderLineModel>() { new SalesOrderLineModel {
                        Description="TIME-BASED COMMISSIONING",
                        QuantityOrdered="1",
                        QuantityShipped = "0",
                        QuantityUniOutForDelivary = "1",
                        DeliveryStatus = "Delivered",
                    }
                    }
                });
            _salesOrdersResponse.SalesOrders.ToList().Add(
                new SalesOrderHeadModel()
                {
                    OrderNumber = "3526001783",
                    OrderType = "Back Order",
                    CustomerCodeInvoice = "RB047",
                    FlagPickList = "Print Document",
                    OrderDate = "2014-11-13 00:00:00.0",
                    DelivaryDate = "2014-12-01 00:00:00.0",
                    OrderDeliveryStatus = "Partially Delivered",
                    SalesOrderLines = new List<SalesOrderLineModel>() { new SalesOrderLineModel {
                        Description="RJ-11 TEL SOCKET",
                        QuantityOrdered="18",
                        QuantityShipped = "0",
                        QuantityUniOutForDelivary = "1",
                        DeliveryStatus = "Partially Delivered",
                    },
                    new SalesOrderLineModel {
                        Description="RJ45 CAT 5E DATA SOCKET",
                        QuantityOrdered="44",
                        QuantityShipped = "0",
                        QuantityUniOutForDelivary = "1",
                        DeliveryStatus = "Partially Delivered",
                    }
                    }
                });
        }

        public void SetMockDataSalesOrderModel()
        {
            _salesOrdersResponse.SalesOrders.ToList().Add(
                new SalesOrderHeadModel()
                {
                    OrderNumber = "3526001783",
                    OrderType = "Back Order",
                    CustomerCodeInvoice = "RB047",
                    FlagPickList = "Print Document",
                    OrderDate = "2014-11-13 00:00:00.0",
                    DelivaryDate = "2014-12-01 00:00:00.0",
                    OrderDeliveryStatus = "Partially Delivered",
                    SalesOrderLines = new List<SalesOrderLineModel>() { new SalesOrderLineModel {
                        Description="RJ-11 TEL SOCKET",
                        QuantityOrdered="18",
                        QuantityShipped = "0",
                        QuantityUniOutForDelivary = "1",
                        DeliveryStatus = "Partially Delivered",
                    },
                    new SalesOrderLineModel {
                        Description="RJ45 CAT 5E DATA SOCKET",
                        QuantityOrdered="44",
                        QuantityShipped = "0",
                        QuantityUniOutForDelivary = "1",
                        DeliveryStatus = "Partially Delivered",
                    }
                    }
                });
        }

        #endregion

    }
}
