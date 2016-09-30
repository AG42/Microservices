using Rhino.Mocks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductInventory.BusinessLayer;
using ProductInventory.DataLayer.Entities;
using ProductInventory.DataLayer.Entities.Datalake;
using ProductInventory.DataLayer.Interfaces;
using ProductInventory.Model;
using System.Linq;

namespace ProductInventory.UnitTest
{
    [TestClass]
    public class ProductInventoryManagerUnitTest
    {
        #region Declarations
        private IDatabaseContext _iDatabase;
        private ProductInventoryManager _inventoryManager;
        private const string COMPANY_CODE = "bh";
        //string productCode = "C002";
        //string customerName = "Customer 1";
        List<ProductInventoryEntity> _productInventoryEntityList = new List<ProductInventoryEntity>();

        readonly ProductInventoryModel _inventoryModel = new ProductInventoryModel();
        StockItemMaster _stockItemMasterEntity = new StockItemMaster();
        readonly List<ProductWarehouseModel> _productWarehouseModelList = new List<ProductWarehouseModel>();
        List<ItemWarehouse> _stockItemsEnttiesList = new List<ItemWarehouse>();
        #endregion

        #region UnitTests
        /// <summary>
        /// Initializes
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _inventoryManager = new ProductInventoryManager(_iDatabase);
        }

        [TestMethod]
        public void GetProductByIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            var data = new Model.Response.ProductSearchByIdResponse { ProductInventory = _inventoryModel };

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", ""))
                            .IgnoreArguments()
                            .Return(_stockItemMasterEntity);

            mockRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", ""))
                .IgnoreArguments()
                .Return(_stockItemsEnttiesList);


            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductById(COMPANY_CODE, "001");
            Assert.IsNotNull(result);

            result = _inventoryManager.GetProductById("K1", "001");
            Assert.IsNotNull(result);
            result = _inventoryManager.GetProductById("KS", "001");
            Assert.IsNotNull(result);
            result = _inventoryManager.GetProductById("N1", "001");
            Assert.IsNotNull(result);
            result = _inventoryManager.GetProductById("KK", "001");
            Assert.IsNotNull(result);
            result = _inventoryManager.GetProductById("KU", "001");
            Assert.IsNotNull(result);
            result = _inventoryManager.GetProductById("NU", "001");
            Assert.IsNotNull(result);


            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductByIdNegTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            var data = new Model.Response.ProductSearchByIdResponse { ProductInventory = _inventoryModel };

            StockItemMaster productMasterModelTest = new StockItemMaster
            {
                sc01001 = ""
            };

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", ""))
                            .IgnoreArguments()
                            .Return(new StockItemMaster());

            mockRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", ""))
                .IgnoreArguments()
                .Return(new List<ItemWarehouse>());


            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductById(COMPANY_CODE, "001");
            Assert.IsNotNull(result);

            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockExceptionRepository.Stub(x => x.GetStockItemByProductCode("bh", ""))
                            .IgnoreArguments()
                            .Return(productMasterModelTest);

            mockExceptionRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", ""))
               .IgnoreArguments()
               .Return(_stockItemsEnttiesList);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            result = _inventoryManager.GetProductById(COMPANY_CODE, "001");
            Assert.IsNotNull(result);

            var mockExcepRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockExcepRepository.Stub(x => x.GetStockItemByProductCode("bh", ""))
                            .IgnoreArguments()
                            .Return(null);

            mockExcepRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", ""))
               .IgnoreArguments()
               .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExcepRepository);

            result = _inventoryManager.GetProductById(COMPANY_CODE, "001");
            Assert.AreEqual(result.ErrorInfo.Count, 1);

        }
        [TestMethod]
        public void GetProductByIdExceptionTest()
        {
            var data = new Model.Response.ProductSearchByIdResponse { ProductInventory = _inventoryModel };

            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockExceptionRepository.Stub(x => x.GetStockItemByProductCode("bh", ""))
                            .IgnoreArguments()
                            .Return(new StockItemMaster());

            mockExceptionRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", ""))
               .IgnoreArguments()
               .Return(new List<ItemWarehouse>());

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductById(COMPANY_CODE, "001");
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void GetProductByIdInputValidationTest()
        {
            var data = new Model.Response.ProductSearchByIdResponse { ProductInventory = _inventoryModel };

            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductById(string.Empty, "001");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductByDescriptionTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductSearchByDescriptionResponse();
            //data.ProductList.AddRange()

            mockRepository.Stub(x => x.GetProductInvetoryByProductName("bh", "007"))
                            .IgnoreArguments()
                            .Return(_productInventoryEntityList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductByDescription(COMPANY_CODE, "001");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductByDescriptionwithEmptyProductsListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetProductInvetoryByProductName("bh", "007"))
              .IgnoreArguments()
              .Return(new List<ProductInventoryEntity>());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductByDescription(COMPANY_CODE, "001");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductByDescriptionwithProductInventoryEntityListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetProductInvetoryByProductName("bh", "007"))
              .IgnoreArguments()
              .Return(GetProductCodeEntityList());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductByDescription(COMPANY_CODE, "001");
            Assert.IsNotNull(result);
        }

        private List<ProductInventoryEntity> GetProductCodeEntityList()
        {
            _productInventoryEntityList = new List<ProductInventoryEntity>
            {
                new ProductInventoryEntity
                {
                    sc01001 = "01001",
                    sc01002 = "COOLER TUBES-",
                    sc01003 = "",
                    sc01037 = "GAS",
                      // sc01066 ="sc01066",
                      sc01106 ="01106",
                }
            };

            return _productInventoryEntityList;
        }
        [TestMethod]
        public void GetProductByDescriptionwithEmptyDescriptionTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductByDescription(COMPANY_CODE, "");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorInfo.Any());
        }
        [TestMethod]
        public void GetProductByDescriptionExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _productInventoryEntityList = new List<ProductInventoryEntity>
            {
                new ProductInventoryEntity
                {
                    sc01001 = "",
                    sc01002 = "COOLER TUBES-",
                    sc01003 = "",
                    sc01037 = "GAS"
                }
            };
            mockExceptionRepository.Stub(x => x.GetProductInvetoryByProductName("bh", "007"))
              .IgnoreArguments()
              .Return(_productInventoryEntityList);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductByDescription(COMPANY_CODE, "001");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductByDescriptionInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductByDescription(string.Empty, "001");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductByLocationIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductSearchByDescriptionResponse();
            //data.ProductList.AddRange()

            mockRepository.Stub(x => x.GetProductInvetoryByLocationId("bh", "007"))
                            .IgnoreArguments()
                            .Return(_productInventoryEntityList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductByLocationId(COMPANY_CODE, "70");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductByLocationIdwithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetProductInvetoryByLocationId("bh", "007"))
              .IgnoreArguments()
              .Return(new List<ProductInventoryEntity>());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductByLocationId(COMPANY_CODE, "70");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductByLocationIdExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _productInventoryEntityList = new List<ProductInventoryEntity>
            {
                new ProductInventoryEntity
                {
                    sc01001 = "",
                    sc01002 = "COOLER TUBES-",
                    sc01003 = "",
                    sc01037 = "GAS"
                }
            };
            mockExceptionRepository.Stub(x => x.GetProductInvetoryByLocationId("bh", "007"))
              .IgnoreArguments()
              .Return(_productInventoryEntityList);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductByLocationId(COMPANY_CODE, "70");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductByLocationIdInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductByLocationId(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductStockBalanceQuantityTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetItemWareHouse("bh", "007", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemsEnttiesList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductStockBalanceQuantity(COMPANY_CODE, "001", "70");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductStockBalanceQuantitywithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetItemWareHouse("bh", "007", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductStockBalanceQuantity(COMPANY_CODE, "001", "70");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductStockBalanceQuantitywithNullWarehouseListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetItemWareHouse("bh", "007", "70"))
              .IgnoreArguments()
              .Return(new List<ItemWarehouse> { null });

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductStockBalanceQuantity(COMPANY_CODE, "001", "70");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorInfo.Any());
        }

        [TestMethod]
        public void GetProductStockBalanceQuantitywithZeroItemTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetItemWareHouse("bh", "007", "70"))
              .IgnoreArguments()
              .Return(new List<ItemWarehouse>());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductStockBalanceQuantity(COMPANY_CODE, "001", "70");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorInfo.Any());
        }
        [TestMethod]
        public void GetProductStockBalanceQuantityExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _stockItemsEnttiesList = new List<ItemWarehouse>
            {
                new ItemWarehouse
                {
                    sc03001 = "007-08831-000",
                    sc03002 = "01",
                    sc03003 = "0.00000",
                    sc03004 = "0.00000",
                    sc03005 = "0.00000"
                }
            };
            mockExceptionRepository.Stub(x => x.GetItemWareHouse("bh", "007", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductStockBalanceQuantity(COMPANY_CODE, "001", "70");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductStockBalanceQuantityInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductStockBalanceQuantity(string.Empty, "001", string.Empty);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductStockBalanceQuantityForAllLocationTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", "007"))
                            .IgnoreArguments()
                            .Return(_stockItemsEnttiesList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductStockBalanceQuantityForAllLocation(COMPANY_CODE, "001");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductStockBalanceQuantityForAllLocationwithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", "007"))
              .IgnoreArguments()
              .Return(new List<ItemWarehouse>());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductStockBalanceQuantityForAllLocation(COMPANY_CODE, "001");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductStockBalanceQuantityForAllLocationExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _stockItemsEnttiesList = new List<ItemWarehouse>
            {
                new ItemWarehouse
                {
                    sc03001 = "007-08831-000",
                    sc03002 = "01",
                    sc03003 = "0.00000",
                    sc03004 = "0.00000",
                    sc03005 = "0.00000"
                }
            };
            mockExceptionRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", "007"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductStockBalanceQuantityForAllLocation(COMPANY_CODE, "001");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductStockBalanceQuantityForAllLocationInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductStockBalanceQuantityForAllLocation(string.Empty, "001");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationwiseProductStockBalanceQuantityTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetItemWareHouseByLocationId("bh", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemsEnttiesList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetLocationwiseProductStockBalanceQuantity(COMPANY_CODE, "70");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetLocationwiseProductStockBalanceQuantitywithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetItemWareHouseByLocationId("bh", "70"))
              .IgnoreArguments()
              .Return(new List<ItemWarehouse>());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetLocationwiseProductStockBalanceQuantity(COMPANY_CODE, "70");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetLocationwiseProductStockBalanceQuantityExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _stockItemsEnttiesList = new List<ItemWarehouse>
            {
                new ItemWarehouse
                {
                    sc03001 = "007-08831-000",
                    sc03002 = "01",
                    sc03003 = "0.00000",
                    sc03004 = "0.00000",
                    sc03005 = "0.00000"
                }
            };
            mockExceptionRepository.Stub(x => x.GetItemWareHouseByLocationId("bh", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetLocationwiseProductStockBalanceQuantity(COMPANY_CODE, "001");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetLocationwiseProductStockBalanceQuantityInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetLocationwiseProductStockBalanceQuantity(string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductAvailableQuantityTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetItemWareHouse("bh", "70", "001"))
                            .IgnoreArguments()
                            .Return(_stockItemsEnttiesList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductAvailableQuantity(COMPANY_CODE, "007-001", "70");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductAvailableQuantitywithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetItemWareHouse("bh", "70", "001"))
              .IgnoreArguments()
              .Return(new List<ItemWarehouse>());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductAvailableQuantity(COMPANY_CODE, "007-001", "70");
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetProductAvailableQuantitywithEmptyLocationIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductAvailableQuantity(COMPANY_CODE, "007-001", "");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorInfo.Any());
        }

        [TestMethod]
        public void GetProductAvailableQuantityExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _stockItemsEnttiesList = new List<ItemWarehouse>
            {
                new ItemWarehouse
                {
                    sc03001 = "007-08831-000",
                    sc03002 = "01",
                    sc03003 = "0.00000",
                    sc03004 = "0.00000",
                    sc03005 = "0.00000"
                }
            };
            mockExceptionRepository.Stub(x => x.GetItemWareHouse("bh", "70", "001"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductAvailableQuantity(COMPANY_CODE, "007-001", "70");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductAvailableQuantityInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductAvailableQuantity(string.Empty, "007-001", "70");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductAvailableQuantityForAllLocationTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemsEnttiesList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductAvailableQuantityForAllLocation(COMPANY_CODE, "007-001");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductAvailableQuantityForAllLocationwithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(new List<ItemWarehouse>());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductAvailableQuantityForAllLocation(COMPANY_CODE, "007-001");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductAvailableQuantityForAllLocationExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _stockItemsEnttiesList = new List<ItemWarehouse>
            {
                new ItemWarehouse
                {
                    sc03001 = "007-08831-000",
                    sc03002 = "01",
                    sc03003 = "0.00000",
                    sc03004 = "0.00000",
                    sc03005 = "0.00000"
                }
            };
            mockExceptionRepository.Stub(x => x.GetItemWareHouseByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductAvailableQuantityForAllLocation(COMPANY_CODE, "007-001");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductAvailableQuantityForAllLocationInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductAvailableQuantityForAllLocation(string.Empty, "007-001");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLocationwiseProductAvailableQuantityTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetItemWareHouseByLocationId("bh", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemsEnttiesList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetLocationwiseProductAvailableQuantity(COMPANY_CODE, "01");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetLocationwiseProductAvailableQuantitywithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetItemWareHouseByLocationId("bh", "70"))
              .IgnoreArguments()
              .Return(new List<ItemWarehouse>());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetLocationwiseProductAvailableQuantity(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetLocationwiseProductAvailableQuantityExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockExceptionRepository.Stub(x => x.GetItemWareHouseByLocationId("bh", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetLocationwiseProductAvailableQuantity(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetLocationwiseProductAvailableQuantityInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetLocationwiseProductAvailableQuantity(string.Empty, "01");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductFamilyTypeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemMasterEntity);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductFamilyType(COMPANY_CODE, "01");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductFamilyTypewithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(new StockItemMaster());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductFamilyType(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductFamilyTypewithEmptyProductCodeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductFamilyType(COMPANY_CODE, "");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorInfo.Any());
        }
        [TestMethod]
        public void GetProductFamilyTypeExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockExceptionRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductFamilyType(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductFamilyTypeInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductFamilyType(string.Empty, "01");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductLineTypeTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemMasterEntity);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductLineType(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
            //Assert.AreEqual(result.LineType, "Refrigerant");

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductLineTypewithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(new StockItemMaster());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProductLineType(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductLineTypeExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockExceptionRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProductLineType(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductLineTypeInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProductLineType(string.Empty, "01");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IsProductActiveTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemMasterEntity);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.IsProductActive(COMPANY_CODE, "01");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void IsProductActivewithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(new StockItemMaster());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.IsProductActive(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IsProductActiveExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockExceptionRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.IsProductActive(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IsProductActiveInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.IsProductActive(string.Empty, "01");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IsProductStockableTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemMasterEntity);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.IsProductStockable(COMPANY_CODE, "01");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void IsProductStockablewithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(new StockItemMaster());

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.IsProductStockable(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IsProductStockableExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            mockExceptionRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.IsProductStockable(COMPANY_CODE, "01");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IsProductStockableInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.IsProductStockable(string.Empty, "01");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProductTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            //var data = new Model.Response.ProductStockBalanceQuantityResponse();
            //data.StockBalanceQuantity = 0;

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
                            .IgnoreArguments()
                            .Return(_stockItemMasterEntity);

            mockRepository.Stub(x => x.GetItemWareHouse("bh", "001", "70"))
                .IgnoreArguments()
                .Return(_stockItemsEnttiesList);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProduct(COMPANY_CODE, "01", "70");
            Assert.IsNotNull(result);

            ////Assert to check the BillingStreet Validation Rules
            //Assert.AreEqual(result.Customers[0].BillingStreet, String.Format(customerList[0].sl01003 + "{0}" + customerList[0].sl01004 + "{0}" + customerList[0].sl01005 + "{0}" + customerList[0].sl01099 + "{0}" +
            //                    customerList[0].sl0194 + "{0}" + customerList[0].sl01195 + "{0}" + customerList[0].sl01196, Environment.NewLine));
            ////Assert to check the length of currecyIsoCode Validation Rule
            //Assert.AreEqual(result.Customers[0].CurrencyIsoCode.Length, 3);

            ////Assert to check the ERP Key Validation Rule
            //Assert.AreEqual(result.Customers[0].ERP_Key, "I" + companyCode + customerList[0].sl01001);

            ////Assert to check the Payment_Terms_Code__c Validation Rule
            //Assert.AreEqual(result.Customers[0].Payment_Terms_Code__c, customerList[0].sl01024.Substring(0, 2));
        }
        [TestMethod]
        public void GetProductwithEmptyListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IDatabaseContext>();
            SetMockDataForProductModels();

            mockRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(null);

            _inventoryManager = new ProductInventoryManager(mockRepository);

            var result = _inventoryManager.GetProduct(COMPANY_CODE, "01", "70");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductExceptionTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();


            StockItemMaster stockItm = new StockItemMaster
            {
                sc01001 = "",
                sc01002 = "COOLER TUBES-",
                sc01003 = "",
                sc01037 = "GAS"

            };

            mockExceptionRepository.Stub(x => x.GetStockItemByProductCode("bh", "70"))
              .IgnoreArguments()
              .Return(stockItm);

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);

            var result = _inventoryManager.GetProduct(COMPANY_CODE, "01", "70");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetProductInputValidationTest()
        {
            var mockExceptionRepository = MockRepository.GenerateMock<IDatabaseContext>();

            _inventoryManager = new ProductInventoryManager(mockExceptionRepository);
            var result = _inventoryManager.GetProduct(string.Empty, string.Empty, string.Empty);
            Assert.IsNotNull(result);
        }
        #endregion

        #region MockData Methods
        /// <summary>
        /// To create a mock data of product for unit test
        /// </summary>
        public void SetMockDataForProductModels()
        {
            #region SampleDataProductEntity
            _productInventoryEntityList.Add(new ProductInventoryEntity
            {
                sc01001 = "007-08831-000",
                sc01002 = "TUBE-",
                sc01003 = "",
                sc01037 = "GAS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0",
                sc03001 = "007-08831-000",
                sc03002 = "01",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });

            _productInventoryEntityList.Add(new ProductInventoryEntity
            {
                sc01001 = "007-08831-000",
                sc01002 = "TUBE-",
                sc01003 = "",
                sc01037 = "GAS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0",
                sc03001 = "007-08831-000",
                sc03002 = "01",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });
            _productInventoryEntityList.Add(new ProductInventoryEntity
            {
                sc01001 = "007-09212-000",
                sc01002 = "COOLER TUBES-",
                sc01003 = "",
                sc01037 = "GAS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0",
                sc03001 = "007-08831-000",
                sc03002 = "01",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });
            _productInventoryEntityList.Add(new ProductInventoryEntity
            {
                sc01001 = "007-09212-000",
                sc01002 = "COOLER TUBES-",
                sc01003 = "",
                sc01037 = "GAS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0",
                sc03001 = "007-08831-000",
                sc03002 = "01",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });
            #endregion

            #region MockDataForProductMasterModel

            new ProductMasterModel
            {
                //ERP_Stock_Code__c = "007-08831-000",
                Description = "TUBE-",
                //ERP_Available_Quantity_c = "",
                ERP_Company_Code__c = "",
                ERP_ID__c = "",
                //ERP_Location_ID_c = "",
                //ERP_Reserved_Quantity_c = "",
                //ERP_Stock_Location_Code_Key__c = "",
                Family = "",
                IsActive = true,
                Name = "",
                ProductCode = "",
                //SVMXC__Allocated_Qty__c = "",
                //SVMXC__Available_Qty__c = "",
                //SVMXC__IsPartner__c = "",
                //SVMXC__IsPartnerRecord__c = "",
                //SVMXC__Location__c = "",
                //SVMXC__Partner_Account__c = "",
                //SVMXC__Partner_Contact__c = "",
                //SVMXC__Product__c = "",
                SVMXC__Product_Cost__c = 0.0,
                SVMXC__Product_Line__c = "",
                //SVMXC__Quantity2__c = "",
                //SVMXC__Reorder_Level2__c = "",
                //SVMXC__Reorder_Quantity2__c = "",
                //SVMXC__Required_Quantity2__c = "",
                //SVMXC__Status__c = "",
                //SVMXC__Stock_Value__c = "",
                SVMXC__Stockable__c = 1,
                SVMXC__Unit_Of_Measure__c = 1
            };



            #endregion

            #region MockDataForProductWarehouselist
            _productWarehouseModelList.Add(new ProductWarehouseModel
            {
                ERP_Stock_Code__c = "007-08831-000",
                ERP_Location_ID__c = "01",
                SVMXC__Quantity2__c = 0.00000,
                ERP_Reserved_Quantity_c = 0.00000
                //SVMXC__Available_Qty__c = 0
            });

            _productWarehouseModelList.Add(new ProductWarehouseModel
            {
                ERP_Stock_Code__c = "007-08831-000",
                ERP_Location_ID__c = "70",
                SVMXC__Quantity2__c = 0.00000,
                ERP_Reserved_Quantity_c = 0.00000
                //SVMXC__Available_Qty__c = 0
            });

            #endregion

            #region SampleDataProduct
            _stockItemsEnttiesList.Add(new ItemWarehouse
            {
                sc03001 = "007-08831-000",
                sc03002 = "01",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });

            _stockItemsEnttiesList.Add(new ItemWarehouse
            {
                sc03001 = "007-08831-000",
                sc03002 = "70",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });

            _stockItemMasterEntity = new StockItemMaster
            {
                sc01001 = "007-08831-000",
                sc01002 = "TUBE-",
                sc01003 = "",
                sc01037 = "GAS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0"
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
            //    sc03002 = "01",
            //    sc03003 = 0.00000,
            //    sc03004 = 0.00000,
            //    SC03005 = 0.00000
            //});
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

