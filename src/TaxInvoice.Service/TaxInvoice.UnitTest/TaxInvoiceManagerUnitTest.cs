using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxInvoice.BusinessLayer;
using TaxInvoice.DataAccessLayer.Interface;
using TaxInvoice.DataAccessLayer;
using TaxInvoice.DataAccessLayer.Entities.Datalake;
using System.Collections.Generic;
using Rhino.Mocks;

namespace TaxInvoice.UnitTest
{
    [TestClass]
    public class TaxInvoiceManagerUnitTest
    {
        IDataLayerContext _dataLayerContext;
        TaxInvoiceManager _taxInvoiceManager;
        public SL17 taxInvoiceModel;
        public List<SL17> taxInvoiceModelList;
        private const string _companyCode = "na";
        private const string _customerCode = "120S0005";
        private const string _invoiceNumber = "100020";
        private decimal _taxAmount = 500;

        [TestInitialize]
        public void Initialization()
        {
            taxInvoiceModel = new SL17();
            taxInvoiceModelList = new List<SL17>();
            _dataLayerContext = new DataLayerContext();
            _taxInvoiceManager = new TaxInvoiceManager(_dataLayerContext);
            SetMockDataForTaxInvoiceModelList();
        }


        [TestMethod]
        public void GetTaxInvoiceByCompanyCodeTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByCompanyCode(_companyCode))
                            .IgnoreArguments().Return(taxInvoiceModelList);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            var result = _taxInvoiceManager.GetTaxInvoiceByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(null);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            result = _taxInvoiceManager.GetTaxInvoiceByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(null);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            result = _taxInvoiceManager.GetTaxInvoiceByCompanyCode(_companyCode);
            Assert.IsNull(result.TaxInvoices);
        }


        [TestMethod]
        public void GetTaxInvoiceByInvoiceNoTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNumber))
                            .IgnoreArguments()
                            .Return(taxInvoiceModelList);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            var result = _taxInvoiceManager.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNumber);
            Assert.IsNotNull(result);

            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNumber))
                            .IgnoreArguments()
                            .Return(null);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            result = _taxInvoiceManager.GetTaxInvoiceByInvoiceNo(string.Empty, _invoiceNumber);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNumber))
                            .IgnoreArguments()
                            .Return(null);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            result = _taxInvoiceManager.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNumber);
            Assert.IsNull(result.TaxInvoices);
        }

        [TestMethod]
        public void GetTaxInvoiceByCustomerCodeTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode))
                            .IgnoreArguments()
                            .Return(taxInvoiceModelList);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            var result = _taxInvoiceManager.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode);
            Assert.IsNotNull(result);

            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode))
                            .IgnoreArguments()
                            .Return(null);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            result = _taxInvoiceManager.GetTaxInvoiceByCustomerCode(string.Empty, _customerCode);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode))
                            .IgnoreArguments()
                            .Return(null);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            result = _taxInvoiceManager.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode);
            Assert.IsNull(result.TaxInvoices);
        }

        [TestMethod]
        public void GetTaxInvoiceByTaxAmountRangeTest()
        {
            // positive test 
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByTaxAmountRange(_companyCode, _taxAmount, _taxAmount))
                            .IgnoreArguments()
                            .Return(taxInvoiceModelList);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            var result = _taxInvoiceManager.GetTaxInvoiceByTaxAmountRange(_companyCode, _taxAmount, _taxAmount);
            Assert.IsNotNull(result);

            // Negative test: Empty company name
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByTaxAmountRange(_companyCode, _taxAmount, _taxAmount))
                            .IgnoreArguments()
                            .Return(null);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            result = _taxInvoiceManager.GetTaxInvoiceByTaxAmountRange(string.Empty, _taxAmount, _taxAmount);
            Assert.IsTrue(result.Status == Common.Enum.ResponseStatus.Failure);

            // Negative Test: Null output
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetTaxInvoiceByTaxAmountRange(_companyCode, _taxAmount, _taxAmount))
                            .IgnoreArguments()
                            .Return(null);
            _taxInvoiceManager = new TaxInvoiceManager(mockRepository);
            result = _taxInvoiceManager.GetTaxInvoiceByTaxAmountRange(_companyCode, _taxAmount, _taxAmount);
            Assert.IsNull(result.TaxInvoices);
        }


        #region Mock Methods
        public void SetMockDataForTaxInvoiceModelList()
        {
            taxInvoiceModelList.Add(new SL17()
            {
                SL17001 = "250006",
                SL17002 = "120S0005",
                SL17004 = "0",
                SL17007 = "465",
                SL17008 = "1190",
                SL17020 = "ZZZZZZ",
                SL17032 = "0",
                SL17038 = "0",
                SL17050 = ""
            });
            taxInvoiceModelList.Add(new SL17()
            {
                SL17001 = "250006",
                SL17002 = "120S0005",
                SL17004 = "1",
                SL17007 = "10493",
                SL17008 = "1190",
                SL17020 = "ZZZZZZ",
                SL17032 = "0",
                SL17038 = "0",
                SL17050 = ""
            });
            taxInvoiceModelList.Add(new SL17()
            {
                SL17001 = "100020",
                SL17002 = "LA003",
                SL17004 = "1",
                SL17007 = "18190",
                SL17008 = "1190",
                SL17020 = "ZZZZZZ",
                SL17032 = "0",
                SL17038 = "0",
                SL17050 = ""
            });
            taxInvoiceModelList.Add(new SL17()
            {
                SL17001 = "250010",
                SL17002 = "121S0004",
                SL17004 = "1",
                SL17007 = "4820",
                SL17008 = "0",
                SL17020 = "ZZZZZZ",
                SL17032 = "0",
                SL17038 = "0",
                SL17050 = ""
            });
        }
        #endregion
    }
}