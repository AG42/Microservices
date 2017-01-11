using CreditStatus.Common;
using CreditStatus.DataLayer;
using CreditStatus.DataLayer.Entities.Datalake;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;

namespace CreditStatus.UnitTest
{
    [TestClass]
   public class DataLayerContextUnitTest
    {
        private ConfigReader _configReader;
        private IDatabase _mocksDatabaseEntities;
        private DataLayerContext _dataLayerContext;
        private List<Sl01> _sl01EntitiesList;
        private string _companyCode = "na";
        private string _customerCode = "4629";
        private string _customerName = "ARGON";
       // private bool _ledgerFlag = true;

        public Sl01 CreditStatus = new Sl01();
      //  public List<Sl01> CreditStatusList;

        [TestInitialize]
        public void Initialize()
        {
            _mocksDatabaseEntities = MockRepository.GenerateMock<IDatabase>();
            _configReader = new ConfigReader();
            _dataLayerContext = new DataLayerContext() { Database = _mocksDatabaseEntities };
            _sl01EntitiesList = new List<Sl01>();
            
        }


        #region Unit Test Methods
        [TestMethod]
        public void GetCreditStatusByCompanyCodeTest()
        {
            SetMockDataForCreditStatusModelList();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Sl01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_sl01EntitiesList);
            var result = _dataLayerContext.GetCreditStatusByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetCreditStatusByCompanyCode(string.Empty);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetCreditStatusByCustomerCodeTest()
        {
            SetMockDataForCreditStatusModelList();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Where<Sl01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey],""))
                .IgnoreArguments()
                .Return(_sl01EntitiesList);
            var result = _dataLayerContext.GetCreditStatusByCustomerCode(_companyCode, _customerCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetCreditStatusByCustomerCode(string.Empty, _customerCode);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetCreditStatusByCustomerNameTest()
        {
            SetMockDataForCreditStatusModelList();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Where<Sl01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey],""))
                .IgnoreArguments()
                .Return(_sl01EntitiesList);
            var result = _dataLayerContext.GetCreditStatusByCustomerName(_companyCode,_customerName);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetCreditStatusByCustomerName(string.Empty, _customerName);
            Assert.IsNull(result);
        }
        #endregion


        #region MockData Methods

        public void SetMockDataForCreditStatusModelList()
        {
            #region SampleCreditStatusModel
            CreditStatus = new Sl01()
            {
                //Customer Code
                Sl01001 = "4629",
                //Customer Name
                Sl01002 = "ARGON PROPERTIES WLL",
                //Unpaid Invoices
                Sl01038 = "500",
                //Ordered Not Shipped
                Sl01057 = "1000",
                //Shipped Not Invoiced
                Sl01058 = "4500",
                //Credit Limit
                Sl01037 = "10010"
            };


            #endregion

            #region SampleCreditStatusModelList
            _sl01EntitiesList.Add(new Sl01()
            {
                //Customer Code
                Sl01001 = "4629",
                //Customer Name
                Sl01002 = "ARGON PROPERTIES WLL",
                //Unpaid Invoices
                Sl01038 = "500",
                //Ordered Not Shipped
                Sl01057 = "1000",
                //Shipped Not Invoiced
                Sl01058 = "4500",
                //Credit Limit
                Sl01037 = "10010"
            });
            _sl01EntitiesList.Add(new Sl01()
            {
                //Customer Code
                Sl01001 = "1001",
                //Customer Name
                Sl01002 = "LSS Technologies",
                //Unpaid Invoices
                Sl01038 = "3000",
                //Ordered Not Shipped
                Sl01057 = "4000",
                //Shipped Not Invoiced
                Sl01058 = "1000",
                //Credit Limit
                Sl01037 = "9000"
            });

            #endregion
        }
    }
    #endregion

}
