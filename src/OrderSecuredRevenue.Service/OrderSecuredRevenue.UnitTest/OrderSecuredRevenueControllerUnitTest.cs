﻿using System.Net.Http;
using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderSecuredRevenue.API.Controllers;
using OrderSecuredRevenue.BusinessLayer.Interface;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using OrderSecuredRevenue.Common.Error;

namespace OrderSecuredRevenue.UnitTest
{
    [TestClass]
    public class OrderSecuredRevenueControllerUnitTest
    {
        #region Declarations
        private IOrderSecuredRevenueManager _mockRepository;
        private OrderSecuredRevenueController _controller;
        private const string CompanyCode = "xh";
        private const string OrderNo = "C001";
        //private const string InvoiceNumber = "13123123";
        //readonly List<OrderSecuredRevenueModel> _orderSecuredRevenueModelList = new List<OrderSecuredRevenueModel>();
        private readonly List<OR03> _salesOrderList = new List<OR03>();
        readonly List<ErrorInfo> _errorInfoList = new List<ErrorInfo>();

        #endregion


        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = MockRepository.GenerateMock<IOrderSecuredRevenueManager>();
            _controller = new OrderSecuredRevenueController(_mockRepository) { Request = new HttpRequestMessage() };
            _controller.Request = new HttpRequestMessage();
            _controller.Request.SetConfiguration(new HttpConfiguration());
            _controller.Request = new HttpRequestMessage(HttpMethod.Get,"");
        }
        /// <summary>
        /// Unit Test for GetOrderSecuredRevenueByOrderNumber controller method
        /// </summary>
        [TestMethod]
        public void GetOrderSecuredRevenueByOrderNumberTest()
        {
            SetMockData();
            var data = new Model.Responses.OrderSecuredRevenueByOrderNoResponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);

            _mockRepository.Stub(x => x.GetOrderSecuredRevenueByOrderNumber(CompanyCode,OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderSecuredRevenueByOrderNumber(CompanyCode,OrderNo);
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Negative Unit Test for GetOrderSecuredRevenueByOrderNumber controller method
        /// </summary>
        [TestMethod]
        public void GetOrderSecuredRevenueByOrderNumberNegTest()
        {
            SetMockData();
            var data = new Model.Responses.OrderSecuredRevenueByOrderNoResponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);
            data.ErrorInfo.AddRange(_errorInfoList);

            _mockRepository.Stub(x => x.GetOrderSecuredRevenueByOrderNumber(CompanyCode, OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderSecuredRevenueByOrderNumber(CompanyCode, OrderNo);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetOrderDetailsByOrderNumber controller method
        /// </summary>
        [TestMethod]
        public void GetOrderDetailsByOrderNumberTest()
        {
            SetMockData();
            var data = new Model.Responses.SalesOrderDetailsByOrderNoResponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);

            _mockRepository.Stub(x => x.GetOrderDetailsByOrderNumber(CompanyCode,OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderDetailsByOrderNumber(CompanyCode,OrderNo);
            Assert.IsNotNull(result);
        }
        /// <summary>
        ///Negative Unit Test for GetOrderDetailsByOrderNumber controller method
        /// </summary>
        [TestMethod]
        public void GetOrderDetailsByOrderNumberNegTest()
        {
            SetMockData();
            var data = new Model.Responses.SalesOrderDetailsByOrderNoResponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);
            data.ErrorInfo.AddRange(_errorInfoList);

            _mockRepository.Stub(x => x.GetOrderDetailsByOrderNumber(CompanyCode, OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderDetailsByOrderNumber(CompanyCode, OrderNo);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetOrderTypeByOrderNumber controller method
        /// </summary>
        [TestMethod]
        public void GetOrderTypeByOrderNumberTest()
        {
            SetMockData();
            var data = new Model.Responses.OrderTypeByOrderNoResponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);

            _mockRepository.Stub(x => x.GetOrderTypeByOrderNumber(CompanyCode,OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderTypeByOrderNumber(CompanyCode,OrderNo);
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Negative Unit Test for GetOrderTypeByOrderNumber controller method
        /// </summary>
        [TestMethod]
        public void GetOrderTypeByOrderNumberNegTest()
        {
            SetMockData();
            var data = new Model.Responses.OrderTypeByOrderNoResponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);
            data.ErrorInfo.AddRange(_errorInfoList);

            _mockRepository.Stub(x => x.GetOrderTypeByOrderNumber(CompanyCode, OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderTypeByOrderNumber(CompanyCode, OrderNo);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetOrderDeliveryDateByOrderNumber controller method
        /// </summary>
        [TestMethod]
        public void GetOrderDeliveryDateByOrderNumberTest()
        {
            SetMockData();
            var data = new Model.Responses.OrderDeliveryDateByOrderNoResponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);

            _mockRepository.Stub(x => x.GetOrderDeliveryDateByOrderNumber(CompanyCode,OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderDeliveryDateByOrderNumber(CompanyCode,OrderNo);
            Assert.IsNotNull(result);
        }
        /// <summary>
        ///Negative Unit Test for GetOrderDeliveryDateByOrderNumber controller method
        /// </summary>
        [TestMethod]
        public void GetOrderDeliveryDateByOrderNumberNegTest()
        {
            SetMockData();
            var data = new Model.Responses.OrderDeliveryDateByOrderNoResponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);
            data.ErrorInfo.AddRange(_errorInfoList);

            _mockRepository.Stub(x => x.GetOrderDeliveryDateByOrderNumber(CompanyCode, OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderDeliveryDateByOrderNumber(CompanyCode, OrderNo);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetCustomers controller method
        /// </summary>
        [TestMethod]
        public void GetserviceNameTest()
        {
            SetMockData();
            //var data = new Model.Responses.OrderSecuredRevenueByInvoiceNumberReponse();
            //data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);

            //_mockRepository.Stub(x => x.GetOrderSecuredRevenueByInvoiceNumber(CompanyCode, OrderNo))
            //                .IgnoreArguments()
            //                .Return(data);

            var result = _controller.GetServiceName();
            Assert.IsNotNull(result);

        }
        private void SetMockData()
        {
            _salesOrderList.Add(new OR03()
            {
                or03001 = "4937000001",
                or03002 = "10",
                or03004 = "1",
                or03008 = "6686.4",
                or03009 = "6686.4",
                or03011 = "-1"
            });

            _errorInfoList.Add(new ErrorInfo("Execption"));

           // _orderSecuredRevenueModelList.Add(new OrderSecuredRevenueModel() { Company_Code="bh", Order_Number="0001", Qty_Ordered="10" });
        }
    }
}
