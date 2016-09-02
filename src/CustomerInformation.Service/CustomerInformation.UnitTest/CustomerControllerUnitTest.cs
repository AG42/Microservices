using CustomerInformation.API.Controllers;
using CustomerInformation.BusinessLayer.Interface;
using CustomerInformation.DataLayer.Entities;
using CustomerInformation.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;

namespace CustomerInformation.UnitTest
{
    [TestClass]
    public class CustomerControllerUnitTest
    {
        #region Declarations
        private ICustomerManager iCustomer;
        private CustomerController controller = null;
        string companyCode = "xh";
        string customerCode = "C001";
        string customerName = "Customer 1";
        List<CustomerInformationModel> customerInformationModelList = new List<CustomerInformationModel>();
        List<CustomerMaster> customerList = new List<CustomerMaster>();
        #endregion

        #region UnitTests
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            controller = new CustomerController(iCustomer);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        /// <summary>
        /// Unit Test for GetCustomers controller method
        /// </summary>
        [TestMethod]
        public void GetCustomersByCompanyCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomers(companyCode))
                            .IgnoreArguments()
                            .Return(customerInformationModelList);

            controller = new CustomerController(mockRepository);

            var result = controller.GetCustomers(companyCode);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        /// <summary>
        /// Unit Test for GetCustomerByID Controller Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerById(companyCode,customerCode))
                            .IgnoreArguments()
                            .Return(new CustomerInformationModel());

            controller = new CustomerController(mockRepository);

            var result = controller.GetCustomerById(companyCode,customerCode);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        /// <summary>
        /// Unit Test for GetCustomerByName Controller Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByNameTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerByName(companyCode, customerName))
                            .IgnoreArguments()
                            .Return(customerInformationModelList);

            controller = new CustomerController(mockRepository);

            var result = controller.GetCustomerByName(companyCode, customerName);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        /// <summary>
        /// Unit Test for GetCustomer with null request Object.
        /// </summary>
        [TestMethod]
        public void GetCustomersNegativeTest()
        {
            var mockRepository = MockRepository.GenerateMock<ICustomerManager>();

            mockRepository.Stub(x => x.GetCustomers(string.Empty))
                            .IgnoreArguments()
                            .Return(new List<CustomerInformationModel>());

            controller = new CustomerController(mockRepository);
            
            //Assert for GetCustomers with null companyCode
            IHttpActionResult resultByCompanyCode = controller.GetCustomers(string.Empty);
            Assert.IsInstanceOfType(resultByCompanyCode, typeof(NotFoundResult));

            //Assert for GetCustomerById with null companyCode & customerCode
            IHttpActionResult resultByCompanyCodeAndCustomerCode = controller.GetCustomerById(string.Empty, string.Empty);
            Assert.IsInstanceOfType(resultByCompanyCodeAndCustomerCode, typeof(NotFoundResult));

            //Assert for GetCustomerByName with null companyCode & customerName
            IHttpActionResult resultByCompanyCodeAndCustomerName = controller.GetCustomerByName(string.Empty,string.Empty);
            Assert.IsInstanceOfType(resultByCompanyCodeAndCustomerName, typeof(NotFoundResult));
        }

        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerModels()
        {
            #region SampleCustomerInformationModelList
            customerInformationModelList.Add(new CustomerInformationModel()
            {
                ERP_Customer_Code_c = "C001",
                AccountName = "Customer 2",
                AddressLine1 = "Commer Zone",
                AddressLine2 = "Brad Pitt, Eric Bana, Orlando Bloom",
                AddressLine3 = "Pune",
                AddressLine4 = "AddressLine4",
                BillingCity = "CityPune Code",
                Phone = "9876543210",
                CurrencyIsoCode = "INR",
                BillingPostalCode = "428201"
            });
            customerInformationModelList.Add(new CustomerInformationModel()
            {
                ERP_Customer_Code_c = "C002",
                AccountName = "Customer 1",
                AddressLine1 = "Commer Zone 21",
                AddressLine2 = "qwerty Brad Pitt, Eric Bana, Orlando Bloom",
                AddressLine3 = "Mumbai",
                AddressLine4 = "AddressLine4 1",
                BillingCity = "Mumbai",
                Phone = "9876987210",
                CurrencyIsoCode = "USD",
                BillingPostalCode = "426201"
            });
            #endregion
            #region SampleDataCustomerMaster
            customerList.Add(new CustomerMaster()
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
            customerList.Add(new CustomerMaster()
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
            customerList.Add(new CustomerMaster()
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
