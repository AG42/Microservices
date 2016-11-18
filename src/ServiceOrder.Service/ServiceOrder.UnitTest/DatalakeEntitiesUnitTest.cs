using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceOrder.DataLayer.Entities.Datalake;
using ServiceOrder.DataLayer.Interfaces;

namespace ServiceOrder.UnitTest
{
    [TestClass]
    public class DatalakeEntitiesUnitTest
    {
    //    IDatalakeAdapter _datalakeAdapter;
    //    private readonly List<SM01> _serviceOrderMasterList;

    //    public DatalakeEntitiesUnitTest()
    //    {
    //        _serviceOrderMasterList = new List<SM01>();
    //    }

    //    [TestInitialize]
    //    public void Initialize()
    //    {
    //        _datalakeAdapter = MockRepository.GenerateMock<IDatalakeAdapter>();
    //        _datalakeAdapter.ConnectionString = "TestConnectionString";
    //        CreateMockData();
    //    }

    //    #region Positive Test Cases
    //    [TestMethod]
    //    public void GetTest()
    //    {
    //        _datalakeAdapter.Stub(x => x.Get<SM01>("query"))
    //            .IgnoreArguments()
    //            .Return(_serviceOrderMasterList);

    //        var datalakeEntities = new DatalakeEntities(_datalakeAdapter);
    //        var result = datalakeEntities.Get<SM01>("tableName");
    //        Assert.IsNotNull(result);
    //    }

    //    [TestMethod]
    //    public void WhereTest()
    //    {
    //        _datalakeAdapter.Stub(x => x.Get<SM01>("query"))
    //            .IgnoreArguments()
    //            .Return(_serviceOrderMasterList);

    //        var datalakeEntities = new DatalakeEntities(_datalakeAdapter);
    //        var result = datalakeEntities.Where<SM01>("tableName", "condition");
    //        Assert.IsNotNull(result);
    //    }

    //    [TestMethod]
    //    public void GetJoinDataTest()
    //    {
    //        _datalakeAdapter.Stub(x => x.Get<SM01>("query"))
    //            .IgnoreArguments()
    //            .Return(_serviceOrderMasterList);

    //        var datalakeEntities = new DatalakeEntities(_datalakeAdapter);
    //        var result = datalakeEntities.GetJoinData<SM01>("tableName", "joinCondition");
    //        Assert.IsNotNull(result);
    //    }

    //    [TestMethod]
    //    public void WhereJoinTest()
    //    {
    //        _datalakeAdapter.Stub(x => x.Get<SM01>("query"))
    //            .IgnoreArguments()
    //            .Return(_serviceOrderMasterList);

    //        var datalakeEntities = new DatalakeEntities(_datalakeAdapter);
    //        var result = datalakeEntities.WhereJoin<SM01>("tableName", "joinCondition", "whereCondition");
    //        Assert.IsNotNull(result);
    //    }

    //    #endregion

    //    #region Negative Test Cases
    //    [TestMethod]
    //    [ExpectedException(typeof(Exception))]
    //    public void GetExceptionTest()
    //    {
    //        _datalakeAdapter.Stub(x => x.Get<SM01>("query"))
    //            .IgnoreArguments().Throw(new Exception());

    //        var datalakeEntities = new DatalakeEntities(_datalakeAdapter);
    //        var result = datalakeEntities.Get<SM01>("tableName");
    //        Assert.Fail();
    //    }

    //    [TestMethod]
    //    [ExpectedException(typeof(Exception))]
    //    public void WhereExceptionTest()
    //    {
    //        _datalakeAdapter.Stub(x => x.Get<SM01>("query"))
    //            .IgnoreArguments().Throw(new Exception());

    //        var datalakeEntities = new DatalakeEntities(_datalakeAdapter);
    //        var result = datalakeEntities.Where<SM01>("tableName", "condition");
    //        Assert.Fail();
    //    }

    //    [TestMethod]
    //    [ExpectedException(typeof(Exception))]
    //    public void GetJoinDataExceptionTest()
    //    {
    //        _datalakeAdapter.Stub(x => x.Get<SM01>("query"))
    //            .IgnoreArguments().Throw(new Exception());

    //        var datalakeEntities = new DatalakeEntities(_datalakeAdapter);
    //        var result = datalakeEntities.GetJoinData<SM01>("tableName", "joinCondition");
    //        Assert.Fail();
    //    }

    //    [TestMethod]
    //    [ExpectedException(typeof(Exception))]
    //    public void WhereJoinExceptionTest()
    //    {
    //        _datalakeAdapter.Stub(x => x.Get<SM01>("query"))
    //            .IgnoreArguments().Throw(new Exception());

    //        var datalakeEntities = new DatalakeEntities(_datalakeAdapter);
    //        var result = datalakeEntities.WhereJoin<SM01>("tableName", "joinCondition", "whereCondition");
    //        Assert.Fail();
    //    }

    //    #endregion
    //    private void CreateMockData()
    //    {
    //        _serviceOrderMasterList.Add(new SM01()
    //        {
    //            SM01001 = "11010839303",
    //            SM01002 = "In11010839303",
    //            SM01003 = "00",
    //            SM01004 = "test",
    //            SM01005 = "",
    //            SM01006 = "",
    //            SM01007 = "test",
    //            SM01209 = "0",
    //            SM01010 = "",
    //            SM01011 = "Building 501 Chiller No 2, ",
    //            SM01012 = "Supply and install new pressure relief",
    //            SM01013 = " valves and three-way valves",
    //            SM01014 = "as per quote reference No: 15120012",
    //            SM01016 = "5823",
    //            SM01036 = "",
    //            SM01037 = "",
    //            SM01038 = "",
    //            SM01049 = "",
    //            SM01064 = "52",
    //            SM01071 = "5823",
    //            SM01078 = "1010839303",
    //            SM01097 = "LM",
    //            SM01110 = "R01",
    //            SM01113 = "NZ12000012_NOCON    1010839303000314",
    //            SM01205 = "0",
    //            SM01210 = "",
    //            SM01146 = "",
    //            SM01147 = ""
    //        });
    //        _serviceOrderMasterList.Add(new SM01()
    //        {
    //            SM01001 = "11010839303000",
    //            SM01002 = "In11010839303000",
    //            SM01003 = "00",
    //            SM01004 = "test",
    //            SM01005 = "",
    //            SM01006 = "",
    //            SM01007 = "test",
    //            SM01209 = "0",
    //            SM01010 = "",
    //            SM01011 = "Building 501 Chiller No 3, ",
    //            SM01012 = "Supply and install new pressure relief test",
    //            SM01013 = " valves and three-way valves test",
    //            SM01014 = "as per quote reference No: 15120012 test",
    //            SM01016 = "5823",
    //            SM01036 = "",
    //            SM01037 = "",
    //            SM01038 = "",
    //            SM01049 = "",
    //            SM01064 = "52",
    //            SM01071 = "5823",
    //            SM01078 = "1010839303",
    //            SM01097 = "LM",
    //            SM01110 = "R01",
    //            SM01113 = "NZ12000012_NOCON    1010839303000314 test",
    //            SM01205 = "0",
    //            SM01210 = "",
    //            SM01146 = "",
    //            SM01147 = ""
    //        });
    //        _serviceOrderMasterList.Add(new SM01
    //        {
    //            SM01001 = "0110108393030",
    //            SM01002 = "0In11010839303000",
    //            SM01003 = "00",
    //            SM01004 = "test1",
    //            SM01005 = "",
    //            SM01006 = "",
    //            SM01007 = "test1",
    //            SM01209 = "0",
    //            SM01010 = "",
    //            SM01011 = "Building 501 Chiller No 3, ",
    //            SM01012 = "Supply and install new pressure relief test",
    //            SM01013 = " valves and three-way valves test",
    //            SM01014 = "as per quote reference No: 15120012 test",
    //            SM01016 = "5823",
    //            SM01036 = "",
    //            SM01037 = "",
    //            SM01038 = "",
    //            SM01049 = "",
    //            SM01064 = "52",
    //            SM01071 = "5823",
    //            SM01078 = "1010839303",
    //            SM01097 = "LM",
    //            SM01110 = "R01",
    //            SM01113 = "NZ12000012_NOCON    1010839303000314 test",
    //            SM01205 = "0",
    //            SM01210 = "",
    //            SM01146 = "",
    //            SM01147 = ""
    //        });
    //    }

    }
}
