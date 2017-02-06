using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxInvoice.BusinessLayer;
using TaxInvoice.BusinessLayer.Interfaces;
using TaxInvoice.API.Controllers;
using TaxInvoice.Model.Response;
using TaxInvoice.Common.Error;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using TaxInvoice.Model.Models;
using Rhino.Mocks;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using TaxInvoice.API;
using TaxInvoice.API.Filters;

namespace TaxInvoice.UnitTest
{
    [TestClass]
    public class TaxInvoiceControllerUnitTest
    {


        #region Declarations

        private ITaxInvoiceManager _taxInvoicemanager;
        private TaxInvoiceController _controller;

        private string _companyCode = "va";
        private string _invoiceNo = "1234";
        private string _customerCode = "C001";

        // readonly TaxInvoiceResponse _taxInvoiceResponse = new TaxInvoiceResponse();
        readonly TaxInvoiceResponses _taxInvoiceResponses = new TaxInvoiceResponses() { TaxInvoices = (new List<TaxInvoiceModel>()).AsEnumerable() };
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();



        #endregion

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _taxInvoicemanager = new TaxInvoiceManager(null);
            _controller = new TaxInvoiceController(_taxInvoicemanager) { Request = new HttpRequestMessage() };
        }

        #endregion


        #region Unit Test Methods

        [TestMethod]
        //Unit Testing Get CreditsStatus By CompanyCode and LedgerFlag
        public void GetTaxInvoiceByCompanyCodeTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ITaxInvoiceManager>();
            SetMockDataForTaxInvoiceModelList();
            var response = new TaxInvoiceResponses { TaxInvoices = _taxInvoiceResponses.TaxInvoices };
            mockRepository.Stub(x => x.GetTaxInvoiceByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetTaxInvoiceByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ITaxInvoiceManager>();
            response = new TaxInvoiceResponses { TaxInvoices = _taxInvoiceResponses.TaxInvoices };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetTaxInvoiceByCompanyCode(_companyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetTaxInvoiceByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get CreditsStatus By CompanyCode and LedgerFlag
        public void GetTaxInvoiceByInvoiceNoTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ITaxInvoiceManager>();
            SetMockDataForTaxInvoiceModelList();
            var response = new TaxInvoiceResponses { TaxInvoices = _taxInvoiceResponses.TaxInvoices };
            mockRepository.Stub(x => x.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNo)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNo);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ITaxInvoiceManager>();
            response = new TaxInvoiceResponses { TaxInvoices = _taxInvoiceResponses.TaxInvoices };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNo)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNo);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //Unit Testing Get CreditsStatus By CompanyCode and LedgerFlag
        public void GetTaxInvoiceByCustomerCodeTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ITaxInvoiceManager>();
            SetMockDataForTaxInvoiceModelList();
            var response = new TaxInvoiceResponses { TaxInvoices = _taxInvoiceResponses.TaxInvoices };
            mockRepository.Stub(x => x.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ITaxInvoiceManager>();
            response = new TaxInvoiceResponses { TaxInvoices = _taxInvoiceResponses.TaxInvoices };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            result = _controller.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(TaxInvoiceController);
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
        public void GetTaxInvoiceStatus()
        {
            var result = _controller.GetTaxInvoice();
            Assert.IsNotNull(result);
        }

        public void MockControllerRequestTestData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/taxinvoice");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "taxinvoice" } });
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

        private void MockController(ITaxInvoiceManager iTaxInvoiceManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/taxinvoice");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "taxinvoice" } });
            _controller = new TaxInvoiceController(iTaxInvoiceManager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        #endregion
        #region SampleCreditStatusModelList
        public void SetMockDataForTaxInvoiceModelList()
        {

            _taxInvoiceResponses.TaxInvoices.ToList().Add(new TaxInvoiceModel()
            {
                CustomerCode = "",
                InvoiceNo = "",
                TaxRateCode = "0",
                TaxType = "x",
                TotalBaseAmount = 12.45M,
                TotalSale = 65.85M,
                TotalSTBase = 14M,
                TotalTaxAmount = 15M,
                VATType = ""
            });
        }
        #endregion

        [TestMethod]
        public void UnityConfigUnitTest()
        {
            UnityConfig.RegisterComponents(new HttpConfiguration());
        }

        

    }
}
