using System.Collections.Generic;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderSecuredRevenue.DataLayer;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;
using Rhino.Mocks;

namespace OrderSecuredRevenue.UnitTest
{
    [TestClass]
    public class DataLayerUnitTest
    {
        private List<OR03> _salesOrderList;
        IDatabase _mocks;
        DatabaseContext _databaseContext;

        [TestInitialize]
        public void Initialize()
        {
            _salesOrderList = new List<OR03>();
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
    }
}
