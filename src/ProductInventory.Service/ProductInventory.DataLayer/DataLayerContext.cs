using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DenodoAdapter;
using ProductInventory.Common.Logger;
using ProductInventory.DataLayer.Entities;
using ProductInventory.DataLayer.Interfaces;

namespace ProductInventory.DataLayer
{
    public class DataLayerContext: IDataLayerContext
    {
        private const string LIKE_OPERATOR = "like";
        private const string EQUAL_OPERATOR = "=";
        private const string AND_OPERATOR = "&";
        private const string STOCKITEM_PRODUCTCODE_FIELD = "sc01001";
        private const string STOCKITEM_PRODUCTNAME_FIELD = "sc01002003";
        private const string ITEMWAREHOUSE_PRODUCTCODE_FIELD = "sc03001";
        private const string ITEMWAREHOUSE_LOCATIONID_FIELD = "sc03002";
        private const string STOCKITEM_VIEWURI_KEY = "StockItemViewUri";
        private const string ITEMWAREHOUSE_VIEWURI_KEY = "ItemWarehouseViewUri";
        private const string PRODUCTINVENTORY_VIEWURI_KEY = "ProductInventoryViewUri";
        public IDenodoContext DenodoContext { get; set;} 
        private readonly string _denodoUrl;
        private readonly ConfigReader _configReader;

        public DataLayerContext()
        {
            try
            {
                _configReader = new ConfigReader();
                _denodoUrl = _configReader.BaseUri;
                DenodoContext = new DenodoContext(_denodoUrl, _configReader.DenodoUsername, _configReader.DenodoPassword);
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace, exception.InnerException);
                throw;
            }
        }

        private static void LogException(Exception exception)
        {
            if (exception.GetType() == typeof(HttpResponseException))
            {
                HttpResponseException responseException = (HttpResponseException)exception;
                ApplicationLogger.Errorlog(responseException.Response.ReasonPhrase, Category.Database,
                    responseException.Response.Content.ReadAsStringAsync().Result, responseException.InnerException);
                ApplicationLogger.InfoLogger(
                    $"Denodo Adapter exception :: {responseException.Response.ReasonPhrase}");
                throw responseException;
            }
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }

        public List<ItemWarehouse> GetItemWareHouse(string companyCode, string productCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouse :: Reading view uri from config");
                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, ITEMWAREHOUSE_VIEWURI_KEY)}?{ITEMWAREHOUSE_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}{AND_OPERATOR}{ITEMWAREHOUSE_LOCATIONID_FIELD}{EQUAL_OPERATOR}{locationId}";
                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
                var itemWarehouseList = DenodoContext.GetData<ItemWarehouse>(finalViewUri);
                ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouseList.Count}");
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
                ApplicationLogger.InfoLogger("DataLayer :: GetStockItemByProductCode :: Reading view uri from config");
                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, STOCKITEM_VIEWURI_KEY)}?{STOCKITEM_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}";
                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
                var stockItem = DenodoContext.GetData<StockItemMaster>(finalViewUri);
                ApplicationLogger.InfoLogger($"StockItem count: {stockItem.Count}");
                return stockItem.FirstOrDefault();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public List<ItemWarehouse> GetItemWareHouseByProductCode(string companyCode, string productCode)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouseByProductCode :: Reading view uri from config");
                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, ITEMWAREHOUSE_VIEWURI_KEY)}?{ITEMWAREHOUSE_PRODUCTCODE_FIELD}{EQUAL_OPERATOR}{productCode}";
                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
                var itemWarehouse = DenodoContext.GetData<ItemWarehouse>(finalViewUri);
                ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouse.Count}");
                return itemWarehouse;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public List<ItemWarehouse> GetItemWareHouseByLocationId(string companyCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetItemWareHouseByLocationId :: Reading view uri from config");
                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, ITEMWAREHOUSE_VIEWURI_KEY)}?{ITEMWAREHOUSE_LOCATIONID_FIELD}{EQUAL_OPERATOR}{locationId}";
                ApplicationLogger.InfoLogger($"DenodoUrl: [{_denodoUrl}{finalViewUri}]");
                var itemWarehouse = DenodoContext.GetData<ItemWarehouse>(finalViewUri);
                ApplicationLogger.InfoLogger($"ItemWarehouse count: {itemWarehouse.Count}");
                return itemWarehouse;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public List<ProductInventoryEntity> GetProductInvetoryByProductName(string companyCode, string productName)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProductInvetoryByProductName :: Reading view uri from config");
                string companyViewUri = _configReader.GetDenodoViewUri(companyCode, PRODUCTINVENTORY_VIEWURI_KEY);
                string filter = $"lower({STOCKITEM_PRODUCTNAME_FIELD}) {LIKE_OPERATOR} '%{productName.ToLower()}%'";
                ApplicationLogger.InfoLogger($"Denodo url: [{_denodoUrl}{companyViewUri}] filter: [{filter}]");
                var productInventory = DenodoContext.SearchData<ProductInventoryEntity>(companyViewUri, filter);
                ApplicationLogger.InfoLogger($"ProductInventory count: {productInventory.Count}");
                return productInventory;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public List<ProductInventoryEntity> GetProductInvetoryByLocationId(string companyCode, string locationId)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: GetProductInvetoryByLocationId :: Reading view uri from config");
                string finalViewUri = $"{_configReader.GetDenodoViewUri(companyCode, PRODUCTINVENTORY_VIEWURI_KEY)}?{ITEMWAREHOUSE_LOCATIONID_FIELD}{EQUAL_OPERATOR}{locationId}";
                ApplicationLogger.InfoLogger($"Denodo url: [{_denodoUrl}{finalViewUri}]");
                var productInventory = DenodoContext.GetData<ProductInventoryEntity>(finalViewUri);
                ApplicationLogger.InfoLogger($"ProductInventory count: {productInventory.Count}");
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
