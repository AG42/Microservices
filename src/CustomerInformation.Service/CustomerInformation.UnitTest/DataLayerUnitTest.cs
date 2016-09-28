using System;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerInformation.DataLayer;
using CustomerInformation.Model;
using System.Net.Http;
using CustomerInformation.DataLayer.Entities.Datalake;
using DenodoAdapter;
using CustomerInformation.DataLayer.Interfaces;
using System.Linq;

namespace CustomerInformation.UnitTest
{
    [TestClass]
    public class DataLayerUnitTest
    {
        #region Declarations

        private const string COMPANY_CODE = "bh";
        private const string CUSTOMER_CODE = "C002";
        private const string CUSTOMER_NAME = "Customer";

        /*
                CustomerInformationModel _customerInformationModel =
                        new CustomerInformationModel();
        */
        Sl01 _sl01 = new Sl01();
        public List<CustomerInformationModel> CustomerInformationModelList = new List<CustomerInformationModel>();
        public List<Sl01> CustomerList = new List<Sl01>();
        IDatalakeEntities _datalakeEntities;
        IDenodoContext mocks;
        DatabaseContext denodoContext;
        #endregion

        #region UnitTest
        [TestInitialize]
        public void Initialize()
        {
            // HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:5001/") };

            // Arrange
            var client = MockRepository.GenerateMock<HttpClient>();
            mocks = MockRepository.GenerateMock<IDenodoContext>();
            _datalakeEntities = MockRepository.GenerateMock<IDatalakeEntities>();
            denodoContext = new DatabaseContext(_datalakeEntities) { DenodoContext = mocks };
        }


        [TestMethod]
        public void GetCustomersTest()
        {
            SetMockDataForCustomerModels();

            _datalakeEntities.Stub(x => x.Get<Sl01>("tableName"))
                 .IgnoreArguments()
                 .Return(CustomerList);
            var result = denodoContext.GetCustomers("xj");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByIdTest()
        {
            SetMockDataForCustomerModels();

            _datalakeEntities.Stub(x => x.Where<Sl01>("tableName", "customerCode"))
                 .IgnoreArguments()
                 .Return(CustomerList);
           
            var result = denodoContext.GetCustomerById(COMPANY_CODE, CUSTOMER_CODE);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByNameTest()
        {
            SetMockDataForCustomerModels();

            _datalakeEntities.Stub(x => x.Where<Sl01>("tableName", "customerCode"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = denodoContext.GetCustomerByName(COMPANY_CODE, CUSTOMER_NAME);
            Assert.IsNotNull(result);
        }

        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DataLayerConstructorExceptionTest()
        {
            SetMockDataForCustomerModels();

            _datalakeEntities.Stub(x => x.Get<Sl01>(""))
                 .IgnoreArguments().Throw(new Exception());

            var dataLayer = new DatabaseContext(_datalakeEntities);
            var result = dataLayer.GetCustomers(null);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerByIdExceptionTest()
        {
            SetMockDataForCustomerModels();
            var mocks = MockRepository.GenerateMock<IDenodoContext>();

            _datalakeEntities.Stub(x => x.Where<Sl01>("tableName", "customerCode"))
                .IgnoreArguments().Throw(new Exception());

            var dataLayer = new DatabaseContext(_datalakeEntities);
            var custresult = dataLayer.GetCustomerById(null, "");
            Assert.IsNotNull(custresult);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerByNameExceptionTest()
        {
            SetMockDataForCustomerModels();
            _datalakeEntities.Stub(x => x.Where<Sl01>("tableName", "customerCode")).Throw(new Exception());

            var dataLayer = new DatabaseContext(_datalakeEntities);
            var result = dataLayer.GetCustomerByName(null, "");
            Assert.IsNotNull(result);
        }

        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerModels()
        {
            #region SampleDataCustomerMaster
            _sl01 = new Sl01
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
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            };

            CustomerList.Add(new Sl01()
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
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"

                //String.Format(customerMaster.sl01003 + "{0}" + customerMaster.sl01004 + "{0}" + customerMaster.sl01005 + "{0}" + customerMaster.sl01099 + "{0}" +
                //                customerMaster.sl0194 + "{0}" + customerMaster.sl01195 + "{0}" + customerMaster.sl01196, Environment.NewLine),
            });
            CustomerList.Add(new Sl01()
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
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            });
            CustomerList.Add(new Sl01()
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
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            });
            #endregion
        }
        #endregion
    }
}
