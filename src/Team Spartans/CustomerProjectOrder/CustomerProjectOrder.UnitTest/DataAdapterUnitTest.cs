using System.Collections.Generic;
using CustomerProjectOrder.DataLayer;
using CustomerProjectOrder.DataLayer.Adapters;
using CustomerProjectOrder.DataLayer.Entities.Datalake;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CustomerProjectOrder.UnitTest
{
    [TestClass]
    public class DataAdapterUnitTest
    {
        #region "Members"
        private readonly List<Pr01> _pr01EntitiesList = new List<Pr01>();
        private ConfigReader _configReader;
        private string _companyCode = "j4";
        #endregion

        #region "Initialization"

        /// <summary>
        /// Initialize all the required resources
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _configReader = new ConfigReader();
        }

        #endregion

        #region "Unit Test Methods"

        /// <summary>
        /// Unit Testing Get T table method
        /// </summary>
        [TestMethod]
        public void GetTest()
        {
            string connectionString = _configReader.DatalakeConnectionString;
            string tableName = _configReader.GetDatalakeTableName(_companyCode);
            string query = $"Select {GetColumns()} from {tableName}";
            DatalakeAdapter datalakeAdapter = new DatalakeAdapter();
            datalakeAdapter.ConnectionString = connectionString;
            var result = datalakeAdapter.Get<Pr01>(query);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PropertiesTest()
        {
            string connectionString = _configReader.DatalakeConnectionString;
            var test = new DatalakeAdapter();
            test.ConnectionString = connectionString;
            Assert.IsNotNull(test.ConnectionString);
        }

        [TestMethod]
        public void ListToDatasetTest()
        {
            SetMockDataForCustomerProjectOrderHeaders();
            var data = _pr01EntitiesList.ToDataSet<Pr01>();
            Assert.IsNotNull(data);
        }

        #endregion

        #region "Private Methods"
        private string GetColumns()
        {
            return
                "pr01001,pr01009,pr01010,pr01011,pr01067,pr01069,pr01003,pr01106";
        }
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
                Pr01011 = "TH92-4240-Q534-R1",
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
