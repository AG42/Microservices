using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerProjectOrder.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CustomerProjectOrder.UnitTest
{
    [TestClass]
    public class ConfigReaderUnitTest
    {
        #region "Members"
        private ConfigReader _configReader;
        private string _companyCode;
        private string parentCompanyCode;
        #endregion

        #region "Initialization"

        /// <summary>
        /// Initialize all the required resources
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _configReader = new ConfigReader();
            _companyCode = "j4";
        }
        #endregion


        #region "Unit Test Methods"

        [TestMethod]
        public void ConstructorTest()
        {
            _configReader = new ConfigReader();
            Assert.IsNotNull(_configReader);
        }

        //[TestMethod]
        //public void GetDenodoViewUriConfigTest()
        //{
        //    string denodoView = _configReader.GetDenodoViewUri(_companyCode);
        //    Assert.IsNotNull(denodoView);
        //}

        [TestMethod]
        public void GetDatalakeTableNameConfigTest()
        {
            var databaseDetails = new Dictionary<string, string>();
            databaseDetails = _configReader.GetDatabaseDetails(_companyCode,parentCompanyCode);
            Assert.IsNotNull(databaseDetails);
        }

        //[TestMethod]
        //public void GetDenodoViewUriDatabaseTest()
        //{
        //    _configReader.ReadFromDatabase = true;
        //    Assert.IsNotNull(_configReader.ReadFromDatabase);
        //    var test = _configReader;
        //    string denodoView = test.GetDenodoViewUri(_companyCode);
        //    Assert.IsNotNull(denodoView);
        //}

        //[TestMethod]
        //public void GetDatabaseDetailsTest()
        //{
        //    _configReader.ReadFromDatabase = true;
        //    Assert.IsNotNull(_configReader.ReadFromDatabase);
        //    var databaseDetails = new Dictionary<string,string>();
        //    databaseDetails = _configReader.GetDatabaseDetails(_companyCode, parentCompanyCode);
        //    Assert.IsNotNull(databaseDetails);
        //}


        #endregion

    }
}
