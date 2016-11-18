using System.Collections.Generic;
using CustomerSiteLocation.BusinessLayer;
using CustomerSiteLocation.DataLayer.Entities.Datalake;
using CustomerSiteLocation.DataLayer.Interfaces;
using CustomerSiteLocation.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using CustomerSiteLocation.Common.Enum;

namespace CustomerSiteLocation.UnitTest
{

    [TestClass]
    public class CustomerSiteLocationManagerManagerUnitTest
    {
        #region Declarations

        private IDataLayerContext _iDataLayer;
        private CustomerSiteLocationManager _customerSiteLocationManager;
        private string CompanyCode = "j1";
        private string Country = "";
        private string ZipCode = "";
        private string SiteNo = "";
        private string Address = "";
        private string TelePhone = "";
        private string FaxNo = "";
        private string CustomerCode = "";
        private string CustomerName = "";
        private string Emaild = "";
        private string ServiceEngineerCode = "";

        CustomerSiteLocationModel _customerSiteLocationModel = new CustomerSiteLocationModel();
        List<CustomerSiteLocationModel> _customerSiteLocationModelList = new List<CustomerSiteLocationModel>();


        Sy80 _sy80 = new Sy80();
        public List<Sy80> CustomerSiteLocationList = new List<Sy80>();

        #endregion

        #region UnitTests
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _customerSiteLocationManager = new CustomerSiteLocationManager(_iDataLayer);
        }
        #endregion

        #region Test methods

        [TestMethod]
        public void GetCustomerSiteLocationsByCompanyCodeTest()
        {
            CompanyCode = "mg";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCompanyCode(CompanyCode))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByCompanyCode(CompanyCode);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCompanyCode(CompanyCode))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCompanyCode(CompanyCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCompanyCode(CompanyCode))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCompanyCode(CompanyCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCompanyCode(CompanyCode))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCompanyCode(CompanyCode);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }

        [TestMethod]
        public void GetCustomerSiteLocationsByCompanyCodeTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCompanyCode(CompanyCode))
                .IgnoreArguments()
                .Throw(new Exception());

            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);

            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerSiteLocationsByCompanyCodeInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);

            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByCompanyCode(string.Empty);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerSiteLocationsByCountryTest()
        {
            CompanyCode = "mg";
            Country = "kw";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCountry(CompanyCode, Country))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByCountry(CompanyCode, Country);
            Assert.IsNotNull(result.CustomerSiteLocations);

            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCountry(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status==ResponseStatus.Failure);

            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCountry(string.Empty, Country);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCountry(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCountry(CompanyCode, Country))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCountry(CompanyCode, Country);
            Assert.IsTrue(result.ErrorInfo.Count>0);

            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCountry(CompanyCode, Country))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCountry(CompanyCode, Country);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCountry(CompanyCode, Country))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCountry(CompanyCode, Country);
            Assert.IsTrue(result.CustomerSiteLocations == null);

        }
        [TestMethod]
        public void GetCustomerSiteLocationsByZipTest()
        {
            CompanyCode = "mg";
            ZipCode = "10120";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            mockRepository.Stub(x => x.GetCustomerSiteLocationsByZip(CompanyCode, ZipCode))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByZip(CompanyCode, ZipCode);
            Assert.IsNotNull(result.CustomerSiteLocations);

            result = _customerSiteLocationManager.GetCustomerSiteLocationsByZip(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _customerSiteLocationManager.GetCustomerSiteLocationsByZip(string.Empty, ZipCode);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            result = _customerSiteLocationManager.GetCustomerSiteLocationsByZip(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByZip(CompanyCode, ZipCode))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByZip(CompanyCode, ZipCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByZip(CompanyCode, ZipCode))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByZip(CompanyCode, ZipCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByZip(CompanyCode, ZipCode))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByZip(CompanyCode, ZipCode);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }
        [TestMethod]
        public void GetCustomerSiteLocationsBySiteNoTest()
        {
            CompanyCode = "mg";
            SiteNo = "100";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsBySiteNo(CompanyCode, SiteNo))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsBySiteNo(CompanyCode, SiteNo);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : SiteNo empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _customerSiteLocationManager.GetCustomerSiteLocationsBySiteNo(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _customerSiteLocationManager.GetCustomerSiteLocationsBySiteNo(string.Empty, SiteNo);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and SiteNo empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _customerSiteLocationManager.GetCustomerSiteLocationsBySiteNo(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsBySiteNo(CompanyCode, SiteNo))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsBySiteNo(CompanyCode, SiteNo);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsBySiteNo(CompanyCode, SiteNo))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsBySiteNo(CompanyCode, SiteNo);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsBySiteNo(CompanyCode, SiteNo))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsBySiteNo(CompanyCode, SiteNo);
            Assert.IsTrue(result.CustomerSiteLocations == null);

        }
        [TestMethod]
        public void GetCustomerSiteLocationsByAddressTest()
        {
            CompanyCode = "mg";
            Address = "wrew";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByAddress(CompanyCode, Address))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByAddress(CompanyCode, Address);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : CompanyCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByAddress(string.Empty, Address);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and Address empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByAddress(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByAddress(CompanyCode, Address))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByAddress(CompanyCode, Address);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByAddress(CompanyCode, Address))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByAddress(CompanyCode, Address);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByAddress(CompanyCode, Address))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByAddress(CompanyCode, Address);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }

        [TestMethod]
        public void GetCustomerSiteByTelephoneNo()
        {
            CompanyCode = "mg";
            TelePhone = "100";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, TelePhone))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, TelePhone);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : TelePhone empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByTelephoneNo(string.Empty, TelePhone);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and TelePhone empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByTelephoneNo(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, TelePhone))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, TelePhone);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, TelePhone))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, TelePhone);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, TelePhone))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, TelePhone);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }

        [TestMethod]
        public void GetCustomerSiteByFaxNoTest()
        {
            CompanyCode = "mg";
            FaxNo = "100";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByFaxNo(CompanyCode, FaxNo))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByFaxNo(CompanyCode, FaxNo);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : FaxNo empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByFaxNo(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByFaxNo(string.Empty, FaxNo);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and FaxNo empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByFaxNo(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByFaxNo(CompanyCode, FaxNo))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByFaxNo(CompanyCode, FaxNo);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByFaxNo(CompanyCode, FaxNo))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByFaxNo(CompanyCode, FaxNo);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByFaxNo(CompanyCode, FaxNo))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByFaxNo(CompanyCode, FaxNo);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }

        [TestMethod]
        public void GetCustomerSiteLocationsByCustomerCodeTest()
        {
            CompanyCode = "mg";
            CustomerCode = "100";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerCode(CompanyCode, CustomerCode))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerCode(CompanyCode, CustomerCode);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : CustomerCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerCode(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerCode(string.Empty, CustomerCode);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and CustomerCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerCode(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerCode(CompanyCode, CustomerCode))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerCode(CompanyCode, CustomerCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerCode(CompanyCode, CustomerCode))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerCode(CompanyCode, CustomerCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerCode(CompanyCode, CustomerCode))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerCode(CompanyCode, CustomerCode);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }

        [TestMethod]
        public void GetCustomerSiteLocationsByCustomerNameTest()
        {
            CompanyCode = "mg";
            CustomerName = "100";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerName(CompanyCode, CustomerName))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerName(CompanyCode, CustomerName);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : CustomerName empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerName(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CustomerName empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerName(string.Empty, CustomerName);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and CustomerName empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerName(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerName(CompanyCode, CustomerName))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerName(CompanyCode, CustomerName);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerName(CompanyCode, CustomerName))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerName(CompanyCode, CustomerName);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerName(CompanyCode, CustomerName))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByCustomerName(CompanyCode, CustomerName);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }

        [TestMethod]
        public void GetCustomerSiteLocationsByEmailIdTest()
        {
            CompanyCode = "mg";
            Emaild = "abc@gmail.com";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByEmailId(CompanyCode, Emaild))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByEmailId(CompanyCode, Emaild);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : Emaild empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByEmailId(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByEmailId(string.Empty, Emaild);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and Emaild empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByEmailId(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByEmailId(CompanyCode, Emaild))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByEmailId(CompanyCode, Emaild);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByEmailId(CompanyCode, Emaild))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByEmailId(CompanyCode, Emaild);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByEmailId(CompanyCode, Emaild))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByEmailId(CompanyCode, Emaild);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }


        [TestMethod]
        public void GetCustomerSiteLocationsByServiceEngineerCodeTest()
        {
            CompanyCode = "mg";
            ServiceEngineerCode = "100";
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerSiteLocationModel();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, ServiceEngineerCode))
                            .IgnoreArguments()
                            .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            var result = _customerSiteLocationManager.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, ServiceEngineerCode);
            Assert.IsNotNull(result.CustomerSiteLocations);

            //...Negative unit test case : ServiceEngineerCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByServiceEngineerCode(string.Empty, ServiceEngineerCode);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode and ServiceEngineerCode empty
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByServiceEngineerCode(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : Throw exception
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, ServiceEngineerCode))
                .IgnoreArguments()
                .Throw(new Exception());
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, ServiceEngineerCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output list is null
            CustomerSiteLocationList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, ServiceEngineerCode))
                         .IgnoreArguments()
                         .Return(CustomerSiteLocationList);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, ServiceEngineerCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, ServiceEngineerCode))
                          .IgnoreArguments()
                          .Return(null);
            _customerSiteLocationManager = new CustomerSiteLocationManager(mockRepository);
            result = _customerSiteLocationManager.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, ServiceEngineerCode);
            Assert.IsTrue(result.CustomerSiteLocations == null);
        }

        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer site location for unit test
        /// </summary>
        public void SetMockDataForCustomerSiteLocationModel()
        {
            #region SampleCustomerProjectOrderModelList
            _customerSiteLocationModelList.Add(new CustomerSiteLocationModel()
            {
                Name = "กระเบื้องกระดาษไทย",
                ERP_Customer_Code__c = "FI010",
                ERP_Site_Code__c = "0001",
                SVMXC__Street__c = "Siam Fiber Lamphang จังหวัด ลำปาง",
                SVMXC__City__c = string.Empty,
                County__c = string.Empty,
                SVMXC__State__c = string.Empty,
                Region__c = string.Empty,
                SVMXC__Country__c = "",
                SVMXC__Zip__c = "",
                SVMXC__Site_Fax__c = "0-2803-6733-5#114",
                SVMXC__Site_Phone__c = "0-2803-6733-5#114",
                SVMXC__Email__c = "",
                SVMXC__Latitude__c = "0.00000000",
                SVMXC__Longitude__c = "0.00000000",
                Altitude__c = "",
                SVMXC__Service_Engineer__c = string.Empty,
                Service_Engineer_Code__c = "",
                SVMXC__Stocking_Location__c = true

            });

            CustomerSiteLocationList.Add(new Sy80()
            {
                //Customer Code
                Sy80001 = "FI010",
                //Siteno
                Sy80002 = "0001",
                //Customer Name
                Sy80003 = "กระเบื้องกระดาษไทย",
                //ZipCode
                Sy80010 = "",
                //telephonno
                Sy80011 = "",
                //telefaxno
                Sy80012 = "",
                //Default Engineer
                Sy80046 = "",
                //City Code
                Sy80045 = "",
                //Country  Code
                Sy80048 = "",
                //Email
                Sy80049 = "",
                //Addressline1
                Sy80004 = "",
                //Addressline2
                Sy80005 = "",
                //Addressline3
                Sy80006 = "",
                //Addressline4
                Sy80007 = "",
                //Addressline5
                Sy80050 = "",
                //Addressline6
                Sy80051 = "",
                //Addressline7
                Sy80052 = "",
                //Longitude
                Sy80053 = "0.00000000",
                //Latitude
                Sy80054 = "0.00000000",
                //Altitude       
                Sy80055 = "0.00000000"
            });
            #endregion

        }
        #endregion
    }
}
