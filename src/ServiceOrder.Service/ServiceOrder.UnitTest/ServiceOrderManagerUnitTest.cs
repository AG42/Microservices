using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceOrder.BusinessLayer;
using ServiceOrder.DataLayer.Entities;
using ServiceOrder.DataLayer.Entities.Datalake;
using ServiceOrder.DataLayer.Interfaces;
using ServiceOrder.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceOrder.UnitTest
{
    [TestClass]
    public class ServiceOrderManagerUnitTest
    {
        #region Declarations
        private IDatabaseContext _iDatabase;
        private ServiceOrderManager _serviceOrderManager;
        private const string COMPANY_CODE = "bh";

        List<SM01> _sm01EntityList = new List<SM01>();
        List<SM03> _sm03EntityList = new List<SM03>();
        List<SM05> _sm05EntityList = new List<SM05>();
        List<SM07> _sm07EntityList = new List<SM07>();
        ServiceOrderModel _serviceOrderModel = new ServiceOrderModel();
        List<ServiceOrderModel> _serviceOrderModelList = new List<ServiceOrderModel>();
        SM01 _sm01Entity = new SM01();
        SM03 _sm03Entity = new SM03();
        SM05 _sm05Entity = new SM05();
        SM07 _sm07Entity = new SM07();

        #endregion

        #region TestMethods
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _serviceOrderManager = new ServiceOrderManager(_iDatabase);
        }

        /// <summary>
        /// Test Method for GetServiceOrderByID
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByOrderNoTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderByServiceOrderNoResponse { ServiceOrderModel = _serviceOrderModel };

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            mockRepository.Stub(x => x.GetServiceOrderActivityLinesAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM03>)_sm03EntityList));

            mockRepository.Stub(x => x.GetServiceOrderCostLinesAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM05>)_sm05EntityList));

            mockRepository.Stub(x => x.GetServiceOrderMaterialLinesAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM07>)_sm07EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderByServiceOrderNo("N1", "001");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test Method for GetServiceOrderByID
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByOrderNoTestNullCollection()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderByServiceOrderNoResponse { ServiceOrderModel = _serviceOrderModel };

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                           .IgnoreArguments()
                           .Return(null);
            var result = _serviceOrderManager.GetServiceOrderByServiceOrderNo("N1", "001");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test Method for GetServiceOrderByID
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByOrderNoTestException()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();
             _sm01EntityList = new List<SM01>();

            var data = new Model.Responses.ServiceOrderByServiceOrderNoResponse { ServiceOrderModel = _serviceOrderModel };

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderByServiceOrderNo("N1", "001");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetServiceOrderByCompanyCodeTest
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByCompanyCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderByCompanyCodeResponse ();
            data.ServiceOrderModels.AddRange(_serviceOrderModelList);

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            mockRepository.Stub(x => x.GetServiceOrderActivityLinesAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM03>)_sm03EntityList));

            mockRepository.Stub(x => x.GetServiceOrderCostLinesAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM05>)_sm05EntityList));

            mockRepository.Stub(x => x.GetServiceOrderMaterialLinesAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM07>)_sm07EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderByCompanyCode("N1");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetServiceOrderByCompanyCodeTest
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByCompanyCodeTestNullCollection()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderByServiceOrderNoResponse { ServiceOrderModel = _serviceOrderModel };

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                           .IgnoreArguments()
                           .Return(null);
            var result = _serviceOrderManager.GetServiceOrderByCompanyCode("N1");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetServiceOrderByCompanyCodeTest
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByCompanyCodeExcpTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderByCompanyCodeResponse();
            data.ServiceOrderModels.AddRange(_serviceOrderModelList);
            _sm01EntityList = new List<SM01>();
            mockRepository.Stub(x => x.GetServiceOrderAsync("bh"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderByCompanyCode("N1");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetServiceOrderStatusByServiceOrderNoTest
        /// </summary>
        [TestMethod]
        public void GetServiceOrderStatusByServiceOrderNoTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderStatusByServiceOrderNoResponse();
            data.OrderStatus = "Confirmed";

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderStatusByServiceOrderNo("N1", "11010839303");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test for GetServiceOrderStatusByServiceOrderNoExcepTest
        /// </summary>
        [TestMethod]
        public void GetServiceOrderStatusByServiceOrderNoExcepTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderStatusByServiceOrderNoResponse();
            data.OrderStatus = "Confirmed";

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(null);

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderStatusByServiceOrderNo("N1", "11010839303");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderStatusByServiceOrderNoNullListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            List<SM01> sm01 = new List<SM01>();
            var data = new Model.Responses.ServiceOrderStatusByServiceOrderNoResponse();
            data.OrderStatus = "Confirmed";

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)sm01));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderStatusByServiceOrderNo("N1", "11010839303");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderTypeByServiceOrderNoTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderTypeByServiceOrderNoResponse();
            data.OrderType = "Test";

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderTypeByServiceOrderNo("N1", "11010839303");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderTypeByServiceOrderNoExcepTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();
            //_sm01List = _sm01EntityList;

            var data = new Model.Responses.ServiceOrderTypeByServiceOrderNoResponse();
            data.OrderType = "Test";

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(null);

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderTypeByServiceOrderNo("N1", "11010839303");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderTypeByServiceOrderNoNullListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
           
            var data = new Model.Responses.ServiceOrderTypeByServiceOrderNoResponse();

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderTypeByServiceOrderNo("N1", "11010839303");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByInvoiceCustomerCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderByInvoiceCustomerCodeResponse();
            data.ServiceOrderModels.AddRange(_serviceOrderModelList);

            mockRepository.Stub(x => x.GetServiceOrdersByInvoiceCustomerCodeAsync("bh",""))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            mockRepository.Stub(x => x.GetServiceOrderActivityLinesByInvoiceCustomerCodeAsync("bh", "11010839303","001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM03>)_sm03EntityList));

            mockRepository.Stub(x => x.GetServiceOrderCostLinesByInvoiceCustomerCodeAsync("bh", "11010839303","002"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM05>)_sm05EntityList));

            mockRepository.Stub(x => x.GetServiceOrderMaterialLinesByInvoiceCustomerCodeAsync("bh", "11010839303","003"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM07>)_sm07EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderByInvoiceCustomerCode("N1","");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByInvoiceCustomerCodeTestNullCollection()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderByServiceOrderNoResponse { ServiceOrderModel = _serviceOrderModel };

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                           .IgnoreArguments()
                           .Return(null);
            var result = _serviceOrderManager.GetServiceOrderByInvoiceCustomerCode("N1","001");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByInvoiceCustomerCodeExceptionTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();
             _sm01EntityList = new List<SM01>();

            var data = new Model.Responses.ServiceOrderByInvoiceCustomerCodeResponse();
            data.ServiceOrderModels.AddRange(_serviceOrderModelList);

            mockRepository.Stub(x => x.GetServiceOrdersByInvoiceCustomerCodeAsync("bh", ""))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderByInvoiceCustomerCode("N1", "");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByInvoiceNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();
            //_sm01List = _sm01EntityList;

            var data = new Model.Responses.ServiceOrderByInvoiceNumberResponse();
            data.ServiceOrderModels.AddRange(_serviceOrderModelList);

            mockRepository.Stub(x => x.GetServiceOrderByInvoiceNumberAsync("bh",""))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            mockRepository.Stub(x => x.GetServiceOrdersByInvoiceCustomerCodeAsync("bh", ""))
                             .IgnoreArguments()
                             .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            mockRepository.Stub(x => x.GetServiceOrderActivityLinesByInvoiceNumberAsync("bh", "11010839303", "001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM03>)_sm03EntityList));

            mockRepository.Stub(x => x.GetServiceOrderCostLinesByInvoiceNumberAsync("bh", "11010839303", "002"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM05>)_sm05EntityList));

            mockRepository.Stub(x => x.GetServiceOrderMaterialLinesByInvoiceNumberAsync("bh", "11010839303", "003"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM07>)_sm07EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderByInvoiceNumber("N1","");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByInvoiceNumberTestNullCollection()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();

            var data = new Model.Responses.ServiceOrderByInvoiceNumberResponse();

            mockRepository.Stub(x => x.GetServiceOrderAsync("bh", "11010839303"))
                           .IgnoreArguments()
                           .Return(null);
            var result = _serviceOrderManager.GetServiceOrderByInvoiceNumber("N1", "001");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Unit Test
        /// </summary>
        [TestMethod]
        public void GetServiceOrderByInvoiceNumberExcptionTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceOrderModels();
            _sm01EntityList = new List<SM01>();

            var data = new Model.Responses.ServiceOrderByInvoiceNumberResponse();
            data.ServiceOrderModels.AddRange(_serviceOrderModelList);

            mockRepository.Stub(x => x.GetServiceOrderByInvoiceNumberAsync("bh", ""))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM01>)_sm01EntityList));

            _serviceOrderManager = new ServiceOrderManager(mockRepository);
            var result = _serviceOrderManager.GetServiceOrderByInvoiceNumber("N1", "");
            Assert.IsNotNull(result);
        }
        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of serviceorder for unit test
        /// </summary>
        public void SetMockDataForServiceOrderModels()
        {
            #region SampleDataSM01Entity
            _sm01EntityList.Add(new SM01
            {
                SM01001 = "11010839303",
                SM01002 = "In11010839303",
                SM01003 = "00",
                SM01004 = "test",
                SM01005 = "",
                SM01006 = "S-BAS",
                SM01007 = "test",
                SM01209 = "0",
                SM01010 = "",
                SM01011 = "Building 501 Chiller No 2, ",
                SM01012 = "Supply and install new pressure relief",
                SM01013 = " valves and three-way valves",
                SM01014 = "as per quote reference No: 15120012",
                SM01016 = "5823",
                SM01036 = "",
                SM01037 = "1900-01-01 00:00:00.0",
                SM01038 = "",
                SM01049 = "",
                SM01064 = "52",
                SM01071 = "5823",
                SM01078 = "1010839303",
                SM01097 = "system commissioning",
                SM01110 = "R01",
                SM01113 = "NZ12000012_NOCON    1010839303000314",
                SM01205 = "0",
                SM01210 = "",
                SM01146 = "",
                SM01147 = ""
            });

            _sm01EntityList.Add(new SM01
            {
                SM01001 = "11010839303000",
                SM01002 = "In11010839303000",
                SM01003 = "00",
                SM01004 = "test",
                SM01005 = "",
                SM01006 = "S-SEC",
                SM01007 = "test",
                SM01209 = "0",
                SM01010 = "",
                SM01011 = "Building 501 Chiller No 3, ",
                SM01012 = "Supply and install new pressure relief test",
                SM01013 = " valves and three-way valves test",
                SM01014 = "as per quote reference No: 15120012 test",
                SM01016 = "5823",
                SM01036 = "",
                SM01037 = "1900-01-01 00:00:00.0",
                SM01038 = "",
                SM01049 = "",
                SM01064 = "52",
                SM01071 = "5823",
                SM01078 = "1010839303",
                SM01097 = "retrofit",
                SM01110 = "R01",
                SM01113 = "NZ12000012_NOCON    1010839303000314 test",
                SM01205 = "0",
                SM01210 = "",
                SM01146 = "",
                SM01147 = ""
            });
            _sm01EntityList.Add(new SM01
            {
                SM01001 = "0110108393030",
                SM01002 = "0In11010839303000",
                SM01003 = "00",
                SM01004 = "test1",
                SM01005 = "",
                SM01006 = "S-EPS",
                SM01007 = "test1",
                SM01209 = "0",
                SM01010 = "",
                SM01011 = "Building 501 Chiller No 3, ",
                SM01012 = "Supply and install new pressure relief test",
                SM01013 = " valves and three-way valves test",
                SM01014 = "as per quote reference No: 15120012 test",
                SM01016 = "5823",
                SM01036 = "",
                SM01037 = "1900-01-01 00:00:00.0",
                SM01038 = "",
                SM01049 = "",
                SM01064 = "52",
                SM01071 = "5823",
                SM01078 = "1010839303",
                SM01097 = "LM",
                SM01110 = "R01",
                SM01113 = "NZ12000012_NOCON    1010839303000314 test",
                SM01205 = "0",
                SM01210 = "",
                SM01146 = "",
                SM01147 = ""
            });
            _sm01EntityList.Add(new SM01
            {
                SM01001 = "01101083930300",
                SM01002 = "0In11010839303000",
                SM01003 = "00",
                SM01004 = "test1",
                SM01005 = "",
                SM01006 = "S-YORK",
                SM01007 = "test1",
                SM01209 = "0",
                SM01010 = "",
                SM01011 = "Building 501 Chiller No 3, ",
                SM01012 = "Supply and install new pressure relief test",
                SM01013 = " valves and three-way valves test",
                SM01014 = "as per quote reference No: 15120012 test",
                SM01016 = "5823",
                SM01036 = "",
                SM01037 = "1900-01-01 00:00:00.0",
                SM01038 = "",
                SM01049 = "",
                SM01064 = "52",
                SM01071 = "5823",
                SM01078 = "1010839303",
                SM01097 = "LM",
                SM01110 = "R01",
                SM01113 = "NZ12000012_NOCON    1010839303000314 test",
                SM01205 = "0",
                SM01210 = "",
                SM01146 = "",
                SM01147 = ""
            });
            _sm01EntityList.Add(new SM01
            {
                SM01001 = "01101083930301",
                SM01002 = "0In11010839303000",
                SM01003 = "00",
                SM01004 = "test1",
                SM01005 = "",
                SM01006 = "S-ELEC",
                SM01007 = "test1",
                SM01209 = "0",
                SM01010 = "",
                SM01011 = "Building 501 Chiller No 3, ",
                SM01012 = "Supply and install new pressure relief test",
                SM01013 = " valves and three-way valves test",
                SM01014 = "as per quote reference No: 15120012 test",
                SM01016 = "5823",
                SM01036 = "",
                SM01037 = "1900-01-01 00:00:00.0",
                SM01038 = "",
                SM01049 = "",
                SM01064 = "52",
                SM01071 = "5823",
                SM01078 = "1010839303",
                SM01097 = "system warranty",
                SM01110 = "R01",
                SM01113 = "NZ12000012_NOCON    1010839303000314 test",
                SM01205 = "0",
                SM01210 = "",
                SM01146 = "",
                SM01147 = ""
            });
            _sm01EntityList.Add(new SM01
            {
                SM01001 = "01101083930301",
                SM01002 = "0In11010839303000",
                SM01003 = "00",
                SM01004 = "test1",
                SM01005 = "",
                SM01006 = "S-HVAC",
                SM01007 = "test1",
                SM01209 = "0",
                SM01010 = "",
                SM01011 = "Building 501 Chiller No 3, ",
                SM01012 = "Supply and install new pressure relief test",
                SM01013 = " valves and three-way valves test",
                SM01014 = "as per quote reference No: 15120012 test",
                SM01016 = "5823",
                SM01036 = "",
                SM01037 = "1900-01-01 00:00:00.0",
                SM01038 = "",
                SM01049 = "",
                SM01064 = "52",
                SM01071 = "5823",
                SM01078 = "1010839303",
                SM01097 = "system warranty",
                SM01110 = "R01",
                SM01113 = "NZ12000012_NOCON    1010839303000314 test",
                SM01205 = "0",
                SM01210 = "",
                SM01146 = "",
                SM01147 = ""
            });
            _sm01EntityList.Add(new SM01
            {
                SM01001 = "01101083930301",
                SM01002 = "0In11010839303000",
                SM01003 = "00",
                SM01004 = "test1",
                SM01005 = "",
                SM01006 = "",
                SM01007 = "test1",
                SM01209 = "0",
                SM01010 = "",
                SM01011 = "Building 501 Chiller No 3, ",
                SM01012 = "Supply and install new pressure relief test",
                SM01013 = " valves and three-way valves test",
                SM01014 = "as per quote reference No: 15120012 test",
                SM01016 = "5823",
                SM01036 = "",
                SM01037 = "1900-01-01 00:00:00.0",
                SM01038 = "",
                SM01049 = "",
                SM01064 = "52",
                SM01071 = "5823",
                SM01078 = "1010839303",
                SM01097 = "system warranty",
                SM01110 = "R01",
                SM01113 = "NZ12000012_NOCON    1010839303000314 test",
                SM01205 = "0",
                SM01210 = "",
                SM01146 = "",
                SM01147 = ""
            });
            _sm01Entity = new SM01
            {
                SM01001 = "0110108393030",
                SM01002 = "0In11010839303000",
                SM01003 = "00",
                SM01004 = "test1",
                SM01005 = "",
                SM01006 = "S-HVAC",
                SM01007 = "test1",
                SM01209 = "0",
                SM01010 = "",
                SM01011 = "Building 501 Chiller No 3, ",
                SM01012 = "Supply and install new pressure relief test",
                SM01013 = " valves and three-way valves test",
                SM01014 = "as per quote reference No: 15120012 test",
                SM01016 = "5823",
                SM01036 = "",
                SM01037 = "1900-01-01 00:00:00.0",
                SM01038 = "",
                SM01049 = "",
                SM01064 = "52",
                SM01071 = "5823",
                SM01078 = "1010839303",
                SM01097 = "LM",
                SM01110 = "R01",
                SM01113 = "NZ12000012_NOCON    1010839303000314 test",
                SM01205 = "0",
                SM01210 = "",
                SM01146 = "",
                SM01147 = ""
            };

            #endregion

            #region SampleDataSM030507
            _sm03EntityList.Add(new SM03
            {
                SM03001 = "11010839303",
                SM03002 = "01",
                SM03003 = "0.00000",
                SM03012 = "0.00000",
                SM03023 = "1900-01-01 00:00:00.000",
                SM03024 = "0.00000",
                SM03028 = "2.2",
                SM03042 = "0.00000",
                SM03071 = ""
            });

            _sm03EntityList.Add(new SM03
            {
                SM03001 = "11010839303",
                SM03002 = "01",
                SM03003 = "0.00000",
                SM03012 = "0.00000",
                SM03023 = "1900-01-01 00:00:00.000",
                SM03024 = "0.00000",
                SM03028 = "2.2",
                SM03042 = "0.00000",
                SM03071 = ""
            });

            _sm03Entity = new SM03
            {
                SM03001 = "11010839303",
                SM03002 = "01",
                SM03003 = "0.00000",
                SM03012 = "0.00000",
                SM03023 = "1900-01-01 00:00:00.000",
                SM03024 = "0.00000",
                SM03028 = "2.2",
                SM03042 = "0.00000",
                SM03071 = ""
            };

            _sm05EntityList.Add(new SM05
            {
                SM05001 = "11010839303",
                SM05002 = "0001",
                SM05003 = "0",
                SM05004 = "40000",
                SM05005 = "Kilometers",
                SM05009 = "16000.00000000",
                SM05012 = "V002",
                SM05019 = "1900-01-01 00:00:00.000",
                SM05020 = "",
                SM05024 = "0.00000000",
                SM05028 = "JSH",
                SM05038 = "0.00000000",
                SM05040 = "0.00000000",
                SM05053 = "2009-11-06 00:00:00.000",
                SM05078 = ""
            });

            _sm05EntityList.Add(new SM05
            {
                SM05001 = "11010839303",
                SM05002 = "0002",
                SM05003 = "0",
                SM05004 = "40000",
                SM05005 = "Kilometers",
                SM05009 = "16000.00000000",
                SM05012 = "V002",
                SM05019 = "1900-01-01 00:00:00.000",
                SM05020 = "",
                SM05024 = "0.00000000",
                SM05028 = "JSH       ",
                SM05038 = "0.00000000",
                SM05040 = "0.00000000",
                SM05053 = "2009-11-06 00:00:00.000",
                SM05078 = ""
            });

            _sm05Entity = new SM05
            {
                SM05001 = "11010839303",
                SM05002 = "0003",
                SM05003 = "0",
                SM05004 = "40000",
                SM05005 = "Kilometers",
                SM05009 = "16000.00000000",
                SM05012 = "V002",
                SM05019 = "1900-01-01 00:00:00.000",
                SM05020 = "",
                SM05024 = "0.00000000",
                SM05028 = "JSH       ",
                SM05038 = "0.00000000",
                SM05040 = "0.00000000",
                SM05053 = "2009-11-06 00:00:00.000",
                SM05078 = ""
            };

            _sm07EntityList.Add(new SM07
            {
                SM07001 = "11010839303",
                SM07002 = "0004",
                SM07003 = "0",
                SM07004 = "01100592000",
                SM07008 = "1.00000000",
                SM07010 = "832252.87000000",
                SM07011 = "1.00000000",
                SM07017 = "0.00000000",
                SM07019 = "C001",
                SM07025 = "0.00000000",
                SM07030 = "1.00000000",
                SM07042 = "2009 - 11 - 09 00:00:00.000",
                SM07043 = "",
                SM07140 = "",
                SM07135 = "0.00000000"
            });

            _sm07EntityList.Add(new SM07
            {
                SM07001 = "11010839303",
                SM07002 = "0003",
                SM07003 = "0",
                SM07004 = "011005920001",
                SM07008 = "1.00000000",
                SM07010 = "832252.87000000",
                SM07011 = "1.00000000",
                SM07017 = "0.00000000",
                SM07019 = "C001",
                SM07025 = "0.00000000",
                SM07030 = "1.00000000",
                SM07042 = "2009 - 11 - 09 00:00:00.000",
                SM07043 = "",
                SM07140 = "",
                SM07135 = "0.00000000"
            });

            _sm07Entity = new SM07
            {
                SM07001 = "11010839303",
                SM07002 = "0001",
                SM07003 = "0",
                SM07004 = "011005920001",
                SM07008 = "1.00000000",
                SM07010 = "832252.87000000",
                SM07011 = "1.00000000",
                SM07017 = "0.00000000",
                SM07019 = "C001",
                SM07025 = "0.00000000",
                SM07030 = "1.00000000",
                SM07042 = "2009 - 11 - 09 00:00:00.000",
                SM07043 = "",
                SM07140 = "",
                SM07135 = "0.00000000"
            };

            //stockItemsEnttiesList.Add(new ItemWarehouse
            //{
            //    //sc01001 = "007-09212-000",
            //    //sc01002 = "COOLER TUBES-",
            //    //sc01003 = "",
            //    //sc01037 = "ESGS",
            //    //sc01066 = 0,
            //    //sc01106 = 0.00000,
            //    //sc01120 = 0,
            //    //sc01135 = 0,
            //    sc03001 = "007-09212-000",
            //    sc03002 = "70",
            //    sc03003 = 0.00000,
            //    sc03004 = 0.00000,
            //    SC03005 = 0.00000
            //});
            #endregion

        }
        #endregion
    }
}
