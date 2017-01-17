using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesLedgerInvoicing.BusinessLayer;
using SalesLedgerInvoicing.DataLayer.Entities.Datalake;
using SalesLedgerInvoicing.DataLayer.Interfaces;
using SalesLedgerInvoicing.Model;
using SalesLedgerInvoicing.Common;
using System.Threading.Tasks;

namespace SalesLedgerInvoicing.UnitTest
{
    [TestClass]
    public class SalesLedgerInvoicingManagerUnitTest
    {
        #region Declarations
        private IDatabaseContext _iDatabase;
        private SalesLedgerInvoicingManager _salesLedgerManager;
        private const string COMPANY_CODE = "bh";

        List<SalesLedgerInvoicesModel> _salesLedgerInvoicesModelList = new List<SalesLedgerInvoicesModel>();

        private List<SL01> _customerDetailsList = new List<SL01>();
        private SL01 sl01 = new SL01();
        private List<SL03> _salesLedgerList = new List<SL03>();
        readonly SalesLedgerInvoicesModel _salesLedgerModel = new SalesLedgerInvoicesModel();
        #endregion

        #region TestMethods
        [TestInitialize]
        public void Initialize()
        {
            _salesLedgerManager = new SalesLedgerInvoicingManager(_iDatabase);
        }

        /// <summary>
        /// Test Method for GetSalesLedgerInvoicingByCustomerCode
        /// </summary>
        [TestMethod]
        public void GetSalesLedgerInvoicingByCustomerCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetSalesLedgerInvoicingModel();
            SetCustomerDetailsListMockData();
            SetCustomerModel();
            SetSalesLedgerMockData();

            var data = new Model.Responses.SalesLedgerInvoicingByCustomerCodeResponse ();
            data.SalesLedgerInvoicingModelList.AddRange(_salesLedgerInvoicesModelList);

            mockRepository.Stub(x => x.GetCustomerDetailsByCustomerCodeAsync("bh", "1001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((SL01)sl01));

            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByCustomerCodeAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)_salesLedgerList));

            _salesLedgerManager = new SalesLedgerInvoicingManager(mockRepository);
            var result = _salesLedgerManager.GetSalesLedgerInvoicingByCustomerCode("N1", "1001");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Gets the sales ledger invoicing by customer name test.
        /// </summary>
        [TestMethod]
        public void GetSalesLedgerInvoicingByCustomerNameTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetSalesLedgerInvoicingModel();
            SetCustomerDetailsListMockData();
            SetSalesLedgerMockData();

            var data = new Model.Responses.SalesLedgerInvoicingByCustomerNameResponse();
            data.SalesLedgerInvoicingModelList.AddRange(_salesLedgerInvoicesModelList);
                
            mockRepository.Stub(x => x.GetCustomerDetailsByNameAsync("bh", "Air"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL01>)_customerDetailsList));

            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByCustomerListAsync("bh", _customerDetailsList))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)_salesLedgerList));

            _salesLedgerManager = new SalesLedgerInvoicingManager(mockRepository);
            var result = _salesLedgerManager.GetSalesLedgerInvoicingByCustomerName("N1", "air");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Gets the sales ledger invoicing by order number test.
        /// </summary>
        [TestMethod]
        public void GetSalesLedgerInvoicingByOrderNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetSalesLedgerInvoicingModel();
            SetCustomerDetailsListMockData();
            SetSalesLedgerMockData();

            var data = new Model.Responses.SalesLedgerInvoicingByOrderNumberResponse();
            data.SalesLedgerInvoicingModelList.AddRange(_salesLedgerInvoicesModelList);

            mockRepository.Stub(x => x.GetCustomerDetailsBySalesLedgerCustomerCodeAsync("bh", _salesLedgerList))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL01>)_customerDetailsList));

            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByOrderNumberAsync("bh", "0001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)_salesLedgerList));

            _salesLedgerManager = new SalesLedgerInvoicingManager(mockRepository);
            var result = _salesLedgerManager.GetSalesLedgerInvoicingByOrderNumber("N1", "air");
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// Gets the sales ledger invoicing by invoice number test.
        /// </summary>
        [TestMethod]
        public void GetSalesLedgerInvoicingByInvoiceNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetSalesLedgerInvoicingModel();
            SetCustomerDetailsListMockData();
            SetSalesLedgerMockData();

            var data = new Model.Responses.SalesLedgerInvoicingByInvoiceNumberResponse();
            data.SalesLedgerInvoicingModelList.AddRange(_salesLedgerInvoicesModelList);

            mockRepository.Stub(x => x.GetCustomerDetailsBySalesLedgerCustomerCodeAsync("bh", _salesLedgerList))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL01>)_customerDetailsList));

            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByInvoiceNumberAsync("bh", "0001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)_salesLedgerList));

            _salesLedgerManager = new SalesLedgerInvoicingManager(mockRepository);
            var result = _salesLedgerManager.GetSalesLedgerInvoicingByInvoiceNumber("N1", "Inv-001");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Gets the sales ledger invoicing by invoice Date test.
        /// </summary>
        [TestMethod]
        public void GetSalesLedgerInvoicingByInvoiceDateTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetSalesLedgerInvoicingModel();
            SetCustomerModel();
            SetCustomerDetailsListMockData();
            SetSalesLedgerMockData();

            var data = new Model.Responses.SalesLedgerInvoicingByInvoiceDateResponse();
            data.SalesLedgerInvoicingModelList.AddRange(_salesLedgerInvoicesModelList);

            mockRepository.Stub(x => x.GetCustomerDetailsBySalesLedgerCustomerCodeAsync("bh", _salesLedgerList))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL01>)_customerDetailsList));

            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByInvoiceDateRangeAsync("bh", "2017-01-11", "2017-01-13"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)_salesLedgerList));

            _salesLedgerManager = new SalesLedgerInvoicingManager(mockRepository);
            var result = _salesLedgerManager.GetSalesLedgerInvoicingByInvoiceDate("N1", "2017-01-11","2017-01-13");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCasesForNullValues()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetCustomerDetailsByCustomerCodeAsync("bh", "1001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((SL01)new SL01()));

            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByCustomerCodeAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)new List<SL03>()));

            mockRepository.Stub(x => x.GetCustomerDetailsByNameAsync("bh", "Air"))
                           .IgnoreArguments()
                           .Return(Task.FromResult((IEnumerable<SL01>)new List<SL01>()));

            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByCustomerListAsync("bh", _customerDetailsList))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)new List<SL03>()));

            mockRepository.Stub(x => x.GetCustomerDetailsBySalesLedgerCustomerCodeAsync("bh", _salesLedgerList))
                           .IgnoreArguments()
                           .Return(Task.FromResult((IEnumerable<SL01>)new List<SL01>()));

            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByOrderNumberAsync("bh", "0001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)new List<SL03>()));


            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByInvoiceNumberAsync("bh", "0001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)new List<SL03>()));

         
            mockRepository.Stub(x => x.GetSalesLedgerInvoicesByInvoiceDateRangeAsync("bh", "2017-01-11", "2017-01-13"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SL03>)new List<SL03>()));


            _salesLedgerManager = new SalesLedgerInvoicingManager(mockRepository);
            var result = _salesLedgerManager.GetSalesLedgerInvoicingByCustomerCode("N1", "1001");
            var resultCustomerName = _salesLedgerManager.GetSalesLedgerInvoicingByCustomerName("N1", "AAa");
            var resultOrderNumber= _salesLedgerManager.GetSalesLedgerInvoicingByOrderNumber("N1", "1001");
            var resultInvoiceNumber = _salesLedgerManager.GetSalesLedgerInvoicingByInvoiceNumber("N1", "1001");
            var resultInvoiceDates = _salesLedgerManager.GetSalesLedgerInvoicingByInvoiceDate("N1", "2017-01-16","2017-01-12");
            Assert.AreNotEqual(result.ErrorInfo.Count,0);
            Assert.AreNotEqual(resultCustomerName.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultOrderNumber.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultInvoiceNumber.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultInvoiceDates.ErrorInfo.Count, 0);
        }

        #endregion

        #region MockData
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

        private void SetCustomerModel()
        {
            sl01 = new SL01()
            {
                Sl01001 = "1005",
                Sl01002 = "LSS Technologies"
            };
        }

        private void SetSalesLedgerMockData()
        {
            _salesLedgerList.Add(new SL03()
            {
                Sl03001 = "1001",
                Sl03002 = "ADV-CHQ303431",
                Sl03004 = "2012-11-08 00:00:00.0",
                Sl03006 = "2012-11-08 00:00:00.0 ",
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
                Sl03002 = "F13/290004",
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
                Sl03002 = "CHQ-804503",
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
                Sl03002 = "F17/700101",
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

        private void SetSalesLedgerInvoicingModel()
        {
            _salesLedgerInvoicesModelList.Add(new SalesLedgerInvoicesModel()
            {
                CustomerCode = "1001",
                CustomerName = "LSS Technologies",
                InvoiceNumber = "ADV-CHQ303431",
                InvoiceDate = "2012-11-08 00:00:00.0",
                DueDate = "2012-11-08 00:00:00.0 ",
                Amount = "0",
                StornoInvoice = "",
                InvoiceType = "5",
                OrderType = "",
                OrderNumber = "",
                TransactionType = "0",
                InvoiceStatus = "0"
            });
            _salesLedgerInvoicesModelList.Add(new SalesLedgerInvoicesModel()
            {
                CustomerCode = "1002",
                CustomerName = "Voltamp Switch Gear Co LLC",
                InvoiceNumber = "QWE-CHQ303431",
                InvoiceDate = "2012-11-02 00:00:00.0",
                DueDate = "2012-11-02 00:00:00.0 ",
                Amount = "0",
                StornoInvoice = "",
                InvoiceType = "2",
                OrderType = "",
                OrderNumber = "",
                TransactionType = "0",
                InvoiceStatus = "0"
            });
        }
        #endregion
    }
}
