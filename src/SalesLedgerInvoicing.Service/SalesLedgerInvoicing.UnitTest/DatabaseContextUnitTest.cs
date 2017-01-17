using System.Collections.Generic;
using System.Linq;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using SalesLedgerInvoicing.DataLayer;
using SalesLedgerInvoicing.DataLayer.Entities.Datalake;

namespace SalesLedgerInvoicing.UnitTest
{
    [TestClass]
    public class DatabaseContextUnitTest
    {
        IDatabase _mocks;
        DatabaseContext _databaseContext;
        private List<SL01> _customerDetailsList;
        private List<SL03> _salesLedgerList;

        [TestInitialize]
        public void Initialize()
        {
            _customerDetailsList = new List<SL01>();
            _salesLedgerList = new List<SL03>();
            _mocks = MockRepository.GenerateMock<IDatabase>();
            _databaseContext = new DatabaseContext() { Database = _mocks };
        }

        [TestMethod]
        public void GetCustomerDetailsByCustomerCodeAsyncTest()
        {
            SetCustomerDetailsListMockData();
            _mocks.Stub(x => x.Where<SL01>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_customerDetailsList);
            var result = _databaseContext.GetCustomerDetailsByCustomerCodeAsync("na", "1001");
            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void GetCustomerDetailsBySalesLedgerCustomerCodeAsyncTest()
        {
            SetCustomerDetailsListMockData();
            SetSalesLedgerMockData();
            _mocks.Stub(x => x.Where<SL01>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_customerDetailsList);
            var result = _databaseContext.GetCustomerDetailsBySalesLedgerCustomerCodeAsync("na", _salesLedgerList);
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Any());
        }

        [TestMethod]
        public void GetCustomerDetailsByNameAsyncTest()
        {
            SetCustomerDetailsListMockData();
            _mocks.Stub(x => x.Where<SL01>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_customerDetailsList);
            var result = _databaseContext.GetCustomerDetailsByNameAsync("na", "LSS");
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Any());
        }

        [TestMethod]
        public void GetSalesLedgerInvoicesByCustomerCodeAsyncTest()
        {
            SetSalesLedgerMockData();
            _mocks.Stub(x => x.Where<SL03>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_salesLedgerList);
            var result = _databaseContext.GetSalesLedgerInvoicesByCustomerCodeAsync("na", "1001");
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Any());
        }

        [TestMethod]
        public void GetSalesLedgerInvoicesByCustomerListAsyncTest()
        {
            SetCustomerDetailsListMockData();
            SetSalesLedgerMockData();
            _mocks.Stub(x => x.Where<SL03>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_salesLedgerList);
            var result = _databaseContext.GetSalesLedgerInvoicesByCustomerListAsync("na", _customerDetailsList);
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Any());
        }

        [TestMethod]
        public void GetSalesLedgerInvoicesByOrderNumberAsyncTest()
        {
            SetSalesLedgerMockData();
            _mocks.Stub(x => x.Where<SL03>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_salesLedgerList);
            var result = _databaseContext.GetSalesLedgerInvoicesByOrderNumberAsync("na", "4939000004");
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Any());
        }

        [TestMethod]
        public void GetSalesLedgerInvoicesByInvoiceNumberAsyncTest()
        {
            SetSalesLedgerMockData();
            _mocks.Stub(x => x.Where<SL03>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_salesLedgerList);
            var result = _databaseContext.GetSalesLedgerInvoicesByInvoiceNumberAsync("na", "ADV-CHQ303431");
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Any());
        }

        [TestMethod]
        public void GetSalesLedgerInvoicesByInvoiceDateRangeAsyncTest()
        {
            SetSalesLedgerMockData();
            _mocks.Stub(x => x.Where<SL03>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(_salesLedgerList);
            var result = _databaseContext.GetSalesLedgerInvoicesByInvoiceDateRangeAsync("na", "2012-11-08 00:00:00.0", "2012-11-11 00:00:00.0");
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.Any());
        }


        private void SetCustomerDetailsListMockData()
        {
            _customerDetailsList.Add(new SL01()
            {
                Sl01001 = "1001",
                Sl01002 = "LSS Technologies"
            });
            _customerDetailsList.Add(new SL01()
            {
                Sl01001 = "1002",
                Sl01002 = "Voltamp Switch Gear Co LLC"
            });
        }

        private void SetSalesLedgerMockData()
        {
           _salesLedgerList.Add(new SL03()
           {
               Sl03001 = "1001",
               Sl03002= "ADV-CHQ303431",
               Sl03004 = "2012-11-08 00:00:00.0",
               Sl03006 = "2012-11-08 00:00:00.0",
               Sl03013 = "0",
               Sl03025 = "",
               Sl03027 = "5",
               Sl03035 = "",
               Sl03036 = "",
               Sl03040 = "0",
               Sl03042 = "0"
           });
           _salesLedgerList.Add(new SL03()
           {
               Sl03001 = "1001",
               Sl03002= "F13/290004",
               Sl03004 = "2013-04-08 00:00:00.0",
               Sl03006 = "2013-04-08 00:00:00.0",
               Sl03013 = "8040",
               Sl03025 = "",
               Sl03027 = "0",
               Sl03035 = "1",
               Sl03036 = "4939000004",
               Sl03040 = "0",
               Sl03042 = "0"
           });
           _salesLedgerList.Add(new SL03()
           {
               Sl03001 = "1009",
               Sl03002= "CHQ-804503",
               Sl03004 = "2012-11-08 00:00:00.0",
               Sl03006 = "2012-11-08 00:00:00.0",
               Sl03013 = "0",
               Sl03025 = "",
               Sl03027 = "5",
               Sl03035 = "",
               Sl03036 = "",
               Sl03040 = "0",
               Sl03042 = "0"
           });
           _salesLedgerList.Add(new SL03()
           {
               Sl03001 = "1009",
               Sl03002= "F17/700101",
               Sl03004 = "2012-11-11 00:00:00.0",
               Sl03006 = "2012-11-11 00:00:00.0",
               Sl03013 = "1250",
               Sl03025 = "",
               Sl03027 = "0",
               Sl03035 = "5",
               Sl03036 = "1500036060",
               Sl03040 = "0",
               Sl03042 = "0"
           });
        }
    }
}
