using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedSecuredMargin.Common;
using System.Collections.Generic;

namespace CreditStatus.UnitTest
{
    [TestClass]
    public class ConfigReaderUnitTest
    {
        #region "Members"
        private ConfigReader _configReader;
        private string _companyCode;
        private string _parentCompanyCode;
        #endregion

        #region "Initialization"

        /// <summary>
        /// Initialize all the required resources
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _configReader = new ConfigReader();
            _companyCode = "na";
            _parentCompanyCode = string.Empty;
        }
        #endregion

        #region "Unit Test Methods"

        [TestMethod]
        public void ConstructorTest()
        {
            _configReader = new ConfigReader();
            Assert.IsNotNull(_configReader);
        }

        [TestMethod]
        public void GetDatalakeTableNameConfigTest()
        {
            Dictionary<string, string> databaseDetails = _configReader.GetDatabaseTableName(_companyCode, _parentCompanyCode);
            Assert.IsNotNull(databaseDetails);
        }

        #endregion
    }
}
