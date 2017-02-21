using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesOrder.DataLayer.Interfaces;
using SalesOrder.BusinessLayer;
using SalesOrder.DataLayer.Entities.Datalake;
using System.Collections.Generic;
using SalesOrder.DataLayer;
using Rhino.Mocks;
using System;

namespace SalesOrder.UnitTest
{
    [TestClass]
    public class SalesOrderManagerUnitTest
    {
        IDataLayerContext _dataLayerContext;
        SalesOrderManager _salesOrderManager;
        public List<Or01> SalesOrderModelList;
        private const string CompanyCode = "na";
        private const string CustomerInvoiceCode = "AKCAS00";
        private const string SalesOrderNumber = "3141006994";
        private const string SalesOrderType = "4";
        private const string FlagPickList = "1";
        readonly string _startDate = DateTime.Now.ToString("dd-MMM-yyyy");
        readonly string _endDate = DateTime.Now.ToString("dd-MMM-yyyy");

        [TestInitialize]
        public void Initialization()
        {
            SalesOrderModelList = new List<Or01>();
            _dataLayerContext = new DataLayerContext();
            _salesOrderManager = new SalesOrderManager(_dataLayerContext);
            SetMockDataForSalesOrderModelList();
        }


        [TestMethod]
        public void GetSalesOrderByCompanyCodeTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByCompanyCode(CompanyCode))
                            .IgnoreArguments()
                            .Return(SalesOrderModelList);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            var result = _salesOrderManager.GetSalesOrderByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);

            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByCompanyCode(CompanyCode))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByCompanyCode(CompanyCode))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByCompanyCode(CompanyCode);
            Assert.IsNull(result.SalesOrders);
        }


        [TestMethod]
        public void GetSalesOrderBySalesOrderNumberTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderBySalesOrderNumber(CompanyCode, SalesOrderNumber))
                            .IgnoreArguments()
                            .Return(SalesOrderModelList);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            var result = _salesOrderManager.GetSalesOrderBySalesOrderNumber(CompanyCode, SalesOrderNumber);
            Assert.IsNotNull(result);

            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderBySalesOrderNumber(CompanyCode, SalesOrderNumber))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderBySalesOrderNumber(string.Empty, SalesOrderNumber);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderBySalesOrderNumber(CompanyCode, SalesOrderNumber))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderBySalesOrderNumber(CompanyCode, SalesOrderNumber);
            Assert.IsNull(result.SalesOrders);
        }


        [TestMethod]
        public void GetSalesOrderByOrderTypeTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByOrderType(CompanyCode, SalesOrderType))
                            .IgnoreArguments()
                            .Return(SalesOrderModelList);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            var result = _salesOrderManager.GetSalesOrderByOrderType(CompanyCode, SalesOrderType);
            Assert.IsNotNull(result);

            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByOrderType(CompanyCode, SalesOrderType))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByOrderType(string.Empty, SalesOrderType);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByOrderType(CompanyCode, SalesOrderType))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByOrderType(CompanyCode, SalesOrderType);
            Assert.IsNull(result.SalesOrders);
        }


        [TestMethod]
        public void GetSalesOrderByCustomerInvoiceCodeTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode))
                            .IgnoreArguments()
                            .Return(SalesOrderModelList);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            var result = _salesOrderManager.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode);
            Assert.IsNotNull(result);

            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByCustomerInvoiceCode(string.Empty, CustomerInvoiceCode);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByCustomerInvoiceCode(CompanyCode, CustomerInvoiceCode);
            Assert.IsNull(result.SalesOrders);
        }


        [TestMethod]
        public void GetSalesOrderByFlagPickListTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList))
                            .IgnoreArguments()
                            .Return(SalesOrderModelList);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            var result = _salesOrderManager.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Success);


            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByFlagPickList(string.Empty, FlagPickList);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByFlagPickList(CompanyCode, FlagPickList);
            Assert.IsNull(result.SalesOrders);
        }


        [TestMethod]
        public void GetSalesOrderByOrderDateRangeTest()
        {
            //...Positive unit test case 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByOrderDateRange(CompanyCode, _startDate, _endDate))
                            .IgnoreArguments()
                            .Return(SalesOrderModelList);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            var result = _salesOrderManager.GetSalesOrderByOrderDateRange(CompanyCode, _startDate, _endDate);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByOrderDateRange(CompanyCode, _startDate, _endDate))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByOrderDateRange(string.Empty, _startDate, _endDate);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByOrderDateRange(CompanyCode, _startDate, _endDate))
                          .IgnoreArguments()
                          .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByOrderDateRange(CompanyCode, _startDate, _endDate);
            Assert.IsNull(result.SalesOrders);
        }


        [TestMethod]
        public void GetSalesOrderByDeliveryDateRangeTest()
        {
            //...Positive unit test case 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByDeliveryDateRange(CompanyCode, _startDate, _endDate))
                            .IgnoreArguments()
                            .Return(SalesOrderModelList);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            var result = _salesOrderManager.GetSalesOrderByDeliveryDateRange(CompanyCode, _startDate, _endDate);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByDeliveryDateRange(CompanyCode, _startDate, _endDate))
                            .IgnoreArguments()
                            .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByDeliveryDateRange(string.Empty, _startDate, _endDate);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetSalesOrderByDeliveryDateRange(CompanyCode, _startDate, _endDate))
                          .IgnoreArguments()
                          .Return(null);
            _salesOrderManager = new SalesOrderManager(mockRepository);
            result = _salesOrderManager.GetSalesOrderByDeliveryDateRange(CompanyCode, _startDate, _endDate);
            Assert.IsNull(result.SalesOrders);
        }


        #region Mock Methods
        void SetMockDataForSalesOrderModelList()
        {
            SalesOrderModelList.Add(new Or01()
            {
                Or01001 = "3141006994",
                Or01002 = "4",
                Or01003 = "INTRSERV",
                Or01008 = "1",
                Or01015 = "01-01-2013",
                Or01016 = "05-01-2013",
                Or03002 = "50",
                Or03006 = "GASKET",
                Or03011 = "2",
                Or03012 = "0",
                Or03021 = "1"
            });
            SalesOrderModelList.Add(new Or01()
            {
                Or01001 = "3141006994",
                Or01002 = "4",
                Or01003 = "INTRSERV",
                Or01008 = "1",
                Or01015 = "02-02-1013",
                Or01016 = "07-02-2013",
                Or03002 = "10",
                Or03006 = "WATER",
                Or03011 = "1",
                Or03012 = "0",
                Or03021 = "1"
            });
            SalesOrderModelList.Add(new Or01()
            {
                Or01001 = "3146000528",
                Or01002 = "1",
                Or01003 = "AKCAS00",
                Or01008 = "1",
                Or01015 = "15-12-2015",
                Or01016 = "25-12-2015",
                Or03002 = "20",
                Or03006 = "INSURANCE",
                Or03011 = "2",
                Or03012 = "0",
                Or03021 = "0"
            });
        }
        #endregion
    }
}