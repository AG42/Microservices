using System.Collections.Generic;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderSecuredRevenue.DataLayer.Interfaces;
using OrderSecuredRevenue.BusinessLayer;
using OrderSecuredRevenue.Model;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using Rhino.Mocks;

namespace OrderSecuredRevenue.UnitTest
{
    [TestClass]
    public class OrderSecuredManagerUnitTest
    {
        private OrderSecuredRevenueManager _orderSecureManager;
        private IDatabaseContext _idatabaseContext;
        private const string CompanyCode = "xh";
        private const string OrderNumber = "000001";
        private const string InvoiceNumber = "1100001";
        public List<OR21> _salesOrderList = new List<OR21>();

        [TestInitialize]
        public void Initialize()
        {
            _orderSecureManager = new OrderSecuredRevenueManager(_idatabaseContext);    
        }

        [TestMethod]
        public void GetOrderSecuredRevenueByOrderNumberTest()
        {
            SetMockData();
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            

            mockRepository.Stub(x => x.GetOrderSecuredRevenueByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(_salesOrderList);

            _orderSecureManager = new OrderSecuredRevenueManager(mockRepository);

            var result = _orderSecureManager.GetOrderSecuredRevenueByOrderNumber(CompanyCode, OrderNumber);
            Assert.IsNotNull(result);

         }

        [TestMethod]
        public void GetOrderSecuredRevenueByInvoiceNumberTest()
        {
            SetMockData();
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();


            mockRepository.Stub(x => x.GetOrderSecuredRevenueByInvoiceNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(_salesOrderList);

            _orderSecureManager = new OrderSecuredRevenueManager(mockRepository);

            var result = _orderSecureManager.GetOrderSecuredRevenueByInvoiceNumber(CompanyCode, OrderNumber);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetCustomer with null request Object.
        /// </summary>
        [TestMethod]
        public void GetOrderSecuredRevenueByOrderNumberNegativeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetOrderSecuredRevenueByOrderNumber(string.Empty,""))
                            .IgnoreArguments()
                            .Return(new List<OR21>());

            _orderSecureManager = new OrderSecuredRevenueManager(mockRepository);

           
            var resultByOrder = _orderSecureManager.GetOrderSecuredRevenueByOrderNumber(string.Empty,"");
            Assert.AreEqual(resultByOrder.OrderSecuredRevenueModels.Count, 0);

            mockRepository.Stub(x => x.GetOrderSecuredRevenueByInvoiceNumber(string.Empty, ""))
                .IgnoreArguments()
                .Return(null);

            _orderSecureManager = new OrderSecuredRevenueManager(mockRepository);


            var resultByinvoice = _orderSecureManager.GetOrderSecuredRevenueByInvoiceNumber(string.Empty, "");
            Assert.AreEqual(resultByinvoice.OrderSecuredRevenueModels.Count, 0);


         
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
