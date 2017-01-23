using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseOrder.BusinessLayer;
using PurchaseOrder.DataLayer;
using PurchaseOrder.DataLayer.Entities.Datalake;
using PurchaseOrder.DataLayer.Interfaces;
using Rhino.Mocks;
using System.Linq;
using System.Collections.Generic;
using PurchaseOrder.Common.Enum;
using System;

namespace PurchaseOrder.UnitTest
{
    [TestClass]
    public class PurchaseOrderManagerUnitTest
    {
        #region Declarations

        private IDataLayerContext _iDataLayer;
        private PurchaseOrderManager _purchaseOrderManager;
        private string _companyCode = "na";
        private string _purchaseOrderNumber = "253464565";
        private string _orderType = "0";
        private string _customerName = "ABCD";
        private string _projectNumber = "346457567";
        private string _deliveryStartDate = DateTime.Now.ToString("dd-MMM-yyyy");
        private string _deliveryEndDate = DateTime.Now.ToString("dd-MMM-yyyy");
        private string _orderStartDate = DateTime.Now.ToString("dd-MMM-yyyy");
        private string _orderEndDate = DateTime.Now.ToString("dd-MMM-yyyy");

        public Pc01 PurchaseOrderModel = new Pc01();
        public List<Pc01> PurchaseOrderList = new List<Pc01>();

        public List<Pc01> PurchaseOrderCustomerList = new List<Pc01>();

        #endregion

        #region Initializes
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _iDataLayer = new DataLayerContext();
            _purchaseOrderManager = new PurchaseOrderManager(_iDataLayer);
        }
        #endregion

        #region--Test Methods......................
        [TestMethod]
        public void GetPurchaseOrderByCompanyCodeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseOrderModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrderByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            var result = _purchaseOrderManager.GetPurchaseOrderByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseOrderList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByCompanyCode(_companyCode))
                         .IgnoreArguments()
                         .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByCompanyCode(_companyCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByCompanyCode(_companyCode))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByCompanyCode(_companyCode);
            Assert.IsTrue(result.PurchaseOrders == null);

        }

        [TestMethod]
        public void GetPurchaseOrderByPurchaseOrderNumberTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseOrderModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber))
                            .IgnoreArguments()
                            .Return(PurchaseOrderModel);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            var result = _purchaseOrderManager.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByPurchaseOrderNumber(string.Empty, _purchaseOrderNumber);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and Purchase Order Number empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseOrderModel = null;
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber))
                         .IgnoreArguments()
                         .Return(PurchaseOrderModel);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, _purchaseOrderNumber))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.PurchaseOrder == null);

        }

        [TestMethod]
        public void GetPurchaseOrderByOrderTypeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseOrderModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderType(_companyCode, _orderType))
                            .IgnoreArguments()
                            .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            var result = _purchaseOrderManager.GetPurchaseOrderByOrderType(_companyCode, _orderType);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderType(_companyCode, _orderType))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByOrderType(string.Empty, _orderType);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and Purchase Order Number empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderType(_companyCode, _orderType))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByOrderType(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseOrderList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderType(_companyCode, _orderType))
                         .IgnoreArguments()
                         .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByOrderType(_companyCode, string.Empty);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderType(_companyCode, _orderType))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByOrderType(_companyCode, string.Empty);
            Assert.IsTrue(result.PurchaseOrders == null);

        }

        [TestMethod]
        public void GetPurchaseOrderByProjectNumberTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseOrderModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrderByProjectNumber(_companyCode, _projectNumber))
                            .IgnoreArguments()
                            .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            var result = _purchaseOrderManager.GetPurchaseOrderByProjectNumber(_companyCode, _projectNumber);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByProjectNumber(_companyCode, _projectNumber))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByProjectNumber(string.Empty, _projectNumber);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and Purchase Order Number empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByProjectNumber(_companyCode, _projectNumber))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByProjectNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseOrderList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByProjectNumber(_companyCode, _projectNumber))
                         .IgnoreArguments()
                         .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByProjectNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByProjectNumber(_companyCode, _projectNumber))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByProjectNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.PurchaseOrders == null);

        }

        [TestMethod]
        public void GetPurchaseOrderByDeliveryDateRangeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseOrderModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrderByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate))
                            .IgnoreArguments()
                            .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            var result = _purchaseOrderManager.GetPurchaseOrderByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByDeliveryDateRange(string.Empty, _deliveryStartDate, _deliveryEndDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Delivery dates are empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByDeliveryDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseOrderList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate))
                         .IgnoreArguments()
                         .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByDeliveryDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByDeliveryDateRange(_companyCode, _deliveryStartDate, _deliveryEndDate))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByDeliveryDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.PurchaseOrders == null);

        }

        [TestMethod]
        public void GetPurchaseOrderByOrderDateRangeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseOrderModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate))
                            .IgnoreArguments()
                            .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            var result = _purchaseOrderManager.GetPurchaseOrderByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByOrderDateRange(string.Empty, _orderStartDate, _orderEndDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Delivery dates are empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByOrderDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseOrderList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate))
                         .IgnoreArguments()
                         .Return(PurchaseOrderList);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByOrderDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrderByOrderDateRange(_companyCode, _orderStartDate, _orderEndDate))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseOrderManager = new PurchaseOrderManager(mockRepository);
            result = _purchaseOrderManager.GetPurchaseOrderByOrderDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.PurchaseOrders == null);

        }

        #endregion.................................

        #region MockData Methods
        public void SetMockDataForPurchaseOrderModel()
        {
            #region SamplePurchaseOrderModel
            PurchaseOrderModel = new Pc01()
            {
                //PurchOrderNo
                Pc01001 = "",

                //Order Type
                Pc01002 = "0",

                //Supplier Code
                Pc01003 = "",

                //Customer Code Delivery
                Pc01004 = "",

                //Remark Line 1
                Pc01005 = "",

                //Remark Line 2
                Pc01006 = "",

                //Purchase Order Print Status
                Pc01007 = "0",


                //Reminder of Confirmation Print Status
                Pc01008 = "0",

                //Reminder of Shipment Print Status
                Pc01009 = "0",

                //Invoice Entered
                Pc01011 = "",

                //Payment Terms
                Pc01012 = "",

                //Delivery Terms
                Pc01013 = "",

                //Delivery Method
                Pc01014 = "",

                //Order Date
                Pc01015 = "",

                //Delivery Date
                Pc01016 = "",

                //Order Discount
                Pc01019 = "0",

                //Order Value 
                Pc01020 = "0",

                //Currency Code
                Pc01022 = "",

                //Purchase Code 
                Pc01046 = "",

                //Project Number 
                Pc01056 = "",

                //Customer Purchase Order Number 
                Pc01058 = "",

                //Customer Request Number 
                Pc01062 = "",

                //Project Linked Option 
                Pc01068 = "0"
            };
            #endregion
        }


        public void SetMockDataForPurchaseOrderModelList()
        {
            #region SamplePurchaseOrderModelList
            PurchaseOrderList.Add(new Pc01()
            {
                //PurchOrderNo
                Pc01001 = "",

                //Order Type
                Pc01002 = "0",

                //Supplier Code
                Pc01003 = "",

                //Customer Code Delivery
                Pc01004 = "",

                //Remark Line 1
                Pc01005 = "",

                //Remark Line 2
                Pc01006 = "",

                //Purchase Order Print Status
                Pc01007 = "0",


                //Reminder of Confirmation Print Status
                Pc01008 = "0",

                //Reminder of Shipment Print Status
                Pc01009 = "0",

                //Invoice Entered
                Pc01011 = "",

                //Payment Terms
                Pc01012 = "",

                //Delivery Terms
                Pc01013 = "",

                //Delivery Method
                Pc01014 = "",

                //Order Date
                Pc01015 = "",

                //Delivery Date
                Pc01016 = "",

                //Order Discount
                Pc01019 = "0",

                //Order Value 
                Pc01020 = "0",

                //Currency Code
                Pc01022 = "",

                //Purchase Code 
                Pc01046 = "",

                //Project Number 
                Pc01056 = "",

                //Customer Purchase Order Number 
                Pc01058 = "",

                //Customer Request Number 
                Pc01062 = "",

                //Project Linked Option 
                Pc01068 = "0"
            });
            PurchaseOrderList.Add(new Pc01()
            {
                //PurchOrderNo
                Pc01001 = "",

                //Order Type
                Pc01002 = "0",

                //Supplier Code
                Pc01003 = "",

                //Customer Code Delivery
                Pc01004 = "",

                //Remark Line 1
                Pc01005 = "",

                //Remark Line 2
                Pc01006 = "",

                //Purchase Order Print Status
                Pc01007 = "0",


                //Reminder of Confirmation Print Status
                Pc01008 = "0",

                //Reminder of Shipment Print Status
                Pc01009 = "0",

                //Invoice Entered
                Pc01011 = "",

                //Payment Terms
                Pc01012 = "",

                //Delivery Terms
                Pc01013 = "",

                //Delivery Method
                Pc01014 = "",

                //Order Date
                Pc01015 = "",

                //Delivery Date
                Pc01016 = "",

                //Order Discount
                Pc01019 = "0",

                //Order Value 
                Pc01020 = "0",

                //Currency Code
                Pc01022 = "",

                //Purchase Code 
                Pc01046 = "",

                //Project Number 
                Pc01056 = "",

                //Customer Purchase Order Number 
                Pc01058 = "",

                //Customer Request Number 
                Pc01062 = "",

                //Project Linked Option 
                Pc01068 = "0"
            });
            #endregion
        }
        #endregion

    }
}
