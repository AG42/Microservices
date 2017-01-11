using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerInformation.DataLayer;
using CustomerInformation.Model;
using System.Net.Http;
using CustomerInformation.DataLayer.Entities.Datalake;
using Microservices.Common.Interface;

namespace CustomerInformation.UnitTest
{
    [TestClass]
    public class DataLayerUnitTest
    {
        #region Declarations

        private const string CompanyCode = "bh";
        private const string CustomerCode = "C002";
        private const string CustomerName = "Customer";
        private const string CustomerAlternateName = "ARGON PROPERTIES WLL";
        private const string CustomerEmailId = "simonkynostan@meritas.bh";
        private const string CountryCode = "bh";
        private const string CustomerPhoneNumber = "00973 1 758 7271";
        private const string Category = "DSL";

        /*
                CustomerInformationModel _customerInformationModel =
                        new CustomerInformationModel();
        */
        Sl01 _sl01 = new Sl01();
        public List<CustomerInformationModel> CustomerInformationModelList = new List<CustomerInformationModel>();
        public List<Sl01> CustomerList = new List<Sl01>();
        IDatabase _mocks;
        DatabaseContext _databaseContext;
        #endregion

        #region UnitTest
        [TestInitialize]
        public void Initialize()
        {
            // HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:5001/") };

            // Arrange
            var client = MockRepository.GenerateMock<HttpClient>();
            _mocks = MockRepository.GenerateMock<IDatabase>();
            _databaseContext = new DatabaseContext() { Database = _mocks };
        }


        [TestMethod]
        public void GetCustomersTest()
        {
            SetMockDataForCustomerModels();
            _mocks.Stub(x => x.Get<Sl01>("tableName","columns"))
                 .IgnoreArguments()
                 .Return(CustomerList);
            var result = _databaseContext.GetCustomers("xj");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByIdTest()
        {
            SetMockDataForCustomerModels();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "customerCode"))
                 .IgnoreArguments()
                 .Return(CustomerList);
           
            var result = _databaseContext.GetCustomerById(CompanyCode, CustomerCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByNameTest()
        {
            SetMockDataForCustomerModels();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "customerCode"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByName(CompanyCode, CustomerName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByAlternateNameTest()
        {
            SetMockDataForCustomerModels();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(CustomerList);

            var result = _databaseContext.GetCustomerByAlternateName(CompanyCode, CustomerAlternateName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByEmailIdTest()
        {
            SetMockDataForCustomerModels();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByEmailId(CompanyCode, CustomerEmailId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByCountryCodeTest()
        {
            SetMockDataForCustomerModels();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByCountryCode(CompanyCode, CountryCode);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetCustomerByTelephoneNumberTest()
        {
            SetMockDataForCustomerModels();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByPhoneNumber(CompanyCode, CustomerPhoneNumber);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetCustomerByCategoryTest()
        {
            SetMockDataForCustomerModels();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByCategory(CompanyCode, Category);
            Assert.IsNotNull(result);
        }


        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void DataLayerConstructorExceptionTest()
        //{
        //    SetMockDataForCustomerModels();

        //    mocks.Stub(x => x.Get<Sl01>("", "columns"))
        //         .IgnoreArguments().Throw(new Exception());

        //    var dataLayer = new DatabaseContext() {Database = mocks};
        //    var result = dataLayer.GetCustomers(null);
        //    Assert.IsNotNull(result);

        //}

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void GetCustomerByIdExceptionTest()
        //{
        //    SetMockDataForCustomerModels();
        //    var mocks = MockRepository.GenerateMock<IDatabase>();

        //    mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "customerCode"))
        //        .IgnoreArguments().Throw(new Exception());

        //    var dataLayer = new DatabaseContext() {Database = mocks};
        //    var custresult = dataLayer.GetCustomerById(null, "");
        //    Assert.IsNotNull(custresult);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void GetCustomerByNameExceptionTest()
        //{
        //    SetMockDataForCustomerModels();
        //    mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "customerCode")).Throw(new Exception());

        //    var dataLayer = new DatabaseContext() {Database = mocks};
        //    var result = dataLayer.GetCustomerByName(null, "");
        //    Assert.IsNotNull(result);
        //}

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
