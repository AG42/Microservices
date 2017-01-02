using System.Net.Http;
using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderSecuredRevenue.API.Controllers;
using OrderSecuredRevenue.BusinessLayer.Interface;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using OrderSecuredRevenue.Model;
using OrderSecuredRevenue.Common.Error;
using OrderSecuredRevenue.Common;

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
        private const string InvoiceNumber = "13123123";
        readonly List<OrderSecuredRevenueModel> _orderSecuredRevenueModelList = new List<OrderSecuredRevenueModel>();
        private readonly List<OR21> _salesOrderList = new List<OR21>();

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
        /// Unit Test for GetCustomers controller method
        /// </summary>
        [TestMethod]
        public void GetOrderSecuredRevenueByOrderNumberTest()
        {
            SetMockData();
            var data = new Model.Responses.OrderSecuredRevenueByOrderNoResponse();
            data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);

            _mockRepository.Stub(x => x.GetOrderSecuredRevenueByOrderNumber(CompanyCode,OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderSecuredRevenueByOrderNumber(CompanyCode,OrderNo);
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Unit Test for GetCustomers controller method
        /// </summary>
        [TestMethod]
        public void GetOrderSecuredRevenueByInvoiceNumberTest()
        {
            SetMockData();
            var data = new Model.Responses.OrderSecuredRevenueByInvoiceNumberReponse();
            data.OrderSecuredRevenueModels.AddRange(_orderSecuredRevenueModelList);

            _mockRepository.Stub(x => x.GetOrderSecuredRevenueByInvoiceNumber(CompanyCode, OrderNo))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetOrderSecuredRevenueByInvoiceNumber(CompanyCode, OrderNo);
            Assert.IsNotNull(result);
        }

        private void SetMockData()
        {
            _salesOrderList.Add(new OR21()
            {
                or21001 = "4937000001",
                or21002 = "10",
                or21004 = "1",
                or21008 = "6686.4",
                or21009 = "6686.4",
                or21011 = "-1",
                or21065 = "280001"
            });
        }
    }
}
