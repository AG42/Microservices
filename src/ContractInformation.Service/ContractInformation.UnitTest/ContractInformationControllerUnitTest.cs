using System;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContractInformation.API.Controllers;
using ContractInformation.BusinessLayer.Interfaces;
using ContractInformation.API.Filters;
using ContractInformation.Common.Error;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Linq;
using ContractInformation.Model.Models;
using ContractInformation.API;
using ContractInformation.BusinessLayer;
using ContractInformation.Model.Response;

namespace ContractInformation.UnitTest
{
    [TestClass]
    public class ContractInformationControllerUnitTest
    {
        #region Decalrations

        private IContractInformationManager _contractInformationManager;
        private ContractInformationController _controller;
        private const string CompanyCode = "bh";
        private const string ContractNumber = "1234456";
        private const string RequestNumber = "2345678";
        private const string PONumber = "";
        private const string CustomerName = "";
        private const string Reference = "";
        private const string SearchKey = "";
        private const string Name = "";
        private const string Status = "";
        private const string StartDate = "";
        private const string EndDate = "";

        readonly List<Contract> _contractList = new List<Contract>();
        readonly List<Customer> _customerList = new List<Customer>();
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        #endregion

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _contractInformationManager = new ContractInformationManager(null);
            _controller = new ContractInformationController(_contractInformationManager) { Request = new HttpRequestMessage() };
        }

        #endregion

        #region Unit Test Methods

        [TestMethod]
        // Unit Testing Get Contracts By CompanyCode by Company Code
        public void GetContractsByCompanyCodeTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByCompanyCode(CompanyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByCompanyCode(CompanyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code and Contract number
        public void GetContractsByServiceContractNumberTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByServiceContractNumber(CompanyCode, ContractNumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByServiceContractNumber(CompanyCode, ContractNumber);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByServiceContractNumber(CompanyCode, ContractNumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByServiceContractNumber(CompanyCode, ContractNumber);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code and Request Number
        public void GetContractsByRequestNumberTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByRequestNumber(CompanyCode, RequestNumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByRequestNumber(CompanyCode, RequestNumber);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByRequestNumber(CompanyCode, RequestNumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByRequestNumber(CompanyCode, RequestNumber);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        // Unit Testing Get Contracts By Company Code and Customer PO Number
        public void GetContractsByCustomerPONumberTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByCustomerPONumber(CompanyCode, PONumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByCustomerPONumber(CompanyCode, PONumber);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByCustomerPONumber(CompanyCode, PONumber)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByCustomerPONumber(CompanyCode, PONumber);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code and Customer Name
        public void GetContractsByCustomerNameTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByCustomerName(CompanyCode, CustomerName)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByCustomerName(CompanyCode, CustomerName);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByCustomerName(CompanyCode, CustomerName)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByCustomerName(CompanyCode, CustomerName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code and Reference
        public void GetContractsByCustomerReferenceTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByCustomerReference(CompanyCode, Reference)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByCustomerReference(CompanyCode, Reference);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByCustomerReference(CompanyCode, Reference)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByCustomerReference(CompanyCode, Reference);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code and SearchKey
        public void GetContractsByCustomerSearchKeyTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKey(CompanyCode, SearchKey)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByCustomerSearchKey(CompanyCode, SearchKey);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKey(CompanyCode, SearchKey)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByCustomerSearchKey(CompanyCode, SearchKey);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code, Name, and Status
        public void GetContractsByCustomerNameAndStatusTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByCustomerNameandStatus(CompanyCode, Name, Status)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByCustomerNameandStatus(CompanyCode, Name, Status);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByCustomerNameandStatus(CompanyCode, Name, Status)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByCustomerNameandStatus(CompanyCode, Name, Status);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code, Reference, and Status
        public void GetContractsByCustomerReferenceAndStatusTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByCustomerReferenceandStatus(CompanyCode, Reference, Status)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByCustomerReferenceandStatus(CompanyCode, Reference, Status);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByCustomerReferenceandStatus(CompanyCode, Reference, Status)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByCustomerReferenceandStatus(CompanyCode, Reference, Status);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code, Reference, and Status
        public void GetContractsByCustomerSearchKeyAndStatusTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKeyandStatus(CompanyCode, SearchKey, Status)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByCustomerSearchKeyandStatus(CompanyCode, SearchKey, Status);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByCustomerSearchKeyandStatus(CompanyCode, SearchKey, Status)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByCustomerSearchKeyandStatus(CompanyCode, SearchKey, Status);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        // Unit Testing Get Contracts By Company Code and Status
        public void GetContractsByStatusTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByStatus(CompanyCode, Status)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByStatus(CompanyCode, Status);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByStatus(CompanyCode, Status)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByStatus(CompanyCode, Status);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Contracts By Company Code and DateRange
        public void GetContractsByDateRangeTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            SetMockDataForContractInformationModel();
            var response = new ContractsResponse { Contracts = _contractList };
            mockRepository.Stub(x => x.GetContractsByDateRange(CompanyCode, StartDate, EndDate)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetContractsByDateRange(CompanyCode, StartDate, EndDate);
            Assert.IsNotNull(result);
            // Positive Scenario without success response
            mockRepository = MockRepository.GenerateMock<IContractInformationManager>();
            response = new ContractsResponse { Contracts = _contractList };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetContractsByDateRange(CompanyCode, StartDate, EndDate)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetContractsByDateRange(CompanyCode, StartDate, EndDate);
            Assert.IsNotNull(result);
        }



        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(ContractInformationController);
            ValidationFilter obj = new ValidationFilter();
            MockControllerRequestTestData();
            obj.OnActionExecuting(_controller.ActionContext);
            Assert.IsTrue(t.GetCustomAttributes(typeof(ValidationFilter), true).Length > 0);
            // Negative Scenario
            MockControllerRequestFilterNegativeData();
            obj.OnActionExecuting(_controller.ActionContext);
            Assert.IsNotNull(_controller.ActionContext.Response);
        }

        [TestMethod]
        public void UnityConfigUnitTest()
        {
            UnityConfig.RegisterComponents(new HttpConfiguration());
        }
        #endregion

        #region Mock Methods

        public void MockControllerRequestTestData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/contract");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "contract" } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/contract");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private void MockController(IContractInformationManager iContractInformationManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/customersitelocation");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "contractInformation" } });
            _controller = new ContractInformationController(iContractInformationManager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private void SetMockDataForContractInformationModel()
        {
            #region SampleContractList

            _contractList.Add(new Contract()
            {
                ServiceContractNumber = "9416000001",
                CustomerCodeInvoice = "5995",
                Remarks = "SIGNED AGREEMENT",
                StartDate = "2015-10-01 00:00:00.0",
                EndDate = "2016-09-30 00:00:00.0",
                Type = "PSA",
                Code = "N",
                Status = "Active",
                Terms = "1",
                CustomerInformation = _customerList.FirstOrDefault()
            });

            _contractList.Add(new Contract()
            {
                ServiceContractNumber = "9416000002",
                CustomerCodeInvoice = "59951",
                Remarks = "SIGNED AGREEMENT",
                StartDate = "2015-10-01 00:00:00.0",
                EndDate = "2016-09-30 00:00:00.0",
                Type = "PSA",
                Code = "N",
                Status = "Active",
                Terms = "1",
                CustomerInformation = _customerList.FirstOrDefault()
            });

            _customerList.Add(new Customer()
            {
                Name = "ABCD",
                Email = "aks200@gmail.com",
                Reference = "NAIR SUJITH",
                RequestNumber = "",
                SearchKey = "0",
                PurchaseOrderNumber = "SIGNED AGREEMENT"
            });

            #endregion
        }

        #endregion
    }
}
