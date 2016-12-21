using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContractInformation.Common;

namespace ContractInformation.UnitTest
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
            _companyCode = "j4";
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
            Dictionary<string, string>  databaseDetails = _configReader.GetDatabaseTableName(_companyCode,_parentCompanyCode);
            Assert.IsNotNull(databaseDetails);
        }

        #endregion

    }
}
