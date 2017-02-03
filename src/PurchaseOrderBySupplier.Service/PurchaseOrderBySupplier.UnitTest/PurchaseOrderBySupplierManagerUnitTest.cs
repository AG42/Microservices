using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseOrderBySupplier.BusinessLayer;
using PurchaseOrderBySupplier.BusinessLayer.Interfaces;
using PurchaseOrderBySupplier.Common.Enum;
using PurchaseOrderBySupplier.DataLayer;
using PurchaseOrderBySupplier.DataLayer.Interfaces;
using PurchaseOrderBySupplier.DataLayer.Entities.Datalake;
using Rhino.Mocks;

namespace PurchaseOrderBySupplier.UnitTest
{
    [TestClass]
    public class PurchaseOrderBySupplierManagerUnitTest
    {
        #region Declarations
        private IDataLayerContext _iDataLayer;
        private IPurchaseOrderBySupplierManager _purchaseOrderBySupplierManager;
        private string _companyCode = "j4";
        private readonly List<Pc12> _pc012 = new List<Pc12>();
        #endregion

        #region Initializes
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _iDataLayer = new DataLayerContext();
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(_iDataLayer);
        }
        #endregion

        #region Unit Test Methods

        [TestMethod]
        public void GetPurchaseOrdersByCompanyCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetPurchaseOrdersBySupplier();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrdersByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(_pc012);
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(mockRepository);
            var result = _purchaseOrderBySupplierManager.GetPurchaseOrdersByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrdersByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(mockRepository);
            result = _purchaseOrderBySupplierManager.GetPurchaseOrdersByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);
        }

        [TestMethod]
        public void GetPurchaseOrdersBySupplierCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetPurchaseOrdersBySupplier();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrdersBySupplierCode(_companyCode,String.Empty))
                            .IgnoreArguments()
                            .Return(_pc012);
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(mockRepository);
            var result = _purchaseOrderBySupplierManager.GetPurchaseOrdersBySupplierCode(_companyCode,String.Empty);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrdersBySupplierCode(_companyCode,String.Empty))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(mockRepository);
            result = _purchaseOrderBySupplierManager.GetPurchaseOrdersBySupplierCode(string.Empty,String.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);
        }

        [TestMethod]
        public void GetPurchaseOrdersBySupplierInvoiceNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetPurchaseOrdersBySupplier();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrdersBySupplierInvoiceNumber(_companyCode,String.Empty))
                            .IgnoreArguments()
                            .Return(_pc012);
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(mockRepository);
            var result = _purchaseOrderBySupplierManager.GetPurchaseOrdersBySupplierInvoiceNumber(_companyCode, String.Empty);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrdersBySupplierInvoiceNumber(_companyCode,String.Empty))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(mockRepository);
            result = _purchaseOrderBySupplierManager.GetPurchaseOrdersBySupplierInvoiceNumber(string.Empty, String.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);
        }
        [TestMethod]
        public void GetPurchaseOrdersByVATCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetPurchaseOrdersBySupplier();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetPurchaseOrdersByVATCode(_companyCode,String.Empty))
                            .IgnoreArguments()
                            .Return(_pc012);
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(mockRepository);
            var result = _purchaseOrderBySupplierManager.GetPurchaseOrdersByVATCode(_companyCode, String.Empty);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetPurchaseOrdersByVATCode(_companyCode,String.Empty))
                            .IgnoreArguments()
                            .Return(null);
            _purchaseOrderBySupplierManager = new PurchaseOrderBySupplierManager(mockRepository);
            result = _purchaseOrderBySupplierManager.GetPurchaseOrdersByVATCode(string.Empty, String.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);
        }
        #endregion

        #region Mock Data Methods

        public void SetPurchaseOrdersBySupplier()
        {
            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0017",
                //Purchase Order Number               
                Pc12001 = "*********3",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });

            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0017",
                //Purchase Order Number               
                Pc12001 = "*********2",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });

            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0018",
                //Purchase Order Number               
                Pc12001 = "*********2",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });

            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0018",
                //Purchase Order Number               
                Pc12001 = "*********4",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });
        }

        #endregion
    }
}
