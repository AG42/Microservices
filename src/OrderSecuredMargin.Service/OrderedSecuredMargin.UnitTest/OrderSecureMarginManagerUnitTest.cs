using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedSecuredMargin.BusinessLayer;
using OrderedSecuredMargin.Common.Enum;
using OrderedSecuredMargin.DataAccessLayer;
using OrderedSecuredMargin.DataAccessLayer.Entities.Datalake;
using OrderedSecuredMargin.DataAccessLayer.Interface;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSecuredMargin.UnitTest
{

    [TestClass]
    public class OrderSecureMarginManagerUnitTest
    {
        #region Declarations
        private IDataLayerContext _iDataLayer;
        private OrderedSecuredMarginManager _orderedSecuredMarginManager;
        private string _companyCode = "na";
        private string _orderNo = "4629";
        public IEnumerable<Or03> orderMargin = new List<Or03>();
        public List<Or03> OrderMarginList = new List<Or03>();

        #endregion

        #region Initializes
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _iDataLayer = new DataLayerContext();
            _orderedSecuredMarginManager = new OrderedSecuredMarginManager(_iDataLayer);
        }
        #endregion

        #region Test methods

        [TestMethod]
        public void GetOrderSecureMarginByCompanyCodeTest()
        {

            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockDataForOrderSecureMarginModelList();

            //...Positive unit test case 
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCompanyCode(_companyCode))
                            .IgnoreArguments()
                            .Return(OrderMarginList);
            _orderedSecuredMarginManager = new OrderedSecuredMarginManager(mockRepository);
            var result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCompanyCode(_companyCode);
            Assert.IsNotNull(result);


            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCompanyCode(string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);


            //...Negative unit test case : Output list is null
            OrderMarginList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCompanyCode(_companyCode))
                         .IgnoreArguments()
                         .Return(OrderMarginList);
            _orderedSecuredMarginManager = new OrderedSecuredMarginManager(mockRepository);
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCompanyCode(_companyCode);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

            //...Negative unit test case : Output is null
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCompanyCode(_companyCode))
                          .IgnoreArguments()
                          .Return(null);
            _orderedSecuredMarginManager = new OrderedSecuredMarginManager(mockRepository);
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCompanyCode(_companyCode);
            Assert.IsTrue(result.orderedSecuredMargin == null);

        }

        [TestMethod]

        public void GetOrderSecureMarginByOrderNoTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockOrderSecureMarginModel();

            //....Postive unit test case
            mockRepository.Stub(x => x.GetOrderSecuredMarginByOrderNo(_companyCode, _orderNo)).IgnoreArguments().Return(orderMargin);
            _orderedSecuredMarginManager = new OrderedSecuredMarginManager(mockRepository);
            var result = _orderedSecuredMarginManager.GetOrderSecuredMarginByOrderNo(_companyCode, _orderNo);
            Assert.IsNotNull(result);

            //...Negative unit test case : CompanyCode empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByOrderNo(string.Empty, _orderNo);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : OrderNumber empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByOrderNo(_companyCode, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : CompanyCode empty, OrderNumber empty
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByOrderNo(string.Empty, string.Empty);
            Assert.IsTrue(result.Status == ResponseStatus.Failure);


            //...Negative unit test case : Output list is null
            OrderMarginList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredMarginByOrderNo(_companyCode, _orderNo))
                         .IgnoreArguments()
                         .Return(null);
            _orderedSecuredMarginManager = new OrderedSecuredMarginManager(mockRepository);
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByOrderNo(_companyCode, _orderNo);
            Assert.IsTrue(result.ErrorInfo.Count > 0);



        }

        [TestMethod]

        public void GetOrderSecureMarginByCostTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            SetMockOrderSecureMarginModel();

            //....Postive unit test case
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCost(_companyCode, 123, 091)).IgnoreArguments().Return(orderMargin);
            _orderedSecuredMarginManager = new OrderedSecuredMarginManager(mockRepository);
            var result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCost(_companyCode, 123, 091);
            Assert.IsNotNull(result);

            //....Negative unit test :CompancodeEmpty

            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCost(string.Empty, 123, 091);
            Assert.IsNotNull(result.Status == ResponseStatus.Failure);

            //...Negative unit test case : mincost empty

            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCost(_companyCode, null, 091);
            //Assert.IsTrue(result.Status == ResponseStatus.Failure);

            ////...Negative unit test case : maxcost empty
            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCost(_companyCode, 123, 0);
            //Assert.IsTrue(result.Status == ResponseStatus.Failure);

            ////--Negative unit test case: two parameter is empty
            //mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            //result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCost(string.Empty,0, 091);
            //Assert.IsTrue(result.Status == ResponseStatus.Failure);


            //...Negative unit test case : Output list is null
            OrderMarginList.Clear();
            mockRepository = MockRepository.GenerateMock<IDataLayerContext>();
            mockRepository.Stub(x => x.GetOrderSecuredMarginByCost(_companyCode, 123, 091))
                         .IgnoreArguments()
                         .Return(null);
            _orderedSecuredMarginManager = new OrderedSecuredMarginManager(mockRepository);
            result = _orderedSecuredMarginManager.GetOrderSecuredMarginByCost(_companyCode, 123, 091);
            Assert.IsTrue(result.ErrorInfo.Count > 0);

        }



        #endregion




        public void SetMockOrderSecureMarginModel()
        {
            orderMargin = new List<Or03>()
            {
                new Or03() {
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
                MarginPercentage="143.57"
                }

            };




        }
        public void SetMockDataForOrderSecureMarginModelList()
        {
            #region SampleOrderSecureMarginModelList
            OrderMarginList.Add(new Or03()
            {
                //OrderNO
                Or03001 = "1215",
                //unitprice
                Or03008 = "1019",
                //unitpricecost
                Or03009 = "0187",
                //unitcode
                Or03010 = "100",
                //orqtyordered
                Or03011 = "57598",
                MarginPercentage = "443.57"


            });
            OrderMarginList.Add(new Or03()
            {
                //OrderNO
                Or03001 = "1918",
                //unitprice
                Or03008 = "2218",
                //unitpricecost
                Or03009 = "32198",
                //unitcode
                Or03010 = "2000",
                //orqtyordered
                Or03011 = "36572",
                MarginPercentage = "1043.57"

            });

            OrderMarginList.Add(new Or03()
            {
                //OrderNO
                Or03001 = "7821",
                //unitprice
                Or03008 = "9871",
                //unitpricecost
                Or03009 = "15679",
                //unitcode
                Or03010 = "300",
                //orqtyordered
                Or03011 = "26718",
                MarginPercentage = "343.57"

            });

            #endregion
        }
    }
}
