using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DenodoAdapter;
using ProductInventory.Common.Logger;
using ProductInventory.DataLayer.Entities;
using ProductInventory.DataLayer.Entities.Datalake;
using ProductInventory.DataLayer.Interfaces;

namespace ProductInventory.DataLayer
{
    public class DatabaseContext: IDatabaseContext
    {
        private const string LIKE_OPERATOR = "like";
        private const string EQUAL_OPERATOR = "=";
        private const string AND_OPERATOR = "&";
        private const string STOCKITEM_PRODUCTCODE_FIELD = "sc01001";
        private const string STOCKITEM_DESCRIPTION1_FIELD = "sc01002";
        private const string STOCKITEM_DESCRIPTION2_FIELD = "sc01003";
        private const string ITEMWAREHOUSE_PRODUCTCODE_FIELD = "sc03001";
        private const string ITEMWAREHOUSE_LOCATIONID_FIELD = "sc03002";
        private const string DATALAKE_STOCKMASTER_TABLE_NAME_KEY = "DatalakeStockMasterTableName";
        private const string DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY = "DatalakeItemWarehouseTableName";
//        private const string STOCKITEM_VIEWURI_KEY = "StockItemViewUri";
//        private const string ITEMWAREHOUSE_VIEWURI_KEY = "ItemWarehouseViewUri";
//        private const string PRODUCTINVENTORY_VIEWURI_KEY = "ProductInventoryViewUri";
        public IDenodoContext DenodoContext { get; set;} 
        private readonly string _denodoUrl;
        private readonly ConfigReader _configReader;
        private readonly IDatalakeEntities _datalakeEntitieses;

        public DatabaseContext(IDatalakeEntities datalakeEntities)
        {
            try
            {
                _configReader = new ConfigReader();
                //                _denodoUrl = _configReader.BaseUri;
                //                DenodoContext = new DenodoContext(_denodoUrl, _configReader.DenodoUsername, _configReader.DenodoPassword);
                _datalakeEntitieses = datalakeEntities;
                _datalakeEntitieses.ConnectionString = _configReader.DatalakeConnectionString;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace, exception.InnerException);
                throw;
            }
        }

        private static void LogException(Exception exception)
        {
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }

        public IEnumerable<ItemWarehouse> GetItemWareHouse(string companyCode, string productCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouse : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                //                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, ITEMWAREHOUSE_VIEWURI_KEY)}?{ITEMWAREHOUSE_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}{AND_OPERATOR}{ITEMWAREHOUSE_LOCATIONID_FIELD}{EQUAL_OPERATOR}{locationId}";
                //                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
                //                var itemWarehouseList = DenodoContext.GetData<ItemWarehouse>(finalViewUri);
                var itemWarehouseList = _datalakeEntitieses.Where<ItemWarehouse>(tableName,$"trim(lower({ITEMWAREHOUSE_PRODUCTCODE_FIELD})){EQUAL_OPERATOR}'{productCode.ToLower().Trim()}' AND trim(lower({ITEMWAREHOUSE_LOCATIONID_FIELD})){EQUAL_OPERATOR}'{locationId.ToLower().Trim()}'");
                ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouseList.Count()}");
                return itemWarehouseList;
            }
            catch (Exception exception) 
            {
                LogException(exception);
                throw;
            }
        }

        public StockItemMaster GetStockItemByProductCode(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetStockItemByProductCode : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_STOCKMASTER_TABLE_NAME_KEY);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                //                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, STOCKITEM_VIEWURI_KEY)}?{STOCKITEM_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}";
                //                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
                //                var stockItem = DenodoContext.GetData<StockItemMaster>(finalViewUri);
                var stockItem = _datalakeEntitieses.Where<StockItemMaster>(tableName, $"trim(lower({STOCKITEM_PRODUCTCODE_FIELD})){EQUAL_OPERATOR}'{productCode.ToLower().Trim()}'");
                ApplicationLogger.InfoLogger($"StockItem count: {stockItem.Count()}");
                return stockItem.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public IEnumerable<ItemWarehouse> GetItemWareHouseByProductCode(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouseByProductCode : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                //                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, ITEMWAREHOUSE_VIEWURI_KEY)}?{ITEMWAREHOUSE_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}";
                //                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
                //                var itemWarehouse = DenodoContext.GetData<ItemWarehouse>(finalViewUri);
                var itemWarehouse = _datalakeEntitieses.Where<ItemWarehouse>(tableName, $"trim(lower({ITEMWAREHOUSE_PRODUCTCODE_FIELD})){EQUAL_OPERATOR}'{productCode.ToLower().Trim()}'");
                ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouse.Count()}");
                return itemWarehouse;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public IEnumerable<ItemWarehouse> GetItemWareHouseByLocationId(string companyCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouseByLocationId : Reading datalake table name from config");
                string tableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY);
                ApplicationLogger.InfoLogger($"Datalake table: [{tableName}]");
                var itemWarehouse = _datalakeEntitieses.Where<ItemWarehouse>(tableName, $"trim(lower({ITEMWAREHOUSE_LOCATIONID_FIELD})){EQUAL_OPERATOR}'{locationId.ToLower().Trim()}'");
                ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouse.Count()}");
                return itemWarehouse;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public IEnumerable<ProductInventoryEntity> GetProductInvetoryByProductName(string companyCode, string productName)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProductInvetoryByProductName : Reading datalake table name from config");
                string stockMasterTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_STOCKMASTER_TABLE_NAME_KEY);
                string itemWarehouseTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY);
                string joinCondition = $" sc01 JOIN {itemWarehouseTableName} sc03 ON sc01.{STOCKITEM_PRODUCTCODE_FIELD} = sc03.{ITEMWAREHOUSE_PRODUCTCODE_FIELD}";
                string whereCondition = $"lower(CONCAT(sc01.{STOCKITEM_DESCRIPTION1_FIELD}, ' ', sc01.{STOCKITEM_DESCRIPTION2_FIELD})) {LIKE_OPERATOR} '%{productName.ToLower().Trim()}%'";
                var productInventory = _datalakeEntitieses.WhereJoin<ProductInventoryEntity>(stockMasterTableName, joinCondition, whereCondition);
                //string filter = $"lower({STOCKITEM_PRODUCTNAME_FIELD}) {LIKE_OPERATOR} '%{productName.ToLower()}%'";
                return productInventory;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public IEnumerable<ProductInventoryEntity> GetProductInvetoryByLocationId(string companyCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProductInvetoryByLocationId : Reading datalake table name from config");
                //                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, PRODUCTINVENTORY_VIEWURI_KEY)}?{ITEMWAREHOUSE_LOCATIONID_FIELD}{EQUAL_OPERATOR}{locationId}";
                //                ApplicationLogger.InfoLogger($"Denodo url: [{_denodoUrl}{finalViewUri}]");
                //                var productInventory = DenodoContext.GetData<ProductInventoryEntity>(finalViewUri);
                //                ApplicationLogger.InfoLogger($"ProductInventory count: {productInventory.Count}");
                string stockMasterTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_STOCKMASTER_TABLE_NAME_KEY);
                string itemWarehouseTableName = _configReader.GetDatalakeTableName(companyCode, DATALAKE_ITEMWAREHOUSE_TABLE_NAME_KEY);
                string joinCondition = $" sc01 JOIN {itemWarehouseTableName} sc03 ON sc01.{STOCKITEM_PRODUCTCODE_FIELD} = sc03.{ITEMWAREHOUSE_PRODUCTCODE_FIELD}";
                string whereCondition = $"trim(lower(sc03.{ITEMWAREHOUSE_LOCATIONID_FIELD})) {EQUAL_OPERATOR} '{locationId.ToLower().Trim()}'";
                var productInventory = _datalakeEntitieses.WhereJoin<ProductInventoryEntity>(stockMasterTableName, joinCondition, whereCondition);
                return productInventory;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

    }
}
