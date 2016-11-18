using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceOrder.DataLayer.Entities.IScala;
using ServiceOrder.DataLayer.Interfaces;
using SM01 = ServiceOrder.DataLayer.Entities.Datalake.SM01;

namespace ServiceOrder.UnitTest
{
    [TestClass]
    public class IScalaEntitiesUnitTest
    {
        ////ISqlAdapter _sqlAdapter;
        ////private readonly List<SM01> _serviceOrderMasterList;

        ////public IScalaEntitiesUnitTest()
        ////{
        ////    _serviceOrderMasterList = new List<SM01>();
        ////}

        ////[TestInitialize]
        ////public void Initialize()
        ////{
        ////    _sqlAdapter = MockRepository.GenerateMock<ISqlAdapter>();
        ////    _sqlAdapter.ConnectionString = "TestConnectionString";
        ////    CreateMockData();
        ////}

        ////#region Positive Test Cases
        ////[TestMethod]
        ////public void GetTest()
        ////{
        ////    _sqlAdapter.Stub(x => x.Get<SM01>("query"))
        ////        .IgnoreArguments()
        ////        .Return(_serviceOrderMasterList);

        ////    var iScalaEntities = new IScalaEntities(_sqlAdapter);
        ////    var result = iScalaEntities.Get<SM01>("tableName");
        ////    Assert.IsNotNull(result);
        ////}

        ////[TestMethod]
        ////public void WhereTest()
        ////{
        ////    _sqlAdapter.Stub(x => x.Get<SM01>("query"))
        ////        .IgnoreArguments()
        ////        .Return(_serviceOrderMasterList);

        ////    var iScalaEntities = new IScalaEntities(_sqlAdapter);
        ////    var result = iScalaEntities.Where<SM01>("tableName", "condition");
        ////    Assert.IsNotNull(result);
        ////}

        ////[TestMethod]
        ////public void GetJoinDataTest()
        ////{
        ////    _sqlAdapter.Stub(x => x.Get<SM01>("query"))
        ////        .IgnoreArguments()
        ////        .Return(_serviceOrderMasterList);

        ////    var iScalaEntities = new IScalaEntities(_sqlAdapter);
        ////    var result = iScalaEntities.GetJoinData<SM01>("tableName", "joinCondition");
        ////    Assert.IsNotNull(result);
        ////}

        ////[TestMethod]
        ////public void WhereJoinTest()
        ////{
        ////    _sqlAdapter.Stub(x => x.Get<SM01>("query"))
        ////        .IgnoreArguments()
        ////        .Return(_serviceOrderMasterList);

        ////    var iScalaEntities = new IScalaEntities(_sqlAdapter);
        ////    var result = iScalaEntities.WhereJoin<SM01>("tableName", "joinCondition", "whereCondition");
        ////    Assert.IsNotNull(result);
        ////}

        ////#endregion
        
        ////#region Negative Test Cases
        ////[TestMethod]
        ////[ExpectedException(typeof(Exception))]
        ////public void GetExceptionTest()
        ////{
        ////    _sqlAdapter.Stub(x => x.Get<SM01>("query"))
        ////        .IgnoreArguments().Throw(new Exception());

        ////    var iScalaEntities = new IScalaEntities(_sqlAdapter);
        ////    var result = iScalaEntities.Get<SM01>("tableName");
        ////    Assert.Fail();
        ////}

        ////[TestMethod]
        ////[ExpectedException(typeof(Exception))]
        ////public void WhereExceptionTest()
        ////{
        ////    _sqlAdapter.Stub(x => x.Get<SM01>("query"))
        ////        .IgnoreArguments().Throw(new Exception());

        ////    var iScalaEntities = new IScalaEntities(_sqlAdapter);
        ////    var result = iScalaEntities.Where<SM01>("tableName", "condition");
        ////    Assert.Fail();
        ////}

        ////[TestMethod]
        ////[ExpectedException(typeof(Exception))]
        ////public void GetJoinDataExceptionTest()
        ////{
        ////    _sqlAdapter.Stub(x => x.Get<SM01>("query"))
        ////        .IgnoreArguments().Throw(new Exception());

        ////    var iScalaEntities = new IScalaEntities(_sqlAdapter);
        ////    var result = iScalaEntities.GetJoinData<SM01>("tableName", "joinCondition");
        ////    Assert.Fail();
        ////}

        ////[TestMethod]
        ////[ExpectedException(typeof(Exception))]
        ////public void WhereJoinExceptionTest()
        ////{
        ////    _sqlAdapter.Stub(x => x.Get<SM01>("query"))
        ////        .IgnoreArguments().Throw(new Exception());

        ////    var iScalaEntities = new IScalaEntities(_sqlAdapter);
        ////    var result = iScalaEntities.WhereJoin<SM01>("tableName", "joinCondition", "whereCondition");
        ////    Assert.Fail();
        ////}

        ////#endregion
        ////private void CreateMockData()
        ////{
        ////    _serviceOrderMasterList.Add(new SM01()
        ////    {
        ////        SM01001 = "11010839303",
        ////        SM01002 = "In11010839303",
        ////        SM01003 = "00",
        ////        SM01004 = "test",
        ////        SM01005 = "",
        ////        SM01006 = "",
        ////        SM01007 = "test",
        ////        SM01209 = "0",
        ////        SM01010 = "",
        ////        SM01011 = "Building 501 Chiller No 2, ",
        ////        SM01012 = "Supply and install new pressure relief",
        ////        SM01013 = " valves and three-way valves",
        ////        SM01014 = "as per quote reference No: 15120012",
        ////        SM01016 = "5823",
        ////        SM01036 = "",
        ////        SM01037 = "",
        ////        SM01038 = "",
        ////        SM01049 = "",
        ////        SM01064 = "52",
        ////        SM01071 = "5823",
        ////        SM01078 = "1010839303",
        ////        SM01097 = "LM",
        ////        SM01110 = "R01",
        ////        SM01113 = "NZ12000012_NOCON    1010839303000314",
        ////        SM01205 = "0",
        ////        SM01210 = "",
        ////        SM01146 = "",
        ////        SM01147 = ""
        ////    });
        ////    _serviceOrderMasterList.Add(new SM01()
        ////    {
        ////        SM01001 = "11010839303000",
        ////        SM01002 = "In11010839303000",
        ////        SM01003 = "00",
        ////        SM01004 = "test",
        ////        SM01005 = "",
        ////        SM01006 = "",
        ////        SM01007 = "test",
        ////        SM01209 = "0",
        ////        SM01010 = "",
        ////        SM01011 = "Building 501 Chiller No 3, ",
        ////        SM01012 = "Supply and install new pressure relief test",
        ////        SM01013 = " valves and three-way valves test",
        ////        SM01014 = "as per quote reference No: 15120012 test",
        ////        SM01016 = "5823",
        ////        SM01036 = "",
        ////        SM01037 = "",
        ////        SM01038 = "",
        ////        SM01049 = "",
        ////        SM01064 = "52",
        ////        SM01071 = "5823",
        ////        SM01078 = "1010839303",
        ////        SM01097 = "LM",
        ////        SM01110 = "R01",
        ////        SM01113 = "NZ12000012_NOCON    1010839303000314 test",
        ////        SM01205 = "0",
        ////        SM01210 = "",
        ////        SM01146 = "",
        ////        SM01147 = ""
        ////    });
        ////    _serviceOrderMasterList.Add(new SM01
        ////    {
        ////        SM01001 = "0110108393030",
        ////        SM01002 = "0In11010839303000",
        ////        SM01003 = "00",
        ////        SM01004 = "test1",
        ////        SM01005 = "",
        ////        SM01006 = "",
        ////        SM01007 = "test1",
        ////        SM01209 = "0",
        ////        SM01010 = "",
        ////        SM01011 = "Building 501 Chiller No 3, ",
        ////        SM01012 = "Supply and install new pressure relief test",
        ////        SM01013 = " valves and three-way valves test",
        ////        SM01014 = "as per quote reference No: 15120012 test",
        ////        SM01016 = "5823",
        ////        SM01036 = "",
        ////        SM01037 = "",
        ////        SM01038 = "",
        ////        SM01049 = "",
        ////        SM01064 = "52",
        ////        SM01071 = "5823",
        ////        SM01078 = "1010839303",
        ////        SM01097 = "LM",
        ////        SM01110 = "R01",
        ////        SM01113 = "NZ12000012_NOCON    1010839303000314 test",
        ////        SM01205 = "0",
        ////        SM01210 = "",
        ////        SM01146 = "",
        ////        SM01147 = ""
        ////    });
        ////}

    }
}
