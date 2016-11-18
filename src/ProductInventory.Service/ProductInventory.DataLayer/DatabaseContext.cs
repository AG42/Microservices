using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DenodoAdapter;
using ProductInventory.Common.Logger;
using ProductInventory.DataLayer.Entities;
using ProductInventory.DataLayer.Entities.Datalake;
using ProductInventory.DataLayer.Interfaces;
using Microservices.Common.Interface;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using ProductInventory.Common;

namespace ProductInventory.DataLayer
{
    public class DatabaseContext : IDatabaseContext
    {
        [Import]
        public IDatabase objDb { get; set; }

        private readonly ConfigReader _configReader;
        // private readonly IDatalakeEntities _datalakeEntitieses;

        public DatabaseContext()
        {
            _configReader = new ConfigReader();
            GetContainer();
            objDb.ConnectionString = _configReader.DatalakeConnectionString;

        }
        public void GetContainer()
        {
            ConfigReader _configReader = new ConfigReader();
            DirectoryCatalog catalog = new DirectoryCatalog(_configReader.ReadConfig(Constants.DataSourceLibraryPath));
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        private static void LogException(Exception exception)
        {
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }

        public IEnumerable<ItemWarehouse> GetItemWareHouse(string companyCode, string productCode, string locationId)
        {

            //var dictTableName = new Dictionary<string, string>();
            ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouse : Reading datalake table name from config");
            var dictItemWareouseTableNameDetail = _configReader.GetDatabaseTableName(companyCode, Constants.DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY, Constants.DATALAKE_ITEMWAREHOUSE_Column_NAME_KEY);
            string itemWareHouseTableName = dictItemWareouseTableNameDetail[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake table: [{itemWareHouseTableName}]");
            //                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, ITEMWAREHOUSE_VIEWURI_KEY)}?{ITEMWAREHOUSE_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}{AND_OPERATOR}{ITEMWAREHOUSE_LOCATIONID_FIELD}{EQUAL_OPERATOR}{locationId}";
            //                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
            //                var itemWarehouseList = DenodoContext.GetData<ItemWarehouse>(finalViewUri);
            var itemWarehouseList = objDb.Where<ItemWarehouse>(itemWareHouseTableName, dictItemWareouseTableNameDetail[Constants.ColumnNameKey], $"trim(lower({Constants.ITEMWAREHOUSE_PRODUCTCODE_FIELD})){Constants.EQUAL_OPERATOR}'{productCode.ToLower().Trim()}' AND trim(lower({Constants.ITEMWAREHOUSE_LOCATIONID_FIELD})){Constants.EQUAL_OPERATOR}'{locationId.ToLower().Trim()}'");
            ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouseList.Count()}");
            return itemWarehouseList;

        }

        public StockItemMaster GetStockItemByProductCode(string companyCode, string productCode)
        {
            //var dictTableName = new Dictionary<string, string>();
            ApplicationLogger.InfoLogger("DataLayer :: GetStockItemByProductCode : Reading datalake table name from config");
            var dictStockMasterTableNameDetail = _configReader.GetDatabaseTableName(companyCode, Constants.DATALAKE_STOCKMASTER_TABLE_NAME_KEY, Constants.DATALAKE_STOCKMASTER_Column_NAME_KEY);
            ApplicationLogger.InfoLogger($"Datalake table: [{dictStockMasterTableNameDetail[Constants.TableNameKey]}]");
            //                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, STOCKITEM_VIEWURI_KEY)}?{STOCKITEM_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}";
            //                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
            //                var stockItem = DenodoContext.GetData<StockItemMaster>(finalViewUri);
            var stockItem = objDb.Where<StockItemMaster>(dictStockMasterTableNameDetail[Constants.TableNameKey], dictStockMasterTableNameDetail[Constants.ColumnNameKey], $"trim(lower({Constants.STOCKITEM_PRODUCTCODE_FIELD})){Constants.EQUAL_OPERATOR}'{productCode.ToLower().Trim()}'");
            ApplicationLogger.InfoLogger($"StockItem count: {stockItem.Count()}");
            return stockItem.FirstOrDefault();

        }

        public IEnumerable<ItemWarehouse> GetItemWareHouseByProductCode(string companyCode, string productCode)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouseByProductCode : Reading datalake table name from config");
            var dictItemWareHosueTableDetail = _configReader.GetDatabaseTableName(companyCode, Constants.DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY, Constants.DATALAKE_ITEMWAREHOUSE_Column_NAME_KEY);
            ApplicationLogger.InfoLogger($"Datalake table: [{dictItemWareHosueTableDetail[Constants.TableNameKey]}]");
            //                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, ITEMWAREHOUSE_VIEWURI_KEY)}?{ITEMWAREHOUSE_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}";
            //                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
            //                var itemWarehouse = DenodoContext.GetData<ItemWarehouse>(finalViewUri);
            var itemWarehouse = objDb.Where<ItemWarehouse>(dictItemWareHosueTableDetail[Constants.TableNameKey], dictItemWareHosueTableDetail[Constants.ColumnNameKey], $"trim(lower({Constants.ITEMWAREHOUSE_PRODUCTCODE_FIELD})){Constants.EQUAL_OPERATOR}'{productCode.ToLower().Trim()}'");
            ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouse.Count()}");
            return itemWarehouse;

        }
        public IEnumerable<ItemWarehouse> GetItemWareHouseByLocationId(string companyCode, string locationId)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouseByLocationId : Reading datalake table name from config");
            var dictItemWareHouseTableNameDetail = _configReader.GetDatabaseTableName(companyCode, Constants.DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY, Constants.DATALAKE_ITEMWAREHOUSE_Column_NAME_KEY);
            ApplicationLogger.InfoLogger($"Datalake table: [{dictItemWareHouseTableNameDetail[Constants.TableNameKey]}]");
            var itemWarehouse = objDb.Where<ItemWarehouse>(dictItemWareHouseTableNameDetail[Constants.TableNameKey], dictItemWareHouseTableNameDetail[Constants.ColumnNameKey], $"trim(lower({Constants.ITEMWAREHOUSE_LOCATIONID_FIELD})){Constants.EQUAL_OPERATOR}'{locationId.ToLower().Trim()}'");
            ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouse.Count()}");
            return itemWarehouse;
        }

        public IEnumerable<ProductInventoryEntity> GetProductInvetoryByProductName(string companyCode, string productName)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetProductInvetoryByProductName : Reading datalake table name from config");
            var dictStockMasterTableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.DATALAKE_STOCKMASTER_TABLE_NAME_KEY, Constants.DATALAKE_STOCKMASTER_Column_NAME_KEY);
            string stockMasterTableName = dictStockMasterTableDetails[Constants.TableNameKey];
            var dictItemWarehouseTableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY, Constants.DATALAKE_ITEMWAREHOUSE_Column_NAME_KEY);
            string itemWarehouseTableName = dictItemWarehouseTableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{stockMasterTableName}], [{itemWarehouseTableName}]");
            string joinCondition = $" sc01 JOIN {itemWarehouseTableName} sc03 ON sc01.{Constants.STOCKITEM_PRODUCTCODE_FIELD} = sc03.{Constants.ITEMWAREHOUSE_PRODUCTCODE_FIELD}";
            string whereCondition = $"lower(CONCAT(sc01.{Constants.STOCKITEM_DESCRIPTION1_FIELD}, ' ', sc01.{Constants.STOCKITEM_DESCRIPTION2_FIELD})) {Constants.LIKE_OPERATOR} '%{productName.ToLower().Trim()}%'";
            string joincolumn = $"{dictStockMasterTableDetails[Constants.ColumnNameKey]}, {dictItemWarehouseTableDetails[Constants.ColumnNameKey]}";
            var productInventory = objDb.WhereJoin<ProductInventoryEntity>(dictStockMasterTableDetails[Constants.TableNameKey], joincolumn, joinCondition, whereCondition);
            //string filter = $"lower({STOCKITEM_PRODUCTNAME_FIELD}) {LIKE_OPERATOR} '%{productName.ToLower()}%'";
            ApplicationLogger.InfoLogger($"ProductInventory count: {productInventory.Count()}");
            return productInventory;
        }

        public IEnumerable<ProductInventoryEntity> GetProductInvetoryByLocationId(string companyCode, string locationId)
        {
            ApplicationLogger.InfoLogger("DataLayer :: GetProductInvetoryByLocationId : Reading datalake table name from config");
            var dictStockMasterTableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.DATALAKE_STOCKMASTER_TABLE_NAME_KEY, Constants.DATALAKE_STOCKMASTER_Column_NAME_KEY);
            string stockMasterTableName = dictStockMasterTableDetails[Constants.TableNameKey];
            var dictItemWarehouseTableDetails = _configReader.GetDatabaseTableName(companyCode, Constants.DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY, Constants.DATALAKE_ITEMWAREHOUSE_Column_NAME_KEY);
            string itemWarehouseTableName = dictItemWarehouseTableDetails[Constants.TableNameKey];
            ApplicationLogger.InfoLogger($"Datalake tables: [{stockMasterTableName}], [{itemWarehouseTableName}]");
            string joinCondition = $" sc01 JOIN {itemWarehouseTableName} sc03 ON sc01.{Constants.STOCKITEM_PRODUCTCODE_FIELD} = sc03.{Constants.ITEMWAREHOUSE_PRODUCTCODE_FIELD}";
            string whereCondition = $"trim(lower(sc03.{Constants.ITEMWAREHOUSE_LOCATIONID_FIELD})) {Constants.EQUAL_OPERATOR} '{locationId.ToLower().Trim()}'";
            string joincolumn = $"{dictStockMasterTableDetails[Constants.ColumnNameKey]}, {dictItemWarehouseTableDetails[Constants.ColumnNameKey]}";
            var productInventory = objDb.WhereJoin<ProductInventoryEntity>(stockMasterTableName, joincolumn, joinCondition, whereCondition);
            ApplicationLogger.InfoLogger($"ProductInventory count: {productInventory.Count()}");
            return productInventory;

        }

    }
}
