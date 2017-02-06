using System.Collections.Generic;
using System.Linq;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceContractManagement.DataLayer;
using ServiceContractManagement.DataLayer.Entities.Datalake;

namespace ServiceContractManagement.UnitTest
{
    [TestClass]
    public class DatabaseContextUnitTest
    {
        private const string CompanyCode = "j1";
        IDatabase _mocks;
        DatabaseContext _databaseContext;
        private List<SM11> _serviceContractMasterList;
        private List<SM13> _serviceContractLinesList;
        [TestInitialize]
        public void Initialize()
        {
            _mocks = MockRepository.GenerateMock<IDatabase>();
            _databaseContext = new DatabaseContext() { Database = _mocks };
            _serviceContractMasterList = new List<SM11>();
            _serviceContractLinesList = new List<SM13>();
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractCodeAsyncTest()
        {
            SetMockDataForServiceContractMaster();
            _mocks.Stub(x => x.Where<SM11>("table", "columns", "condition"))
                .IgnoreArguments()
                .Return(_serviceContractMasterList);
            var result = _databaseContext.GetServiceContractDetailsByContractCodeAsync(CompanyCode, "961120034").Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractDateRangeAsyncTest()
        {
            SetMockDataForServiceContractMaster();
            _mocks.Stub(x => x.Where<SM11>("table", "columns", "condition"))
                .IgnoreArguments()
                .Return(_serviceContractMasterList);
            var result =
                _databaseContext.GetServiceContractDetailsByContractDateRangeAsync(CompanyCode, "2/1/2009 12:00:00 AM",
                    "2/1/2009 12:00:00 AM").Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetServiceContractLineDetailsByContractCodeAsyncTest()
        {
            SetMockDataForServiceContractLines();
            _mocks.Stub(x => x.Where<SM13>("table", "columns", "condition"))
                .IgnoreArguments()
                .Return(_serviceContractLinesList);
            var result = _databaseContext.GetServiceContractLineDetailsByContractCodeAsync(CompanyCode, "180907").Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetServieContractLineDetailsByContractCodeListAsyncTest()
        {
            SetMockDataForServiceContractLines();
            SetMockDataForServiceContractMaster();
            _mocks.Stub(x => x.Where<SM13>("table", "columns", "condition"))
                .IgnoreArguments()
                .Return(_serviceContractLinesList);
            var result =
                _databaseContext.GetServieContractLineDetailsByContractCodeListAsync(CompanyCode,
                    _serviceContractMasterList).Result;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        private void SetMockDataForServiceContractMaster()
        {
            _serviceContractMasterList.Add(new SM11()
            {
                SM11001 = "961120034",
                SM11003 = "NA008",
                SM11004 = "K.rattanam",
                SM11011 = "30",
                SM11012 = "2/1/2009 12:00:00 AM",
                SM11014 = "2",
                SM11015 = "2/1/2009 12:00:00 AM",
                SM11016 = "2/1/2009 12:00:00 AM",
                SM11017 = "AWA",
                SM11018 = "YSK-M 2006/02",
                SM11022 = "0",
                SM11023 = "0",
                SM11027 = "0",
                SM11034 = "PSV",
                SM11068 = "R"
            });
            _serviceContractMasterList.Add(new SM11()
            {
                SM11001 = "2900020014",
                SM11003 = "SA170",
                SM11004 = "K.Taveesak",
                SM11011 = "30",
                SM11012 = "3/1/2009 12:00:00 AM",
                SM11014 = "1",
                SM11015 = "3/1/2009 12:00:00 AM",
                SM11016 = "3/1/2009 12:00:00 AM",
                SM11017 = "WHO",
                SM11018 = "TH92-4212-0014",
                SM11022 = "0",
                SM11023 = "0",
                SM11027 = "0",
                SM11034 = "OMV",
                SM11068 = "N"
            });
            _serviceContractMasterList.Add(new SM11()
            {
                SM11001 = "3511520084",
                SM11003 = "TH031",
                SM11004 = "K.Somchai",
                SM11011 = "1",
                SM11012 = "3/1/2009 12:00:00 AM",
                SM11014 = "2",
                SM11015 = "3/1/2009 12:00:00 AM",
                SM11016 = "3/1/2009 12:00:00 AM",
                SM11017 = "PTH",
                SM11018 = "YCB-M2005/001",
                SM11022 = "0",
                SM11023 = "0",
                SM11027 = "0",
                SM11034 = "PSV",
                SM11068 = "R"
            });
        }
        private void SetMockDataForServiceContractLines()
        {
            _serviceContractLinesList.Add(new SM13()
            {
                SM13001 = "180907",
                SM13002 = "10",
                SM13008 = "132000",
                SM13019 = "7",
                SM13027 = "0",
                SM13042 = "1",
                SM13050 = "0"
            });
            _serviceContractLinesList.Add(new SM13()
            {
                SM13001 = "961120034",
                SM13002 = "120",
                SM13008 = "46728.97",
                SM13019 = "7",
                SM13027 = "0",
                SM13042 = "1",
                SM13050 = "0"
            });
            _serviceContractLinesList.Add(new SM13()
            {
                SM13001 = "2900020014",
                SM13002 = "10",
                SM13008 = "98130.83",
                SM13019 = "7",
                SM13027 = "0",
                SM13042 = "1",
                SM13050 = "0"
            });
        }
    }
}
