using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseOrderBySupplier.Common;
using Microservices.Common.Interface;
using PurchaseOrderBySupplier.DataLayer;
using PurchaseOrderBySupplier.DataLayer.Entities.Datalake;
using Rhino.Mocks;

namespace PurchaseOrderBySupplier.UnitTest
{
    /// <summary>
    /// Summary description for DataLayerContextUnitTest
    /// </summary>
    [TestClass]
    public class DataLayerContextUnitTest
    {
        private ConfigReader _configReader;
        private IDatabase _mocksDatabaseEntities;
        private DataLayerContext _dataLayerContext;
        private string _companyCode = "j4";
        private readonly List<Pc12> _pc012 = new List<Pc12>();

        #region initializations

        [TestInitialize]
        public void Initialize()
        {
            _mocksDatabaseEntities = MockRepository.GenerateMock<IDatabase>();
            _configReader = new ConfigReader();
            _dataLayerContext = new DataLayerContext() { Database = _mocksDatabaseEntities };
            SetPurchaseOrdersBySupplier();
        }

        #endregion

        #region Unit Test Methods
        [TestMethod]
        public void GetPurchaseOrdersSupplierByCompanyCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Pc12>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_pc012);
            var result = _dataLayerContext.GetPurchaseOrdersByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrdersByCompanyCode(string.Empty);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrdersBySupplierInvoiceNumberTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string invoiceNo = "00494024";
            _mocksDatabaseEntities.Stub(x => x.Where<Pc12>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty))
                .IgnoreArguments()
                .Return(_pc012);
            var result = _dataLayerContext.GetPurchaseOrdersBySupplierInvoiceNumber(_companyCode, invoiceNo);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrdersBySupplierInvoiceNumber(string.Empty, invoiceNo);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrdersBySupplierCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string supplierCode = "J0017";
            _mocksDatabaseEntities.Stub(x => x.Where<Pc12>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty))
                .IgnoreArguments()
                .Return(_pc012);
            var result = _dataLayerContext.GetPurchaseOrdersBySupplierCode(_companyCode, supplierCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrdersBySupplierCode(string.Empty, supplierCode);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrdersByVATCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string vatCode = "0";
            _mocksDatabaseEntities.Stub(x => x.Where<Pc12>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty))
                .IgnoreArguments()
                .Return(_pc012);
            var result = _dataLayerContext.GetPurchaseOrdersByVATCode(_companyCode, vatCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrdersByVATCode(string.Empty, vatCode);
            Assert.IsNull(result);
        }

        #endregion


        #region Mock Data Methods

        public void SetPurchaseOrdersBySupplier()
        {
            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494024",
                //Supplier Code            
                Pc12007 = "J0017",
                //Purchase Order Number               
                Pc12001 = "*********3",
                //Amount
                Pc12010 = "3201.66",
                //Sales Tax Amount
                Pc12012 = "3201.66",
                //VAT Code
                Pc12013 = "0"
            });
            _pc012.Add(new Pc12()
            {
                //Invoice Number
                Pc12008 = "00494025",
                //Supplier Code            
                Pc12007 = "J0018",
                //Purchase Order Number               
                Pc12001 = "*********4",
                //Amount
                Pc12010 = "3202.66",
                //Sales Tax Amount
                Pc12012 = "3202.66",
                //VAT Code
                Pc12013 = "0"
            });

        }

        #endregion

    }
}
