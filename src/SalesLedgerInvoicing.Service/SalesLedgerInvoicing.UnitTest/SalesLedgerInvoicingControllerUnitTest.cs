using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using SalesLedgerInvoicing.BusinessLayer.Interface;
using SalesLedgerInvoicing.Common.Error;
using SalesLedgerInvoicing.Model;
using SalesLedgerInvoicing.Model.Responses;

namespace SalesLedgerInvoicing.UnitTest
{
    [TestClass]
    public class SalesLedgerInvoicingControllerUnitTest
    {
        private ISalesLedgerInvoicingManager _salesLedgerInvoicingManagerMock;

        private API.Controllers.SalesLedgerInvoicingController _salesLedgerInvoicingController;
        private List<SalesLedgerInvoicesModel> _salesLedgerInvoicesModelList;
        private List<ErrorInfo> _errorInfoList;
        private readonly string _companyCode = "na";
        private readonly string _customerCode = "1009";
        private readonly string _customerName = "LSS";
        private readonly string _orderNumber = "4939000004";
        private readonly string _invoiceNumber = "ADV-CHQ303431";
        private readonly string _invoiceFromDate = "2012-11-08";
        private readonly string _invoiceToDate = "2012-11-11";

        [TestInitialize]
        public void Initialize()
        {
            _salesLedgerInvoicingManagerMock = MockRepository.GenerateMock<ISalesLedgerInvoicingManager>();
            _salesLedgerInvoicingController = new API.Controllers.SalesLedgerInvoicingController(_salesLedgerInvoicingManagerMock) { Request = new HttpRequestMessage() };
            _salesLedgerInvoicingController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                new HttpConfiguration());
            _salesLedgerInvoicesModelList = new List<SalesLedgerInvoicesModel>();
            _errorInfoList = new List<ErrorInfo>();
        }

        [TestMethod]
        public void GetServiceNameTest()
        {
            var response = _salesLedgerInvoicingController.GetServiceName();
            Assert.AreEqual("Sales Ledger Invoicing Service is running...", response);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByCustomerCodeTest()
        {
            SetMockData();
            var data = new SalesLedgerInvoicingByCustomerCodeResponse() { SalesLedgerInvoicingModelList = _salesLedgerInvoicesModelList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByCustomerCode(_companyCode, _customerCode))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByCustomerCode(_companyCode, _customerCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByCustomerCode_Error_Test()
        {
            SetErrorMockData();
            var data = new SalesLedgerInvoicingByCustomerCodeResponse() { ErrorInfo = _errorInfoList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByCustomerCode(_companyCode, _customerCode))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByCustomerCode(_companyCode, _customerCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByCustomerCode_NoData_Test()
        {
            var data = new SalesLedgerInvoicingByCustomerCodeResponse() { SalesLedgerInvoicingModelList = null };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByCustomerCode(_companyCode, _customerCode))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByCustomerCode(_companyCode, _customerCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByCustomerNameTest()
        {
            SetMockData();
            var data = new SalesLedgerInvoicingByCustomerNameResponse() { SalesLedgerInvoicingModelList = _salesLedgerInvoicesModelList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByCustomerName(_companyCode, _customerName))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByCustomerName(_companyCode, _customerName);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByCustomerName_Error_Test()
        {
            SetErrorMockData();
            var data = new SalesLedgerInvoicingByCustomerNameResponse() { ErrorInfo = _errorInfoList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByCustomerName(_companyCode, _customerName))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByCustomerName(_companyCode, _customerName);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByCustomerName_NoData_Test()
        {
            var data = new SalesLedgerInvoicingByCustomerNameResponse() { SalesLedgerInvoicingModelList = null };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByCustomerName(_companyCode, _customerName))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByCustomerName(_companyCode, _customerName);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByOrderNumberTest()
        {
            SetMockData();
            var data = new SalesLedgerInvoicingByOrderNumberResponse() { SalesLedgerInvoicingModelList = _salesLedgerInvoicesModelList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByOrderNumber(_companyCode, _orderNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByOrderNumber(_companyCode, _orderNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByOrderNumber_Error_Test()
        {
            SetErrorMockData();
            var data = new SalesLedgerInvoicingByOrderNumberResponse() { ErrorInfo = _errorInfoList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByOrderNumber(_companyCode, _orderNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByOrderNumber(_companyCode, _orderNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByOrderNumber_NoData_Test()
        {
            var data = new SalesLedgerInvoicingByOrderNumberResponse() { SalesLedgerInvoicingModelList = null };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByOrderNumber(_companyCode, _orderNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByOrderNumber(_companyCode, _orderNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByInvoiceNumberTest()
        {
            SetMockData();
            var data = new SalesLedgerInvoicingByInvoiceNumberResponse() { SalesLedgerInvoicingModelList = _salesLedgerInvoicesModelList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByInvoiceNumber(_companyCode, _invoiceNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByInvoiceNumber(_companyCode, _invoiceNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByInvoiceNumber_Error_Test()
        {
            SetErrorMockData();
            var data = new SalesLedgerInvoicingByInvoiceNumberResponse() { ErrorInfo = _errorInfoList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByInvoiceNumber(_companyCode, _invoiceNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByInvoiceNumber(_companyCode, _invoiceNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByInvoiceNumber_NoData_Test()
        {
            var data = new SalesLedgerInvoicingByInvoiceNumberResponse() { SalesLedgerInvoicingModelList = null };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByInvoiceNumber(_companyCode, _invoiceNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByInvoiceNumber(_companyCode, _invoiceNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByInvoiceDateTest()
        {
            SetMockData();
            var data = new SalesLedgerInvoicingByInvoiceDateResponse() { SalesLedgerInvoicingModelList = _salesLedgerInvoicesModelList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByInvoiceDate(_companyCode, _invoiceFromDate, _invoiceToDate))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByInvoiceDate(_companyCode, _invoiceFromDate, _invoiceToDate);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByInvoiceDate_Error_Test()
        {
            SetErrorMockData();
            var data = new SalesLedgerInvoicingByInvoiceDateResponse() { ErrorInfo = _errorInfoList };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByInvoiceDate(_companyCode, _invoiceFromDate, _invoiceToDate))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByInvoiceDate(_companyCode, _invoiceFromDate, _invoiceToDate);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetSalesLedgerInvoicingByInvoiceDate_NoData_Test()
        {
            SetErrorMockData();
            var data = new SalesLedgerInvoicingByInvoiceDateResponse() { SalesLedgerInvoicingModelList = null };
            _salesLedgerInvoicingManagerMock.Stub(
                    x => x.GetSalesLedgerInvoicingByInvoiceDate(_companyCode, _invoiceFromDate, _invoiceToDate))
                .IgnoreArguments()
                .Return(data);
            var response = _salesLedgerInvoicingController.GetSalesLedgerInvoicingByInvoiceDate(_companyCode, _invoiceFromDate, _invoiceToDate);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        public void SetMockData()
        {
            _salesLedgerInvoicesModelList.Add(new SalesLedgerInvoicesModel()
            {
                Amount = "0",
                CustomerCode = "1001",
                CustomerName = "LSS Technologies",
                DueDate = "2012-11-08 00:00:00.0",
                InvoiceDate = "2012-11-08 00:00:00.0",
                InvoiceNumber = "ADV-CHQ303431",
                InvoiceStatus = "0",
                InvoiceType = "5",
                OrderNumber = "",
                OrderType = "",
                StornoInvoice = "",
                TransactionType = "0"
            });
            _salesLedgerInvoicesModelList.Add(new SalesLedgerInvoicesModel()
            {
                Amount = "8040",
                CustomerCode = "1001",
                CustomerName = "LSS Technologies",
                DueDate = "2013-04-08 00:00:00.0",
                InvoiceDate = "2013-04-08 00:00:00.0",
                InvoiceNumber = "F13/290004",
                InvoiceStatus = "0",
                InvoiceType = "0",
                OrderNumber = "4939000004",
                OrderType = "1",
                StornoInvoice = "",
                TransactionType = "0"
            });
            _salesLedgerInvoicesModelList.Add(new SalesLedgerInvoicesModel()
            {
                Amount = "0",
                CustomerCode = "1009",
                CustomerName = "Voltamp Switch Gear Co LLC",
                DueDate = "2012-11-08 00:00:00.0",
                InvoiceDate = "2012-11-08 00:00:00.0",
                InvoiceNumber = "CHQ-804503",
                InvoiceStatus = "0",
                InvoiceType = "5",
                OrderNumber = "",
                OrderType = "",
                StornoInvoice = "",
                TransactionType = "0"
            });
            _salesLedgerInvoicesModelList.Add(new SalesLedgerInvoicesModel()
            {
                Amount = "1250",
                CustomerCode = "1009",
                CustomerName = "Voltamp Switch Gear Co LLC",
                DueDate = "2012-11-11 00:00:00.0",
                InvoiceDate = "2012-11-11 00:00:00.0",
                InvoiceNumber = "F17/700101",
                InvoiceStatus = "0",
                InvoiceType = "0",
                OrderNumber = "1500036060",
                OrderType = "5",
                StornoInvoice = "",
                TransactionType = "0"
            });
        }
        private void SetErrorMockData()
        {
            _errorInfoList.Add(new ErrorInfo("Exception"));
        }
    }
}
