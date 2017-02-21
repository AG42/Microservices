using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseLedger.BusinessLayer;
using PurchaseLedger.DataLayer;
using PurchaseLedger.DataLayer.Entities.Datalake;
using PurchaseLedger.DataLayer.Interfaces;
using Rhino.Mocks;
using System.Collections.Generic;
using PurchaseLedger.Common.Enum;
using System;

namespace PurchaseLedger.UnitTest
{
    [TestClass]
    public class PurchaseLedgerManagerUnitTest
    {
        #region Declarations

        private IDataLayerContext _iDataLayer;
        private PurchaseLedgerManager _purchaseLedgerManager;
        private string _companyCode = "na";
        private string _supplierCode = "1020";
        private string _invoiceNumber = "1028289";
        private string _supplierName = "AL MOAYYED AIRCONDITIONING";
        private string _orderNumber = "4931000056";
        readonly string _invoiceStartDate = DateTime.Now.ToString("dd-MMM-yyyy");
        readonly string _invoiceEndDate = DateTime.Now.ToString("dd-MMM-yyyy");
        readonly string _dueStartDate = DateTime.Now.ToString("dd-MMM-yyyy");
        readonly string _dueEndDate = DateTime.Now.ToString("dd-MMM-yyyy");
        public List<Pl03> PurchaseLedgerList = new List<Pl03>();

        #endregion

        #region Initializes
        [TestInitialize]
        public void Initialize()
        {
            _iDataLayer = new DataLayerContext();
            _purchaseLedgerManager = new PurchaseLedgerManager(_iDataLayer);
        }
        #endregion

        #region Test Methods
        [TestMethod]
        public void GetPurchaseLedgerByCompanyCodeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseLedgerModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseLedgerByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            var result = _purchaseLedgerManager.GetPurchaseLedgerByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseLedgerList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByCompanyCode(_companyCode))
                         .IgnoreArguments()
                         .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByCompanyCode(_companyCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByCompanyCode(_companyCode))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByCompanyCode(_companyCode);
            Assert.IsTrue(result.Suppliers == null);

        }

        [TestMethod]
        public void GetPurchaseLedgerBySupplierCodeUnitTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseLedgerModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode))
                            .IgnoreArguments()
                            .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            var result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierCode(string.Empty, _supplierCode);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Supplier Code empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierCode(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Company Code and Supplier Code empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierCode(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseLedgerList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode))
                         .IgnoreArguments()
                         .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierCode(_companyCode, string.Empty);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierCode(_companyCode, _supplierCode))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierCode(_companyCode, string.Empty);
            Assert.IsTrue(result.Suppliers == null);

        }


        [TestMethod]
        public void GetPurchaseLedgerByOrderNoTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseLedgerModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseLedgerByOrderNo(_companyCode, _orderNumber))
                            .IgnoreArguments()
                            .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            var result = _purchaseLedgerManager.GetPurchaseLedgerByOrderNo(_companyCode, _orderNumber);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByOrderNo(_companyCode, _orderNumber))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByOrderNo(string.Empty, _orderNumber);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Order Number empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByOrderNo(_companyCode, _orderNumber))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByOrderNo(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseLedgerList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByOrderNo(_companyCode, _orderNumber))
                         .IgnoreArguments()
                         .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByOrderNo(_companyCode, _orderNumber);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByOrderNo(_companyCode, _orderNumber))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByOrderNo(_companyCode, _orderNumber);
            Assert.IsTrue(result.Suppliers == null);

        }

        [TestMethod]
        public void GetPurchaseLedgerByInvoiceNoTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseLedgerModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNumber))
                            .IgnoreArguments()
                            .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            var result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNumber);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNumber))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceNo(string.Empty, _invoiceNumber);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Inovice Number empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNumber))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceNo(_companyCode,string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseLedgerList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNumber))
                         .IgnoreArguments()
                         .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNumber);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNumber))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceNo(_companyCode, _invoiceNumber);
            Assert.IsTrue(result.Suppliers == null);

        }

        [TestMethod]
        public void GetPurchaseLedgerByInvoiceDateRangeUnitTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseLedgerModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoiceStartDate, _invoiceEndDate))
                            .IgnoreArguments()
                            .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            var result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoiceStartDate, _invoiceEndDate);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoiceStartDate, _invoiceEndDate))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceDateRange(string.Empty, _invoiceStartDate, _invoiceEndDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Invoice dates are empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoiceStartDate, _invoiceEndDate))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseLedgerList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoiceStartDate, _invoiceEndDate))
                         .IgnoreArguments()
                         .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoiceStartDate, _invoiceEndDate);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoiceStartDate, _invoiceEndDate))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByInvoiceDateRange(_companyCode, _invoiceStartDate, _invoiceEndDate);
            Assert.IsTrue(result.Suppliers == null);

        }

        [TestMethod]
        public void GetPurchaseLedgerByDueDateRangeUnitTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseLedgerModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseLedgerByDueDateRange(_companyCode, _dueStartDate, _dueEndDate))
                            .IgnoreArguments()
                            .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            var result = _purchaseLedgerManager.GetPurchaseLedgerByDueDateRange(_companyCode, _dueStartDate, _dueEndDate);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByDueDateRange(_companyCode, _dueStartDate, _dueEndDate))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByDueDateRange(string.Empty, _dueStartDate, _dueEndDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Due dates are empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByDueDateRange(_companyCode, _dueStartDate, _dueEndDate))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByDueDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseLedgerList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByDueDateRange(_companyCode, _dueStartDate, _dueEndDate))
                         .IgnoreArguments()
                         .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByDueDateRange(_companyCode, _dueStartDate, _dueEndDate);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerByDueDateRange(_companyCode, _dueStartDate, _dueEndDate))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerByDueDateRange(_companyCode, _dueStartDate, _dueEndDate);
            Assert.IsTrue(result.Suppliers == null);

        }

        [TestMethod]
        public void GetPurchaseLedgerBySupplierNameTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForPurchaseLedgerModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName))
                            .IgnoreArguments()
                            .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            var result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierName(string.Empty, _supplierName);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Supplier name is empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierName(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Output list is null
            PurchaseLedgerList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName))
                         .IgnoreArguments()
                         .Return(PurchaseLedgerList);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName))
                          .IgnoreArguments()
                          .Return(null);
            _purchaseLedgerManager = new PurchaseLedgerManager(mockRepository);
            result = _purchaseLedgerManager.GetPurchaseLedgerBySupplierName(_companyCode, _supplierName);
            Assert.IsTrue(result.Suppliers == null);

        }
        #endregion

        #region MockData Methods
        public void SetMockDataForPurchaseLedgerModelList()
        {
            #region SamplePurchaseOrderModelList
            PurchaseLedgerList.Add(new Pl03()
            {
                // Supplier Code
                Pl01001="",

                //Supplier Name
                Pl01002="",

                // InvoiceNo
                Pl03002="",

                //InvoiceDate
                Pl03004="",

                // OrderNo
                Pl03033="",

                //DueDate
                Pl03006="",
                //LatePaymDate
                Pl03025="",
                //Discount1
                Pl03008="",
                //Discount2
                Pl03010="",
                //Discount3
                Pl03012="",
                //InvAmoOriCur
                Pl03014="",
                //SalesTaxAmnt
                Pl03016="",
                //PaiAmoOriCur
                Pl03027="",
                //InvoicePaidFlag
                Pl03077=""
        });
            PurchaseLedgerList.Add(new Pl03()
            {
                // Supplier Code
                Pl01001 = "",

                //Supplier Name
                Pl01002 = "",

                // InvoiceNo
                Pl03002 = "",

                //InvoiceDate
                Pl03004 = "",

                // OrderNo
                Pl03033 = "",

                //DueDate
                Pl03006 = "",
                //LatePaymDate
                Pl03025 = "",
                //Discount1
                Pl03008 = "",
                //Discount2
                Pl03010 = "",
                //Discount3
                Pl03012 = "",
                //InvAmoOriCur
                Pl03014 = "",
                //SalesTaxAmnt
                Pl03016 = "",
                //PaiAmoOriCur
                Pl03027 = "",
                //InvoicePaidFlag
                Pl03077 = ""
            });
            #endregion
        }
        #endregion

    }
}
