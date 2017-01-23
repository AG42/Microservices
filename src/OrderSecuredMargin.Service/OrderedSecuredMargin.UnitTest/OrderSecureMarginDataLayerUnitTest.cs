using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedSecuredMargin.Common;
using OrderedSecuredMargin.DataAccessLayer;
using OrderedSecuredMargin.DataAccessLayer.Entities.Datalake;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSecuredMargin.UnitTest
{
    [TestClass]
    public class OrderSecureMarginDataLayerUnitTest
    {
        #region  Declartions

        private ConfigReader _configReader;
        private IDatabase _mocksDatabaseEntities;
        private DataLayerContext _dataLayerContext;
        private List<Or03> _or03EntitiesList;
        private string _companyCode = "na";
        private string _orderNo = "4629";
        private const string OrderNo = "or03001";
        private const string UnitPrice = "or03008";
        private const string UnitCostPric = "or03009";
        private const string UnitCode = "or03010";
        private const string ORQtyOrdered = "or03011";


        public Or03 OrderMargin = new Or03();

        #endregion

        #region initializations
        [TestInitialize]
        public void Initialize()
        {
            _mocksDatabaseEntities = MockRepository.GenerateMock<IDatabase>();
            _configReader = new ConfigReader();
            _dataLayerContext = new DataLayerContext() { Database = _mocksDatabaseEntities };
            _or03EntitiesList = new List<Or03>();

        }

        #endregion


        #region Unit Test Methods
        [TestMethod]
        public void GetOrderSecureMarginByCompanyCodeTest()
        {
            SetMockOrderSecureMarginModelList();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Or03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_or03EntitiesList);
            var result = _dataLayerContext.GetOrderSecuredMarginByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredMarginByCompanyCode(string.Empty);
            Assert.IsNull(result);

        }


        [TestMethod]
        public void GetOrderSecureMarginByOrderNoTest()
        {
            SetMockOrderSecureMarginModelList();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Where<Or03>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey], ""))
                .IgnoreArguments()
                .Return(_or03EntitiesList);
            var result = _dataLayerContext.GetOrderSecuredMarginByOrderNo(_companyCode, _orderNo);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredMarginByOrderNo(string.Empty, _orderNo);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetOrderSecureMarginCostTest()
        {
            SetMockOrderSecureMarginModelList();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);

            string tableNameData = tableName[Constants.TableNameKey] + $" Group BY {OrderNo} HAVING (((SUM(CASE WHEN {UnitPrice}=0 THEN {UnitCostPric} ELSE {UnitPrice} END * {ORQtyOrdered}) - SUM({UnitCostPric} * {ORQtyOrdered})) / SUM(CASE WHEN {UnitPrice}=0 THEN {int.MaxValue} ELSE {UnitPrice} END * CASE WHEN {ORQtyOrdered}=0 THEN {int.MaxValue} ELSE {ORQtyOrdered} END)) * 100) >=   1  AND(((SUM({UnitPrice} * {ORQtyOrdered}) - SUM({UnitCostPric} * {ORQtyOrdered})) / SUM( CASE WHEN {UnitPrice} = 0 THEN {int.MaxValue} ELSE {UnitPrice} END * CASE WHEN {ORQtyOrdered} = 0 THEN {int.MaxValue} ELSE {ORQtyOrdered} END )) * 100) <=  100 ";
            string coulmnNname = $" {OrderNo}, ((SUM({UnitPrice} *  {ORQtyOrdered}) - SUM({UnitCostPric} * {ORQtyOrdered})) / SUM({UnitPrice} * {ORQtyOrdered})) * 100.0  as MarginPercentage";
            _mocksDatabaseEntities.Stub(x => x.Get<Or03>(tableNameData, coulmnNname))
                .IgnoreArguments()
                .Return(_or03EntitiesList);
            var result = _dataLayerContext.GetOrderSecuredMarginByCost(_companyCode, 1, 90);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetOrderSecuredMarginByCost(string.Empty, 1, 90);
            Assert.IsNull(result);
        }
        #endregion

        #region MockData Methods

        public void SetMockOrderSecureMarginModelList()
        {
            #region SampleOrderSecureMarginModel
            OrderMargin = new Or03()
            {
                //OrderNO
                Or03001 = "4629",
                //unitprice
                Or03008 = "1725",
                //unitpricecost
                Or03009 = "23915",
                //unitcode
                Or03010 = "1000",
                //orqtyordered
                Or03011 = "46598",
                //MarginPercentage
                // MarginPercentage = 80
            };


            #endregion

            _or03EntitiesList = new List<Or03>();
            _or03EntitiesList.Add(OrderMargin);

            //#region SampleOrderSecureMarginModel
            //OrderMargin = new Or03()
            //{
            //    //OrderNO
            //    Or03001 = "4621",
            //    //unitprice
            //    Or03008 = "17250",
            //    //unitpricecost
            //    Or03009 = "33915",
            //    //unitcode
            //    Or03010 = "2000",
            //    //orqtyordered
            //    Or03011 = "56598",

            //};
            //#endregion

            //#region SampleOrderSecureMarginModel
            //OrderMargin = new Or03()
            //{
            //    //OrderNO
            //    Or03001 = "4627",
            //    //unitprice
            //    Or03008 = "11725",
            //    //unitpricecost
            //    Or03009 = "43915",
            //    //unitcode
            //    Or03010 = "1000",
            //    //orqtyordered
            //    Or03011 = "26598",

            //};

            //#endregion
        }

        #endregion


    }

}

