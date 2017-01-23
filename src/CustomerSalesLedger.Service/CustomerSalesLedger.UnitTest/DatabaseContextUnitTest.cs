using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CustomerSalesLedger.DataLayer;
using CustomerSalesLedger.DataLayer.Entities.Datalake;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CustomerSalesLedger.UnitTest
{
    [TestClass]
    public class DatabaseContextUnitTest
    {
        #region Declarations

        private const string CompanyCode = "na";
        private const string CustomerCode = "C002";
        private const string CustomerName = "Customer";
        private const string CustomerAlternateName = "ARGON PROPERTIES WLL";
        private const string CustomerEmailId = "simonkynostan@meritas.bh";
        private const string CustomerPhoneNumber = "00973 1 758 7271";
        private const string Category = "DSL";

        public List<Sl01> CustomerList = new List<Sl01>();
        IDatabase _mocks;
        DatabaseContext _databaseContext;
        #endregion

        #region UnitTest
        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            _mocks = MockRepository.GenerateMock<IDatabase>();
            _databaseContext = new DatabaseContext() { Database = _mocks };
        }


        [TestMethod]
        public void GetCustomerByIdTest()
        {
            SetMockData();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "customerCode"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerById(CompanyCode, CustomerCode);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByNameTest()
        {
            SetMockData();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "customerCode"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByName(CompanyCode, CustomerName);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetCustomerByAlternateNameTest()
        {
            SetMockData();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                .IgnoreArguments()
                .Return(CustomerList);

            var result = _databaseContext.GetCustomerByAlternateName(CompanyCode, CustomerAlternateName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCustomerByEmailIdTest()
        {
            SetMockData();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByEmailId(CompanyCode, CustomerEmailId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetCustomerByPhoneNumberTest()
        {
            SetMockData();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByPhoneNumber(CompanyCode, CustomerPhoneNumber);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetCustomerByCategoryTest()
        {
            SetMockData();

            _mocks.Stub(x => x.Where<Sl01>("tableName", "columns", "condition"))
                 .IgnoreArguments()
                 .Return(CustomerList);

            var result = _databaseContext.GetCustomerByCategory(CompanyCode, Category);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of customer for unit test
        /// </summary>
        public void SetMockData()
        {
            #region SampleDataCustomerMaster
            CustomerList.Add(new Sl01()
            {
                Sl01001 = "4629",
                Sl01002 = "ARGON PROPERTIES WLL",
                Sl01010 = "DSL",
                Sl01011 = "+973 39034203",
                Sl01018 = "0",
                Sl01021 = "",
                Sl01024 = "2",
                Sl01038 = "0",
                Sl01045 = "0",
                Sl01048 = "0",
                Sl01049 = "0",
                Sl01050 = "",
                Sl01051 = "",
                Sl01054 = "ARGON PROPERTIES WLL",
                Sl01068 = "",
                Sl01193 = "",
                Sl01112 = "0"
            });
            CustomerList.Add(new Sl01()
            {
                Sl01001 = "1001",
                Sl01002 = "LSS Technologies",
                Sl01010 = "",
                Sl01011 = "00973 1 758 7271",
                Sl01018 = "0",
                Sl01021 = "",
                Sl01024 = "1",
                Sl01038 = "0",
                Sl01045 = "0",
                Sl01048 = "442",
                Sl01049 = "1",
                Sl01050 = "",
                Sl01051 = "",
                Sl01054 = "LSS TECHNOLOGIES",
                Sl01068 = "",
                Sl01193 = "",
                Sl01112 = "0"
            });
            CustomerList.Add(new Sl01()
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
