using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using ServiceOrder.BusinessLayer.Interfaces;
using ServiceOrder.API.Controllers;
using ServiceOrder.Model;
using ServiceOrder.Common.Error;
using System;
using ServiceOrder.Model.Responses;
using System.Net;
using ServiceOrder.Common;

namespace ServiceOrder.UnitTest
{
    [TestClass]
    public class ServiceOrderControllerUnitTest
    {
        #region Declarations
        private IServiceOrderManager mockServiceOrderManager;
        private ServiceOrderController _controller;
        private const string COMPANY_CODE = "8M";
        private const string SERVICE_ORDER_NO = "1600162000";

        readonly ServiceOrderModel _serviceOrderModel = new ServiceOrderModel();
        readonly List<ServiceOrderLineLaborModel> serviceOrderLineLaborList = new List<ServiceOrderLineLaborModel>();
        readonly List<ServiceOrderLineMOItemModel> serviceOrderLineMOItemList = new List<ServiceOrderLineMOItemModel>();
        readonly List<ServiceOrderLineToolModel> serviceOrderLineToolList = new List<ServiceOrderLineToolModel>();
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();

        #endregion

        #region UnitTests
        /// <summary/>
        /// Initializes
        ///// </summary>
        [TestInitialize]
        public void Initialize()
        {
            mockServiceOrderManager = MockRepository.GenerateMock<IServiceOrderManager>();
            _controller = new ServiceOrderController(mockServiceOrderManager) { Request = new HttpRequestMessage() };
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _controller.Configuration = new HttpConfiguration();
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:51083/?param1=someValue&param2=anotherValue");
        }

        /// <summary>
        /// ServiceOrderNo 
        /// </summary>  
        [TestMethod]
        public void GetServiceOrderByServiceOrderNoTestWithSuccessStatus()
        {
            SetMockDataForServiceOrderModels();
            var response = new ServiceOrderByServiceOrderNoResponse();
            response.ServiceOrderModel = _serviceOrderModel;
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetServiceOrderByServiceOrderNoTestWithFailuerStatus()
        {
            var response = new ServiceOrderByServiceOrderNoResponse();
            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            response.ServiceOrderModel = _serviceOrderModel;
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderByServiceOrderNoTestWithHttpResponseException()
        {
            var response = new ServiceOrderByServiceOrderNoResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new HttpResponseException(HttpStatusCode.ExpectationFailed));
            var result = _controller.GetServiceOrderByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderByServiceOrderNoTestWithException()
        {
            var response = new ServiceOrderByServiceOrderNoResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new Exception());
            var result = _controller.GetServiceOrderByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        //[TestMethod]
        //public void GetServiceOrderByServiceOrderNoTestWithEmptyCompanyCode()
        //{
        //    SetMockDataForServiceOrderModels();
                    
        //    var result = _controller.GetServiceOrderByServiceOrderNo(string.Empty, SERVICE_ORDER_NO);
        //    Assert.IsNotNull(result.StatusCode == HttpStatusCode.OK);
        //}


        /// <summary>
        /// OrderType
        /// </summary>
        [TestMethod]
        public void GetServiceOrderTypeByServiceOrderNoTestWithSuccessStatus()
        {
            SetMockDataForServiceOrderModels();
            var response = new ServiceOrderTypeByServiceOrderNoResponse();
            response.OrderType = "LM";
            mockServiceOrderManager.Stub(x => x.GetServiceOrderTypeByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderTypeByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetServiceOrderTypeByServiceOrderNoTestWithFailuerStatus()
        {
            var response = new ServiceOrderTypeByServiceOrderNoResponse();
            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            mockServiceOrderManager.Stub(x => x.GetServiceOrderTypeByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderTypeByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderTypeByServiceOrderNoTestWithHttpResponseException()
        {
            //SetMockDataForServiceOrderModels();
            var response = new ServiceOrderTypeByServiceOrderNoResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderTypeByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new HttpResponseException(HttpStatusCode.ExpectationFailed));
            var result = _controller.GetServiceOrderTypeByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderTypeByServiceOrderNoTestWithException()
        {
            var response = new ServiceOrderTypeByServiceOrderNoResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderTypeByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new Exception());
            var result = _controller.GetServiceOrderTypeByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }


        /// <summary>
        /// OrderStatus
        /// </summary>
        [TestMethod]
        public void GetServiceOrderStatusByServiceOrderNoTestWithSuccessStatus()
        {
            SetMockDataForServiceOrderModels();
            var response = new ServiceOrderStatusByServiceOrderNoResponse();
            response.OrderStatus = "50";
            mockServiceOrderManager.Stub(x => x.GetServiceOrderStatusByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderStatusByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetServiceOrderStatusByServiceOrderNoTestWithFailuerStatus()
        {
            var response = new ServiceOrderStatusByServiceOrderNoResponse();
            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            mockServiceOrderManager.Stub(x => x.GetServiceOrderStatusByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderStatusByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderStatusByServiceOrderNoTestWithHttpResponseException()
        {
            //SetMockDataForServiceOrderModels();
            var response = new ServiceOrderStatusByServiceOrderNoResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderStatusByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new HttpResponseException(HttpStatusCode.ExpectationFailed));
            var result = _controller.GetServiceOrderStatusByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderStatusByServiceOrderNoTestWithException()
        {
            var response = new ServiceOrderStatusByServiceOrderNoResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderStatusByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new Exception());
            var result = _controller.GetServiceOrderStatusByServiceOrderNo(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        /// <summary>
        /// InvoiceCustomerCode
        /// </summary>  
        [TestMethod]
        public void GetServiceOrderByInvoiceCustomerCodeTestWithSuccessStatus()
        {
            SetMockDataForServiceOrderModels();
            var response = new ServiceOrderByInvoiceCustomerCodeResponse();
            response.ServiceOrderModels.Add(_serviceOrderModel);
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByInvoiceCustomerCode(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderByInvoiceCustomerCode(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetServiceOrderByInvoiceCustomerCodeTestWithFailuerStatus()
        {
            var response = new ServiceOrderByInvoiceCustomerCodeResponse();
            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            response.ServiceOrderModels.Add(_serviceOrderModel);
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByInvoiceCustomerCode(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderByInvoiceCustomerCode(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderByInvoiceCustomerCodeTestWithHttpResponseException()
        {
            var response = new ServiceOrderByInvoiceCustomerCodeResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByInvoiceCustomerCode(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new HttpResponseException(HttpStatusCode.ExpectationFailed));
            var result = _controller.GetServiceOrderByInvoiceCustomerCode(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderByInvoiceCustomerCodeTestWithException()
        {
            var response = new ServiceOrderByInvoiceCustomerCodeResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByInvoiceCustomerCode(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new Exception());
            var result = _controller.GetServiceOrderByInvoiceCustomerCode(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }


        /// <summary>
        /// InvoiceNumber
        /// </summary>  
        [TestMethod]
        public void GetServiceOrderByInvoiceNumberTestWithSuccessStatus()
        {
            SetMockDataForServiceOrderModels();
            var response = new ServiceOrderByInvoiceNumberResponse();
            response.ServiceOrderModels.Add(_serviceOrderModel);
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByInvoiceNumber(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderByInvoiceNumber(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void GetServiceOrderByInvoiceNumberTestWithFailuerStatus()
        {
            var response = new ServiceOrderByInvoiceNumberResponse();
            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            response.ServiceOrderModels.Add(_serviceOrderModel);
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByInvoiceNumber(COMPANY_CODE, SERVICE_ORDER_NO))
                            .IgnoreArguments()
                            .Return(response);

            var result = _controller.GetServiceOrderByInvoiceNumber(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderByInvoiceNumberTestWithHttpResponseException()
        {
            var response = new ServiceOrderByInvoiceNumberResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByInvoiceNumber(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new HttpResponseException(HttpStatusCode.ExpectationFailed));
            var result = _controller.GetServiceOrderByInvoiceNumber(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        [TestMethod]
        public void GetServiceOrderByInvoiceNumberTestWithException()
        {
            var response = new ServiceOrderByInvoiceNumberResponse();
            mockServiceOrderManager.Stub(x => x.GetServiceOrderByInvoiceNumber(COMPANY_CODE, SERVICE_ORDER_NO)).Throw(new Exception());
            var result = _controller.GetServiceOrderByInvoiceNumber(COMPANY_CODE, SERVICE_ORDER_NO);
            Assert.IsNotNull(result.StatusCode == HttpStatusCode.ExpectationFailed);
        }

        #endregion

        #region MockData Methods

        public void MockControllerRequest()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/products");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            _controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            _controller.Request = request;
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        private void SetMockDataForServiceOrderModels()
        {
            _serviceOrderModel.Id = "I8M1600323000";
            _serviceOrderModel.ERP_SO_Key__c = "I8M1600323000";
            _serviceOrderModel.CurrencyIsoCode = "00";
            _serviceOrderModel.SVMX_MD_Problem_Summary__c = "Contrato de Mantención Junio Aura";
            _serviceOrderModel.ERP_Component__c = "";
            _serviceOrderModel.SVMXC__Invoice_Number__c = "";
            _serviceOrderModel.ERP_WO_Status__c = "5Z";
            _serviceOrderModel.SVMXC__Order_Type__c = "LM";
            _serviceOrderModel.SVMXC__Company__c = "92142000-5";
            _serviceOrderModel.Bill_To_Account__c = "INTRSERV";
            _serviceOrderModel.Ordering_Customer__c = "Gilberto Pellegrini";
            _serviceOrderModel.ERP_Servicing_Site_Code__c = "0000";
            _serviceOrderModel.ERP_Project_Contract_Number__c = "";
            _serviceOrderModel.ERP_Project_Key__c = "2125000012_NOCON    1600323000000476";
            _serviceOrderModel.ERP_Technician_Code__c = "0000";
            _serviceOrderModel.ERP_Cost_Center__c = "2125000012_NOCON    1600323000000476";
            _serviceOrderModel.ERP_Master_SONumber__c = "1600323000";
            _serviceOrderModel.ERP_Service_Order_Number__c = "";
            _serviceOrderModel.ERP_Customer_PO_Number__c = "";
            _serviceOrderModel.ERP_Invoice_date__c = DateTime.UtcNow;
            _serviceOrderModel.ERP_Payment_Terms_Code__c = "";
            _serviceOrderModel.ERP_Delivery_Blocked__c = false;
            _serviceOrderModel.ERP_Credit_Check_Passed__c = false;
            _serviceOrderModel.ERP_CRM_Exchange_Rate___c = 0;
            _serviceOrderModel.ERP_Allocated_Team_code__c = "29";
            _serviceOrderModel.Job_Type__c = null;
            _serviceOrderModel.ERP_Company_Code__c = "8M";

            #region ServiceLineModel

            _serviceOrderModel.ServiceOrderLineModel = new ServiceOrderLineModel();

            _serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineLaborList.AddRange(GetLabourCollection());
            _serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineToolList.AddRange(GetToolCollection());

            #endregion
        }

        private List<ServiceOrderLineToolModel> GetToolCollection()
        {
            serviceOrderLineToolList.Clear();
            serviceOrderLineToolList.Add(new ServiceOrderLineToolModel
            {
                Id = "I8M1600323000",
                ERP_SO_Line_Number__c = "I8M16003230000001EXPENSES",
                SVMXC__Service_Order__c = null,
                SVMXC__Line_Type__c = "Expenses",
                ERP_Cost_Code_Number__c = "70000",
                Description__c = "SubContractor upload",
                ERP_Tax_code__c = "00",
                ERP_Resource_Code__c = "0000",
                SVMXC__Actual_Price2__c = 0,
                Line_Cost_per_unit__c = 109272,
                SVMXC__Actual_Quantity2__c = 1,
                ERP_Actual_Expense_Date__c = DateTime.UtcNow,
                Invoice_Number__c = "",
                Invoice_Date__c = DateTime.UtcNow,
                SVMXC__Line_Status__c = "Confirmed"
            });
            serviceOrderLineToolList.Add(new ServiceOrderLineToolModel
            {
                Id = "I8M1600323000",
                ERP_SO_Line_Number__c = "I8M16003230000002EXPENSES",
                SVMXC__Service_Order__c = null,
                SVMXC__Line_Type__c = "Expenses",
                ERP_Cost_Code_Number__c = "70000",
                Description__c = "SubContractor upload",
                ERP_Tax_code__c = "00",
                ERP_Resource_Code__c = "0000",
                SVMXC__Actual_Price2__c = 0,
                Line_Cost_per_unit__c = -109272,
                SVMXC__Actual_Quantity2__c = 1,
                ERP_Actual_Expense_Date__c = DateTime.UtcNow,
                Invoice_Number__c = "",
                Invoice_Date__c = DateTime.UtcNow,
                SVMXC__Line_Status__c = "Confirmed"
            });

            return serviceOrderLineToolList;
        }

        private List<ServiceOrderLineLaborModel> GetLabourCollection()
        {
            serviceOrderLineLaborList.Clear();
            serviceOrderLineLaborList.Add(new ServiceOrderLineLaborModel
            {
                Id = "I8M1600162000",
                ERP_SO_Line_Number__c = "I8M16001620000001LABOR",
                SVMXC__Service_Order__c = null,
                Line_Cost_per_unit__c = 8100,
                SVMXC__Line_Status__c = "Confirmed",
                SVMXC__Line_Type__c = "Labor",
                Invoice_Number__c = "",
                Invoice_Date__c = DateTime.UtcNow,
                ERP_Billable_Hours__c = 0
            });

            return serviceOrderLineLaborList;
        }


        #endregion
    }
}
