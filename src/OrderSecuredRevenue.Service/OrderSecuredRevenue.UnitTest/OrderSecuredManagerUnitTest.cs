using System.Collections.Generic;
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
        //private const string InvoiceNumber = "1100001";
        private readonly List<OR03> _salesOrderList = new List<OR03>();
        private readonly List<OR01> _salesOrderHeadList = new List<OR01>();
        private SalesOrderModel _salesOrderModel = new SalesOrderModel();
        private readonly List<SalesOrderDetailsLineModel> _salesOrderDetailsList = new List<SalesOrderDetailsLineModel>();
        private SalesOrderDetailsModel _salesOrderDetails = new SalesOrderDetailsModel();


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
        public void GetOrderDetailsByOrderNumberTest()
        {
            SetMockData();
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            var data = new Model.Responses.SalesOrderDetailsByOrderNoResponse { SalesOrderDetails = _salesOrderDetails };


            mockRepository.Stub(x => x.GetSalesOrderDetailsByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(_salesOrderHeadList);

            mockRepository.Stub(x => x.GetSalesOrderLineDetailsByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(_salesOrderList);

            _orderSecureManager = new OrderSecuredRevenueManager(mockRepository);

            var result = _orderSecureManager.GetOrderDetailsByOrderNumber(CompanyCode, OrderNumber);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderTypeByOrderNumberTest()
        {
            SetMockData();
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            var data = new Model.Responses.OrderTypeByOrderNoResponse { OrderType="Invoice" };


            mockRepository.Stub(x => x.GetSalesOrderDetailsByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(_salesOrderHeadList);

            mockRepository.Stub(x => x.GetSalesOrderLineDetailsByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(_salesOrderList);

            _orderSecureManager = new OrderSecuredRevenueManager(mockRepository);

            var result = _orderSecureManager.GetOrderTypeByOrderNumber(CompanyCode, OrderNumber);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderDeliveryDateByOrderNumberTest()
        {
            SetMockData();
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            var data = new Model.Responses.OrderDeliveryDateByOrderNoResponse { DeliveryDate = System.DateTime.Now };


            mockRepository.Stub(x => x.GetSalesOrderDetailsByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(_salesOrderHeadList);

            mockRepository.Stub(x => x.GetSalesOrderLineDetailsByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(_salesOrderList);

            _orderSecureManager = new OrderSecuredRevenueManager(mockRepository);

            var result = _orderSecureManager.GetOrderDeliveryDateByOrderNumber(CompanyCode, OrderNumber);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderSecuredRevenueByInvoiceNumberTest()
        {
            //SetMockData();
            //var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();


            //mockRepository.Stub(x => x.GetOrderSecuredRevenueByInvoiceNumber(CompanyCode, OrderNumber))
            //                .IgnoreArguments()
            //                .Return(_salesOrderList);

            //_orderSecureManager = new OrderSecuredRevenueManager(mockRepository);

            //var result = _orderSecureManager.GetOrderSecuredRevenueByInvoiceNumber(CompanyCode, OrderNumber);
            //Assert.IsNotNull(result);
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
                            .Return(null);

            
            mockRepository.Stub(x => x.GetSalesOrderDetailsByOrderNumber(CompanyCode, OrderNumber))
                 .IgnoreArguments()
                 .Return(null);


            mockRepository.Stub(x => x.GetSalesOrderLineDetailsByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(null);

            _orderSecureManager = new OrderSecuredRevenueManager(mockRepository);

            var resultByOrder = _orderSecureManager.GetOrderSecuredRevenueByOrderNumber(string.Empty,"");
            var resultOrderDetailsByOrderNo = _orderSecureManager.GetOrderDetailsByOrderNumber(string.Empty, "");
            var resultOrderTypeByOrderNo = _orderSecureManager.GetOrderTypeByOrderNumber(string.Empty, "");
            var resultOrderDeliveryDateByOrderNo = _orderSecureManager.GetOrderDeliveryDateByOrderNumber(string.Empty, "");

            Assert.IsNotNull(resultByOrder.ErrorInfo);
            Assert.IsNotNull(resultOrderDetailsByOrderNo.ErrorInfo);
            Assert.IsNotNull(resultOrderTypeByOrderNo.ErrorInfo);
            Assert.IsNotNull(resultOrderDeliveryDateByOrderNo.ErrorInfo);
        }

        /// <summary>
        /// Unit Test for GetCustomer with null request Object.
        /// </summary>
        [TestMethod]
        public void GetOrderDetailsByOrderNumberNegativeTest()
        {
            var mockOrderRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockData();
            //var data = new Model.Responses.SalesOrderDetailsByOrderNoResponse { SalesOrderDetails = salesOrderDetails };

            mockOrderRepository.Stub(x => x.GetSalesOrderDetailsByOrderNumber(CompanyCode, OrderNumber))
                 .IgnoreArguments()
                 .Return(_salesOrderHeadList);


            mockOrderRepository.Stub(x => x.GetSalesOrderLineDetailsByOrderNumber(CompanyCode, OrderNumber))
                            .IgnoreArguments()
                            .Return(null);

            _orderSecureManager = new OrderSecuredRevenueManager(mockOrderRepository);

            var resultOrderDetailsByOrderNo = _orderSecureManager.GetOrderDetailsByOrderNumber(string.Empty, "");

            Assert.IsNotNull(resultOrderDetailsByOrderNo.ErrorInfo);
        }


        private void SetMockData()
        {
            _salesOrderModel = new SalesOrderModel() {
                OrderNumber= "4937000001",
                DeliveryDate = "21-Jun-1999",
                OrderType="6",
                InvoiceNo="012345"
            };

            _salesOrderModel.SalesOrderLineDetailsList.AddRange(_salesOrderDetailsList);

            _salesOrderDetailsList.Add(new SalesOrderDetailsLineModel()
            {
                Order_Number = "4937000001",
                Line_Number = "10",
                Line_Type = "1",
                Unit_Price = "6686.4",
                Unit_Cost_Price = "6686.4",
                Qty_Ordered = "-1"
            });

            _salesOrderDetails = new SalesOrderDetailsModel() {
                SalesOrderDetails = _salesOrderModel
            };

            _salesOrderList.Add(new OR03()
            {
                or03001 = "4937000001",
                or03002 = "10",
                or03004 = "1",
                or03008 = "6686.4",
                or03009 = "6686.4",
                or03011 = "-1"
                //or21065 = "280001"
            });

            _salesOrderHeadList.Add(new OR01()
            {
                or01001 = "4937000001",
                or01002 = "10",
                or01004 = "1"
                //or21065 = "280001"
            });

        }
    }
}
