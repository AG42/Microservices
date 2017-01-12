using System;
using System.Collections.Generic;
using CustomerSiteLocation.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerSiteLocation.DataLayer.Entities.Datalake;
using Rhino.Mocks;
using Microservices.Common.Interface;
using CustomerSiteLocation.Common;

namespace CustomerSiteLocation.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {

        #region "Members"

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
        IDatabase _mocksDatabaseEntities;
        DataLayerContext _datalayer;
        private string parentCompanyCode = "";

        #endregion

        #region "Initialization"

        /// <summary>
        /// Initialize all the required resources
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _mocksDatabaseEntities = MockRepository.GenerateMock<IDatabase>();
            _configReader = new ConfigReader();
            _datalayer = new DataLayerContext() { objDb = _mocksDatabaseEntities };
            SetMockDataForCustomerSiteLocationHeaders();
        }

        #endregion

        #region "Unit Test Methods"
        /// <summary>
        /// Unit testing Get all sites for Company code
        /// </summary>
        /// 

        [TestMethod]
        public void GetCustomerSiteByCompanyCodeTest()
        {
            var dicTableName = new Dictionary<string, string>();
           
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Get<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext();
            var result = _datalayer.GetCustomerSiteLocationsByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Company code Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteByCompanyCodeTestException()
        {
            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Get<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var result = _datalayer.GetCustomerSiteLocationsByCompanyCode(_companyCode);
        }

        /// <summary>
        /// Unit testing Get all sites for Fax no test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteByFaxNoTest()
        {
            var dicTableName = new Dictionary<string, string>();
           
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var result = _datalayer.GetCustomerSiteLocationsByFaxNo(_companyCode, FaxNoField);
            Assert.IsNotNull(result);

        }

        /// <summary>
        /// Unit testing Get all sites for Fax No Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteByFaxNoTestException()
        {
            var dicTableName = new Dictionary<string, string>();

            dicTableName = _configReader.GetDatabaseTableName(_companyCode, parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var result = _datalayer.GetCustomerSiteLocationsByFaxNo(_companyCode, FaxNoField);
        }

        /// <summary>
        /// Unit testing Get all sites for Telephone no test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteByTelephoneNoTest()
        {
            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>(dicTableName[Constants.TableNameKey], dicTableName[Constants.ColumnNameKey], "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var result = _datalayer.GetCustomerSiteLocationsByTelephoneNo(_companyCode, TelephoneNoField);
            Assert.IsNotNull(result);
        }
        
        /// <summary>
        /// Unit testing Get all sites for Address test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByAddressTest()
        {
            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var result = _datalayer.GetCustomerSiteLocationsByAddress(_companyCode, AddressField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Customer Name test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByCustomerNameTest()
        {
            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var result = _datalayer.GetCustomerSiteLocationsByAddress(_companyCode, CustomerNameField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Country  test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByCountryTest()
        {
            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var datalayer = new DataLayerContext();
            var result = _datalayer.GetCustomerSiteLocationsByCountry(_companyCode, CountryField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Country Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsByCountryTestException()
        {

            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var result = _datalayer.GetCustomerSiteLocationsByCountry(_companyCode, CountryField);
        }
        /// <summary>
        /// Unit testing Get all sites for Site No test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsBySiteNoTest()
        {
            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var result = _datalayer.GetCustomerSiteLocationsBySiteNo(_companyCode, SiteNoField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for Site No Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetCustomerSiteLocationsBySiteNoTestException()
        {
            var dicTableName = new Dictionary<string, string>();
           
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            var result = _datalayer.GetCustomerSiteLocationsBySiteNo(_companyCode, SiteNoField);
        }

        /// <summary>
        /// Unit testing Get all sites for Zip test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByZipTest()
        {
            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var result = _datalayer.GetCustomerSiteLocationsByZip(_companyCode, ZipField);
            Assert.IsNotNull(result);
        }

         /// <summary>
        /// Unit testing Get all sites for Customer Code test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByCustomerCodeTest()
        {
            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var result = _datalayer.GetCustomerSiteLocationsByCustomerCode(_companyCode, CustomerCodeField);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all sites for  Email test
        /// </summary>
        [TestMethod]
        public void GetCustomerSiteLocationsByEmailIdTest()
        {

            var dicTableName = new Dictionary<string, string>();
            
            dicTableName = _configReader.GetDatabaseTableName(_companyCode,  parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Where<Sy80>("tablename", "columnname", "condition"))
                .IgnoreArguments()
                .Return(_sy80EntitiesList);

            var result = _datalayer.GetCustomerSiteLocationsByEmailId(_companyCode, EmailField);
            Assert.IsNotNull(result);
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
