//using System.Collections.Generic;
//using CustomerProjectOrder.DataLayer.Entities.Datalake;
//using CustomerProjectOrder.DataLayer.Interfaces;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Rhino.Mocks;

//namespace CustomerProjectOrder.UnitTest
//{
//    [TestClass]
//    public class DatalakeEntitiesUnitTest
//    {
//        #region "Members"

//        private IDatalakeAdapter _datalakeAdapter;
//        private readonly List<Pr01> _pr01EntitiesList = new List<Pr01>();
//        #endregion

//        #region "Initialization"

//        /// <summary>
//        /// Initialize all the required resources
//        /// </summary>
//        [TestInitialize]
//        public void Initialize()
//        {
//            _datalakeAdapter = MockRepository.GenerateMock<IDatalakeAdapter>();
//            _datalakeAdapter.ConnectionString = "TestConnectionString";
//        }

//        #endregion

//        #region "Unit Test Methods"
//        /// <summary>
//        /// Unit Testing Get table method
//        /// </summary>
//        [TestMethod]
//        public void GetDataFromTable()
//        {
//            SetMockDataForCustomerProjectOrderHeaders();
//            _datalakeAdapter.Stub(x => x.Get<Pr01>("")).IgnoreArguments().Return(_pr01EntitiesList);
//            var data = new DatalakeEntities(_datalakeAdapter);
//            var result = data.Get<Pr01>("table");
//            Assert.IsNotNull(result);
//        }

//        /// <summary>
//        /// Unit testing Get method with where clause
//        /// </summary>
//        [TestMethod]
//        public void GetDataFromTableWithWhereCondition()
//        {
//            _datalakeAdapter.Stub(x => x.Get<Pr01>("")).IgnoreArguments().Return(_pr01EntitiesList);
//            var data = new DatalakeEntities(_datalakeAdapter);
//            var result = data.Where<Pr01>("table", "Condition");
//            Assert.IsNotNull(result);
//        }

//        /// <summary>
//        /// Unit testing Public properties
//        /// </summary>
//        [TestMethod]
//        public void PublicPropertiesTest()
//        {
//            var data = new DatalakeEntities(_datalakeAdapter);
//            data.ConnectionString = "TestConnectionString";
//            Assert.IsNotNull(data.ConnectionString);
//        }

//        #endregion

//        #region Private Methods
//        /// <summary>
//        /// To create a mock data of customer project order header for unit test
//        /// </summary>
//        private void SetMockDataForCustomerProjectOrderHeaders()
//        {
//            #region SampleCustomerProjectOrderHeaders
//            _pr01EntitiesList.Add(new Pr01()
//            {
//                Pr01001 = "M100030010",
//                Pr01003 = "PL001",
//                Pr01106 = "7000128779",
//                Pr01009 = "INTEGATION CARRIER",
//                Pr01010 = "MDR - Residual Value",
//                Pr01011 = "JCC-11-1120-SIY-01",
//                Pr01067 = "2010-12-14 00:00:00.0",
//                Pr01069 = "2011-02-28 00:00:00.0",
//            });
//            _pr01EntitiesList.Add(new Pr01()
//            {
//                Pr01001 = "P000030020",
//                Pr01003 = "NX001",
//                Pr01106 = "TH618000199004",
//                Pr01009 = "NXP-Install Tripod&Access",
//                Pr01010 = "SYS PRIME RETROFIT",
//                Pr01011 = "TH92-4240-Q534-R1",
//                Pr01067 = "2009-09-29 00:00:00.0",
//                Pr01069 = "2009-12-30 00:00:00.0"
//            });
//            _pr01EntitiesList.Add(new Pr01()
//            {
//                Pr01001 = "P200050060",
//                Pr01003 = "SE003",
//                Pr01106 = "SK1260221",
//                Pr01009 = "Fire alarm control panel",
//                Pr01010 = "SYS PRIME RETROFIT",
//                Pr01011 = "JCC-11-0509-SIR-01",
//                Pr01067 = "2011-08-09 00:00:00.0",
//                Pr01069 = "2011-08-10 00:00:00.0"
//            });
//            #endregion
//        }

//        #endregion
//    }

//}
