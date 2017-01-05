using System.Collections.Generic;
using Rhino.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreditStatus.BusinessLayer;
using CreditStatus.DataLayer.Interfaces;
using CreditStatus.DataLayer.Entities.Datalake;
using CreditStatus.Common.Enum;
using CreditStatus.DataLayer;

namespace CreditStatus.UnitTest
{
    [TestClass]
   public class CreditStatusManagerUnitTest
    {

        #region Declarations

        private IDataLayerContext _iDataLayer;
        private CreditStatusManager _creditStatusManager;
        private string _companyCode = "na";
        private string _customerCode = "4629";
        private string _customerName = "ARGON PROPERTIES WLL";
        private bool _ledgerFlag = true;

        public Sl01 CreditStatus = new Sl01();
        public List<Sl01> CreditStatusList = new List<Sl01>();

        #endregion

        #region Initializes
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _iDataLayer = new DataLayerContext();
            _creditStatusManager = new CreditStatusManager(_iDataLayer);
        }
        #endregion

        #region Test methods

        [TestMethod]
        public void GetCreditStatusByCompanyCodeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCreditStatusModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCreditStatusByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(CreditStatusList);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            var result = _creditStatusManager.GetCreditStatusByCompanyCode(_companyCode, _ledgerFlag);
            Assert.IsNotNull(result);

            //...Positive unit test case with leger flag false
            mockRepository.Stub(x => x.GetCreditStatusByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(CreditStatusList);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            result = _creditStatusManager.GetCreditStatusByCompanyCode(_companyCode, false);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _creditStatusManager.GetCreditStatusByCompanyCode(string.Empty,_ledgerFlag);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);


            //...Negative unit test case : Output list is null
            CreditStatusList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCreditStatusByCompanyCode(_companyCode))
                         .IgnoreArguments()
                         .Return(CreditStatusList);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            result = _creditStatusManager.GetCreditStatusByCompanyCode(_companyCode,_ledgerFlag);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCreditStatusByCompanyCode(_companyCode))
                          .IgnoreArguments()
                          .Return(null);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            result = _creditStatusManager.GetCreditStatusByCompanyCode(_companyCode,_ledgerFlag);
            Assert.IsTrue(result.Credits == null);

        }

        [TestMethod]
        public void GetCreditStatusByCustomerCodeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCreditStatusModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCreditStatusByCustomerCode(_companyCode, _customerCode))
                            .IgnoreArguments()
                            .Return(CreditStatus);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            var result = _creditStatusManager.GetCreditStatusByCustomerCode(_companyCode, _customerCode, _ledgerFlag);
            Assert.IsNotNull(result);


            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _creditStatusManager.GetCreditStatusByCustomerCode(string.Empty, _customerCode, _ledgerFlag);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CustomerName empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _creditStatusManager.GetCreditStatusByCustomerCode(_customerCode, string.Empty, _ledgerFlag);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty and CustomerName empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _creditStatusManager.GetCreditStatusByCustomerCode(string.Empty, string.Empty, _ledgerFlag);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);


            //...Negative unit test case : Output list is null
            CreditStatusList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCreditStatusByCustomerCode(_companyCode, _customerCode))
                         .IgnoreArguments()
                         .Return(null);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            result = _creditStatusManager.GetCreditStatusByCustomerCode(_companyCode, _customerCode, _ledgerFlag);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

        }

        [TestMethod]
        public void GetCreditStatusByCustomerNameTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCreditStatusModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCreditStatusByCustomerName(_companyCode, _customerName))
                            .IgnoreArguments()
                            .Return(CreditStatusList);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            var result = _creditStatusManager.GetCreditStatusByCustomerName(_companyCode,_customerName, _ledgerFlag);
            Assert.IsNotNull(result);


            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _creditStatusManager.GetCreditStatusByCustomerName(string.Empty,_customerName, _ledgerFlag);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CustomerName empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _creditStatusManager.GetCreditStatusByCustomerName(_customerCode, string.Empty, _ledgerFlag);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty and CustomerName empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _creditStatusManager.GetCreditStatusByCustomerName(string.Empty, string.Empty, _ledgerFlag);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);


            //...Negative unit test case : Output list is null
            CreditStatusList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCreditStatusByCustomerName(_companyCode, _customerName))
                         .IgnoreArguments()
                         .Return(CreditStatusList);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            result = _creditStatusManager.GetCreditStatusByCustomerName(_companyCode, _customerName, _ledgerFlag);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCreditStatusByCustomerName(_companyCode, _customerName))
                          .IgnoreArguments()
                          .Return(null);
            _creditStatusManager = new CreditStatusManager(mockRepository);
            result = _creditStatusManager.GetCreditStatusByCustomerName(_companyCode, _customerName, _ledgerFlag);
            Assert.IsTrue(result.Credits == null);

        }

        #endregion

        #region MockData Methods
        public void SetMockDataForCreditStatusModel()
        {
            #region SampleCreditStatusModel
            CreditStatus = new Sl01()
            {
                //Customer Code
                Sl01001 = "4629",
                //Customer Name
                Sl01002 = "ARGON PROPERTIES WLL",
                //Unpaid Invoices
                Sl01038 = "500",
                //Ordered Not Shipped
                Sl01057 = "1000",
                //Shipped Not Invoiced
                Sl01058 = "4500",
                //Credit Limit
                Sl01037 = "10010"
            };
            #endregion
        }


        public void SetMockDataForCreditStatusModelList()
        {
            #region SampleCreditStatusModelList
            CreditStatusList.Add(new Sl01()
            {
                //Customer Code
                Sl01001 = "4629",
                //Customer Name
                Sl01002 = "ARGON PROPERTIES WLL",
                //Unpaid Invoices
                Sl01038 = "500",
                //Ordered Not Shipped
                Sl01057 = "1000",
                //Shipped Not Invoiced
                Sl01058 = "4500",
                //Credit Limit
                Sl01037 = "10010"
            });
            CreditStatusList.Add(new Sl01()
            {
                //Customer Code
                Sl01001 = "1001",
                //Customer Name
                Sl01002 = "LSS Technologies",
                //Unpaid Invoices
                Sl01038 = "3000",
                //Ordered Not Shipped
                Sl01057 = "4000",
                //Shipped Not Invoiced
                Sl01058 = "1000",
                //Credit Limit
                Sl01037 = "9000"
            });

            CreditStatusList.Add(new Sl01()
            {
                //Customer Code
                Sl01001 = null,
                //Customer Name
                Sl01002 = null,
                //Unpaid Invoices
                Sl01038 = null,
                //Ordered Not Shipped
                Sl01057 = null,
                //Shipped Not Invoiced
                Sl01058 = null,
                //Credit Limit
                Sl01037 = null
            });

            #endregion
        }
        #endregion
    }

}
