using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSiteLocation.DataLayer;
using CustomerSiteLocation.DataLayer.Adapters;
using CustomerSiteLocation.DataLayer.Entities.Datalake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerSiteLocation.UnitTest
{
    [TestClass]
    public class DataAdapterUnitTest
    {
        #region "Members"
        private readonly List<Sy80> _sy80EntitiesList = new List<Sy80>();
        private ConfigReader _configReader;
        private string _companyCode = "J1";
        private string datalakeTableNameKey="NULL";
        private string parentCompanyCode = "NULL";

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
            string tableName = _configReader.GetDatalakeTableName(_companyCode,datalakeTableNameKey, parentCompanyCode);
            string query = $"Select {GetColumns()} from {tableName}";
            DatalakeAdapter datalakeAdapter = new DatalakeAdapter();
            datalakeAdapter.ConnectionString = connectionString;
            var result = datalakeAdapter.Get<Sy80>(query);
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
            SetMockDataForCustomerSiteLocationHeaders();
            var data = _sy80EntitiesList.ToDataSet<Sy80>();
            Assert.IsNotNull(data);
        }

        #endregion

        #region Private Methods
        private string GetColumns()
        {
            return
                "sy80001,sy80002,sy80003,sy80004,sy80005,sy80006,sy80007,sy80050,sy80051,sy80045,sy80048,sy80010,sy80012,sy80011,sy80049,sy80054,sy80053,sy80055,sy80046";
        }
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
