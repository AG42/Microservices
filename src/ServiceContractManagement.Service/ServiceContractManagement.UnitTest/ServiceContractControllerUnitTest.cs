using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceContractManagement.BusinessLayer.Interface;
using ServiceContractManagement.Common.Error;
using ServiceContractManagement.Model;
using ServiceContractManagement.Model.Responses;
using ServiceContractManagement.API.Controllers;

namespace ServiceContractManagement.UnitTest
{
    [TestClass]
    public class ServiceContractControllerUnitTest
    {
        private IServiceContractManager _serviceContractManagerMock;

        private ServiceContractController _serviceContractController;
        private List<ServiceContractMasterModel> _serviceContractMasterModelList;
        private ServiceContractMasterModel _serviceContractMasterModel;
        private List<ErrorInfo> _errorInfoList;
        private readonly string _companyCode = "j1";
        private readonly string _contractNumber = "9200910138";
        private readonly string _startDate = "2015-01-09";
        private readonly string _endDate = "2016-01-09";

        [TestInitialize]
        public void Initialize()
        {
            _serviceContractManagerMock = MockRepository.GenerateMock<IServiceContractManager>();
            _serviceContractController = new ServiceContractController(_serviceContractManagerMock) { Request = new HttpRequestMessage() };
            _serviceContractController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                new HttpConfiguration());
            _serviceContractMasterModelList = new List<ServiceContractMasterModel>();
            _serviceContractMasterModel = new ServiceContractMasterModel();
            _errorInfoList = new List<ErrorInfo>();
        }

        #region  Test Methods

        [TestMethod]
        public void GetServiceNameTest()
        {
            var response = _serviceContractController.GetServiceName();
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractCodeTestWithValidInput()
        {            
            var data = new ServiceContractDetailsByContractNumberResponse() { ServiceContractDetails = SetServiceContractMasterModelMockData() };
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDetailsByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDetailsByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractCodeTestWithNullData()
        {
            var data = new ServiceContractDetailsByContractNumberResponse() { ServiceContractDetails = null };
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDetailsByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDetailsByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractCodeTestWithFailedStatus()
        {
            var data = new ServiceContractDetailsByContractNumberResponse() { ServiceContractDetails = SetServiceContractMasterModelMockData()};
            data.ErrorInfo.Add(new ErrorInfo("test"));
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDetailsByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDetailsByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractDateTestWithValidInput()
        {
            var data = new ServiceContractDetailsByContractDateResponse();
            data.ServiceContractDetails.Add(SetServiceContractMasterModelMockData());
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDetailsByContractDate(_companyCode, _startDate, _endDate))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDetailsByContractDate(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void GetServiceContractDetailsByContractDateTestWithNoData()
        {
            var data = new ServiceContractDetailsByContractDateResponse();
            // data.ServiceContractDetails.Add(SetServiceContractMasterModelMockData());
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDetailsByContractDate(_companyCode, _startDate, _endDate))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDetailsByContractDate(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractDateTestWithFailedStatus()
        {
            var data = new ServiceContractDetailsByContractDateResponse();
            data.ErrorInfo.Add(new ErrorInfo("test"));
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDetailsByContractDate(_companyCode, _startDate, _endDate))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDetailsByContractDate(_companyCode, _startDate, _endDate);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [TestMethod]
        public void GetServiceContractTypeByContractNumberTestWithValidInput()
        {
            var data = new ServiceContractTypeByContractNumberResponse();
            
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractTypeByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractTypeByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        public void GetServiceContractTypeByContractNumberTestWithNoData()
        {
            var data = new ServiceContractTypeByContractNumberResponse {ContractType = null};
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractTypeByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractTypeByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractTypeByContractNumberTestWithFailedInput()
        {
            var data = new ServiceContractTypeByContractNumberResponse();
            data.ErrorInfo.Add(new ErrorInfo("test"));
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractTypeByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractTypeByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractUnitPriceByContractNumberTestWithValidInput()
        {
            var data = new ServiceContractUnitPriceByContractNumberResponse();
            data.ServiceContractLineItemUnitPrice.Add(new ServiceContractLinesUnitPriceModel());
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractUnitPriceByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractUnitPriceByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractUnitPriceByContractNumberTestWithNoData()
        {
            var data = new ServiceContractUnitPriceByContractNumberResponse();
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractUnitPriceByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractUnitPriceByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractUnitPriceByContractNumberTestWithFailedStatus()
        {
            var data = new ServiceContractUnitPriceByContractNumberResponse();
            data.ErrorInfo.Add(new ErrorInfo("test"));
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractUnitPriceByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractUnitPriceByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractInvoiceQuantityByContractNumberTestWithValidInput()
        {
            var data = new ServiceContractInvoiceQuantityByContractNumberResponse();
            data.ServiceContractLineItemQuantity.Add(new ServiceContractLinesInvoiceQtyModel());
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractInvoiceQuantityByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractInvoiceQuantityByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractInvoiceQuantityByContractNumberTestWithNoData()
        {
            var data = new ServiceContractInvoiceQuantityByContractNumberResponse();
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractInvoiceQuantityByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractInvoiceQuantityByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractInvoiceQuantityByContractNumberTestWithFailedStatus()
        {
            var data = new ServiceContractInvoiceQuantityByContractNumberResponse();
            data.ErrorInfo.Add(new ErrorInfo("test"));
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractInvoiceQuantityByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractInvoiceQuantityByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractDebitCreditValueByContractNumberTestWithValidInput()
        {
            var data = new ServiceContractDebitCreditValueByContractNumberResponse();
            data.ServiceContractLineItemDebitCreditValue.Add(new ServiceContractLinesDebitCreditValueModel());
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDebitCreditValueByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDebitCreditValueByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractDebitCreditValueByContractNumberTestWithNoData()
        {
            var data = new ServiceContractDebitCreditValueByContractNumberResponse();
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDebitCreditValueByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDebitCreditValueByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractDebitCreditValueByContractNumberTestWithFailedStatus()
        {
            var data = new ServiceContractDebitCreditValueByContractNumberResponse();
            data.ErrorInfo.Add(new ErrorInfo("test"));
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractDebitCreditValueByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractDebitCreditValueByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractPaymentTermsByContractNumberTestWithValidInput()
        {
            var data = new ServiceContractPaymentTermsByContractNumberResponse {PaymentTerms = "10"};
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractPaymentTermsByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractPaymentTermsByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractPaymentTermsByContractNumberTestWithNoData()
        {
            var data = new ServiceContractPaymentTermsByContractNumberResponse();
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractPaymentTermsByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractPaymentTermsByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractPaymentTermsByContractNumberTestWithFailedStatus()
        {
            var data = new ServiceContractPaymentTermsByContractNumberResponse();
            data.ErrorInfo.Add(new ErrorInfo("test"));
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractPaymentTermsByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractPaymentTermsByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractContractValueByContractNumberTestWithValidInput()
        {
            var data = new ServiceContractValueByContractNumberResponse {ContractValue = "10"};
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractValueByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractValueByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractValueByContractNumberTestWithNoData()
        {
            var data = new ServiceContractValueByContractNumberResponse();
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractValueByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractValueByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetServiceContractValueByContractNumberTestWithFailedStatus()
        {
            var data = new ServiceContractValueByContractNumberResponse();
            data.ErrorInfo.Add(new ErrorInfo("test"));
            _serviceContractManagerMock.Stub(
                    x => x.GetServiceContractValueByContractNumber(_companyCode, _contractNumber))
                .IgnoreArguments()
                .Return(data);
            var response = _serviceContractController.GetServiceContractValueByContractNumber(_companyCode, _contractNumber);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

#endregion

        #region Pricate Methods for Mock data

        private ServiceContractMasterModel SetServiceContractMasterModelMockData()
        {
            var result = new ServiceContractMasterModel
            {
                ServiceContractNo = "9416000001",
                CustomerCode = "JO025",
                CustomerReference = "คุณอนพัทย์",
                PaymantTerms = "60",
                ContractDate = "2015-10-01 00:00:00.0",
                InvoicingInterval = "1",
                ContractCode = "PSN",
                ContractType = "R",
                ContractValue = "0.00000000",
                ContractStartDate = "2015-10-01 00:00:00.0",
                ContractEndDate = "2016-09-30 00:00:00.0",
                SalesmanNo = "WTH",
                OurReference = "TH152-4212-0015",
                InvoiceCurrencyCode = "0",
                ContractCurrencyCode = "0",

            };
            result.ServiceContractLineDetails.Add(new ServiceContractLinesModel
            {
                ServiceContractNo = "9416000004",
                LineNo = "000010",
                UnitPriceOCU = "1818336.00000000",
                Price_UnitCode = "1",
                DebitCreditValueOCU = "0.00000000",
                InvoiceQunatity = "0.00000000",
                SalesTaxCode = "07"
            });

            return result;
        }

        private void SetErrorMockData()
        {
            _errorInfoList.Add(new ErrorInfo("Exception"));
        }

        #endregion
    }
}
