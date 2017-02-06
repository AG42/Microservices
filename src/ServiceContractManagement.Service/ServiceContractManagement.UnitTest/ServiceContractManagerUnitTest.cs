using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceContractManagement.BusinessLayer;
using ServiceContractManagement.DataLayer.Entities.Datalake;
using ServiceContractManagement.DataLayer.Interfaces;
using ServiceContractManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rhino.Mocks;

namespace ServiceContractManagement.UnitTest
{
    [TestClass]
    public class ServiceContractManagerUnitTest
    {
        #region Declarations
        private IDatabaseContext _iDatabase;
        private ServiceContractManager _serviceContractManager;
        private const string COMPANY_CODE = "j1";
        private SM11 _sm11 = new SM11();
        private SM13 _sm13 = new SM13();
        private readonly List<SM11> _serviceContractMasterList  = new List<SM11>();
        private readonly List<SM13> _serviceContractLinesList = new List<SM13>();
        private ServiceContractMasterModel _contractMasterModel = new ServiceContractMasterModel();
        private readonly List<ServiceContractMasterModel> _contractMasterModelList = new List<ServiceContractMasterModel>();
        private readonly List<ServiceContractLinesModel> _contractLineModelList = new List<ServiceContractLinesModel>();
        private ServiceContractLinesModel _contractLineModel = new ServiceContractLinesModel();

        #endregion

        #region TestMethods
        [TestInitialize]
        public void Initialize()
        {
            _serviceContractManager = new ServiceContractManager(_iDatabase);
        }

        /// <summary>
        /// Test Method for GetServiceContractDetailsByContractCode
        /// </summary>
        [TestMethod]
        public void GetServiceContractDetailsByContractCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractDetailsByContractNumberResponse
            {
                ServiceContractDetails = _contractMasterModel
            };

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult(_sm11));

            mockRepository.Stub(x => x.GetServiceContractLineDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM13>)_serviceContractLinesList));

            _serviceContractManager = new  ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractDetailsByContractNumber("j1", "961120034");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Gets the service contract details by contract date test.
        /// </summary>
        [TestMethod]
        public void GetServiceContractDetailsByContractDateTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractDetailsByContractDateResponse();
            data.ServiceContractDetails.Add(_contractMasterModel);

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractDateRangeAsync("j1", "2015-01-05", "2016-01-06"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM11>)_serviceContractMasterList));

            mockRepository.Stub(x => x.GetServieContractLineDetailsByContractCodeListAsync("j1", _serviceContractMasterList))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM13>)_serviceContractLinesList));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractDetailsByContractDate("j1", "2015-01-05", "2016-01-06");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServiceContractTypeByContractNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractTypeByContractNumberResponse {ContractType = "Test"};

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult(_sm11));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractTypeByContractNumber("j1", "961120034");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServiceContractUnitPriceByContractNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractUnitPriceByContractNumberResponse();
            data.ServiceContractLineItemUnitPrice.Add(new ServiceContractLinesUnitPriceModel());

            mockRepository.Stub(x => x.GetServiceContractLineDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM13>)_serviceContractLinesList));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractUnitPriceByContractNumber("j1", "961120034");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServiceContractInvoiceQuantityByContractNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractInvoiceQuantityByContractNumberResponse();
            data.ServiceContractLineItemQuantity.Add(new ServiceContractLinesInvoiceQtyModel());

            mockRepository.Stub(x => x.GetServiceContractLineDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM13>)_serviceContractLinesList));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractInvoiceQuantityByContractNumber("j1", "961120034");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServiceContractDebitCreditValueByContractNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractDebitCreditValueByContractNumberResponse();
            data.ServiceContractLineItemDebitCreditValue.Add(new ServiceContractLinesDebitCreditValueModel());

            mockRepository.Stub(x => x.GetServiceContractLineDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM13>)_serviceContractLinesList));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractDebitCreditValueByContractNumber("j1", "961120034");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServiceContractPaymentTermsByContractNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractPaymentTermsByContractNumberResponse {PaymentTerms = "60"};

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult(_sm11));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractPaymentTermsByContractNumber("j1", "961120034");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServiceContractSalesmanNoByContractNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractSalesmanNoByContractNumberResponse {SalesmanNo = "60ty566"};

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult(_sm11));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractSalesmanNoByContractNumber("j1", "961120034");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServiceContractValueByContractNumberTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractValueByContractNumberResponse {ContractValue = "123345"};

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractCodeAsync("j1", "961120034"))
                            .IgnoreArguments()
                            .Return(Task.FromResult(_sm11));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractValueByContractNumber("j1", "961120034");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCasesForNullValues()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractCodeAsync("bh", "1001"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((SM11)null));

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractDateRangeAsync("bh", "2016-01-01", "2015-01-01"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM11>)null));

            mockRepository.Stub(x => x.GetServiceContractLineDetailsByContractCodeAsync("bh", "123445"))
                           .IgnoreArguments()
                           .Return(Task.FromResult((IEnumerable<SM13>)null));

            mockRepository.Stub(x => x.GetServieContractLineDetailsByContractCodeListAsync("bh", _serviceContractMasterList))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM13>)null));



            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractDebitCreditValueByContractNumber("N1", "1001");
            var resultPayterm = _serviceContractManager.GetServiceContractPaymentTermsByContractNumber("N1", "AAa");
            var resultSalesmanNoNumber = _serviceContractManager.GetServiceContractSalesmanNoByContractNumber("N1", "1001");
            var resultContractNumber = _serviceContractManager.GetServiceContractDetailsByContractNumber("N1", "1001");
            var resultunitprice = _serviceContractManager.GetServiceContractUnitPriceByContractNumber("N1", "AAa");
            var resultContractValue = _serviceContractManager.GetServiceContractValueByContractNumber("N1", "1001");
            var resultContractType= _serviceContractManager.GetServiceContractTypeByContractNumber("N1", "1001");
            var resultInvoiceDates = _serviceContractManager.GetServiceContractDetailsByContractDate("N1", "2017-01-16", "2017-01-12");
            var resultInvoiceQty = _serviceContractManager.GetServiceContractInvoiceQuantityByContractNumber("N1", "1001");

            Assert.AreNotEqual(result.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultPayterm.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultSalesmanNoNumber.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultContractNumber.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultInvoiceDates.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultContractValue.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultunitprice.ErrorInfo.Count, 0);
            Assert.AreNotEqual(resultContractType.ErrorInfo.Count, 0);
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractDateNullTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractDetailsByContractDateResponse();
            data.ServiceContractDetails.Add(_contractMasterModel);

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractDateRangeAsync("j1", "2015-01-05", "2016-01-06"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM11>)_serviceContractMasterList));

  
            mockRepository.Stub(x => x.GetServieContractLineDetailsByContractCodeListAsync("j1", _serviceContractMasterList))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM13>)null));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var result = _serviceContractManager.GetServiceContractDetailsByContractDate("j1", "2015-01-05", "2016-01-06");
            Assert.AreNotEqual(result.ErrorInfo.Count,0);
//            Assert.AreNotEqual(result.ErrorInfo.Count, 0);
        }

        [TestMethod]
        public void GetServiceContractDetailsByContractCodeNullTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForServiceContractMaster();
            SetMockDataForServiceContractLines();
            SetMockDataForServiceMasterModel();

            var data = new Model.Responses.ServiceContractDetailsByContractNumberResponse
            {
                ServiceContractDetails = _contractMasterModel
            };

            mockRepository.Stub(x => x.GetServiceContractDetailsByContractCodeAsync("j1", "23456"))
                .IgnoreArguments()
                .Return(Task.FromResult(_sm11));

            mockRepository.Stub(x => x.GetServiceContractLineDetailsByContractCodeAsync("j1", "123456"))
                            .IgnoreArguments()
                            .Return(Task.FromResult((IEnumerable<SM13>)null));

            _serviceContractManager = new ServiceContractManager(mockRepository);
            var resultByContractCode = _serviceContractManager.GetServiceContractDetailsByContractNumber("j1", "1234567");
            Assert.AreNotEqual(resultByContractCode.ErrorInfo.Count, 0);

        }

        #endregion

        #region MockData
        private void SetMockDataForServiceContractMaster()
        {
            _sm11 = new SM11() {
                SM11001 = "961120034",
                SM11003 = "NA008",
                SM11004 = "K.rattanam",
                SM11011 = "30",
                SM11012 = "2/1/2009 12:00:00 AM",
                SM11014 = "2",
                SM11015 = "2/1/2009 12:00:00 AM",
                SM11016 = "2/1/2009 12:00:00 AM",
                SM11017 = "AWA",
                SM11018 = "YSK-M 2006/02",
                SM11022 = "0",
                SM11023 = "0",
                SM11027 = "0",
                SM11034 = "PSV",
                SM11068 = "R"
            };
            _serviceContractMasterList.Add(new SM11()
            {
                SM11001 = "961120034",
                SM11003 = "NA008",
                SM11004 = "K.rattanam",
                SM11011 = "30",
                SM11012 = "2/1/2009 12:00:00 AM",
                SM11014 = "2",
                SM11015 = "2/1/2009 12:00:00 AM",
                SM11016 = "2/1/2009 12:00:00 AM",
                SM11017 = "AWA",
                SM11018 = "YSK-M 2006/02",
                SM11022 = "0",
                SM11023 = "0",
                SM11027 = "0",
                SM11034 = "PSV",
                SM11068 = "R"
            });
            _serviceContractMasterList.Add(new SM11()
            {
                SM11001 = "2900020014",
                SM11003 = "SA170",
                SM11004 = "K.Taveesak",
                SM11011 = "30",
                SM11012 = "3/1/2009 12:00:00 AM",
                SM11014 = "1",
                SM11015 = "3/1/2009 12:00:00 AM",
                SM11016 = "3/1/2009 12:00:00 AM",
                SM11017 = "WHO",
                SM11018 = "TH92-4212-0014",
                SM11022 = "0",
                SM11023 = "0",
                SM11027 = "0",
                SM11034 = "OMV",
                SM11068 = "N"
            });
            _serviceContractMasterList.Add(new SM11()
            {
                SM11001 = "3511520084",
                SM11003 = "TH031",
                SM11004 = "K.Somchai",
                SM11011 = "1",
                SM11012 = "3/1/2009 12:00:00 AM",
                SM11014 = "2",
                SM11015 = "3/1/2009 12:00:00 AM",
                SM11016 = "3/1/2009 12:00:00 AM",
                SM11017 = "PTH",
                SM11018 = "YCB-M2005/001",
                SM11022 = "0",
                SM11023 = "0",
                SM11027 = "0",
                SM11034 = "PSV",
                SM11068 = "R"
            });
        }
        private void SetMockDataForServiceContractLines()
        {
            _sm13 = new SM13() {
                SM13001 = "180907",
                SM13002 = "10",
                SM13008 = "132000",
                SM13019 = "7",
                SM13027 = "0",
                SM13042 = "1",
                SM13050 = "0"
            };
            _serviceContractLinesList.Add(new SM13()
            {
                SM13001 = "180907",
                SM13002 = "10",
                SM13008 = "132000",
                SM13019 = "7",
                SM13027 = "0",
                SM13042 = "1",
                SM13050 = "0"
            });
            _serviceContractLinesList.Add(new SM13()
            {
                SM13001 = "961120034",
                SM13002 = "120",
                SM13008 = "46728.97",
                SM13019 = "7",
                SM13027 = "0",
                SM13042 = "1",
                SM13050 = "0"
            });
            _serviceContractLinesList.Add(new SM13()
            {
                SM13001 = "2900020014",
                SM13002 = "10",
                SM13008 = "98130.83",
                SM13019 = "7",
                SM13027 = "0",
                SM13042 = "1",
                SM13050 = "0"
            });
        }

        private void SetMockDataForServiceMasterModel() {


            _contractMasterModel = new ServiceContractMasterModel() {
                ServiceContractNo = "961120034",
                CustomerCode = "NA008",
                CustomerReference = "K.rattanam",
                PaymantTerms = "30",
                ContractDate = "2/1/2009 12:00:00 AM",
                InvoicingInterval = "2",
                ContractStartDate = "2/1/2009 12:00:00 AM",
                ContractEndDate = "2/1/2009 12:00:00 AM",
                SalesmanNo = "AWA",
                OurReference = "YSK-M 2006/02",
                InvoiceCurrencyCode = "0",
                ContractCurrencyCode = "0"
            };

            _contractLineModelList.Add(new ServiceContractLinesModel()
            {
                ServiceContractNo = "180907",
                LineNo = "10",
                UnitPriceOCU = "132000",
                SalesTaxCode = "7",
                Price_UnitCode = "1",
                InvoiceQunatity = "0",
                ActualQuantity = "0"
            });
            _contractLineModelList.Add(new ServiceContractLinesModel()
            {
                ServiceContractNo = "961120034",
                LineNo = "120",
                UnitPriceOCU = "46728.97",
                SalesTaxCode = "7",
                Price_UnitCode = "1",
                InvoiceQunatity = "0",
                ActualQuantity = "0"
            });

            _contractMasterModel.ServiceContractLineDetails.AddRange(_contractLineModelList);

            _contractMasterModelList.Add(_contractMasterModel);

        }
        #endregion
    }
}
