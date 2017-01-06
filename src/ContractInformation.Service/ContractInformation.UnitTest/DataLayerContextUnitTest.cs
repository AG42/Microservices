using ContractInformation.Common;
using ContractInformation.DataLayer;
using ContractInformation.DataLayer.Entities.Datalake;
using Microservices.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ContractInformation.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {
        private ConfigReader _configReader;
        private IDatabase _mocksDatabaseEntities;
        private DataLayerContext _dataLayerContext;
        private List<Cmh1Na00> _cmhEntitiesList;
        private readonly string _companyCode = "na";
        private const string CustomerName = "ABCD";
        private const string PONumber = "1234567";
        private const string RequestNumber = "2345678";
        private const string ContractNumber = "9416000001";
        private const string Status = "Active";
        private const string Reference = "NAIR SUJITH";
        private const string SearchKey = "0";
        private const string StartDate = "2015-10-01 00:00:00.0";
        private const string EndDate = "2016-09-30 00:00:00.0";


        [TestInitialize]
        public void Initialize()
        {
            _mocksDatabaseEntities = MockRepository.GenerateMock<IDatabase>();
            _configReader = new ConfigReader();
            _dataLayerContext = new DataLayerContext() { Database = _mocksDatabaseEntities };
            _cmhEntitiesList = new List<Cmh1Na00>();
            //SetMockDataForContractInformation();
        }

        #region Unit Test Methods
        /// <summary>
        /// Unit testing Get all contracts for Company code
        /// </summary>
        [TestMethod]
        public void GetContractInformationByCompanyCodeTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByCompanyCode(_companyCode);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerNameTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByCustomerName(_companyCode, CustomerName);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerPONumberTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByCustomerPONumber(_companyCode, PONumber);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void GetContractsByRequestNumberTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByRequestNumber(_companyCode, RequestNumber);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void GetContractsByServiceContractNumberTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByServiceContractNumber(_companyCode, ContractNumber);
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void GetContractsByCustomerNameandStatusTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByCustomerNameandStatus(_companyCode, CustomerName, Status);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerReferenceTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByCustomerReference(_companyCode, Reference);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerReferenceandStatusTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByCustomerReferenceandStatus(_companyCode, Reference, Status);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerSearchKeyTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByCustomerSearchKey(_companyCode, SearchKey);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByCustomerSearchKeyandStatusTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByCustomerSearchKeyandStatus(_companyCode, SearchKey, Status);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByDateRangeTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByDateRange(_companyCode, StartDate, EndDate);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetContractsByStatusTest()
        {
            SetMockDataForContractInformation();
            var tableName = new Dictionary<string, string>();
            tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            _mocksDatabaseEntities.Stub(x => x.Get<Cmh1Na00>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]))
                .IgnoreArguments()
                .Return(_cmhEntitiesList);
            var result = _dataLayerContext.GetContractsByStatus(_companyCode, Status);
            Assert.IsNotNull(result);
        }
        #endregion

        #region Mock Methods
        private void SetMockDataForContractInformation()
        {
            #region SampleContractInformationHeaders
            _cmhEntitiesList.Add(new Cmh1Na00()
            {
                Cmh1001 = "ab",
                Cmh1002 = "ss",
                Cmh1004 = "rr",
                Cmh1005 = "mm",
                Cmh1015 = "10-Jul-2016",
                Cmh1016 = "17-Jul-2016",
                //Cmh1018 = "aa",
                //Cmh1021 = "gg",
                Cmh1034 = "pp",
                Cmh1040 = "ss",
                Cmh1068 = "nn",
                Cmh1091 = "ll",
                Cmh1093 = "gg",
                Cmh1096 = "tst",
                Cmh1123 = "sssaa",
                Cmh1132 = "ssadfg",
                //Cmh1158 = "llsss",
                Cmh1220 = "ssddwerr"
            });

            _cmhEntitiesList.Add(new Cmh1Na00()
            {
                Cmh1001 = "sd",
                Cmh1002 = "ghi",
                Cmh1004 = "lst",
                Cmh1005 = "mms",
                Cmh1015 = "10-Jul-2016",
                Cmh1016 = "17-Jul-2017",
                //Cmh1018 = "aaa",
                //Cmh1021 = "ggt",
                Cmh1034 = "ppr",
                Cmh1040 = "ssmn",
                Cmh1068 = "nnws",
                Cmh1091 = "llps",
                Cmh1093 = "ggtw",
                Cmh1096 = "tstd",
                Cmh1123 = "sssaa",
                Cmh1132 = "ssadfg",
                //Cmh1158 = "llsss",
                Cmh1220 = "ssddwerr"
            });

            _cmhEntitiesList.Add(new Cmh1Na00()
            {
                Cmh1001 = "abghg",
                Cmh1002 = "ssrst",
                Cmh1004 = "rrmno",
                Cmh1005 = "mm",
                Cmh1015 = "10-Jul-2016",
                Cmh1016 = "17-Jul-2017",
                //Cmh1018 = "aa",
                //Cmh1021 = "gg",
                Cmh1034 = "pp",
                Cmh1040 = "ss",
                Cmh1068 = "nn",
                Cmh1091 = "ll",
                Cmh1093 = "gg",
                Cmh1096 = "tst",
                Cmh1123 = "sssaa",
                Cmh1132 = "ssadfg",
                //Cmh1158 = "llsss",
                Cmh1220 = "ssddwerr"
            });
            #endregion
        }
        #endregion


    }
}

