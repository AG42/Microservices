using System;
using System.Collections.Generic;
using Rhino.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContractInformation.BusinessLayer;
using ContractInformation.DataLayer.Interfaces;
using ContractInformation.DataLayer.Entities.Datalake;
using ContractInformation.Common.Enum;
using ContractInformation.DataLayer;

namespace ContractInformation.UnitTest
{
    [TestClass]
    public class ContractInformationManagerUnitTest
    {
        #region Declarations

        private IDataLayerContext _iDataLayer;
        private ContractInformationManager _contractInformationManager;
        private string _companyCode = "j1";
        private string _customerName = "";
        private string _poNumber = "";
        private string _requestNumber = "";
        private string _contractNumber = "";
        private string _name = "";
        private string _status = "";
        private string _reference = "";
        private string _searchKey = "";
        private string _startDate = "";
        private string _endDate = "";

        public List<Cmh1Na00> ContractInformationList = new List<Cmh1Na00>();

        #endregion

        #region Initializes
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _iDataLayer = new DataLayerContext();
            _contractInformationManager = new ContractInformationManager(_iDataLayer);
        }
        #endregion

        #region Test methods

        [TestMethod]
        public void GetContractsByCompanyCodeTest()
        {
            _companyCode = "na";

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetContractsByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByCompanyCode(_companyCode);
            Assert.IsNotNull(result);


            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _contractInformationManager.GetContractsByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);


            //...Negative unit test case : Output list is null
            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCompanyCode(_companyCode))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCompanyCode(_companyCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCompanyCode(_companyCode))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCompanyCode(_companyCode);
            Assert.IsTrue(result.Contracts == null);

        }
        //[TestMethod]
        //public void GetContractsByCompanyCodeTestException()
        //{
        //    var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
        //    mockRepository.Stub(x => x.GetContractsByCompanyCode(_companyCode))
        //        .IgnoreArguments()
        //        .Throw(new Exception());

        //    _contractInformationManager = new ContractInformationManager(mockRepository);

        //    var result = _contractInformationManager.GetContractsByCompanyCode(_companyCode);
        //    Assert.IsNotNull(result);
        //}
        [TestMethod]
        public void GetContractsByCompanyCodeInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCompanyCode(string.Empty);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetContractsByCustomerNameTest()
        {
            _companyCode = "na";
            _customerName = "ABCD";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByCustomerName(_companyCode, _customerName))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByCustomerName(_companyCode, _customerName);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByCustomerName(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerName(string.Empty, _customerName);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerName(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerName(_companyCode, _customerName))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerName(_companyCode, _customerName);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByCustomerName(CompanyCode, CustomerName))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByCustomerName(CompanyCode, CustomerName);
            //Assert.IsNotNull(result);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerName(_companyCode, _customerName))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerName(_companyCode, _customerName);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByCustomerNameTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerName(_companyCode, _customerName))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerName(_companyCode, _customerName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetContractsByCustomerNameInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerName(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerPoNumberTest()
        {
            _companyCode = "na";
            _poNumber = "SIGNED AGREEMENT";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByCustomerPONumber(_companyCode, _customerName))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByCustomerPONumber(_companyCode, _poNumber);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByCustomerPONumber(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerPONumber(string.Empty, _poNumber);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerPONumber(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByCustomerPONumber(CompanyCode, PoNumber))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByCustomerPONumber(CompanyCode, PoNumber);
            //Assert.IsTrue(result.ErrorInfo.Count > 0);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerPONumber(_companyCode, _poNumber))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerPONumber(_companyCode, _poNumber);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerName(_companyCode, _poNumber))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerPONumber(_companyCode, _poNumber);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByCustomerPoNumberTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerPONumber(_companyCode, _poNumber))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerPONumber(_companyCode, _poNumber);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerPoNumberInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerPONumber(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByRequestNumberTest()
        {
            _companyCode = "na";
            _requestNumber = "1234";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByRequestNumber(_companyCode, _requestNumber))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByRequestNumber(_companyCode, _requestNumber);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByRequestNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByRequestNumber(string.Empty, _requestNumber);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByRequestNumber(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByRequestNumber(_companyCode, _requestNumber))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByRequestNumber(_companyCode, _requestNumber);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByRequestNumber(_companyCode, _requestNumber))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByRequestNumber(_companyCode, _requestNumber);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByRequestNumberTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByRequestNumber(_companyCode, _requestNumber))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByRequestNumber(_companyCode, _requestNumber);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByRequestNumberInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByRequestNumber(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByServiceContractNumberTest()
        {
            _companyCode = "na";
            _contractNumber = "9416000002";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByServiceContractNumber(_companyCode, _contractNumber))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByServiceContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByServiceContractNumber(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByServiceContractNumber(string.Empty, _contractNumber);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByServiceContractNumber(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByServiceContractNumber(CompanyCode, ContractNumber))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByServiceContractNumber(CompanyCode, ContractNumber);
            //Assert.IsTrue(result.ErrorInfo.Count > 0);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByServiceContractNumber(_companyCode, _contractNumber))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByServiceContractNumber(_companyCode, _contractNumber);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByServiceContractNumber(_companyCode, _contractNumber))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByServiceContractNumber(_companyCode, _contractNumber);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByServiceContractNumberTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByServiceContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByServiceContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByServiceContractNumberInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByServiceContractNumber(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerNameandStatusTest()
        {
            _companyCode = "na";
            _name = "ABCD";
            _status = "Active";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByCustomerNameandStatus(_companyCode, _name, _startDate))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByCustomerNameandStatus(_companyCode, _name, _status);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByCustomerNameandStatus(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerNameandStatus(string.Empty, _name, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerNameandStatus(string.Empty, string.Empty, _status);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByCustomerNameandStatus(CompanyCode, Name, Status))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByCustomerNameandStatus(CompanyCode, Name, Status);
            //Assert.IsTrue(result.ErrorInfo.Count > 0);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerNameandStatus(_companyCode, _name, _status))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerNameandStatus(_companyCode, _name, _status);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerNameandStatus(_companyCode, _name, _status))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerNameandStatus(_companyCode, _name, _status);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByCustomerNameandStatusTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerNameandStatus(_companyCode, _name, _status))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerNameandStatus(_companyCode, _name, _status);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerNameandStatusInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerNameandStatus(string.Empty, string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerReferenceTest()
        {
            _companyCode = "na";
            _reference = "NAIR SUJITH";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByCustomerReference(_companyCode, _reference))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByCustomerReference(_companyCode, _reference);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByCustomerReference(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerReference(string.Empty, _reference);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerReference(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerPONumber(_companyCode, _reference))
                .IgnoreArguments()
                .Throw(new Exception());
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerReference(_companyCode, _reference);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerReference(_companyCode, _reference))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerReference(_companyCode, _reference);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerReference(_companyCode, _reference))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerReference(_companyCode, _reference);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByCustomerReferenceTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerReference(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerReference(_companyCode, _reference);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerReferenceInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerReference(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerReferenceandStatusTest()
        {
            _companyCode = "na";
            _reference = "NAIR SUJITH";
            _status = "Active";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByCustomerReferenceandStatus(_companyCode, _reference, _status))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(_companyCode, _reference, _status);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(string.Empty, _reference, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(string.Empty, string.Empty, _status);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByCustomerReferenceandStatus(CompanyCode, Reference,Status))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(CompanyCode, Reference, Status);
            //Assert.IsTrue(result.ErrorInfo.Count > 0);


            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerReferenceandStatus(_companyCode, _reference, _status))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(_companyCode, _reference, _status);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerReferenceandStatus(_companyCode, _reference, _status))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(_companyCode, _reference, _status);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByCustomerReferenceandStatusTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerReferenceandStatus(_companyCode, _reference, _status))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(_companyCode, _reference, _status);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerReferenceandStatusInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerReferenceandStatus(string.Empty, string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerSearchKeyTest()
        {
            _companyCode = "na";
            _searchKey = "0";

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByCustomerSearchKey(_companyCode, _searchKey))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByCustomerSearchKey(_companyCode, _searchKey);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByCustomerSearchKey(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerSearchKey(string.Empty, _searchKey);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByCustomerSearchKey(CompanyCode, SearchKey))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByCustomerSearchKey(CompanyCode, SearchKey);
            //Assert.IsTrue(result.ErrorInfo.Count > 0);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKey(_companyCode, _searchKey))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerSearchKey(_companyCode, _searchKey);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKey(_companyCode, _searchKey))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerSearchKey(_companyCode, _searchKey);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByCustomerSearchKeyTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKey(_companyCode, _searchKey))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerSearchKey(_companyCode, _searchKey);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerSearchKeyInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerSearchKey(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerSearchKeyandStatusTest()
        {
            _companyCode = "na";
            _searchKey = "0";
            _status = "Active";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByCustomerSearchKeyandStatus(_companyCode, _searchKey, _status))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(_companyCode, _searchKey, _status);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(string.Empty, _searchKey, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(string.Empty, string.Empty, _status);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByCustomerSearchKeyandStatus(CompanyCode, SearchKey, Status))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(CompanyCode, SearchKey, Status);
            //Assert.IsTrue(result.ErrorInfo.Count > 0);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKeyandStatus(_companyCode, _searchKey, _status))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(_companyCode, _searchKey, _status);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKeyandStatus(_companyCode, _searchKey, _status))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(_companyCode, _searchKey, _status);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByCustomerSearchKeyandStatusTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKeyandStatus(_companyCode, _searchKey, _status))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(_companyCode, _searchKey, _status);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerSearchKeyandStatusInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(string.Empty, string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByDateRangeTest()
        {
            _companyCode = "na";
            _startDate = "2015-10-01 00:00:00.0";
            _endDate = "2016-09-30 00:00:00.0";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByDateRange(_companyCode, _startDate, _endDate))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByDateRange(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByDateRange(string.Empty, _startDate, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByDateRange(string.Empty, string.Empty, _endDate);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByDateRange(CompanyCode, startDate, endDate))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByDateRange(CompanyCode, startDate, endDate);
            //Assert.IsTrue(result.ErrorInfo.Count > 0);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByDateRange(_companyCode, _startDate, _endDate))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByDateRange(_companyCode, _startDate, _endDate);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByDateRange(_companyCode, _startDate, _endDate))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByDateRange(_companyCode, _startDate, _endDate);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByDateRangeTestexception()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByDateRange(_companyCode, _startDate, _endDate))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByDateRange(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByDateRangeInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByCustomerSearchKeyandStatus(string.Empty, string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByStatusTest()
        {
            _companyCode = "na";
            _status = "Active";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForContractModel();

            mockRepository.Stub(x => x.GetContractsByStatus(_companyCode, _status))
                            .IgnoreArguments()
                            .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            var result = _contractInformationManager.GetContractsByStatus(_companyCode, _status);
            Assert.IsNotNull(result);

            result = _contractInformationManager.GetContractsByStatus(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _contractInformationManager.GetContractsByStatus(string.Empty, _status);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);



            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //mockRepository.Stub(x => x.GetContractsByStatus(CompanyCode, Status))
            //    .IgnoreArguments()
            //    .Throw(new Exception());
            //_contractInformationManager = new ContractInformationManager(mockRepository);
            //result = _contractInformationManager.GetContractsByStatus(CompanyCode, Status);
            //Assert.IsTrue(result.ErrorInfo.Count > 0);

            ContractInformationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByStatus(_companyCode, _status))
                         .IgnoreArguments()
                         .Return(ContractInformationList);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByStatus(_companyCode, _status);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByStatus(_companyCode, _status))
                          .IgnoreArguments()
                          .Return(null);
            _contractInformationManager = new ContractInformationManager(mockRepository);
            result = _contractInformationManager.GetContractsByStatus(_companyCode, _status);
            Assert.IsTrue(result.Contracts == null);
        }
        [TestMethod]
        public void GetContractsByStatusTestexception()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetContractsByStatus(_companyCode, _status))
                .IgnoreArguments()
                .Throw(new Exception());

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByStatus(_companyCode, _status);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetContractsByStatusInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _contractInformationManager = new ContractInformationManager(mockRepository);

            var result = _contractInformationManager.GetContractsByStatus(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        #endregion

        #region MockData Methods

        public void SetMockDataForContractModel()
        {
            #region SampleCustomerProjectOrderModelList
            ContractInformationList.Add(new Cmh1Na00()
            {
                Cmh1001 = "sd",
                Cmh1002 = "ghi",
                Cmh1004 = "lst",
                Cmh1005 = "mms",
                Cmh1015 = "10-Jul-2016",
                Cmh1016 = "17-Jul-2017",
                Cmh1034 = "ppr",
                Cmh1040 = "ssmn",
                Cmh1068 = "nnws",
                Cmh1091 = "llps",
                Cmh1093 = "ggtw",
                Cmh1096 = "tstd",
                Cmh1123 = "sssaa",
                Cmh1132 = "ssadfg",
                Cmh1220 = "ssddwerr"
            });
            ContractInformationList.Add(new Cmh1Na00()
            {
                Cmh1001 = "sd",
                Cmh1002 = "ghi",
                Cmh1004 = "lst",
                Cmh1005 = "mms",
                Cmh1015 = "10-Jul-2016",
                Cmh1016 = "17-Jul-2017",
                Cmh1034 = "ppr",
                Cmh1040 = "ssmn",
                Cmh1068 = "nnws",
                Cmh1091 = "1",
                Cmh1093 = "ggtw",
                Cmh1096 = "tstd",
                Cmh1123 = "sssaa",
                Cmh1132 = "ssadfg",
                Cmh1220 = "ssddwerr"
            });
            ContractInformationList.Add(new Cmh1Na00()
            {
                Cmh1001 = "sd",
                Cmh1002 = "ghi",
                Cmh1004 = "lst",
                Cmh1005 = "mms",
                Cmh1015 = "10-Jul-2016",
                Cmh1016 = "17-Jul-2017",
                Cmh1034 = "ppr",
                Cmh1040 = "ssmn",
                Cmh1068 = "nnws",
                Cmh1091 = "2",
                Cmh1093 = "ggtw",
                Cmh1096 = "tstd",
                Cmh1123 = "sssaa",
                Cmh1132 = "ssadfg",
                Cmh1220 = "ssddwerr"
            });
            ContractInformationList.Add(new Cmh1Na00()
            {
                Cmh1001 = "sd",
                Cmh1002 = "ghi",
                Cmh1004 = "lst",
                Cmh1005 = "mms",
                Cmh1015 = "10-Jul-2016",
                Cmh1016 = "17-Jul-2017",
                Cmh1034 = "ppr",
                Cmh1040 = "ssmn",
                Cmh1068 = "nnws",
                Cmh1091 = "3",
                Cmh1093 = "ggtw",
                Cmh1096 = "tstd",
                Cmh1123 = "sssaa",
                Cmh1132 = "ssadfg",
                Cmh1220 = "ssddwerr"
            });
            #endregion
        }
    }
    #endregion
}
