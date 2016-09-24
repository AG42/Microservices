using System;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerInformation.DataLayer.Entities;
using CustomerInformation.DataLayer;
using CustomerInformation.Model;
using System.Net.Http;
using DenodoAdapter;

namespace CustomerInformation.UnitTest
{
    [TestClass]
    public class DataLayerUnitTest
    {
        #region Declarations
        string companyCode = "xj";
        string customerCode = "C002";
        string customerName = "Customer";
        CustomerInformationModel customerInformationModel =
                new CustomerInformationModel();
        CustomerMaster customerMaster = new CustomerMaster();
        public List<CustomerInformationModel> customerInformationModelList = new List<CustomerInformationModel>();
        public List<CustomerMaster> customerList = new List<CustomerMaster>();
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:5001/") };

            // Arrange
            var client = MockRepository.GenerateMock<HttpClient>();
            var spec = MockRepository.GenerateMock<IDenodoContext>();
        }


        [TestMethod]
        public void GetCustomersTest()
        {
            //string BASE_URI = "http://c201mf92:9090/server/poc/";
            string _viewUri = "{CompanyCode}_sl01/views/sl01";
            string COMPANYCODE_PLACEHOLDER = "{CompanyCode}";
            string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);
            SetMockDataForCustomerModels();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            mocks.Stub(x => x.GetData<CustomerMaster>(companyViewUri))
                 .IgnoreArguments()
                 .Return(customerList);

            var dataLayer = new DataLayerContext(mocks);
            var result = dataLayer.GetCustomers("xj");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByIdTest()
        {
            //string BASE_URI = "http://c201mf92:9090/server/poc/";
            string _viewUri = "{CompanyCode}_sl01/views/sl01";
            string COMPANYCODE_PLACEHOLDER = "{CompanyCode}";
            string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);

            SetMockDataForCustomerModels();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            mocks.Stub(x => x.GetData<CustomerMaster>(companyViewUri, customerCode))
                 .IgnoreArguments()
                 .Return(customerMaster);

            var dataLayer = new DataLayerContext(mocks);
            var result = dataLayer.GetCustomerById(companyCode, customerCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByNameTest()
        {
            string _viewUri = "{CompanyCode}_sl01/views/sl01";
            string COMPANYCODE_PLACEHOLDER = "{CompanyCode}";
            string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);

            SetMockDataForCustomerModels();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            mocks.Stub(x => x.SearchData<CustomerMaster>(companyViewUri, customerName))
                 .IgnoreArguments()
                 .Return(customerList);

            var dataLayer = new DataLayerContext(mocks);
            var result = dataLayer.GetCustomerByName(companyCode, customerName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.UriFormatException))]
        public void GetCustomersExceptionTest()
        {
            SetMockDataForCustomerModels();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            var dataLayer = new DataLayerContext();
            var result = dataLayer.GetCustomers("01");
            Assert.IsNotNull(result);

        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidCastException))]
        public void DataLayerConstructorExceptionTest()
        {
            SetMockDataForCustomerModels();
            var client = MockRepository.GenerateMock<HttpClient>();

            //mocks.Stub(x => x.GetData<CustomerMaster>(companyViewUri, "'"))
            //     .IgnoreArguments()
            //     .Return(customerMaster);

            var dataLayer = new DataLayerContext(client);
            var result = dataLayer.GetCustomers(null);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        [ExpectedException(typeof(System.UriFormatException))]
        public void GetCustomerByIdExceptionTest()
        {
            SetMockDataForCustomerModels();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            //mocks.Stub(x => x.GetData<CustomerMaster>(companyViewUri, customerCode))
            //     .IgnoreArguments()
            //     .Return(customerMaster);

            var dataLayer = new DataLayerContext();
            var custresult = dataLayer.GetCustomerById("bh", "");
            Assert.IsNotNull(custresult);
        }

        [TestMethod]
        [ExpectedException(typeof(System.UriFormatException))]
        public void GetCustomerByNameExceptionTest()
        {
            SetMockDataForCustomerModels();
            //var mocks = MockRepository.GenerateMock<IDenodoContext>();
            //mocks.Stub(x => x.SearchData<CustomerMaster>(companyViewUri, customerName)).Throw(new Exception());

            var dataLayer = new DataLayerContext();
            var result = dataLayer.GetCustomerByName(null, "");
            Assert.IsNotNull(result);
        }
        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerModels()
        {
            #region SampleDataCustomerMaster
            customerMaster = new CustomerMaster
            {
                sl01001 = "C002",
                sl01002 = "Customer Test2",
                sl01003 = "Commer Zone",
                sl01004 = "Brad Pitt, Eric Bana, Orlando Bloom",
                sl01005 = "Pune",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "428201",
                sl0194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            };

            customerList.Add(new CustomerMaster()
            {
                sl01001 = "C006",
                sl01002 = "Customer 2",
                sl01003 = "Commer Zone",
                sl01004 = "Brad Pitt, Eric Bana, Orlando Bloom",
                sl01005 = "Pune",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "428201",
                sl0194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"

                //String.Format(customerMaster.sl01003 + "{0}" + customerMaster.sl01004 + "{0}" + customerMaster.sl01005 + "{0}" + customerMaster.sl01099 + "{0}" +
                //                customerMaster.sl0194 + "{0}" + customerMaster.sl01195 + "{0}" + customerMaster.sl01196, Environment.NewLine),
            });
            customerList.Add(new CustomerMaster()
            {
                sl01001 = "C009",
                sl01002 = "Customer 1",
                sl01003 = "Commer Zone 1234",
                sl01004 = "Qwerty Leonardo DiCaprio, Kate Winslet",
                sl01005 = "Pune qwerty",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "425001",
                sl0194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            });
            customerList.Add(new CustomerMaster()
            {
                sl01001 = "C004",
                sl01002 = "Customer 1",
                sl01003 = "Commer Zone 12",
                sl01004 = "Leonardo DiCaprio, Kate Winslet qwerty",
                sl01005 = "Pune",
                sl01099 = "AddressLine41",
                sl01152 = "CityPune Code",
                sl01011 = "9898543210",
                sl01022 = "USD",
                sl01083 = "405201",
                sl0194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            });
            #endregion
        }
        #endregion
    }


}
