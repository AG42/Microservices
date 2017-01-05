using System;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreditStatus.API.Controllers;
using CreditStatus.BusinessLayer.Interfaces;
using CreditStatus.API.Filters;
using CreditStatus.Common.Error;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Linq;
using CreditStatus.Model.Models;
using CreditStatus.API;
using CreditStatus.BusinessLayer;
using CreditStatus.Model.Response;

namespace CreditStatus.UnitTest
{
    [TestClass]
    public  class CreditStatusControllerUnitTest
    {
        #region Declarations

        private ICreditStatusManager _creditStatusManager;
        private CreditStatusController _controller;

        private string _companyCode = "na";
        private string _customerCode = "";
        private string _customerName = "";
        private bool _ledgerFlag = true;

        readonly CreditResponse _creditResponse = new CreditResponse();
        readonly CreditsResponse _creditsResponse = new CreditsResponse() {  Credits = new List<CreditStatusModel>()};
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        

        #endregion

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _creditStatusManager = new CreditStatusManager(null);
            _controller = new CreditStatusController(_creditStatusManager) { Request = new HttpRequestMessage() };
        }

        #endregion

        #region Unit Test Methods

        [TestMethod]
        //Unit Testing Get CreditsStatus By CompanyCode and LedgerFlag
        public void GetContractsByCompanyCodeTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICreditStatusManager>();
            SetMockDataForCreditStatusModelList();
            var response = new CreditsResponse { Credits = _creditsResponse.Credits };
            mockRepository.Stub(x => x.GetCreditStatusByCompanyCode(_companyCode, _ledgerFlag)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCreditStatusByCompanyCode(_companyCode, _ledgerFlag);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICreditStatusManager>();
            response = new CreditsResponse { Credits = _creditsResponse.Credits };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCreditStatusByCompanyCode(_companyCode, _ledgerFlag)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetCreditStatusByCompanyCode(_companyCode, _ledgerFlag);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get CreditsStatus By CompanyCode, CustomerName  and LedgerFlag
        public void GetContractsByCustomerNameTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICreditStatusManager>();
            SetMockDataForCreditStatusModelList();
            var response = new CreditsResponse { Credits = _creditsResponse.Credits };
            mockRepository.Stub(x => x.GetCreditStatusByCustomerName(_companyCode, _customerName, _ledgerFlag)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCreditStatusByCustomerName(_companyCode, _customerName, _ledgerFlag);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICreditStatusManager>();
            response = new CreditsResponse { Credits = _creditsResponse.Credits };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCreditStatusByCustomerName(_companyCode, _customerName, _ledgerFlag)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetCreditStatusByCustomerName(_companyCode, _customerName, _ledgerFlag);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get CreditStatus By CompanyCode, CustomerCode  and LedgerFlag
        public void GetContractsByCustomerCodeTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICreditStatusManager>();
            SetMockDataForCreditStatusModelList();
            var response = new CreditResponse { Credit = _creditResponse.Credit };
            mockRepository.Stub(x => x.GetCreditStatusByCustomerCode(_companyCode, _customerCode, _ledgerFlag)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCreditStatusByCustomerCode(_companyCode, _customerCode, _ledgerFlag);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICreditStatusManager>();
            response = new CreditResponse { Credit = _creditResponse.Credit };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCreditStatusByCustomerCode(_companyCode, _customerCode, _ledgerFlag)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetCreditStatusByCustomerCode(_companyCode, _customerCode, _ledgerFlag);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(CreditStatusController);
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
        //Unit Testing Get CreditStatus By CompanyCode, CustomerCode  and LedgerFlag
        public void GetCreditStatus()
        {
            var result = _controller.GetCreditStatus();
            Assert.IsNotNull(result);
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
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/creditstatus");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "contract" } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/creditstatus");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private void MockController(ICreditStatusManager iCreditStatusManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/creditstatus");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "creditstatus" } });
            _controller = new CreditStatusController(iCreditStatusManager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        #endregion

        #region SampleCreditStatusModelList
        public void SetMockDataForCreditStatusModelList()
        {
            
            _creditsResponse.Credits.ToList().Add(new CreditStatusModel()
            {
              CustomerCode="",
              CustomerName="",
              Status=true
            });
        }
        #endregion

    }
}
