//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CustomerProjectOrder.DataLayer;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Rhino.Mocks;

//namespace CustomerProjectOrder.UnitTest
//{
//    [TestClass]
//    public class ConfigReaderUnitTest
//    {
//        #region "Members"
//        private ConfigReader _configReader;
//        private string _companyCode;
//        #endregion

//        #region "Initialization"

//        /// <summary>
//        /// Initialize all the required resources
//        /// </summary>
//        [TestInitialize]
//        public void Initialize()
//        {
//            _configReader = new ConfigReader();
//            _companyCode = "j4";
//        }
//        #endregion


//        #region "Unit Test Methods"

//        [TestMethod]
//        public void ConstructorTest()
//        {
//            _configReader = new ConfigReader();
//            Assert.IsNotNull(_configReader);
//        }

//        //[TestMethod]
//        //public void GetDenodoViewUriConfigTest()
//        //{
//        //    string denodoView = _configReader.GetDenodoViewUri(_companyCode);
//        //    Assert.IsNotNull(denodoView);
//        //}

//        [TestMethod]
//        public void GetDatalakeTableNameConfigTest()
//        {
//            string datalateTableName = _configReader.GetDatabaseDetails(_companyCode);
//            Assert.IsNotNull(datalateTableName);
//        }

//        [TestMethod]
//        public void GetDenodoViewUriDatabaseTest()
//        {
//            _configReader.ReadFromDatabase = true;
//            Assert.IsNotNull(_configReader.ReadFromDatabase);
//            var test = _configReader;
//            string denodoView = test.GetDenodoViewUri(_companyCode);
//            Assert.IsNotNull(denodoView);
//        }

//        [TestMethod]
//        public void GetDatalakeTableNameDatabaseTest()
//        {
//            _configReader.ReadFromDatabase = true;
//            Assert.IsNotNull(_configReader.ReadFromDatabase);
//            string datalateTableName = _configReader.GetDatalakeTableName(_companyCode);
//            Assert.IsNotNull(datalateTableName);
//        }


//        #endregion

//    }
//}
