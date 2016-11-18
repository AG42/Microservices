using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSiteLocation.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerSiteLocation.DataLayer.Entities.Datalake;
using CustomerSiteLocation.DataLayer.Interfaces;
using Rhino.Mocks;

namespace CustomerSiteLocation.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {

        #region "Members"

        private const string EqualOperator = "=";
        private const string LessThanEqualOperator = "<=";
        private const string GreaterThanEqualOperator = ">=";
        private const string AndOperator = "AND";
        private const string LikeOperator = "like";
        private const string EmailField = "sy80049";
        private const string CustomerCodeField = "sy80001";
        private const string ZipField = "sy80010";
        private const string SiteNoField = "sy80002";
        private const string CountryField = "sy80048";
        private const string CustomerNameField = "sy80003";

        private const string FaxNoField = "sy80012";
        private const string TelephoneNoField = "sy80011";
        private const string AddressField = "sy80004";

        private readonly string _companyCode = "j1";
        private readonly List<Sy80> _sy80EntitiesList = new List<Sy80>();
        private ConfigReader _configReader;
        private IDatalakeEntities _mocksDatalakeEntities;
        private string datalakeTableNameKey = "";
        private string parentCompanyCode = "";

        #endregion

        #region "Initialization"

        /// <summary>
        /// Initialize all the required resources
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _mocksDatalakeEntities = MockRepository.GenerateMock<IDatalakeEntities>();
            _configReader = new ConfigReader();
        }

        #endregion

        #region "Unit Test Methods"
        /// <summary>
        /// Unit testing Get all sites for Company code
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteByCompanyCodeTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Company code Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteByCompanyCodeTestException()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByCompanyCode(_companyCode);



        }

        /// <summary>
        /// Unit testing Get all sites for Fax no test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteByFaxNoTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByFaxNo(_companyCode, FaxNoField);
            Assert.IsNotNull(result);

        }

        /// <summary>
        /// Unit testing Get all sites for Fax No Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteByFaxNoTestException()
        {

            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByFaxNo(_companyCode, FaxNoField);
        }

        /// <summary>
        /// Unit testing Get all sites for Telephone no test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteByTelephoneNoTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByTelephoneNo(_companyCode, TelephoneNoField);
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Unit testing Get all sites for Telephone no Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteByTelephoneNoTestException()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByTelephoneNo(_companyCode, TelephoneNoField);
        }
        /// <summary>
        /// Unit testing Get all sites for Address test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByAddressTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByAddress(_companyCode, AddressField);
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Unit testing Get all sites for Address Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsByAddressTestException()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByAddress(_companyCode, AddressField);
        }

        /// <summary>
        /// Unit testing Get all sites for Customer Name test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByCustomerNameTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByAddress(_companyCode, CustomerNameField);
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Unit testing Get all sites for Customer Name Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsByCustomerNameTestException()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());
            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByCustomerName(_companyCode, CustomerNameField);
        }

        /// <summary>
        /// Unit testing Get all sites for Country  test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByCountryTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByCountry(_companyCode, CountryField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Country Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsByCountryTestException()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByCountry(_companyCode, CountryField);
        }
        /// <summary>
        /// Unit testing Get all sites for Site No test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsBySiteNoTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsBySiteNo(_companyCode, SiteNoField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Site No Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsBySiteNoTestException()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsBySiteNo(_companyCode, SiteNoField);
        }

        /// <summary>
        /// Unit testing Get all sites for Zip test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByZipTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName,_companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByZip(_companyCode, ZipField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Zip Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsByZipTestException()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(_configReader.GetDatalakeTableName(tableName, datalakeTableNameKey,parentCompanyCode),_companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByZip(_companyCode, ZipField);
        }
        /// <summary>
        /// Unit testing Get all sites for Customer Code test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByCustomerCodeTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(_configReader.GetDatalakeTableName(tableName, datalakeTableNameKey,parentCompanyCode),_companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByCustomerCode(_companyCode, CustomerCodeField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for  Customer Code Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsByCustomerCodeTestExcepiton()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName,_companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByCustomerCode(_companyCode, CustomerCodeField);
        }

        /// <summary>
        /// Unit testing Get all sites for  Email test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByEmailIdTest()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByEmailId(_companyCode, EmailField);
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// Unit testing Get all sites for  Email Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsByEmailIdTestException()
        {
            SetMockDataForCustomerSiteLocationHeaders();
            string tableName = _configReader.GetDatalakeTableName(_companyCode, datalakeTableNameKey, parentCompanyCode);

            _mocksDatalakeEntities.Stub(x => x.Get<Sy80>(tableName, _companyCode))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var datalayer = new DataLayerContext(_mocksDatalakeEntities);
            var result = datalayer.GetCustomerSiteLocationsByEmailId(_companyCode, EmailField);
        }



        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DataLayerConstrutorTestException()
        {
            var datalayer = new DataLayerContext(null);
        }
        #endregion
        #region Private Methods

        /// <summary>
        /// To create a mock data of customerSiteLocation header for unit test
        /// </summary>
        private void SetMockDataForCustomerSiteLocationHeaders()
        {
            #region SampleCustomerSiteLocationHeaders

            _sy80EntitiesList.Add(new Sy80()
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

            _sy80EntitiesList.Add(new Sy80()
            {
                Sy80001 = "1945",
                Sy80002 = "0004",
                Sy80003 = "HABTOOR STAFF ACCOM. DIP BLOCK H&J",
                Sy80004 = "DIP P.O. BOX 24454 Dubai",
                Sy80005 = "",
                Sy80006 = "",
                Sy80007 = "",
                Sy80010 = "",
                Sy80011 = "04-3995000",
                Sy80012 = "04-3992262",
                Sy80045 = "",
                Sy80046 = "",
                Sy80048 = "",
                Sy80049 = "",
                Sy80050 = "",
                Sy80051 = "",
                Sy80052 = "",
                Sy80053 = "",
                Sy80054 = "",
                Sy80055 = ""
            });


            #endregion
        }

        #endregion
    }
}
