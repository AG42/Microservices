using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderSecuredCost.DataLayer.Entities.Datalake;
using OrderSecuredCost.DataLayer.Interfaces;
using OrderSecuredCost.BusinessLayer;
using System.Collections.Generic;
using OrderSecuredCost.DataLayer;
using Rhino.Mocks;
using OrderSecuredCost.Common.Enum;

namespace OrderSecuredCost.UnitTest
{
    [TestClass]
    public class OrderSecuredCostManagerUnitTest
    {
        #region Declarations

        private IDataLayerContext _iDataLayer;
        private OrderSecuredCostManager _orderSecuredCostManager;
        private string _companyCode = "na";
        private string _deliveryStartDate = "2015-08-11";
        private string _deliveryEndDate = "2015-11-18";
        private string _minOrderCost= "9849.91";
        private string _maxOrderCost = "97492.89";
        private string _orderStartDate = "2014-07-07";
        private string _orderEndDate = "2015-01-15";
        private string _orderType = "1";
        private string _purchaseOrderNumber = "4930000009";
        private string _userId = "CKHOURM";

        public Pc01 OrderSecuredCostModel = new Pc01();
        public List<Pc01> OrderSecuredCostList = new List<Pc01>();

        #endregion

        #region Initializes
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _iDataLayer = new DataLayerContext();
            _orderSecuredCostManager = new OrderSecuredCostManager(_iDataLayer);
        }
        #endregion

        #region Test methods

        [TestMethod]
        public void GetCreditStatusByCompanyCodeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForOrderSecuredCostList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetOrderSecuredCostByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            var result = _orderSecuredCostManager.GetOrderSecuredCostByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);


            //...Negative unit test case : Output list is null
            OrderSecuredCostList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByCompanyCode(_companyCode))
                         .IgnoreArguments()
                         .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByCompanyCode(_companyCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByCompanyCode(_companyCode))
                          .IgnoreArguments()
                          .Return(null);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByCompanyCode(_companyCode);
            Assert.IsTrue(result.OrderSecuredCosts == null);

        }

        [TestMethod]

        public void GetOrderSecuredCostByDeliveryDateRange()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForOrderSecuredCostList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate))
                            .IgnoreArguments()
                            .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            var result = _orderSecuredCostManager.GetOrderSecuredCostByDeliveryDateRange(_companyCode,_deliveryStartDate,_deliveryEndDate);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByDeliveryDateRange(string.Empty, _deliveryStartDate, _deliveryEndDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : _deliveryStartDate empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByDeliveryDateRange(_companyCode, string.Empty, _deliveryEndDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure); 

                  //...Negative unit test case : _deliveryEndDate empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByDeliveryDateRange(string.Empty, _deliveryStartDate, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            OrderSecuredCostList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate))
                         .IgnoreArguments()
                         .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate))
                          .IgnoreArguments()
                          .Return(null);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate);
            Assert.IsTrue(result.OrderSecuredCosts == null);

        }

        [TestMethod]

        public void GetOrderSecuredCostByOrderCostRange()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForOrderSecuredCostList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost))
                            .IgnoreArguments()
                            .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            var result = _orderSecuredCostManager.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderCostRange(string.Empty, _minOrderCost, _maxOrderCost);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : _minOrderCost empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderCostRange(_companyCode, string.Empty, _maxOrderCost);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : _maxOrderCost empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderCostRange(string.Empty, _minOrderCost, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            OrderSecuredCostList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost))
                         .IgnoreArguments()
                         .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost))
                          .IgnoreArguments()
                          .Return(null);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderCostRange(_companyCode, _minOrderCost, _maxOrderCost);
            Assert.IsTrue(result.OrderSecuredCosts == null);

        }


        [TestMethod]

        public void GetOrderSecuredCostByOrderDateRange()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForOrderSecuredCostList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate))
                            .IgnoreArguments()
                            .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            var result = _orderSecuredCostManager.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderDateRange(string.Empty, _orderStartDate, _orderEndDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : _orderStartDate empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderDateRange(_companyCode, string.Empty, _orderEndDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : _orderEndDate empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderDateRange(string.Empty, _orderStartDate, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            OrderSecuredCostList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate))
                         .IgnoreArguments()
                         .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate))
                          .IgnoreArguments()
                          .Return(null);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate);
            Assert.IsTrue(result.OrderSecuredCosts == null);

        }

        [TestMethod]

        public void GetOrderSecuredCostByOrderType()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForOrderSecuredCostList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderType(_companyCode, _orderType))
                            .IgnoreArguments()
                            .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            var result = _orderSecuredCostManager.GetOrderSecuredCostByOrderType(_companyCode, _orderType);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderType(string.Empty, _orderType);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : _orderType empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderType(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

          

            //...Negative unit test case : Output list is null
            OrderSecuredCostList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderType(_companyCode, _orderType))
                         .IgnoreArguments()
                         .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderType(_companyCode, _orderType);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByOrderType(_companyCode, _orderType))
                          .IgnoreArguments()
                          .Return(null);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByOrderType(_companyCode, _orderType);
            Assert.IsTrue(result.OrderSecuredCosts == null);

        }

        [TestMethod]

        public void GetOrderSecuredCostByPurchaseOrderNumber()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForOrderSecuredCostList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber))
                            .IgnoreArguments()
                            .Return(OrderSecuredCostModel);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            var result = _orderSecuredCostManager.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByPurchaseOrderNumber(string.Empty, _purchaseOrderNumber);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : _purchaseOrderNumber empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber))
                          .IgnoreArguments()
                          .Return(null);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber);
            Assert.IsTrue(result.OrderSecuredCost == null);

        }


        [TestMethod]

        public void GetOrderSecuredCostByUserID()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForOrderSecuredCostList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetOrderSecuredCostByUserID(_companyCode, _userId))
                            .IgnoreArguments()
                            .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            var result = _orderSecuredCostManager.GetOrderSecuredCostByUserID(_companyCode, _userId);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByUserID(string.Empty, _userId);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : _userId empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderSecuredCostManager.GetOrderSecuredCostByUserID(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);



            //...Negative unit test case : Output list is null
            OrderSecuredCostList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByUserID(_companyCode, _userId))
                         .IgnoreArguments()
                         .Return(OrderSecuredCostList);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByUserID(_companyCode, _userId);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredCostByUserID(_companyCode, _userId))
                          .IgnoreArguments()
                          .Return(null);
            _orderSecuredCostManager = new OrderSecuredCostManager(mockRepository);
            result = _orderSecuredCostManager.GetOrderSecuredCostByUserID(_companyCode, _userId);
            Assert.IsTrue(result.OrderSecuredCosts == null);

        }
        #endregion

        #region MockData Methods
        public void SetMockDataForOrderSecuredCostModel()
        {
            #region SampleOrderSecuredCostModel
            OrderSecuredCostModel = new Pc01()
            {
                //PurchOrderNo
                Pc01001 = "2434534",

                //OrderType
                Pc01002 = "0",

                //OrderDate
                Pc01015 = "2011-02-17 00:00:00.0",

                //DelDate
                Pc01016 = "2011-02-17 00:00:00.0",

                //OrderDiscount
                Pc01019 = "0",

                //OrderValue
                Pc01020 = "1312.2345",

                //ExtRemark1
                Pc01060 = "bacd",

                //InvoiceNumber
                //Pc01066 = "46576576",

                //UserID
                Pc01073 = "45634",

                //FreightAmount
                Pc01088 = "0",

                //PackingAmount
                Pc01089 = "56.678",

                //InsuranceAmount
                Pc01090 = "45.345",

                //InvoicingFee
                Pc01091 = "33",
            };
            #endregion
        }


        public void SetMockDataForOrderSecuredCostList()
        {
            #region SampleOrderSecuredCostList
            OrderSecuredCostList.Add(new Pc01()
            {
                //PurchOrderNo
                Pc01001 = "2434534",

                //OrderType
                Pc01002 = "0",

                //OrderDate
                Pc01015 = "2011-02-17 00:00:00.0",

                //DelDate
                Pc01016 = "2011-02-17 00:00:00.0",

                //OrderDiscount
                Pc01019 = "0",

                //OrderValue
                Pc01020 = "1312.2345",

                //ExtRemark1
                Pc01060 = "bacd",

                //InvoiceNumber
                //Pc01066 = "46576576",

                //UserID
                Pc01073 = "45634",

                //FreightAmount
                Pc01088 = "0",

                //PackingAmount
                Pc01089 = "56.678",

                //InsuranceAmount
                Pc01090 = "45.345",

                //InvoicingFee
                Pc01091 = "33",
            });
            #endregion
        }
        #endregion
    }
}
