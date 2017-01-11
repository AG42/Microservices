using System;
using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerInformation.BusinessLayer;
using CustomerInformation.DataLayer.Entities.Datalake;
using CustomerInformation.DataLayer.Interfaces;

namespace CustomerInformation.UnitTest
{
    [TestClass]
    public class CustomerManagerUnitTest
    {
        #region Declarations
        private IDatabaseContext _iDatabase;
        private CustomerManager _customerManager;
        private const string COMPANY_CODE = "xh";
        private const string CUSTOMER_CODE = "C002";
        private const string CUSTOMER_NAME = "Customer 1";
        private const string CUSTOMER_EMAIL = "xyz@xyz.com";
        private const string CUSTOMER_ALTERNATENAME = "Customer 1";
        private const string CUSTOMER_TELEPHONENUMBER = "00000000";
        private const string CUSTOMER_COUNTRYCODE = "01";
        private const string CUSTOMER_CATEGORY = "TEST";
        Sl01 _sl01 = new Sl01();
        public List<Sl01> CustomerList = new List<Sl01>();
        #endregion 

        #region UnitTests
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _customerManager = new CustomerManager(_iDatabase);
        }

        /// <summary>
        /// Unit Test for GetCustomers controller method
        /// </summary>
        [TestMethod]
        public void GetCustomersByCompanyCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomers(COMPANY_CODE))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerManager = new CustomerManager(mockRepository);

            var result = _customerManager.GetCustomers(COMPANY_CODE);
            Assert.IsNotNull(result.Customers);

            //Assert to check the BillingStreet Validation Rules
            Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(CustomerList[0].sl01003 + "{0}" + CustomerList[0].sl01004 + "{0}" + CustomerList[0].sl01005 + "{0}" + CustomerList[0].sl01099 + "{0}" +
                                CustomerList[0].sl01194 + "{0}" + CustomerList[0].sl01195 + "{0}" + CustomerList[0].sl01196, Environment.NewLine));
            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            Assert.AreEqual(result.Customers[0].ERP_Key, "I" + COMPANY_CODE + CustomerList[0].sl01001);

            //Assert to check the Payment_Terms_Code__c Validation Rule
            Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, CustomerList[0].sl01024.Substring(0, 2));
        }

        /// <summary>
        /// Unit Test for GetCustomerByID Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerById(COMPANY_CODE, CUSTOMER_CODE))
                            .IgnoreArguments()
                            .Return(_sl01);

            _customerManager = new CustomerManager(mockRepository);

            var result = _customerManager.GetCustomerById(COMPANY_CODE, CUSTOMER_CODE);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result.CustomerInformationModel.BillingStreet, String.Format(_sl01.sl01003 + "{0}" + _sl01.sl01004 + "{0}" + _sl01.sl01005 + "{0}" + _sl01.sl01099 + "{0}" +
                                _sl01.sl01194 + "{0}" + _sl01.sl01195 + "{0}" + _sl01.sl01196, Environment.NewLine));
            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.CustomerInformationModel.CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            Assert.AreEqual(result.CustomerInformationModel.ERP_Key, "I" + COMPANY_CODE + _sl01.sl01001);

            //Assert to check the Payment_Terms_Code__c Validation Rule
            Assert.AreEqual(result.CustomerInformationModel.Payment_Terms_Code__c, _sl01.sl01024.Substring(0, 2));
        }

        /// <summary>
        /// Unit Test for GetCustomerByName Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByNameTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerByName(COMPANY_CODE, CUSTOMER_CODE))
                            .IgnoreArguments()
                            .Return(CustomerList);
            _customerManager = new CustomerManager(mockRepository);

            var result = _customerManager.GetCustomerByName(COMPANY_CODE, CUSTOMER_NAME);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(CustomerList[0].sl01003 + "{0}" + CustomerList[0].sl01004 + "{0}" + CustomerList[0].sl01005 + "{0}" + CustomerList[0].sl01099 + "{0}" +
                                CustomerList[0].sl01194 + "{0}" + CustomerList[0].sl01195 + "{0}" + CustomerList[0].sl01196, Environment.NewLine));

            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            Assert.AreEqual(result.Customers[0].ERP_Key, "I" + COMPANY_CODE + CustomerList[0].sl01001);

            //Assert to check the Payment_Terms_Code__c Validation Rule
            Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, CustomerList[0].sl01024.Substring(0, 2));
        }


        /// <summary>
        /// Unit Test for GetCustomerByName Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByEmailIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerByEmailId(COMPANY_CODE, CUSTOMER_EMAIL))
                            .IgnoreArguments()
                            .Return(CustomerList);
            _customerManager = new CustomerManager(mockRepository);

            var result = _customerManager.GetCustomerByEmailId(COMPANY_CODE, CUSTOMER_EMAIL);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(CustomerList[0].sl01003 + "{0}" + CustomerList[0].sl01004 + "{0}" + CustomerList[0].sl01005 + "{0}" + CustomerList[0].sl01099 + "{0}" +
                                CustomerList[0].sl01194 + "{0}" + CustomerList[0].sl01195 + "{0}" + CustomerList[0].sl01196, Environment.NewLine));

            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            Assert.AreEqual(result.Customers[0].ERP_Key, "I" + COMPANY_CODE + CustomerList[0].sl01001);

            //Assert to check the Payment_Terms_Code__c Validation Rule
            Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, CustomerList[0].sl01024.Substring(0, 2));
        }

        /// <summary>
        /// Unit Test for GetCustomerByName Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByPhoneNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerByPhoneNumber(COMPANY_CODE, CUSTOMER_TELEPHONENUMBER))
                            .IgnoreArguments()
                            .Return(CustomerList);
            _customerManager = new CustomerManager(mockRepository);

            var result = _customerManager.GetCustomerByPhoneNumber(COMPANY_CODE, CUSTOMER_TELEPHONENUMBER);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(CustomerList[0].sl01003 + "{0}" + CustomerList[0].sl01004 + "{0}" + CustomerList[0].sl01005 + "{0}" + CustomerList[0].sl01099 + "{0}" +
                                CustomerList[0].sl01194 + "{0}" + CustomerList[0].sl01195 + "{0}" + CustomerList[0].sl01196, Environment.NewLine));

            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            Assert.AreEqual(result.Customers[0].ERP_Key, "I" + COMPANY_CODE + CustomerList[0].sl01001);

            //Assert to check the Payment_Terms_Code__c Validation Rule
            Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, CustomerList[0].sl01024.Substring(0, 2));
        }

        /// <summary>
        /// Unit Test for GetCustomerByName Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByCategoryTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerByCategory(COMPANY_CODE, CUSTOMER_CATEGORY))
                            .IgnoreArguments()
                            .Return(CustomerList);
            _customerManager = new CustomerManager(mockRepository);

            var result = _customerManager.GetCustomerByCategory(COMPANY_CODE, CUSTOMER_CATEGORY);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(CustomerList[0].sl01003 + "{0}" + CustomerList[0].sl01004 + "{0}" + CustomerList[0].sl01005 + "{0}" + CustomerList[0].sl01099 + "{0}" +
                                CustomerList[0].sl01194 + "{0}" + CustomerList[0].sl01195 + "{0}" + CustomerList[0].sl01196, Environment.NewLine));

            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            Assert.AreEqual(result.Customers[0].ERP_Key, "I" + COMPANY_CODE + CustomerList[0].sl01001);

            //Assert to check the Payment_Terms_Code__c Validation Rule
            Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, CustomerList[0].sl01024.Substring(0, 2));
        }

        /// <summary>
        /// Unit Test for GetCustomerByName Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByCustomerAlternateNameTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerByAlternateName(COMPANY_CODE, CUSTOMER_ALTERNATENAME))
                            .IgnoreArguments()
                            .Return(CustomerList);
            _customerManager = new CustomerManager(mockRepository);

            var result = _customerManager.GetCustomerByCustomerAlternateName(COMPANY_CODE, CUSTOMER_ALTERNATENAME);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(CustomerList[0].sl01003 + "{0}" + CustomerList[0].sl01004 + "{0}" + CustomerList[0].sl01005 + "{0}" + CustomerList[0].sl01099 + "{0}" +
                                CustomerList[0].sl01194 + "{0}" + CustomerList[0].sl01195 + "{0}" + CustomerList[0].sl01196, Environment.NewLine));

            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            Assert.AreEqual(result.Customers[0].ERP_Key, "I" + COMPANY_CODE + CustomerList[0].sl01001);

            //Assert to check the Payment_Terms_Code__c Validation Rule
            Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, CustomerList[0].sl01024.Substring(0, 2));
        }

        /// <summary>
        /// Unit Test for GetCustomerByName Method 
        /// </summary>
        [TestMethod]
        public void GetCustomerByCountryCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForCustomerModels();

            mockRepository.Stub(x => x.GetCustomerByCountryCode(COMPANY_CODE, CUSTOMER_COUNTRYCODE))
                            .IgnoreArguments()
                            .Return(CustomerList);
            _customerManager = new CustomerManager(mockRepository);

            var result = _customerManager.GetCustomerByCountryCode(COMPANY_CODE, CUSTOMER_COUNTRYCODE);
            Assert.IsNotNull(result);

            //Assert to check the BillingStreet Validation Rule
            Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(CustomerList[0].sl01003 + "{0}" + CustomerList[0].sl01004 + "{0}" + CustomerList[0].sl01005 + "{0}" + CustomerList[0].sl01099 + "{0}" +
                                CustomerList[0].sl01194 + "{0}" + CustomerList[0].sl01195 + "{0}" + CustomerList[0].sl01196, Environment.NewLine));

            //Assert to check the length of currecyIsoCode Validation Rule
            Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            //Assert to check the ERP Key Validation Rule
            Assert.AreEqual(result.Customers[0].ERP_Key, "I" + COMPANY_CODE + CustomerList[0].sl01001);

            //Assert to check the Payment_Terms_Code__c Validation Rule
            Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, CustomerList[0].sl01024.Substring(0, 2));
        }


        /// <summary>
        /// Unit Test for GetCustomer with null request Object.
        /// </summary>
        [TestMethod]
        public void GetCustomersNegativeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetCustomers(string.Empty))
                            .IgnoreArguments()
                            .Return(CustomerList);

            _customerManager = new CustomerManager(mockRepository);

            //Assert for GetCustomers with null companyCode
            var resultByCompanyCode = _customerManager.GetCustomers(string.Empty);
            Assert.AreEqual(resultByCompanyCode.Customers.Count, 0);

            //Assert for GetCustomerById with null companyCode & customerCode
            var resultByCompanyCodeAndCustomerCode = _customerManager.GetCustomerById(string.Empty, string.Empty);
            Assert.IsNotNull(resultByCompanyCodeAndCustomerCode.ErrorInfo);

            //Assert for GetCustomerByName with null companyCode & customerName
            var resultByCompanyCodeAndCustomerName = _customerManager.GetCustomerByName(string.Empty, string.Empty);
            Assert.IsNotNull(resultByCompanyCodeAndCustomerName.ErrorInfo);

            //Assert for GetCustomerByALTERNATEName with null companyCode & customerName
            var resultByCompanyCodeAndAlternateName = _customerManager.GetCustomerByCustomerAlternateName(string.Empty, string.Empty);
            Assert.IsNotNull(resultByCompanyCodeAndAlternateName.ErrorInfo);

            //Assert for GetCustomerByALTERNATEName with null companyCode & customerName
            var resultByCompanyCodeAndCategory = _customerManager.GetCustomerByCategory(string.Empty, string.Empty);
            Assert.IsNotNull(resultByCompanyCodeAndCategory.ErrorInfo);

            //Assert for GetCustomerByALTERNATEName with null companyCode & customerName
            var resultByCompanyCodeAndcountry = _customerManager.GetCustomerByCountryCode(string.Empty, string.Empty);
            Assert.IsNotNull(resultByCompanyCodeAndcountry.ErrorInfo);

            //Assert for GetCustomerByALTERNATEName with null companyCode & customerName
            var resultByCompanyCodeAndemail = _customerManager.GetCustomerByEmailId(string.Empty, string.Empty);
            Assert.IsNotNull(resultByCompanyCodeAndemail.ErrorInfo);

            //Assert for GetCustomerByALTERNATEName with null companyCode & customerName
            var resultByCompanyCodeAndtelenum = _customerManager.GetCustomerByPhoneNumber(string.Empty, string.Empty);
            Assert.IsNotNull(resultByCompanyCodeAndtelenum.ErrorInfo);
        }

        /// <summary>
        /// Unit Test for GetCustomer with null request Object.
        /// </summary>
        [TestMethod]
        public void GetCustomersNullResultTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetCustomers(string.Empty))
                            .IgnoreArguments()
                            .Return(null);

            _customerManager = new CustomerManager(mockRepository);

            //Assert for GetCustomers with null companyCode
            var resultByCompanyCode = _customerManager.GetCustomers("EX");
            Assert.AreEqual(resultByCompanyCode.Customers.Count, 0);

            //Assert for GetCustomerById with null companyCode & customerCode
            var resultByCompanyCodeAndCustomerCode = _customerManager.GetCustomerById("EX", "EX");
            Assert.IsNotNull(resultByCompanyCodeAndCustomerCode.ErrorInfo);

            //Assert for GetCustomerByName with null companyCode & customerName
            var resultByCompanyCodeAndCustomerName = _customerManager.GetCustomerByName("EX", "EX");
            Assert.IsNotNull(resultByCompanyCodeAndCustomerName.ErrorInfo);

            mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetCustomerByPhoneNumber(string.Empty, ""))
                            .IgnoreArguments()
                            .Return(null);

            _customerManager = new CustomerManager(mockRepository);

            //Assert for GetCustomerById with null companyCode & customerCode
            var resultByCompanyCodeAndtelephone = _customerManager.GetCustomerByPhoneNumber("EX", "EX");
            Assert.IsNotNull(resultByCompanyCodeAndtelephone.ErrorInfo);

            mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetCustomerByEmailId(string.Empty, ""))
                            .IgnoreArguments()
                            .Return(null);

            _customerManager = new CustomerManager(mockRepository);

            //Assert for GetCustomerById with null companyCode & customerCode
            var resultByCompanyCodeAndemail = _customerManager.GetCustomerByEmailId("EX", "EX");
            Assert.IsNotNull(resultByCompanyCodeAndemail.ErrorInfo);

            mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetCustomerByCategory(string.Empty, ""))
                            .IgnoreArguments()
                            .Return(null);

            _customerManager = new CustomerManager(mockRepository);

            //Assert for GetCustomerById with null companyCode & customerCode
            var resultByCompanyCodeAndCategory = _customerManager.GetCustomerByCategory("EX", "EX");
            Assert.IsNotNull(resultByCompanyCodeAndCategory.ErrorInfo);

            mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetCustomerByAlternateName(string.Empty, ""))
                            .IgnoreArguments()
                            .Return(null);

            _customerManager = new CustomerManager(mockRepository);

            //Assert for GetCustomerById with null companyCode & customerCode
            var resultByCompanyCodeAndAname = _customerManager.GetCustomerByCustomerAlternateName("EX", "EX");
            Assert.IsNotNull(resultByCompanyCodeAndAname.ErrorInfo);

            mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetCustomerByCountryCode(string.Empty, ""))
                            .IgnoreArguments()
                            .Return(null);

            _customerManager = new CustomerManager(mockRepository);

            //Assert for GetCustomerById with null companyCode & customerCode
            var resultByCompanyCodeAndCountry = _customerManager.GetCustomerByCountryCode("EX", "EX");
            Assert.IsNotNull(resultByCompanyCodeAndCountry.ErrorInfo);
        }

        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockDataForCustomerModels()
        {
            #region SampleDataCustomerMaster
            _sl01 = new Sl01
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
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7",
                sl01060 = "1",
                sl01024 = "11"
            };

            CustomerList.Add(new Sl01()
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
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7",
                sl01060 = "0",
                sl01024 = "Daily"

                //String.Format(customerMaster.sl01003 + "{0}" + customerMaster.sl01004 + "{0}" + customerMaster.sl01005 + "{0}" + customerMaster.sl01099 + "{0}" +
                //                customerMaster.sl0194 + "{0}" + customerMaster.sl01195 + "{0}" + customerMaster.sl01196, Environment.NewLine),
            });
            CustomerList.Add(new Sl01()
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
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7",
                sl01060 = "1",
                sl01024 = "10"
            });
            CustomerList.Add(new Sl01()
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
                sl01194 = "Address Line 5",
                sl01195 = "Address Line 6",
                sl01196 = "Address Line 7",
                sl01060 = "",
                sl01024 = "Monthly"
            });
            #endregion
        }
        #endregion
    }
}
