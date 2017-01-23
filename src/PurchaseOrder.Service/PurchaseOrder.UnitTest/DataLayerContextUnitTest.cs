using System.Collections.Generic;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseOrder.Common;
using PurchaseOrder.DataLayer;
using PurchaseOrder.DataLayer.Entities.Datalake;
using Rhino.Mocks;

namespace PurchaseOrder.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {
        private ConfigReader _configReader;
        private IDatabase _mocksDatabaseEntities;
        private DataLayerContext _dataLayerContext;
        private string _companyCode = "na";
        readonly List<Pc01> _pc01S = new List<Pc01>();
        readonly List<Pc04> _pc04S = new List<Pc04>();
        #region initializations
       
        [TestInitialize]
        public void Initialize()
        {
            _mocksDatabaseEntities = MockRepository.GenerateMock<IDatabase>();
            _configReader = new ConfigReader();
            _dataLayerContext = new DataLayerContext() { Database = _mocksDatabaseEntities };
            SetPurchaseOrders();
        }

        #endregion

        #region Unit Test Methods
        [TestMethod]
        public void GetPurchaseOrdersByCompanyCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Pc01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_pc01S);
            var result = _dataLayerContext.GetPurchaseOrderByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrderByCompanyCode(string.Empty);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrdersByOrderTypeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string orderType = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty))
                .IgnoreArguments()
                .Return(_pc01S);
            var result = _dataLayerContext.GetPurchaseOrderByOrderType(_companyCode, orderType);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrderByOrderType(string.Empty, orderType);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrderByPurchaseOrderNumberTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string orderNo = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty))
                .IgnoreArguments()
                .Return(_pc01S);
            var result = _dataLayerContext.GetPurchaseOrderByPurchaseOrderNumber(_companyCode, orderNo);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrderByPurchaseOrderNumber(string.Empty, orderNo);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrderByProjectNumberTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string projectNo = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty))
                .IgnoreArguments()
                .Return(_pc01S);
            var result = _dataLayerContext.GetPurchaseOrderByProjectNumber(_companyCode, projectNo);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrderByProjectNumber(string.Empty, projectNo);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrderByOrderDateRangeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string startDate = string.Empty;
            string endDate = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty))
                .IgnoreArguments()
                .Return(_pc01S);
            var result = _dataLayerContext.GetPurchaseOrderByOrderDateRange(_companyCode, startDate,endDate);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrderByOrderDateRange(string.Empty, startDate,endDate);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrderByDeliveryDateRangeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string startDate = string.Empty;
            string endDate = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty))
                .IgnoreArguments()
                .Return(_pc01S);
            var result = _dataLayerContext.GetPurchaseOrderByDeliveryDateRange(_companyCode, startDate, endDate);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrderByDeliveryDateRange(string.Empty, startDate, endDate);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPurchaseOrderByCustomerNameTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string customerName = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Pc04>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty,string.Empty))
                .IgnoreArguments()
                .Return(_pc04S);
            var result = _dataLayerContext.GetPurchaseOrderByCustomerName(_companyCode, customerName);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetPurchaseOrderByCustomerName(string.Empty, customerName);
            Assert.IsNull(result);

        }

        #endregion

        #region Mock Data Methods

        public void SetPurchaseOrders()
        {
            _pc01S.Add(new Pc01()
            {
                Pc01001 = "3755100132",
                Pc01002 = "Normal Order",
                Pc01003 = "01015060",
                Pc01004 = "+",
                Pc01005 = "P:P2N1010020, S:100 A:105,",
                Pc01006 = "L:000020-000000",
                Pc01007 = "0",
                Pc01008 = "1",
                Pc01009 = "2",
                Pc01011 = "0",
                Pc01012 = "3",
                Pc01013 = "4",
                Pc01014 = "1",
                Pc01015 = "2012 - 05-03 00:00:00.0",
                Pc01016 = "2024-12-30 00:00:00.0",
                Pc01019 = "0.00",
                Pc01020 = "409109.00",
                Pc01022 = "0",
                Pc01046 = "",
                Pc01056 = "P2N1010020",
                Pc01058 = "SP1002",
                Pc01062 = "",
                Pc01068 = "1"
            });
            _pc01S.Add(new Pc01()
            {
                Pc01001 = "3755100133",
                Pc01002 = "1",
                Pc01003 = "01015060",
                Pc01004 = "+",
                Pc01005 = "P:P2N1010020, S:100 A:105,",
                Pc01006 = "L:000020-000000",
                Pc01007 = "0",
                Pc01008 = "1",
                Pc01009 = "2",
                Pc01011 = "0",
                Pc01012 = "3",
                Pc01013 = "4",
                Pc01014 = "1",
                Pc01015 = "2012 - 05-03 00:00:00.0",
                Pc01016 = "2024-12-30 00:00:00.0",
                Pc01019 = "0.00",
                Pc01020 = "409109.00",
                Pc01022 = "0",
                Pc01046 = "",
                Pc01056 = "P2N1010020",
                Pc01058 = "SP1002",
                Pc01062 = "",
                Pc01068 = "1"
            });

            _pc04S.Add(new Pc04()
            {
                Pc04002 = "JCI",
                Pc04008 = "98901230918",
                Pc01001 = "3755100133",
                Pc01002 = "1",
                Pc01003 = "01015060",
                Pc01004 = "+",
                Pc01005 = "P:P2N1010020, S:100 A:105,",
                Pc01006 = "L:000020-000000",
                Pc01007 = "0",
                Pc01008 = "1",
                Pc01009 = "2",
                Pc01011 = "0",
                Pc01012 = "3",
                Pc01013 = "4",
                Pc01014 = "1",
                Pc01015 = "2012 - 05-03 00:00:00.0",
                Pc01016 = "2024-12-30 00:00:00.0",
                Pc01019 = "0.00",
                Pc01020 = "409109.00",
                Pc01022 = "0",
                Pc01046 = "",
                Pc01056 = "P2N1010020",
                Pc01058 = "SP1002",
                Pc01062 = "",
                Pc01068 = "1"
            });

            _pc04S.Add(new Pc04()
            {
                Pc04002 = "JCI",
                Pc04008 = "absbcjb",
                Pc01001 = "3755100132",
                Pc01002 = "1",
                Pc01003 = "01015060",
                Pc01004 = "+",
                Pc01005 = "P:P2N1010020, S:100 A:105,",
                Pc01006 = "L:000020-00000",
                Pc01007 = "0",
                Pc01008 = "1",
                Pc01009 = "2",
                Pc01011 = "0",
                Pc01012 = "3",
                Pc01013 = "4",
                Pc01014 = "1",
                Pc01015 = "2012 - 05-03 00:00:00.0",
                Pc01016 = "2024-12-30 00:00:00.0",
                Pc01019 = "0.00",
                Pc01020 = "409109.00",
                Pc01022 = "0",
                Pc01046 = "",
                Pc01056 = "P2N1010020",
                Pc01058 = "SP1002",
                Pc01062 = "",
                Pc01068 = "1"
            });
        }

        #endregion

        }
}
