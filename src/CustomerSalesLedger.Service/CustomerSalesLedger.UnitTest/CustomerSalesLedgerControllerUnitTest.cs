using CustomerSalesLedger.API.Controllers;
using CustomerSalesLedger.BusinessLayer.Interfaces;
using CustomerSalesLedger.Common;
using CustomerSalesLedger.Common.Error;
using CustomerSalesLedger.DataLayer.Entities.Datalake;
using CustomerSalesLedger.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomerSalesLedger.UnitTest
{
    [TestClass]
    public class CustomerSalesLedgerControllerUnitTest
    {
        #region Declarations
        private ICustomerSalesLedgerManager _mockRepository;
        private CustomerSalesLedgerController _controller;
        private const string COMPANY_CODE = "xh";
        private const string CUSTOMER_CODE = "C001";
        private const string CUSTOMER_NAME = "Customer 1";
        private const string CUSTOMER_CATEGORY = "DSE";
        private const string CUSTOMER_PHONENO = "13123123";
        private const string CUSTOMER_EMAILID = "TEST@TEST.COM";
        private const string CUSTOMER_COUNTRYCODE = "BH";
        readonly List<CustomerSalesLedgerModel> _customerSalesLedgerModeList = new List<CustomerSalesLedgerModel>();
        private readonly List<Sl01> _customerList = new List<Sl01>();

        #endregion

        #region unit test method
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = MockRepository.GenerateMock<ICustomerSalesLedgerManager>();
            _controller = new CustomerSalesLedgerController(_mockRepository) { Request = new HttpRequestMessage() };
            _controller.Request = new HttpRequestMessage();
            _controller.Request.SetConfiguration(new HttpConfiguration());
        }

        /// <summary>
        /// Unit Test for GetCustomers controller method
        /// </summary>

        [TestMethod]
        public void GetServiceName()
        {
            var result = _controller.GetServiceName();
            Assert.IsTrue(result.Equals("Customer Sales Ledger service is running...."));
        }



        [TestMethod]
        public void GetCustomerByNameTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByCustomerNameResponse();
            data.CustomerSalesLedger.AddRange(_customerSalesLedgerModeList);
            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByCustomerName(COMPANY_CODE, CUSTOMER_NAME))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetCustomerSalesLedgerByCustomerName(COMPANY_CODE, CUSTOMER_NAME);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByNameTestWithFailureResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByCustomerNameResponse();
            data.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));

            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByCustomerName(COMPANY_CODE, CUSTOMER_NAME))
                           .IgnoreArguments()
                           .Return(data);

            var result = _controller.GetCustomerSalesLedgerByCustomerName(COMPANY_CODE, CUSTOMER_NAME);
            Assert.IsNotNull(result);
        }


        [TestMethod]

        public void GetCustomerByAlternativeNameTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByAlternateNameResponse();
            data.CustomerSalesLedger.AddRange(_customerSalesLedgerModeList);
            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByAlternateName(COMPANY_CODE, CUSTOMER_NAME))
                            .IgnoreArguments()
                            .Return(data);

            var result = _controller.GetCustomerSalesLedgerByAlternateName(COMPANY_CODE, CUSTOMER_NAME);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetCustomersByAlternativeNameWithFailureResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByAlternateNameResponse();
            data.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));

            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByAlternateName(COMPANY_CODE, CUSTOMER_NAME))
                           .IgnoreArguments()
                           .Return(data);

            var result = _controller.GetCustomerSalesLedgerByAlternateName(COMPANY_CODE, CUSTOMER_NAME);
            Assert.IsNotNull(result);
        }


        [TestMethod]

        public void GetCustomesByCategoryTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByCategoryResponse();

            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByCategory(COMPANY_CODE, CUSTOMER_CATEGORY))
                           .IgnoreArguments()
                           .Return(data);

            var result = _controller.GetCustomerSalesLedgerByCategory(COMPANY_CODE, CUSTOMER_CATEGORY);
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void GetCustomesByCategoryWithFailureResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByCategoryResponse();
            data.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));

            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByCategory(COMPANY_CODE, CUSTOMER_CATEGORY)).IgnoreArguments().Return(data);

            var result = _controller.GetCustomerSalesLedgerByCategory(COMPANY_CODE, CUSTOMER_CATEGORY);
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void GetCustomerByCustomerCodeWithResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByCustomerCodeResponse();
            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByCustomerCode(COMPANY_CODE, CUSTOMER_CODE)).IgnoreArguments().Return(data);
            var result = _controller.GetCustomerSalesLedgerByCustomerCode(COMPANY_CODE, CUSTOMER_CODE);
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void GetCustomersByCustomerCodewithFailureResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByCustomerCodeResponse();
            data.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByCustomerCode(COMPANY_CODE, CUSTOMER_CODE)).IgnoreArguments().Return(data);
            var result = _controller.GetCustomerSalesLedgerByCustomerCode(COMPANY_CODE, CUSTOMER_CODE);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomersByEmailWithResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByEmailIdResponse();

            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByEmailId(COMPANY_CODE, CUSTOMER_EMAILID))
                           .IgnoreArguments()
                           .Return(data);

            var result = _controller.GetCustomerSalesLedgerByEmailId(COMPANY_CODE, CUSTOMER_EMAILID);
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void GetCustomersByEmailWithFailureResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesLedgerByEmailIdResponse();
            data.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));

            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByEmailId(COMPANY_CODE, CUSTOMER_EMAILID)).IgnoreArguments().Return(data);

            var result = _controller.GetCustomerSalesLedgerByEmailId(COMPANY_CODE, CUSTOMER_EMAILID);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetCustomersByTelephoneNumberWithResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesledgerByPhoneNoResponse();

            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByPhoneNumber(COMPANY_CODE, CUSTOMER_PHONENO)).IgnoreArguments() .Return(data);

            var result = _controller.GetCustomerSalesLedgerByPhoneNumber(COMPANY_CODE, CUSTOMER_PHONENO);
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void GetCustomersByTelephoneNumberWithFailureResponseTest()
        {
            SetMockDataForCustomerSalesLedgerModel();
            var data = new Model.Response.CustomerSalesledgerByPhoneNoResponse();
            data.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            _mockRepository.Stub(x => x.GetCustomerSalesLedgerByPhoneNumber(COMPANY_CODE, CUSTOMER_PHONENO)).IgnoreArguments().Return(data);

            var result = _controller.GetCustomerSalesLedgerByPhoneNumber(COMPANY_CODE, CUSTOMER_PHONENO);
            Assert.IsNotNull(result);





        }

        #endregion

        #region MockData
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerSalesLedgerModel()
        {
            #region SampleCustomerSalesLedgerModelList
            _customerSalesLedgerModeList.Add(new CustomerSalesLedgerModel()
            {
                CustomerCode = "001",
                CustomerName = "abc",
                HighestCreditLimit = "300",
                TaxRegistrationNumber = "00anc",
                PaymentTerms = "def",
                Balance = "564",
                Budget = "7683",
                TotalDaysDuePayment = "0987",
                NumberOfinvoices = "123",
                LastInvoiceDate = "12-dec-2016",
                LastPaymentDate = "12-jan-2016",
                CreditCode = "0023",
                Category = "F",
                WayOfPayment = "direct",
                AlternateName = "zrs",
                EmailId = "sam@gmail.com",
                PhoneNumber = "982391"
            });

            #endregion

            #region SampleDataCustomerMasterList
            _customerList.Add(new Sl01()
            {
                Sl01001 = "C001",
                Sl01002 = "Customer 2",
                Sl01010 = "Commer Zone",
                Sl01011 = "9876543210",
                Sl01018 = "6782352",
                Sl01021 = "abcd15e",
                Sl01024 = "double",
                Sl01038 = "99289",
                Sl01045 = "10927",
                Sl01048 = "15",
                Sl01049 = "20",
                Sl01050 = "12-dec-2016",
                Sl01051 = "12-jan-2017",
                Sl01054 = "be",
                Sl01068 = "001b",
                Sl01112 = "poc",
                Sl01193 = "was@gmail.com"
            });
            _customerList.Add(new Sl01()
            {
                Sl01001 = "C002",
                Sl01002 = "Customer 3",
                Sl01010 = "westin Zone",
                Sl01011 = "9076543210",
                Sl01018 = "9882352",
                Sl01021 = "zxabcd15e",
                Sl01024 = "tripe",
                Sl01038 = "1982289",
                Sl01045 = "110927",
                Sl01048 = "151",
                Sl01049 = "220",
                Sl01050 = "18-dec-2016",
                Sl01051 = "17-jan-2017",
                Sl01054 = "de",
                Sl01068 = "1001b",
                Sl01112 = "direct",
                Sl01193 = "viv@gmail.com"
            });
            _customerList.Add(new Sl01()
            {
                Sl01001 = "1002",
                Sl01002 = "Voltamp Switch Gear Co LLC",
                Sl01010 = "",
                Sl01011 = "00 973 17735564",
                Sl01018 = "0",
                Sl01021 = "",
                Sl01024 = "1",
                Sl01038 = "0",
                Sl01045 = "0",
                Sl01048 = "0",
                Sl01049 = "0",
                Sl01050 = "",
                Sl01051 = "",
                Sl01054 = "VOLTAMP SWITCH GEAR CO LLC",
                Sl01068 = "",
                Sl01193 = "",
                Sl01112 = "0"
            });
            #endregion

        }

        #endregion

    }
}
