using System.Collections.Generic;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderSecuredRevenue.DataLayer;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using Rhino.Mocks;
using System;

namespace OrderSecuredRevenue.UnitTest
{
    [TestClass]
    public class DataLayerUnitTest
    {
        private List<OR03> _salesOrderList;
        private List<OR01> _salesOrderDetails;
        IDatabase _mocks;
        DatabaseContext _databaseContext;

        [TestInitialize]
        public void Initialize()
        {
            _salesOrderList = new List<OR03>();
            _salesOrderDetails = new List<OR01>();
            _mocks = MockRepository.GenerateMock<IDatabase>();
            _databaseContext = new DatabaseContext() { Database = _mocks };
        }

        [TestMethod]
        public void GetOrderSecuredRevenueByOrderNumberTest()
        {
            SetMockData();
            _mocks.Stub(x => x.Where<OR03>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_salesOrderList);
            var result = _databaseContext.GetOrderSecuredRevenueByOrderNumber("na", "4937000001");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderDetailsByOrderNumberTest()
        {
            SetSalseOrderMasterMockData();
            _mocks.Stub(x => x.Where<OR01>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_salesOrderDetails);
            var result = _databaseContext.GetSalesOrderDetailsByOrderNumber("na", "4937000001");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderLineDetailsByOrderNumberTest()
        {
            SetMockData();
            _mocks.Stub(x => x.Where<OR03>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_salesOrderList);
            var result = _databaseContext.GetSalesOrderLineDetailsByOrderNumber("na", "4937000001");
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void GetOrderSecuredRevenueByInvoiceNumber()
        //{
        //    SetMockData();
        //    _mocks.Stub(x => x.Where<OR03>("tableName", "columns", "condition"))
        //        .IgnoreArguments()
        //        .Return(_salesOrderList);
        //    var result = _databaseContext.GetOrderSecuredRevenueByInvoiceNumber("na", "280001");
        //    Assert.IsNotNull(result);
        //}

        private void SetMockData()
        {
            _salesOrderList.Add(new OR03()
            {
                or03001 = "4937000001",
                or03002 = "10",
                or03004 = "1",
                or03008 = "6686.4",
                or03009 = "6686.4",
                or03011 = "-1",
                //or21065 = "280001"
            });
        }

        private void SetSalseOrderMasterMockData()
        {
            _salesOrderDetails.Add(new OR01()
            {
                or01001 = "3755000010",
                or01002 = "0",
                or01003 = "110P0001",
                or01004 = "160P0002",
                or01007 = "1",
                or01010 = "0",
                or01012 = "0",
                or01013 = "4",
                or01014 = "1",
                or01015 = "00:00.0",
                or01016 = "00:00.0",
                or01019 = "21",
                or01020 = "0",
                or01021 = "0",
                or01024 = "0",
                or01028 = "0",
                or01043 = "0",
                or01044 = "0",
                or01055 = "0",
                or01056 = "0",
                or01057 = "0",
                or01058 = "0",
                or01059 = "0",
                or01060 = "0",
                or01067 = "0",
                or01072 = "SP1001",
                or01091 = "",
                or01092 = "",
                or01093 = "",
                or01094 = "",
                or01096 = "",
                or01097 = "",
                or01098 = "",
                or01099 = "",
                or01137 = "",
                or01138 = "",
                or01139 = "",
                or01156 = "",
                or01157 = "",
                or01168 = "",
                or01169 = "",
                or01170 = "",
                or01214 = "",
                or01216 = "",
                or01217 = DateTime.Now.ToShortDateString(),
                or01218 = DateTime.Now.ToShortDateString(),
                or01219 = DateTime.Now.ToShortDateString(),
                or01220 = "",
                or01221 = "",
                or01222 = "",
                or01223 = "",
                or01224 = ""
            });
        }
    }
}
