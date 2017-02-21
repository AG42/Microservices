using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SalesOrder.Common;

namespace SalesOrder.UnitTest
{
    [TestClass]
    public class ConfigReaderUnitTest
    {
        #region Variables
        private ConfigReader _configReader;
        private string _companyCode;
        private string _parentCompanyCode;
        #endregion


        #region initialisation
        [TestInitialize]
        public void Initialize()
        {
            _configReader = new ConfigReader();
            _companyCode = "na";
            _parentCompanyCode = string.Empty;
        }
        #endregion


        #region Test methods
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ConstructorTest()
        {
            _configReader = new ConfigReader();
            Assert.IsNotNull(_configReader);
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetDatalakeTableNameConfigTest()
        {
            Dictionary<string, string> databaseDetails = _configReader.GetDatabaseTableName(_companyCode, _parentCompanyCode);
            Assert.IsNotNull(databaseDetails);
        }
        #endregion
    }
}