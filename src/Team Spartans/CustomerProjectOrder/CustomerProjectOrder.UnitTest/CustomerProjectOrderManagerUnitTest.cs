using System;
using System.Collections.Generic;
using CustomerProjectOrder.BusinessLayer;
using CustomerProjectOrder.DataLayer.Entities.Datalake;
using CustomerProjectOrder.DataLayer.Interfaces;
using CustomerProjectOrder.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CustomerProjectOrder.UnitTest
{
    [TestClass]
    public class CustomerProjectOrderManagerUnitTest
    {
        #region Declarations

        private IDataLayerContext _iDataLayer;
        private CustomerProjectOrderManager _customerProjectOrderManager;
        private const string CompanyCode = "bh";
        private const string ProjectNumber = " ";

         CustomerProjectOrderModel _customerProjectOrderModel = new CustomerProjectOrderModel();
         List<CustomerProjectOrderModel> _customerProjectOrderList = new List<CustomerProjectOrderModel>();


        Pr01 _pr01 = new Pr01();
        public List<Pr01> CustomerList = new List<Pr01>();

        #endregion

        #region UnitTests
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _customerProjectOrderManager = new CustomerProjectOrderManager(_iDataLayer);
        }
        [TestMethod]
        public void GetProjectByNumberTestWhiteSpaceForProjectNumber()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();

            mockRepository.Stub(x => x.GetProjectByNumber(CompanyCode, ProjectNumber))
                            .IgnoreArguments()
                            .Return(new Pr01());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByNumber(CompanyCode, ProjectNumber);
            Assert.IsNotNull(result);
           
        }
        [TestMethod]
        public void GetProjectByNumberTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();

            mockRepository.Stub(x => x.GetProjectByNumber(CompanyCode, "M100030010"))
                            .IgnoreArguments()
                            .Return(new Pr01());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByNumber(CompanyCode, "M100030010");
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void GetProjectByNumberTestProjectNumberLength()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();

            mockRepository.Stub(x => x.GetProjectByNumber(CompanyCode, "M100030010999999999999999999"))
                            .IgnoreArguments()
                            .Return(new Pr01());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByNumber(CompanyCode, "M100030010999999999999999999");
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void GetProjectByNumberTestNeg()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            mockRepository.Stub(x => x.GetProjectByNumber(string.Empty, string.Empty))
                           .IgnoreArguments()
                           .Return(new Pr01());
            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByNumber(string.Empty, string.Empty);
            Assert.IsNotNull(result.ErrorInfo);

         }
        [TestMethod]
        public void GetProjectByNumberTestException()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDataLayerContext>();
          
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockExceptionRepository.Stub(x => x.GetProjectByNumber(CompanyCode, "M100030010"))
                            .IgnoreArguments()
                            .Throw(new Exception());
            _customerProjectOrderManager = new CustomerProjectOrderManager(mockExceptionRepository);

            var result = _customerProjectOrderManager.GetProjectByNumber(CompanyCode, "M100030010");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProjectByNumberTestNullException()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDataLayerContext>();

            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockExceptionRepository.Stub(x => x.GetProjectByNumber(CompanyCode, "M100030010"))
                .IgnoreArguments()
                .Return(null);
            _customerProjectOrderManager = new CustomerProjectOrderManager(mockExceptionRepository);

            var result = _customerProjectOrderManager.GetProjectByNumber(CompanyCode, "M100030010");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProjectByNumberTestInputValidation()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDataLayerContext>();

            mockExceptionRepository.Stub(x => x.GetProjectByNumber(string.Empty, string.Empty))
                            .IgnoreArguments()
                            .Return(null);
            _customerProjectOrderManager = new CustomerProjectOrderManager(mockExceptionRepository);

            var result = _customerProjectOrderManager.GetProjectByNumber(string.Empty, string.Empty);
            Assert.IsNotNull(result.ErrorInfo);
        }

        [TestMethod]
        public void GetProjectByNameTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByName(CompanyCode, "Ritz Carlton VILLA CHILLE"))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByName(CompanyCode, "Ritz Carlton VILLA CHILLE");
            Assert.IsNotNull(result.CustomerProjectOrders);

            CustomerList.Clear();
            mockRepository.Stub(x => x.GetProjectByName(CompanyCode, string.Empty))
                          .IgnoreArguments()
                          .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

             result = _customerProjectOrderManager.GetProjectByName(CompanyCode, string.Empty);
            Assert.IsNotNull(result.CustomerProjectOrders);

            mockRepository.Stub(x => x.GetProjectByName(CompanyCode, string.Empty))
                          .IgnoreArguments()
                          .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

             result = _customerProjectOrderManager.GetProjectByName(CompanyCode, "Ritz Carlton VILLA CHILLE");
            Assert.IsNotNull(result.CustomerProjectOrders);
        }

        [TestMethod]
        public void GetProjectByNameTestwithEmptyProjectList()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByName(CompanyCode, "SYS PRIME RETROFIT" + "TH92 - 4240 - Q534 - R1"))
                            .IgnoreArguments()
                            .Return(new List<Pr01>());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByName(CompanyCode, "SYS PRIME RETROFIT" + "TH92 - 4240 - Q534 - R1");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByNameTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetProjectByName(CompanyCode, "SYS PRIME RETROFIT" + "TH92 - 4240 - Q534 - R1"))
                            .IgnoreArguments()
                            .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByName(CompanyCode, "SYS PRIME RETROFIT" + "TH92 - 4240 - Q534 - R1");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByNameTestNullException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            mockRepository.Stub(x => x.GetProjectByName(CompanyCode,"Ritz Carlton VILLA CHILLE"))
                .IgnoreArguments()
                .Throw(new Exception());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByName(CompanyCode, "Ritz Carlton VILLA CHILLE");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByNameTestInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
           
            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByName(string.Empty, "SYS PRIME RETROFIT" + "TH92 - 4240 - Q534 - R1");
            Assert.IsNotNull(result);


            result = _customerProjectOrderManager.GetProjectByName(string.Empty, string.Empty);
            Assert.IsNotNull(result);


            result = _customerProjectOrderManager.GetProjectByName(CompanyCode, "SYS PRIME RETROFIT" + "TH92 - 4240 - Q534 - R1");
            Assert.IsNotNull(result);


            result = _customerProjectOrderManager.GetProjectByName(CompanyCode, string.Empty);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByDurationTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09"))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09");
            Assert.IsNotNull(result.CustomerProjectOrders[0].ERP_Project_Start_Date__c);
            Assert.IsNotNull(result.CustomerProjectOrders[0].ERP_Project_End_Date__c);


            CustomerList.Clear();

            mockRepository.Stub(x => x.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09"))
                          .IgnoreArguments()
                          .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

             result = _customerProjectOrderManager.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09");
            Assert.IsNotNull(result.CustomerProjectOrders);


            mockRepository.Stub(x => x.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09"))
                          .IgnoreArguments()
                          .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            result = _customerProjectOrderManager.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09");
            Assert.IsNotNull(result.CustomerProjectOrders);


        }


        [TestMethod]
        public void GetProjectByDurationTestWhiteSpaceForDate()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse
            {
                CustomerProjectOrder = _customerProjectOrderModel
            };

            mockRepository.Stub(x => x.GetProjectByDuration(CompanyCode, "  ", "  "))
                .IgnoreArguments()
                .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByDuration(CompanyCode, "  ", "  ");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByDurationTestwithEmptyList()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09"))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByDurationTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
           
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09"))
                            .IgnoreArguments()
                            .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProjectByDurationTestNullException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
           
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09"))
                .IgnoreArguments()
                .Throw(new Exception());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByDuration(CompanyCode, "2016/09/01", "2016/09/09");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProjectByDurationTestInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByDuration(string.Empty, string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByCustomerPONoTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCustomerPONo(CompanyCode, "7000128779"))
                            .IgnoreArguments()
                            .Return(new Pr01());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCustomerPONo(CompanyCode, "7000128779");
            Assert.IsNotNull(result.CustomerProjectOrder);

          
        }
        [TestMethod]
        public void GetProjectByCustomerPONoTestLengthValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCustomerPONo(CompanyCode, "70001287799999999999999999999999999999999999999999999999"))
                            .IgnoreArguments()
                            .Return(new Pr01());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCustomerPONo(CompanyCode, "70001287799999999999999999999999999999999999999999999999");
            Assert.IsNotNull(result);


        }
        [TestMethod]
        public void GetProjectByCustomerPONoTestwithEmptyList()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCustomerPONo(CompanyCode, "7000128779"))
                            .IgnoreArguments()
                            .Return(new Pr01());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCustomerPONo(CompanyCode, "7000128779");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProjectByCustomerPONoTestWhiteSpace()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCustomerPONo(CompanyCode, " "))
                            .IgnoreArguments()
                            .Return(new Pr01());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCustomerPONo(CompanyCode, "  ");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByCustomerPONoTestNullException()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
          
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCustomerPONo(CompanyCode, "7000128779"))
                            .IgnoreArguments()
                            .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCustomerPONo(CompanyCode, "7000128779");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByCustomerPONoTestException()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCustomerPONo(CompanyCode, "7000128779"))
                            .IgnoreArguments()
                            .Throw(new Exception());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCustomerPONo(CompanyCode, "7000128779");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByCustomerPONoTestInputValidation()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            mockRepository.Stub(x => x.GetProjectByCustomerPONo(string.Empty, string.Empty))
                         .IgnoreArguments()
                         .Throw(new Exception());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCustomerPONo(string.Empty, string.Empty);
            Assert.IsNotNull(result);


          
        }

        [TestMethod]
        public void GetProjectByAccountTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };

            mockRepository.Stub(x => x.GetProjectByAccount(CompanyCode, "3838"))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByAccount(CompanyCode, "3838");
            Assert.IsNotNull(result.CustomerProjectOrders);

            result = _customerProjectOrderManager.GetProjectByAccount(CompanyCode, "38389999999999999999999999999999999999999999999999999999999");
            Assert.IsNotNull(result);

            CustomerList.Clear();
            mockRepository.Stub(x => x.GetProjectByAccount(CompanyCode, string.Empty))
                         .IgnoreArguments()
                         .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            result = _customerProjectOrderManager.GetProjectByAccount(CompanyCode, string.Empty);
            Assert.IsNotNull(result);



            mockRepository.Stub(x => x.GetProjectByAccount(CompanyCode, string.Empty))
                           .IgnoreArguments()
                           .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

             result = _customerProjectOrderManager.GetProjectByAccount(CompanyCode, string.Empty);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetProjectByAccountTestwithEmptyList()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByAccount(CompanyCode, "3838"))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByAccount(CompanyCode, "3838");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByAccountTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };

            mockRepository.Stub(x => x.GetProjectByAccount(CompanyCode, "3838"))
                            .IgnoreArguments()
                            .Throw(new Exception());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByAccount(CompanyCode, "3838");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProjectByAccountTestNullException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            var data = new Model.Response.CustomerProjectOrdersResponse { CustomerProjectOrders = _customerProjectOrderList };

            mockRepository.Stub(x => x.GetProjectByAccount(CompanyCode, "3838"))
                .IgnoreArguments()
                .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByAccount(CompanyCode, "3838");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByAccountTestInputValidation()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByAccount(string.Empty, "3838");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByCompanyCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();

            mockRepository.Stub(x => x.GetProjectByCompanyCode(CompanyCode))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCompanyCode(CompanyCode);
            Assert.IsNotNull(result.CustomerProjectOrders);

            CustomerList.Clear();

            mockRepository.Stub(x => x.GetProjectByCompanyCode(CompanyCode))
                         .IgnoreArguments()
                         .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            result = _customerProjectOrderManager.GetProjectByCompanyCode(CompanyCode);
            Assert.IsNotNull(result.CustomerProjectOrders);


            mockRepository.Stub(x => x.GetProjectByCompanyCode(CompanyCode))
                          .IgnoreArguments()
                          .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

             result = _customerProjectOrderManager.GetProjectByCompanyCode(CompanyCode);
            Assert.IsNotNull(result.CustomerProjectOrders);


        }

        [TestMethod]
        public void GetProjectByCompanyCodeTestwithEmptyList()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerProjectModel();
            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCompanyCode(CompanyCode))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProjectByCompanyCodeTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCompanyCode(CompanyCode))
                .IgnoreArguments()
                .Throw(new Exception());

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetProjectByCompanyCodeTestNullException()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            var data = new Model.Response.CustomerProjectOrderResponse { CustomerProjectOrder = _customerProjectOrderModel };

            mockRepository.Stub(x => x.GetProjectByCompanyCode(CompanyCode))
                .IgnoreArguments()
                .Return(null);

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCompanyCode(CompanyCode);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProjectByCompanyCodeTestInputValidation()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            _customerProjectOrderManager = new CustomerProjectOrderManager(mockRepository);

            var result = _customerProjectOrderManager.GetProjectByCompanyCode(string.Empty);
            Assert.IsNotNull(result);
        }


     
        #region MockData Methods
        /// <summary>
        /// To create a mock data of product for unit test
        /// </summary>
        public void SetMockDataForCustomerProjectModel()
        {
            #region SampleCustomerProjectOrderModelList
            _customerProjectOrderList.Add(new CustomerProjectOrderModel()
            {
                ERP_Project_Number__c = "M100030010",
                Account = "PL001",
                ERP_Customer_PO_Number__c = "7000128779",
                Subject = "INTEGATION CARRIER",
                Description = "MDR - Residual Value" + "JCC-11-1120-SIY-01",
                ERP_Project_Start_Date__c = "2010-12-14 00:00:00.0",
                ERP_Project_End_Date__c = "2011-02-28 00:00:00.0",
                Status = "Working",
                Origin = "Integrated",
                RecordType = "Projects",
                ERP_Project_Key__c = "I" + CompanyCode + "M100030010"

            });
            _customerProjectOrderList.Add(new CustomerProjectOrderModel()
            {
                ERP_Project_Number__c = "P000030020",
                Account = "NX001",
                ERP_Customer_PO_Number__c = "TH618000199004",
                Subject = "NXP-Install Tripod&Access",
                Description = "SYS PRIME RETROFIT" + "TH92-4240-Q534-R1",
                ERP_Project_Start_Date__c = "2009-09-29 00:00:00.0",
                ERP_Project_End_Date__c = "2009-12-30 00:00:00.0",
                Status = "Working",
                Origin = "Integrated",
                RecordType = "Projects",
                ERP_Project_Key__c = "I" + CompanyCode + "M100030010"
            });
            _customerProjectOrderList.Add(new CustomerProjectOrderModel()
            {
                ERP_Project_Number__c = "P200050060",
                Account = "SE003",
                ERP_Customer_PO_Number__c = "SK1260221",
                Subject = "Fire alarm control panel",
                Description = "SYS PRIME RETROFIT" + "JCC-11-0509-SIR-01",
                ERP_Project_Start_Date__c = "2011-08-09 00:00:00.0",
                ERP_Project_End_Date__c = "2011-08-10 00:00:00.0",
                Status = "Working",
                Origin = "Integrated",
                RecordType = "Projects",
                ERP_Project_Key__c = "I" + CompanyCode + "M100030010"
            });
            #endregion

            CustomerList.Add(new Pr01()
                {
                Pr01001 = "P161004840",
            Pr01003 = "3838",
            Pr01009 = "Ritz Carlton VILLA CHILLE",
            Pr01010 = "SMALL TONNAGE",
            Pr01011 = "",
            Pr01067 = "2010-12-14 00:00:00.0",
            Pr01069 = "2011-02-28 00:00:00.0",
            Pr01106 = "CAPEX201601-01228",
        } 
                
                );
            CustomerList.Add(new Pr01()
            {
                Pr01001 = "P2H2100068",
                Pr01003 = "1790",
                Pr01009 = "FY 10 Phase-2",
                Pr01010 = "WARRANTY CONSTRUCTION",
                Pr01011 = "",
                Pr01067 = "2016-12-14 00:00:00.0",
                Pr01069 = "2016-02-28 00:00:00.0",
                Pr01106 = "250-E-4168-R1",
            }

               );


        }
        #endregion
        #endregion
    }
}
