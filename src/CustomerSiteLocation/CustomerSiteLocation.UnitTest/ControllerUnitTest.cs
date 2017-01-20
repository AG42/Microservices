using System;
using System.Collections.Generic;
using CustomerSiteLocation.API.Controllers;
using CustomerSiteLocation.BusinessLayer.Interface;
using CustomerSiteLocation.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using CustomerSiteLocation.API;
using CustomerSiteLocation.API.Filters;
using CustomerSiteLocation.Common.Error;
using CustomerSiteLocation.Model.Response;
using Rhino.Mocks;

namespace CustomerSiteLocation.UnitTest
{
    [TestClass]
    public class ControllerUnitTest
    {
        #region Decalrations

        private ICustomerSiteLocationManager _customerSiteLocationManager;
        private CustomerSiteLocationController _controller;
        private const string CompanyCode = "bh";
        readonly List<CustomerSiteLocationModel> _customerSiteLocationModels = new List<CustomerSiteLocationModel>();
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        #endregion

        [TestInitialize]
        public void Initialize()
        {
            _controller = new CustomerSiteLocationController(_customerSiteLocationManager) { Request = new HttpRequestMessage() };
        }

        #region Unit Test Methods

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code
        public void GetCutomerSiteLocationsByCompanyCodeTest()
        {
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse {CustomerSiteLocations = _customerSiteLocationModels};
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCompanyCode(CompanyCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCutomerSiteLocationsByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels};
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCompanyCode(CompanyCode)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCutomerSiteLocationsByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and CustomerName
        public void GetCustomerSiteLocationsByCustomerNameTest()
        {
            string customerName = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerName(CompanyCode, customerName)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByCustomerName(CompanyCode, customerName);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerName(CompanyCode,customerName)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByCustomerName(CompanyCode,customerName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Customer Code
        public void GetCustomerSiteLocationsByCustomerCodeTest()
        {
            string customerCode = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerCode(CompanyCode, customerCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByCustomerCode(CompanyCode, customerCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCustomerCode(CompanyCode, customerCode)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByCustomerCode(CompanyCode, customerCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Country
        public void GetCustomerSiteLocationsByCountryTest()
        {
            string country = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCountry(CompanyCode, country)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByCountry(CompanyCode, country);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByCountry(CompanyCode, country)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByCountry(CompanyCode, country);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Telephone
        public void GetCustomerSiteByTelephoneNoTest()
        {
            string telephone = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, telephone)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, telephone);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, telephone)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByTelephoneNo(CompanyCode, telephone);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Address
        public void GetCustomerSiteLocationsByAddressTest()
        {
            string address = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByAddress(CompanyCode, address)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByAddress(CompanyCode, address);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByAddress(CompanyCode, address)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByAddress(CompanyCode, address);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Fax No
        public void GetCustomerSiteByFaxNoTest()
        {
            string faxno = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByFaxNo(CompanyCode, faxno)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByFaxNo(CompanyCode, faxno);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByFaxNo(CompanyCode, faxno)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByFaxNo(CompanyCode, faxno);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Email Id
        public void GetCustomerSiteLocationsByEmailIdTest()
        {
            string emailId = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByEmailId(CompanyCode, emailId)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByEmailId(CompanyCode, emailId);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByEmailId(CompanyCode, emailId)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByEmailId(CompanyCode, emailId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Site No
        public void GetCustomerSiteLocationsBySiteNoTest()
        {
            string siteNo = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsBySiteNo(CompanyCode, siteNo)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsBySiteNo(CompanyCode, siteNo);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsBySiteNo(CompanyCode, siteNo)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsBySiteNo(CompanyCode, siteNo);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Zip
        public void GetCustomerSiteLocationsByZipTest()
        {
            string zip = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByZip(CompanyCode, zip)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByZip(CompanyCode, zip);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByZip(CompanyCode, zip)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByZip(CompanyCode, zip);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Unit Testing Get Customer Site Locations by Company Code and Zip
        public void GetCustomerSiteLocationsByServiceEngineerCodeTest()
        {
            string engineerCode = "Test";
            // Positive Scenario with success response
            var mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            SetMockDataForCustomerSiteLocationModel();
            var response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, engineerCode)).IgnoreArguments().Return(response);
            MockController(mockRepository);
            var result = _controller.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, engineerCode);
            Assert.IsNotNull(result);
            // Positive Scenario without sucess response
            mockRepository = MockRepository.GenerateMock<ICustomerSiteLocationManager>();
            response = new CustomerSiteLocationsResponse { CustomerSiteLocations = _customerSiteLocationModels };
            _errorsList.Add(new ErrorInfo("errorMessage") { Message = "Error Message" });
            response.ErrorInfo.AddRange(_errorsList);
            mockRepository.Stub(x => x.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, engineerCode)).Return(response);
            MockController(mockRepository);
            result = _controller.GetCustomerSiteLocationsByServiceEngineerCode(CompanyCode, engineerCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Validation Filter Unit Test
        public void ValidationFilterTest()
        {
            // Positive Scenario
            Type t = typeof(CustomerSiteLocationController);
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
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/customersitelocation");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "customersitelocation" } });
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        public void MockControllerRequestFilterNegativeData()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/customersitelocation");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", null }});
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private void MockController(ICustomerSiteLocationManager iCustomerSiteLocationManager)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/customersitelocation");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "customersitelocation" } });
            _controller = new CustomerSiteLocationController(iCustomerSiteLocationManager) { Request = new HttpRequestMessage() };
            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        private void SetMockDataForCustomerSiteLocationModel()
        {
            #region SampleCustomerProjectOrderModelList

            _customerSiteLocationModels.Add(new CustomerSiteLocationModel()
            {
                Name = "กระเบื้องกระดาษไทย",
                ERP_Customer_Code__c = "FI010",
                ERP_Site_Code__c = "0001",
                SVMXC__Street__c = "Siam Fiber Lamphang จังหวัด ลำปาง",
                SVMXC__City__c = string.Empty,
                County__c = string.Empty,
                SVMXC__State__c = string.Empty,
                Region__c = string.Empty,
                SVMXC__Country__c = "",
                SVMXC__Zip__c = "",
                SVMXC__Site_Fax__c = "0-2803-6733-5#114",
                SVMXC__Site_Phone__c = "0-2803-6733-5#114",
                SVMXC__Email__c = "",
                SVMXC__Latitude__c = "0.00000000",
                SVMXC__Longitude__c = "0.00000000",
                Altitude__c = "",
                SVMXC__Service_Engineer__c = string.Empty,
                Service_Engineer_Code__c = "",
                SVMXC__Stocking_Location__c = true

            });
            #endregion
        }

        #endregion
    }
}
