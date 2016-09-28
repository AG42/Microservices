using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerInformation.API.Controllers;
using CustomerInformation.BusinessLayer.Interface;
using CustomerInformation.DataLayer.Entities.Datalake;
using CustomerInformation.Model;
using CustomerInformation.Common.Error;

namespace CustomerInformation.UnitTest
{
    [TestClass]
    public class CustomerControllerUnitTest
    {
        #region Declarations
        private ICustomerManager _iCustomer;
        private CustomerController _controller;
        private const string COMPANY_CODE = "xh";
        private const string CUSTOMER_CODE = "C001";
        private const string CUSTOMER_NAME = "Customer 1";
        readonly List<CustomerInformationModel> _customerInformationModelList = new List<CustomerInformationModel>();
        private readonly List<Sl01> _customerList = new List<Sl01>();
        readonly List<ErrorInfo> _errorsList = new List<ErrorInfo>();
        #endregion

        #region UnitTests
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //_controller = new CustomerController(_iCustomer) {Request = new HttpRequestMessage()};
            ////_controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _controller = new CustomerController(_iCustomer) { Request = new HttpRequestMessage() };
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:51083/?param1=someValue&param2=anotherValue");
        }

        /// <summary>
        /// Unit Test for GetCustomers controller method
        /// </summary>
        [TestMethod]
        public void GetCustomersByCompanyCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();
            var data = new Model.Response.CustomerSearchByCompanyCodeResponse();
            data.Customers.AddRange(_customerInformationModelList);

            mockRepository.Stub(x => x.GetCustomers(COMPANY_CODE))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                                "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };

            var result = _controller.GetCustomers(COMPANY_CODE);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetCustomersTestException()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();
            var data = new Model.Response.CustomerSearchByCompanyCodeResponse();
            _errorsList.Add(new ErrorInfo("errorMessage") { ErrorMessage = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetCustomers(COMPANY_CODE))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                                "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };
            var result = _controller.GetCustomers(COMPANY_CODE);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetCustomerByID Controller Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerById(COMPANY_CODE, CUSTOMER_CODE))
                            .IgnoreArguments()
                            .Return(new Model.Response.CustomerSearchByIdResponse());

            _controller = new CustomerController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                                "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };

            var result = _controller.GetCustomerById(COMPANY_CODE, CUSTOMER_CODE);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetCustomerByIdTestException()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();
            var data = new Model.Response.CustomerSearchByIdResponse();
            _errorsList.Add(new ErrorInfo("errorMessage") { ErrorMessage = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetCustomerById(COMPANY_CODE,"C001"))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                                "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };
            var result = _controller.GetCustomerById(COMPANY_CODE,"C001");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetCustomerByName Controller Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByNameTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();
            var data = new Model.Response.CustomerSearchByNameResponse();
            data.Customers.AddRange(_customerInformationModelList);
            mockRepository.Stub(x => x.GetCustomerByName(COMPANY_CODE, CUSTOMER_NAME))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                                "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };

            var result = _controller.GetCustomerByName(COMPANY_CODE, CUSTOMER_NAME);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(OkResult));
        }
        /// <summary>
        /// Unit Test 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetCustomerNameTestException()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();
            var data = new Model.Response.CustomerSearchByNameResponse();
            _errorsList.Add(new ErrorInfo("errorMessage") { ErrorMessage = "Error Message" });
            data.ErrorInfo.AddRange(_errorsList);

            mockRepository.Stub(x => x.GetCustomerByName(COMPANY_CODE, CUSTOMER_NAME))
                            .IgnoreArguments()
                            .Return(data);

            _controller = new CustomerController(mockRepository)
            {
                Request =
                    new HttpRequestMessage(HttpMethod.Get,
                                "http://localhost:51083/?param1=someValue&param2=anotherValue")
            };
            var result = _controller.GetCustomerByName(COMPANY_CODE, CUSTOMER_NAME);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetCustomer with null request Object.
        /// </summary>
        //[TestMethod]
        //public void GetCustomersNegativeTest()
        //{
        //    var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
        //    var data = new Model.Response.CustomerSearchByCompanyCodeResponse();
        //    mockRepository.Stub(x => x.GetCustomers(string.Empty))
        //                    .IgnoreArguments()
        //                    .Return(data);

        //    _controller = new CustomerController(mockRepository);

        //    //Assert for GetCustomers with null companyCode
        //    IHttpActionResult resultByCompanyCode = _controller.GetCustomers(string.Empty);
        //    Assert.IsInstanceOfType(resultByCompanyCode, typeof(NotFoundResult));

        //    //Assert for GetCustomerById with null companyCode & customerCode
        //    IHttpActionResult resultByCompanyCodeAndCustomerCode = _controller.GetCustomerById(string.Empty, string.Empty);
        //    Assert.IsInstanceOfType(resultByCompanyCodeAndCustomerCode, typeof(NotFoundResult));

        //    //Assert for GetCustomerByName with null companyCode & customerName
        //    IHttpActionResult resultByCompanyCodeAndCustomerName = _controller.GetCustomerByName(string.Empty, string.Empty);
        //    Assert.IsInstanceOfType(resultByCompanyCodeAndCustomerName, typeof(NotFoundResult));
        //}

        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerModels()
        {
            #region SampleCustomerInformationModelList
            _customerInformationModelList.Add(new CustomerInformationModel()
            {
                ERP_Customer_Code_c = "C001",
                BillingCity = "CityPune Code",
                Phone = "9876543210",
                CurrencyIsoCode = "INR",
                BillingPostalCode = "428201"
            });
            _customerInformationModelList.Add(new CustomerInformationModel()
            {
                ERP_Customer_Code_c = "C002",
                BillingCity = "Mumbai",
                Phone = "9876987210",
                CurrencyIsoCode = "USD",
                BillingPostalCode = "426201"
            });
            #endregion
            #region SampleDataCustomerMaster
            _customerList.Add(new Sl01()
            {
                sl01001 = "C001",
                sl01002 = "Customer 2",
                sl01003 = "Commer Zone",
                sl01004 = "Brad Pitt, Eric Bana, Orlando Bloom",
                sl01005 = "Pune",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "428201"
            });
            _customerList.Add(new Sl01()
            {
                sl01001 = "C002",
                sl01002 = "Customer 1",
                sl01003 = "Commer Zone 12",
                sl01004 = "Leonardo DiCaprio, Kate Winslet",
                sl01005 = "Pune",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "425001"

            });
            _customerList.Add(new Sl01()
            {
                sl01001 = "C003",
                sl01002 = "Customer 4",
                sl01003 = "Commer Zone 12",
                sl01004 = "Leonardo DiCaprio, Kate Winslet qwerty",
                sl01005 = "Pune",
                sl01099 = "AddressLine41",
                sl01152 = "CityPune Code",
                sl01011 = "9898543210",
                sl01022 = "USD",
                sl01083 = "405201"

            });
            #endregion
        }
        #endregion
    }


}
