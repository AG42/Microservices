using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxInvoice.Common;
using Microservices.Common.Interface;
using TaxInvoice.DataAccessLayer;
using Rhino.Mocks;
using TaxInvoice.DataAccessLayer.Entities.Datalake;
using System.Collections.Generic;
using System.Linq;

namespace TaxInvoice.UnitTest
{
    [TestClass]
    public class DataLayerContextUnitTest
    {
        #region Properties
        private IDatabase Database { get; set; }
        private DataLayerContext _dataLayerContext;
        private ConfigReader _configReader;

        internal List<SL17> TaxInvoiceDetailList;
        private string _companyCode = "na";
        private string _invoiceNo = "243545";
        private string _customerCode = "Abc";
        private decimal _minTaxAmount = Convert.ToDecimal(1.00);
        private decimal _maxTaxAmount = Convert.ToDecimal(100);

        #endregion

        #region..TestInitialize......
        [TestInitialize]
        public void Initialize()
        {
            _configReader = new ConfigReader();
            Database = MockRepository.GenerateMock<IDatabase>();
            _dataLayerContext = new DataLayerContext() { Database = Database };
            SetMockDataForTaxInvoiceDetailList();
        }
        #endregion.....................

        #region...Unit Test Methods
        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by company id
        /// </summary>
        public void GetTaxInvoiceByCompanyCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            Database.Stub(x => x.Get<SL17>(tableName[Constants.TableNameKey], tableName[Constants.ColumnNameKey]));

            var result = _dataLayerContext.GetTaxInvoiceByCompanyCode(_companyCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetTaxInvoiceByCompanyCode(string.Empty);
            Assert.IsNull(result);
        }

        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of order secured cost by order type
        /// </summary>
        public void GetTaxInvoiceByInvoiceNoTest()
        {

            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            Database.Stub(x => x.Where<SL17>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(TaxInvoiceDetailList);

            var result = _dataLayerContext.GetTaxInvoiceByInvoiceNo(_companyCode, _invoiceNo);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetTaxInvoiceByInvoiceNo("", _invoiceNo);
            Assert.IsNull(result);
        }

        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of TaxInvoice list by CustomerCode
        /// </summary>
        public void GetTaxInvoiceByCustomerCodeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            Database.Stub(x => x.Where<SL17>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(TaxInvoiceDetailList);

            var result = _dataLayerContext.GetTaxInvoiceByCustomerCode(_companyCode, _customerCode);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetTaxInvoiceByCustomerCode("", _customerCode);
            Assert.IsNull(result);
        }

        [TestMethod]
        /// <summary>
        /// This method is test all null and not null scenarios of TaxInvoice list by CustomerCode
        /// </summary>
        public void GetTaxInvoiceByTaxAmountRangeTest()
        {
            var tableName = _configReader.GetDatabaseTableName(_companyCode, string.Empty);
            Database.Stub(x => x.Where<SL17>(tableName[Constants.TableNameKey],
                        tableName[Constants.ColumnNameKey], ""))
                        .IgnoreArguments()
                        .Return(TaxInvoiceDetailList);

            var result = _dataLayerContext.GetTaxInvoiceByTaxAmountRange(_companyCode, _minTaxAmount, _minTaxAmount);
            Assert.IsNotNull(result);

            result = _dataLayerContext.GetTaxInvoiceByTaxAmountRange("", _minTaxAmount, _minTaxAmount);
            Assert.IsNull(result);
        }
        #endregion.................

        #region Mock Method
        /// <summary>
        /// initialise entities with mock data
        /// </summary>
        public void SetMockDataForTaxInvoiceDetailList()
        {
            // list of order secured cost Pc01 entities
            TaxInvoiceDetailList = new List<SL17>()
            {
                new SL17()
                {
                    SL17001="",
                    SL17002="",
                    SL17003="",
                    SL17004="",
                    SL17007="",
                    SL17008="",
                    SL17020="",
                    SL17032="",
                    SL17038="",
                    SL17050=""
                },

                new SL17()
                {
                    SL17001="",
                    SL17002="",
                    SL17003="",
                    SL17004="",
                    SL17007="",
                    SL17008="",
                    SL17020="",
                    SL17032="",
                    SL17038="",
                    SL17050=""
                }
            };
        }
        #endregion


    }
}
