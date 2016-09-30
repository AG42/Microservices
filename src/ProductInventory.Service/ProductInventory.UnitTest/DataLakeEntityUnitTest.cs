using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.Generic;
using ProductInventory.DataLayer.Interfaces;
using ProductInventory.DataLayer.Entities.Datalake;
using ProductInventory.Model;
using ProductInventory.DataLayer.Entities;

namespace ProductInventory.UnitTest
{
    [TestClass]
    public class DataLakeEntityUnitTest
    {
        #region Declaration
        IDatalakeAdapter _datalakeAdapter;
        readonly List<ProductInventoryEntity> _productInventoryEntityList = new List<ProductInventoryEntity>();
        StockItemMaster _stockItemMasterEntity = new StockItemMaster();
        readonly List<StockItemMaster> _stockItemMasterList = new List<StockItemMaster>();
        readonly List<ProductWarehouseModel> _productWarehouseModelList = new List<ProductWarehouseModel>();
        readonly List<ItemWarehouse> _stockItemsEntitiesList = new List<ItemWarehouse>();

        //Sl01 _sl01 = new Sl01();
        //public List<Sl01> CustomerList = new List<Sl01>();

        #endregion

        #region Methods
        [TestInitialize]
        public void Initialize()
        {
            _datalakeAdapter = MockRepository.GenerateMock<IDatalakeAdapter>();
            _datalakeAdapter.ConnectionString = "TestConnectionString   ";
        }


        [TestMethod]
        public void GetDataFromTable()
        {
            _datalakeAdapter.Stub(x => x.Get<ItemWarehouse>("")).IgnoreArguments().Return(_stockItemsEntitiesList);
            var data = new DatalakeEntities(_datalakeAdapter);
            var result = data.Get<ItemWarehouse>("tableName");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDataFromTableWithWhereCondition()
        {
            _datalakeAdapter.Stub(x => x.Get<StockItemMaster>("")).IgnoreArguments().Return(_stockItemMasterList);
            var data = new DatalakeEntities(_datalakeAdapter);
            var result = data.Where<StockItemMaster>("tableName","whereCondition");
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
                sc01037 = "ESGS",
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
                sc01037 = "ESGS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0",
                sc03001 = "007-08831-000",
                sc03002 = "70",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });
            _productInventoryEntityList.Add(new ProductInventoryEntity
            {
                sc01001 = "007-09212-000",
                sc01002 = "COOLER TUBES-",
                sc01003 = "",
                sc01037 = "ESGS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0",
                sc03001 = "007-08831-000",
                sc03002 = "70",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });
            _productInventoryEntityList.Add(new ProductInventoryEntity
            {
                sc01001 = "007-09212-000",
                sc01002 = "COOLER TUBES-",
                sc01003 = "",
                sc01037 = "ESGS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0",
                sc03001 = "007-08831-000",
                sc03002 = "70",
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
            _stockItemsEntitiesList.Add(new ItemWarehouse
            {
                sc03001 = "007-08831-000",
                sc03002 = "01",
                sc03003 = "0.00000",
                sc03004 = "0.00000",
                sc03005 = "0.00000"
            });

            _stockItemsEntitiesList.Add(new ItemWarehouse
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
                sc01037 = "ESGS",
                sc01066 = "0",
                sc01106 = "0.00000",
                sc01120 = "0",
                sc01135 = "0"
            };

            _stockItemMasterList.Add(_stockItemMasterEntity);
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
