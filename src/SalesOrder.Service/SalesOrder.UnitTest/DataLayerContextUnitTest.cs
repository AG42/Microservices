using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesOrder.Common;
using SalesOrder.DataLayer;
using SalesOrder.DataLayer.Entities.Datalake;
using System.Collections.Generic;
using Microservices.Common.Interface;
using Rhino.Mocks;

namespace SalesOrder.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {
        #region Declarations
        private ConfigReader _configReader;
        private IDatabase _mocksDatabaseEntities;
        private DataLayerContext _dataLayerContext;
        private string _companyCode = "m1";
        readonly List<Or01> _oR01S = new List<Or01>();
        #endregion

        #region Initializations

        [TestInitialize]
        public void Initialize()
        {
            _mocksDatabaseEntities = MockRepository.GenerateMock<IDatabase>();
            _configReader = new ConfigReader();
            _dataLayerContext = new DataLayerContext() { Database = _mocksDatabaseEntities };
            SetSalesOrders();
        }

        #endregion

        #region Unit Test Methods
        [TestMethod]
        public void GetSalesOrderByCompanyCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.GetJoinData<Or01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], ""))
                .IgnoreArguments()
                .Return(_oR01S);
            var result = _dataLayerContext.GetSalesOrderByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetSalesOrderByCompanyCode(string.Empty);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetSalesOrderBySalesOrderNumberTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string orderNo = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Or01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty,""))
                .IgnoreArguments()
                .Return(_oR01S);
            var result = _dataLayerContext.GetSalesOrderBySalesOrderNumber(_companyCode, orderNo);
            Assert.IsNotNull(result);
            result = _dataLayerContext.GetSalesOrderBySalesOrderNumber(string.Empty, orderNo);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByOrderTypeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string orderType = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Or01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty,""))
                .IgnoreArguments()
                .Return(_oR01S);
            var result = _dataLayerContext.GetSalesOrderByOrderType(_companyCode, orderType);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetSalesOrderByOrderType(string.Empty, orderType);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByCustomerInvoiceCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string invoiceCode = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Or01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty,""))
                .IgnoreArguments()
                .Return(_oR01S);
            var result = _dataLayerContext.GetSalesOrderByCustomerInvoiceCode(_companyCode, invoiceCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetSalesOrderByCustomerInvoiceCode(string.Empty, invoiceCode);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByFlagPickListTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string flagPickList = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Or01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty, string.Empty))
                .IgnoreArguments()
                .Return(_oR01S);
            var result = _dataLayerContext.GetSalesOrderByFlagPickList(_companyCode, flagPickList);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetSalesOrderByFlagPickList(string.Empty, flagPickList);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByOrderDateRangeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string minOrderDate = string.Empty;
            string maxOrderDate = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Or01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty, string.Empty))
                .IgnoreArguments()
                .Return(_oR01S);
            var result = _dataLayerContext.GetSalesOrderByOrderDateRange(_companyCode, minOrderDate, maxOrderDate);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetSalesOrderByOrderDateRange(string.Empty, minOrderDate, maxOrderDate);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetSalesOrderByDeliveryDateRangeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            string minDeliveryDate = string.Empty;
            string maxDeliveryDate = string.Empty;
            _mocksDatabaseEntities.Stub(x => x.WhereJoin<Or01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], string.Empty, string.Empty))
                .IgnoreArguments()
                .Return(_oR01S);
            var result = _dataLayerContext.GetSalesOrderByDeliveryDateRange(_companyCode, minDeliveryDate, maxDeliveryDate);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetSalesOrderByDeliveryDateRange(string.Empty, minDeliveryDate, maxDeliveryDate);
            Assert.IsNull(result);
        }

        #endregion

        #region Mock Data Methods

        public void SetSalesOrders()
        {
            _oR01S.Add(new Or01 {
                Or01001 = "3521001512",
                Or01002 = "Normal Order",
                Or01003 = "RK042",
                Or01008 = "3",
                Or01015 = "2012-03-09 00:00:00.0",
                Or01016 = "2012-03-16 00:00:00.0",
                Or03002 = "20",
                Or03006 = "DIGITAL THERMOSTAT C/W",
                Or03011 = "5",
                Or03012 = "0",
                Or03021 = "1",
            });
            _oR01S.Add(new Or01
            {
                Or01001 = "3526001050",
                Or01002 = "Back Order",
                Or01003 = "RD032",
                Or01008 = "3",
                Or01015 = "2013-02-26 00:00:00.0",
                Or01016 = "2013-06-15 00:00:00.0",
                Or03002 = "30",
                Or03006 = "TIME-BASED COMMISSIONING",
                Or03011 = "1",
                Or03012 = "0",
                Or03021 = "1",
            });

        }
        #endregion

    }
}
