using System;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerInformation.BusinessLayer;
using CustomerInformation.DataLayer.Entities;
using CustomerInformation.DataLayer.Interfaces;
using CustomerInformation.Model;

namespace CustomerInformation.UnitTest
{
    [TestClass]
    public class CustomerManagerUnitTest
    {
        #region Declarations
        private IDataLayerContext iDataLayer;
        private CustomerManager customerManager = null;
        string companyCode = "xh";
        string customerCode = "C002";
        string customerName = "Customer 1";
        CustomerInformationModel customerInformationModel =
                new CustomerInformationModel();
        CustomerMaster customerMaster = new CustomerMaster();
        public List<CustomerInformationModel> customerInformationModelList = new List<CustomerInformationModel>();
        public List<CustomerMaster> customerList = new List<CustomerMaster>();
        #endregion

        #region UnitTests
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            customerManager = new CustomerManager(iDataLayer);
        }

        /// <summary>
        /// Unit Test for GetCustomers controller method
        /// </summary>
        [TestMethod]
        public void GetCustomersByCompanyCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomers(companyCode))
                            .IgnoreArguments()
                            .Return(customerList);

            customerManager = new CustomerManager(mockRepository);

            var result = customerManager.GetCustomers(companyCode);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rules
            Assert.AreEqual(result[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
                                customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);
        }

        /// <summary>
        /// Unit Test for GetCustomerByID Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerById(companyCode, customerCode))
                            .IgnoreArguments()
                            .Return(customerMaster);

            customerManager = new CustomerManager(mockRepository);

            var result = customerManager.GetCustomerById(companyCode, customerCode);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result.BillingStreet, String.Format(customerMaster.sl01003 + "{0}" + customerMaster.sl01004 + "{0}" + customerMaster.sl01005 + "{0}" + customerMaster.sl01099 + "{0}" +
                                customerMaster.sl0194 + "{0}" + customerMaster.sl01195 + "{0}" + customerMaster.sl01196, Environment.NewLine));
            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.ERP_Key, "I" + companyCode + customerMaster.sl01001);

        }

        /// <summary>
        /// Unit Test for GetCustomerByName Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByNameTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerByName(companyCode, customerCode))
                            .IgnoreArguments()
                            .Return(customerList);
            customerManager = new CustomerManager(mockRepository);

            var result = customerManager.GetCustomerByName(companyCode, customerName);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
                                customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));

            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result[0].ERP_Key, "I"+companyCode+customerList[0].sl01001);
        }

        /// <summary>
        /// Unit Test for GetCustomer with null request Object.
        /// </summary>
        [TestMethod]
        public void GetCustomersNegativeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();

            mockRepository.Stub(x => x.GetCustomers(string.Empty))
                            .IgnoreArguments()
                            .Return(customerList);

            customerManager = new CustomerManager(mockRepository);

            //Assert for GetCustomers with null companyCode
            var resultByCompanyCode = customerManager.GetCustomers(string.Empty);
            Assert.IsNull(resultByCompanyCode);

            //Assert for GetCustomerById with null companyCode & customerCode
            var resultByCompanyCodeAndCustomerCode = customerManager.GetCustomerById(string.Empty, string.Empty);
            Assert.IsNull(resultByCompanyCode);

            //Assert for GetCustomerByName with null companyCode & customerName
            var resultByCompanyCodeAndCustomerName = customerManager.GetCustomerByName(string.Empty, string.Empty);
            Assert.IsNull(resultByCompanyCode);
        }

        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerModels()
        {
            #region SampleDataCustomerMaster
            customerMaster = new CustomerMaster
            {
                sl01001 = "C002",
                sl01002 = "Customer Test2",
                sl01003 = "Commer Zone",
                sl01004 = "Brad Pitt, Eric Bana, Orlando Bloom",
                sl01005 = "Pune",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "428201",
                sl0194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            };

            customerList.Add(new CustomerMaster()
            {
                sl01001 = "C006",
                sl01002 = "Customer 2",
                sl01003 = "Commer Zone",
                sl01004 = "Brad Pitt, Eric Bana, Orlando Bloom",
                sl01005 = "Pune",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "428201",
                sl0194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"

                //String.Format(customerMaster.sl01003 + "{0}" + customerMaster.sl01004 + "{0}" + customerMaster.sl01005 + "{0}" + customerMaster.sl01099 + "{0}" +
                //                customerMaster.sl0194 + "{0}" + customerMaster.sl01195 + "{0}" + customerMaster.sl01196, Environment.NewLine),
            });
            customerList.Add(new CustomerMaster()
            {
                sl01001 = "C009",
                sl01002 = "Customer 1",
                sl01003 = "Commer Zone 1234",
                sl01004 = "Qwerty Leonardo DiCaprio, Kate Winslet",
                sl01005 = "Pune qwerty",
                sl01099 = "AddressLine4",
                sl01152 = "CityPune Code",
                sl01011 = "9876543210",
                sl01022 = "INR",
                sl01083 = "425001",
                sl0194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            });
            customerList.Add(new CustomerMaster()
            {
                sl01001 = "C004",
                sl01002 = "Customer 1",
                sl01003 = "Commer Zone 12",
                sl01004 = "Leonardo DiCaprio, Kate Winslet qwerty",
                sl01005 = "Pune",
                sl01099 = "AddressLine41",
                sl01152 = "CityPune Code",
                sl01011 = "9898543210",
                sl01022 = "USD",
                sl01083 = "405201",
                sl0194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7"
            });
            #endregion
        }
        #endregion
    }
}
