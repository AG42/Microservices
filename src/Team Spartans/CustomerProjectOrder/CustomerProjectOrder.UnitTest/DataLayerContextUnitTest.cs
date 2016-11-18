using System;
using System.Collections.Generic;
using System.Net.Http;
using CustomerProjectOrder.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Web.Http;
using CustomerProjectOrder.DataLayer.Entities.Datalake;
using CustomerProjectOrder.DataLayer.Interfaces;
using Microservices.Common.Interface;
using CustomerProjectOrder.Common;

namespace CustomerProjectOrder.UnitTest
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
        private const string ProjectnumberField = "pr01001";
        private const string ProjectnameField = "pr01009";
        private const string ProjectstartField = "pr01067";
        private const string ProjectendField = "pr01069";
        private const string CustomerPONumber = "pr01106";
        private const string CustomerAccountNumber = "pr01003";
        private readonly string _companyCode = "j1";
        private readonly string parentCompanyCode = "";
        private readonly List<Pr01> _pr01EntitiesList = new List<Pr01>();
        private ConfigReader _configReader;
        private IDatabase _mocksDatabaseEntities;
        private DataLayerContext _dataLayerContext;
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
            _dataLayerContext = new DataLayerContext() { Database = _mocksDatabaseEntities };
        }
        #endregion

        #region "Unit Test Methods"
        /// <summary>
        /// Unit testing Get all project orders for Company code
        /// </summary>
        [TestMethod]
        public void GetProjectOrdersByCompanyCodeTest()
        {
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Get<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY]))
                .IgnoreArguments()
                .Return(_pr01EntitiesList);

            
            var result = _dataLayerContext.GetProjectByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get all project orders for Company code Exception test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetProjectOrdersByCompanyCodeTestException()
        {
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            _mocksDatabaseEntities.Stub(x => x.Get<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY]))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByCompanyCode(_companyCode);
        }

        /// <summary>
        /// Unit testing Get Project Order by Project Order Number
        /// </summary>
        [TestMethod]
        public void GetProjectOrderByProjectNumberTest()
        {
            string projectNumber = "M100030010";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({ProjectnumberField})){EqualOperator}'{projectNumber.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Return(_pr01EntitiesList);

            var datalayer = new DataLayerContext();
            var result = datalayer.GetProjectByNumber(_companyCode, projectNumber);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Order by Project Order Number Exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetProjectOrderByProjectNumberTestException()
        {
            string projectNumber = "M100030010";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({ProjectnumberField})){EqualOperator}'{projectNumber.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

           // var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByNumber(_companyCode, projectNumber);
        }

        /// <summary>
        /// Unit testing Get Project Order by Project Order Name
        /// </summary>
        [TestMethod]
        public void GetProjectOrderByProjectNameTest()
        {
            string projectName = "INTE";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({ProjectnameField})){LikeOperator}'{projectName.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Return(_pr01EntitiesList);

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByName(_companyCode, projectName);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Order by Project Order Name Exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetProjectOrderByProjectNameTestException()
        {
            string projectName = "INTE";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({ProjectnameField})){LikeOperator}'{projectName.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByName(_companyCode, projectName);
        }

        /// <summary>
        /// Unit testing Get Project Order by Account
        /// </summary>
        [TestMethod]
        public void GetProjectOrderByAccountTest()
        {
            string account = "PL001";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({CustomerAccountNumber})){EqualOperator}'{account.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Return(_pr01EntitiesList);

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByAccount(_companyCode, account);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Order by Account
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetProjectOrderByAccountTestException()
        {
            string account = "PL001";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({CustomerAccountNumber})){EqualOperator}'{account.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByAccount(_companyCode, account);
        }

        /// <summary>
        /// Unit testing Get Project Order by Customer PO No
        /// </summary>
        [TestMethod]
        public void GetProjectOrderByCustomerPONoTest()
        {
            string customerPONo = "TH618000199004";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({CustomerPONumber})){EqualOperator}'{customerPONo.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Return(_pr01EntitiesList);

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByCustomerPONo(_companyCode, customerPONo);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Order by Customer PO No Exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetProjectOrderByCustomerPONoTestException()
        {
            string customerPONo = "TH618000199004";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({CustomerPONumber})){EqualOperator}'{customerPONo.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByCustomerPONo(_companyCode, customerPONo);
        }

        /// <summary>
        /// Unit testing Get Project Orders by Duration
        /// </summary>
        [TestMethod]
        public void GetProjectOrdersByDurationTest()
        {
            string startDate = "2010-12-14";
            string endDate = "2011-02-28";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({ProjectstartField})){GreaterThanEqualOperator}'{startDate.ToLower().Trim()}' {AndOperator} trim(lower({ProjectendField})){LessThanEqualOperator}'{endDate.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Return(_pr01EntitiesList);

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByDuration(_companyCode, startDate,endDate);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit testing Get Project Orders by Duration Exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetProjectOrdersByDurationTestException()
        {
            string startDate = "2010-12-14";
            string endDate = "2011-02-28";
            SetMockDataForCustomerProjectOrderHeaders();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({ProjectstartField})){GreaterThanEqualOperator}'{startDate.ToLower().Trim()}' {AndOperator} trim(lower({ProjectendField})){LessThanEqualOperator}'{endDate.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Throw(new NullReferenceException());

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByDuration(_companyCode, startDate, endDate);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DataLayerConstrutorTestException()
        {
            var datalayer = new DataLayerContext();
        }

        /// <summary>
        /// Unit testing Get Project Orders by Duration Exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetProjectOrdersByDurationTestHttpResponseException()
        {
            string startDate = "2010-12-14";
            string endDate = "2011-02-28";
            SetMockDataForCustomerProjectOrderHeaders();
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new ByteArrayContent(new byte[0]);
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);

            string filter = $"trim(lower({ProjectstartField})){GreaterThanEqualOperator}'{startDate.ToLower().Trim()}' {AndOperator} trim(lower({ProjectendField})){LessThanEqualOperator}'{endDate.ToLower().Trim()}'";
            _mocksDatabaseEntities.Stub(x => x.Where<Pr01>(tableName[Constants.DATABASE_TABLE_NAME_KEY], tableName[Constants.DATABASE_COLUMN_NAME_KEY], filter))
                .IgnoreArguments()
                .Throw(new HttpResponseException(response));

            //var datalayer = new DataLayerContext();
            var result = _dataLayerContext.GetProjectByDuration(_companyCode, startDate, endDate);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// To create a mock data of customer project order header for unit test
        /// </summary>
        private void SetMockDataForCustomerProjectOrderHeaders()
        {
            #region SampleCustomerProjectOrderHeaders
            _pr01EntitiesList.Add(new Pr01()
            {
                Pr01001 = "M100030010",
                Pr01003 = "PL001",
                Pr01106 = "7000128779",
                Pr01009 = "INTEGATION CARRIER",
                Pr01010 = "MDR - Residual Value",
                Pr01011 = "JCC-11-1120-SIY-01",
                Pr01067 = "2010-12-14 00:00:00.0",
                Pr01069 = "2011-02-28 00:00:00.0",
            });
            _pr01EntitiesList.Add(new Pr01()
            {
                Pr01001 = "P000030020",
                Pr01003 = "NX001",
                Pr01106 = "TH618000199004",
                Pr01009 = "NXP-Install Tripod&Access",
                Pr01010 = "SYS PRIME RETROFIT",
                Pr01011= "TH92-4240-Q534-R1",
                Pr01067 = "2009-09-29 00:00:00.0",
                Pr01069 = "2009-12-30 00:00:00.0"
            });
            _pr01EntitiesList.Add(new Pr01()
            {
                Pr01001 = "P200050060",
                Pr01003 = "SE003",
                Pr01106 = "SK1260221",
                Pr01009 = "Fire alarm control panel",
                Pr01010 = "SYS PRIME RETROFIT",
                Pr01011 = "JCC-11-0509-SIR-01",
                Pr01067 = "2011-08-09 00:00:00.0",
                Pr01069 = "2011-08-10 00:00:00.0"
            });
            #endregion
        }

        #endregion
    }
}
