using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseLedger.Common;
using Microservices.Common.Interface;
using PurchaseLedger.DataLayer;
using PurchaseLedger.DataLayer.Entities.Datalake;
using Rhino.Mocks;

namespace PurchaseLedger.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {
        private ConfigReader _configReader;
        private IDatabase _mocksDatabaseEntities;
        private DataLayerContext _dataLayerContext;
        private string _companyCode = "j4";
        private readonly List<Pl03> _pl03S = new List<Pl03>();

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _mocksDatabaseEntities = MockRepository.GenerateMock<IDatabase>();
            _configReader = new ConfigReader();
            _dataLayerContext = new DataLayerContext() { Database = _mocksDatabaseEntities };
            SetPurchaseLedgerBySupplier();
        }
        #endregion

        #region Unit Test Methods
        [TestMethod]
        public void GetPurchaseLedgerByCompanyCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.GetJoinData<Pl03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey],String.Empty))
                .IgnoreArguments()
                .Return(_pl03S);
            var result = _dataLayerContext.GetPurchaseLedgerByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseLedgerByCompanyCode(string.Empty);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetPurchaseLedgerByInvoiceNoTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Pl03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey],string.Empty,string.Empty))
                .IgnoreArguments()
                .Return(_pl03S);
            var result = _dataLayerContext.GetPurchaseLedgerByInvoiceNo(_companyCode,string.Empty);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseLedgerByInvoiceNo(string.Empty,string.Empty);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetPurchaseLedgerByOrderNoTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Pl03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty, string.Empty))
                .IgnoreArguments()
                .Return(_pl03S);
            var result = _dataLayerContext.GetPurchaseLedgerByOrderNo(_companyCode, string.Empty);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseLedgerByOrderNo(string.Empty, string.Empty);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetPurchaseLedgerBySupplierCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Pl03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty, string.Empty))
                .IgnoreArguments()
                .Return(_pl03S);
            var result = _dataLayerContext.GetPurchaseLedgerBySupplierCode(_companyCode, string.Empty);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseLedgerBySupplierCode(string.Empty, string.Empty);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetPurchaseLedgerBySupplierNameTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Pl03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty, string.Empty))
                .IgnoreArguments()
                .Return(_pl03S);
            var result = _dataLayerContext.GetPurchaseLedgerBySupplierName(_companyCode, string.Empty);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseLedgerBySupplierName(string.Empty, string.Empty);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseLedgerByDueDateTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Pl03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty, string.Empty))
                .IgnoreArguments()
                .Return(_pl03S);
            var result = _dataLayerContext.GetPurchaseLedgerByDueDateRange(_companyCode, string.Empty,string.Empty);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseLedgerByDueDateRange(string.Empty, string.Empty, string.Empty);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetPurchaseLedgerByInvoiceDateTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Pl03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty, string.Empty))
                .IgnoreArguments()
                .Return(_pl03S);
            var result = _dataLayerContext.GetPurchaseLedgerByInvoiceDateRange(_companyCode, string.Empty, string.Empty);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseLedgerByInvoiceDateRange(string.Empty, string.Empty, string.Empty);
            Assert.IsNull(result);
        }

        #endregion

        #region Mock Data Methods

        private void SetPurchaseLedgerBySupplier()
        {
            _pl03S.Add(
                new Pl03()
                {
                    Pl01001 = "1001",
                    Pl01002 = "AL MOAYYED AIRCONDITIONING",
                    Pl03002 = "1028288",
                    Pl03004 = "10/6/2015",
                    Pl03006 = "11/30/2015",
                    Pl03008 = "0",
                    Pl03010 = string.Empty,
                    Pl03012 = string.Empty,
                    Pl03014 = "400",
                    Pl03016 = "0",
                    Pl03025 = "5/16/2016",
                    Pl03027 = "400",
                    Pl03033 = "4930000042",
                    Pl03077 = string.Empty
                });
            _pl03S.Add(
                new Pl03()
                {
                    Pl01001 = "1001",
                    Pl01002 = "AL MOAYYED AIRCONDITIONING",
                    Pl03002 = "1028289",
                    Pl03004 = "10/6/2015",
                    Pl03006 = "11/30/2015",
                    Pl03008 = "0",
                    Pl03010 = string.Empty,
                    Pl03012 = string.Empty,
                    Pl03014 = "400",
                    Pl03016 = "0",
                    Pl03025 = "5/16/2016",
                    Pl03027 = "400",
                    Pl03033 = "4930000042",
                    Pl03077 = string.Empty
                });
            _pl03S.Add(
                new Pl03()
                {
                    Pl01001 = "1018",
                    Pl01002 = "Geo Chem Middle East",
                    Pl03002 = "DXL/027763",
                    Pl03004 = "10/6/2015",
                    Pl03006 = "11/30/2015",
                    Pl03008 = "0",
                    Pl03010 = string.Empty,
                    Pl03012 = string.Empty,
                    Pl03014 = "400",
                    Pl03016 = "0",
                    Pl03025 = "5/16/2017",
                    Pl03027 = "400",
                    Pl03033 = "4930000022",
                    Pl03077 = string.Empty
                });
        }
        #endregion

    }
}
