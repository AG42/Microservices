using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceOrder.DataLayer;
using ServiceOrder.DataLayer.Entities.Datalake;
using ServiceOrder.DataLayer.Interfaces;

namespace ServiceOrder.UnitTest
{
    [TestClass]
    public class DatabaseContextUnitTest
    {
        IDatalakeEntities _datalakeEntities;

        private readonly List<SM01> _serviceOrderMasterList;
        private readonly List<SM03> _serviceOrderActivityLineList;
        private readonly List<SM05> _serviceOrderCostLinesList;
        private readonly List<SM07> _serviceOrderMaterialLinesList;
        private readonly List<SL01> _customerFileList;
        public DatabaseContextUnitTest()
        {
            _serviceOrderMasterList = new List<SM01>();
            _serviceOrderActivityLineList = new List<SM03>();
            _serviceOrderCostLinesList = new List<SM05>();
            _serviceOrderMaterialLinesList = new List<SM07>();
            _customerFileList = new List<SL01>();
        }

        //[TestInitialize]
        //public void Initialize()
        //{
        //    _datalakeEntities = MockRepository.GenerateMock<IDatalakeEntities>();
        //    _datalakeEntities.ConnectionString = "TestConnectionString";
        //    CreateMockData();
        //}

        //#region Positive Test Cases

        //[TestMethod]
        //public void GetServiceOrderAsycTest()
        //{
        //    _datalakeEntities.Stub(x => x.Get<SM01>("tablename"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderMasterList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderAsync("n1").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetCustomerDetailsTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SL01>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_customerFileList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetCustomerDetailsAsync("n1", "In11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderWithOrderNoTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM01>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderMasterList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderAsync("n1", "11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderActivityLinesTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM03>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderActivityLineList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderActivityLinesAsync("n1", "11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderCostLinesTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM05>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderCostLinesList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderCostLinesAsync("n1", "11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderMaterialLinesTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM07>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderMaterialLinesList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderMaterialLinesAsync("n1", "11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrdersByInvoiceCustomerCodeTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM01>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderMasterList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrdersByInvoiceCustomerCodeAsync("n1", "In11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderActivityLinesByInvoiceCustomerCodeTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM03>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderActivityLineList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderActivityLinesByInvoiceCustomerCodeAsync("n1", "11010839303", "In11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderCostLinesByInvoiceCustomerCodeTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM05>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderCostLinesList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderCostLinesByInvoiceCustomerCodeAsync("n1", "11010839303", "In11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderMaterialLinesByInvoiceCustomerCodeTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM07>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderMaterialLinesList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderMaterialLinesByInvoiceCustomerCodeAsync("n1", "11010839303", "In11010839303").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderByInvoiceNumberTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM01>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderMasterList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderByInvoiceNumberAsync("n1", "15-631411").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderActivityLinesByInvoiceNumberTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM03>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderActivityLineList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderActivityLinesByInvoiceNumberAsync("n1", "11010839303", "15-631411").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderCostLinesByInvoiceNumberTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM05>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderCostLinesList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderCostLinesByInvoiceNumberAsync("n1", "11010839303", "15-631411").Result;
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetServiceOrderMaterialLinesByInvoiceNumberTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM07>("tableName", "condition"))
        //        .IgnoreArguments()
        //        .Return(_serviceOrderMaterialLinesList);
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderMaterialLinesByInvoiceNumberAsync("n1", "11010839303", "15-631411").Result;
        //    Assert.IsNotNull(result);
        //}
        //#endregion

        //#region Negative Test Cases
        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderAsycExceptionTest()
        //{
        //        _datalakeEntities.Stub(x => x.Get<SM01>("tablename"))
        //            .IgnoreArguments().Throw(new Exception());
        //        var databaseContext = new DatabaseContext(_datalakeEntities);
        //        var result = databaseContext.GetServiceOrderAsync("n1").Result;
        //        Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetCustomerDetailsExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SL01>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetCustomerDetailsAsync("n1", "In11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderWithOrderNoExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM01>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderAsync("n1", "11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderActivityLinesExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM03>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderActivityLinesAsync("n1", "11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderCostLinesExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM05>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderCostLinesAsync("n1", "11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderMaterialLinesExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM07>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderMaterialLinesAsync("n1", "11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrdersByInvoiceCustomerCodeExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM01>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrdersByInvoiceCustomerCodeAsync("n1", "In11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderActivityLinesByInvoiceCustomerCodeExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM03>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderActivityLinesByInvoiceCustomerCodeAsync("n1", "11010839303", "In11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderCostLinesByInvoiceCustomerCodeExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM05>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderCostLinesByInvoiceCustomerCodeAsync("n1", "11010839303", "In11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderMaterialLinesByInvoiceCustomerCodeExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM07>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderMaterialLinesByInvoiceCustomerCodeAsync("n1", "11010839303", "In11010839303").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderByInvoiceNumberExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM01>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderByInvoiceNumberAsync("n1", "15-631411").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderActivityLinesByInvoiceNumberExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM03>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderActivityLinesByInvoiceNumberAsync("n1", "11010839303", "15-631411").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderCostLinesByInvoiceNumberExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM05>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderCostLinesByInvoiceNumberAsync("n1", "11010839303", "15-631411").Result;
        //    Assert.Fail();
        //}

        //[TestMethod]
        //[ExpectedException(typeof(AggregateException))]
        //public void GetServiceOrderMaterialLinesByInvoiceNumberExceptionTest()
        //{
        //    _datalakeEntities.Stub(x => x.Where<SM07>("tableName", "condition"))
        //        .IgnoreArguments().Throw(new Exception());
        //    var databaseContext = new DatabaseContext(_datalakeEntities);
        //    var result = databaseContext.GetServiceOrderMaterialLinesByInvoiceNumberAsync("n1", "11010839303", "15-631411").Result;
        //    Assert.Fail();
        //}
        //#endregion
        //private void CreateMockData()
        //{
        //    _serviceOrderMasterList.Add(new SM01()
        //    {
        //        SM01001 = "11010839303",
        //        SM01002 = "In11010839303",
        //        SM01003 = "00",
        //        SM01004 = "test",
        //        SM01005 = "",
        //        SM01006 = "",
        //        SM01007 = "test",
        //        SM01209 = "0",
        //        SM01010 = "",
        //        SM01011 = "Building 501 Chiller No 2, ",
        //        SM01012 = "Supply and install new pressure relief",
        //        SM01013 = " valves and three-way valves",
        //        SM01014 = "as per quote reference No: 15120012",
        //        SM01016 = "5823",
        //        SM01036 = "",
        //        SM01037 = "",
        //        SM01038 = "",
        //        SM01049 = "",
        //        SM01064 = "52",
        //        SM01071 = "5823",
        //        SM01078 = "1010839303",
        //        SM01097 = "LM",
        //        SM01110 = "R01",
        //        SM01113 = "NZ12000012_NOCON    1010839303000314",
        //        SM01205 = "0",
        //        SM01210 = "",
        //        SM01146 = "",
        //        SM01147 = ""
        //    });
        //    _serviceOrderMasterList.Add(new SM01()
        //    {
        //        SM01001 = "11010839303000",
        //        SM01002 = "In11010839303000",
        //        SM01003 = "00",
        //        SM01004 = "test",
        //        SM01005 = "",
        //        SM01006 = "",
        //        SM01007 = "test",
        //        SM01209 = "0",
        //        SM01010 = "",
        //        SM01011 = "Building 501 Chiller No 3, ",
        //        SM01012 = "Supply and install new pressure relief test",
        //        SM01013 = " valves and three-way valves test",
        //        SM01014 = "as per quote reference No: 15120012 test",
        //        SM01016 = "5823",
        //        SM01036 = "",
        //        SM01037 = "",
        //        SM01038 = "",
        //        SM01049 = "",
        //        SM01064 = "52",
        //        SM01071 = "5823",
        //        SM01078 = "1010839303",
        //        SM01097 = "LM",
        //        SM01110 = "R01",
        //        SM01113 = "NZ12000012_NOCON    1010839303000314 test",
        //        SM01205 = "0",
        //        SM01210 = "",
        //        SM01146 = "",
        //        SM01147 = ""
        //    });
        //    _serviceOrderMasterList.Add(new SM01
        //    {
        //        SM01001 = "0110108393030",
        //        SM01002 = "0In11010839303000",
        //        SM01003 = "00",
        //        SM01004 = "test1",
        //        SM01005 = "",
        //        SM01006 = "",
        //        SM01007 = "test1",
        //        SM01209 = "0",
        //        SM01010 = "",
        //        SM01011 = "Building 501 Chiller No 3, ",
        //        SM01012 = "Supply and install new pressure relief test",
        //        SM01013 = " valves and three-way valves test",
        //        SM01014 = "as per quote reference No: 15120012 test",
        //        SM01016 = "5823",
        //        SM01036 = "",
        //        SM01037 = "",
        //        SM01038 = "",
        //        SM01049 = "",
        //        SM01064 = "52",
        //        SM01071 = "5823",
        //        SM01078 = "1010839303",
        //        SM01097 = "LM",
        //        SM01110 = "R01",
        //        SM01113 = "NZ12000012_NOCON    1010839303000314 test",
        //        SM01205 = "0",
        //        SM01210 = "",
        //        SM01146 = "",
        //        SM01147 = ""
        //    });

        //    _serviceOrderActivityLineList.Add(new SM03()
        //    {
        //        SM03001 = "11010839303",
        //        SM03002 = "01",
        //        SM03003 = "0.00000",
        //        SM03012 = "0.00000",
        //        SM03023 = "1900-01-01 00:00:00.000",
        //        SM03024 = "0.00000",
        //        SM03028 = "",
        //        SM03042 = "0.00000",
        //        SM03071 = ""
        //    });
        //    _serviceOrderActivityLineList.Add(new SM03()
        //    {
        //        SM03001 = "11010839303",
        //        SM03002 = "01",
        //        SM03003 = "0.00000",
        //        SM03012 = "0.00000",
        //        SM03023 = "1900-01-01 00:00:00.000",
        //        SM03024 = "0.00000",
        //        SM03028 = "",
        //        SM03042 = "0.00000",
        //        SM03071 = ""
        //    });

        //    _serviceOrderCostLinesList.Add(new SM05()
        //    {
        //        SM05001 = "11010839303",
        //        SM05002 = "0001",
        //        SM05003 = "0",
        //        SM05004 = "40000",
        //        SM05005 = "Kilometers",
        //        SM05009 = "16000.00000000",
        //        SM05012 = "V002",
        //        SM05019 = "1900-01-01 00:00:00.000",
        //        SM05020 = "",
        //        SM05024 = "0.00000000",
        //        SM05028 = "JSH       ",
        //        SM05038 = "0.00000000",
        //        SM05040 = "0.00000000",
        //        SM05053 = "2009-11-06 00:00:00.000",
        //        SM05078 = ""
        //    });
        //    _serviceOrderCostLinesList.Add(new SM05()
        //    {
        //        SM05001 = "11010839303",
        //        SM05002 = "0002",
        //        SM05003 = "0",
        //        SM05004 = "40000",
        //        SM05005 = "Kilometers",
        //        SM05009 = "16000.00000000",
        //        SM05012 = "V002",
        //        SM05019 = "1900-01-01 00:00:00.000",
        //        SM05020 = "",
        //        SM05024 = "0.00000000",
        //        SM05028 = "JSH       ",
        //        SM05038 = "0.00000000",
        //        SM05040 = "0.00000000",
        //        SM05053 = "2009-11-06 00:00:00.000",
        //        SM05078 = ""
        //    });

        //    _serviceOrderMaterialLinesList.Add(new SM07()
        //    {
        //        SM07001 = "11010839303",
        //        SM07002 = "0004",
        //        SM07003 = "0",
        //        SM07004 = "01100592000",
        //        SM07008 = "1.00000000",
        //        SM07010 = "832252.87000000",
        //        SM07011 = "1.00000000",
        //        SM07017 = "0.00000000",
        //        SM07019 = "C001",
        //        SM07025 = "0.00000000",
        //        SM07030 = "1.00000000",
        //        SM07042 = "2009 - 11 - 09 00:00:00.000",
        //        SM07043 = "",
        //        SM07140 = "",
        //        SM07135 = "0.00000000"
        //    });
        //    _serviceOrderMaterialLinesList.Add(new SM07()
        //    {
        //        SM07001 = "11010839303",
        //        SM07002 = "0003",
        //        SM07003 = "0",
        //        SM07004 = "011005920001",
        //        SM07008 = "1.00000000",
        //        SM07010 = "832252.87000000",
        //        SM07011 = "1.00000000",
        //        SM07017 = "0.00000000",
        //        SM07019 = "C001",
        //        SM07025 = "0.00000000",
        //        SM07030 = "1.00000000",
        //        SM07042 = "2009 - 11 - 09 00:00:00.000",
        //        SM07043 = "",
        //        SM07140 = "",
        //        SM07135 = "0.00000000"
        //    });

        //    _customerFileList.Add(new SL01()
        //    {
        //        SL01001 = "In11010839303",
        //        SL01198 = ""
        //    });
        //    _customerFileList.Add(new SL01()
        //    {
        //        SL01001 = "In1101083930300",
        //        SL01198 = ""
        //    });
        //}

    }
}
