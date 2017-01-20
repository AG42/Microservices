using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OrderSecuredCost.DataLayer;
using OrderSecuredCost.Common;
using Microservices.Common.Interface;
using Rhino.Mocks;
using OrderSecuredCost.DataLayer.Entities.Datalake;

namespace OrderSecuredCost.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {
        #region Properties
        private IDatabase _database { get; set; }
        private DataLayerContext _dataLayerContext;
        private ConfigReader _configReader;

        internal Pc01 _orderSecuredCost;
        internal List<Pc01> _pc01Entities;
        private string _companyCode = "na";
        private string _startdate = "2014-01-01";
        private string _enddate = "2015-12-31";
        private string _orderCost = "39482.23";
        private string _orderType = "1";
        private string _purchaseOrderNo = "439480984";
        private string _userId = "CKHOURM";

        #endregion

        [TestInitialize]
        public void Initialize()
        {
            _configReader = new ConfigReader();
            _database = MockRepository.GenerateMock<IDatabase>();
            _dataLayerContext = new DataLayerContext() { Database = _database };
            
            // initialise mock data
            SetMockDataForOrderSecuredCostModelList();
        }


        #region unit test method

        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by company id
        /// </summary>
        public void GetOrderSecuredCostByCompanyCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _database.Stub(x => x.Get<Pc01>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]));

            var result = _dataLayerContext.GetOrderSecuredCostByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredCostByCompanyCode(string.Empty);
            Assert.IsNull(result);
        }


        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by 
        /// delivery date range
        /// </summary>
        public void GetOrderSecuredCostByDeliveryDateRangeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _database.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(_pc01Entities);

            var result = _dataLayerContext.GetOrderSecuredCostByDeliveryDateRange(_companyCode, _startdate, _enddate);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredCostByDeliveryDateRange("", _startdate, _enddate);
            Assert.IsNull(result);
        }


        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by order cost range
        /// </summary>
        public void GetOrderSecuredCostByOrderCostRangeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _database.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(_pc01Entities);

            var result = _dataLayerContext.GetOrderSecuredCostByOrderCostRange(_companyCode, _orderCost, _orderCost);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredCostByOrderCostRange("", _orderCost, _orderCost);
            Assert.IsNull(result);
        }


        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by order date range
        /// </summary>
        public void GetOrderSecuredCostByOrderDateRangeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _database.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(_pc01Entities);

            var result = _dataLayerContext.GetOrderSecuredCostByOrderDateRange(_companyCode, _startdate, _enddate);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredCostByOrderDateRange("", _startdate, _enddate);
            Assert.IsNull(result);
        }


        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by order type
        /// </summary>
        public void GetOrderSecuredCostByOrderTypeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _database.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(_pc01Entities);

            var result = _dataLayerContext.GetOrderSecuredCostByOrderType(_companyCode, _orderType);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredCostByOrderType("", _orderType);
            Assert.IsNull(result);
        }


        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by purchase order number
        /// </summary>
        public void GetOrderSecuredCostByPurchaseOrderNumberTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _database.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(_pc01Entities);

            var result = _dataLayerContext.GetOrderSecuredCostByPurchaseOrderNumber(_companyCode, _purchaseOrderNo);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredCostByPurchaseOrderNumber("", _purchaseOrderNo);
            Assert.IsNull(result);
        }


        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by user id
        /// </summary>
        public void GetOrderSecuredCostByUserIDTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _database.Stub(x => x.Where<Pc01>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(_pc01Entities);

            var result = _dataLayerContext.GetOrderSecuredCostByUserID(_companyCode, _userId);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredCostByUserID("", _userId);
            Assert.IsNull(result);
        }

        #endregion


        #region Mock Method
        /// <summary>
        /// initialise entities with mock data
        /// </summary>
        public void SetMockDataForOrderSecuredCostModelList()
        {
            // single entity of Order Secured Cost entity
            _orderSecuredCost = new Pc01()
            {
                Pc01001 = "4930000007",     //PurchOrderNo
                Pc01002 = "2",              //OrderType
                Pc01015 = "07-07-2014",     //OrderDate
                Pc01016 = "11-18-2015",     //DelDate
                Pc01019 = "0",              //OrderDiscount
                Pc01020 = "24508.86",       //OrderValue
                Pc01060 = "",               //ExtRemark1
              //  Pc01066 = "3409429",        //InvoiceNumber
                Pc01073 = "CKHOURM",        //UserID
                Pc01088 = "0",              //FreightAmount
                Pc01089 = "0",              //PackingAmount
                Pc01090 = "0",              //InsuranceAmount
                Pc01091 = "0"               //InvoicingFee
            };

            // list of order secured cost Pc01 entities
            _pc01Entities = new List<Pc01>()
            {
                new Pc01()
                {
                    Pc01001 = "4930000007",     //PurchOrderNo
                    Pc01002 = "2",              //OrderType
                    Pc01015 = "07-07-2014",     //OrderDate
                    Pc01016 = "11-18-2015",     //DelDate
                    Pc01019 = "0",              //OrderDiscount
                    Pc01020 = "24508.86",       //OrderValue
                    Pc01060 = "",               //ExtRemark1
                  //  Pc01066 = "3409429",        //InvoiceNumber
                    Pc01073 = "CKHOURM",        //UserID
                    Pc01088 = "0",              //FreightAmount
                    Pc01089 = "0",              //PackingAmount
                    Pc01090 = "0",              //InsuranceAmount
                    Pc01091 = "0"               //InvoicingFee
                },

                new Pc01()
                {
                    Pc01001 = "4930000009",     //PurchOrderNo
                    Pc01002 = "1",              //OrderType
                    Pc01015 = "07-10-2014",     //OrderDate
                    Pc01016 = "08-13-2015",     //DelDate
                    Pc01019 = "0",              //OrderDiscount
                    Pc01020 = "28716.36",       //OrderValue
                    Pc01060 = "",               //ExtRemark1
                  //  Pc01066 = "3409678",        //InvoiceNumber
                    Pc01073 = "CKHOURM",        //UserID
                    Pc01088 = "0",              //FreightAmount
                    Pc01089 = "0",              //PackingAmount
                    Pc01090 = "0",              //InsuranceAmount
                    Pc01091 = "0"               //InvoicingFee
                }
            };
        }
        #endregion
    }
}
