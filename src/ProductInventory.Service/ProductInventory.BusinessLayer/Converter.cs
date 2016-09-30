using System.Collections.Generic;
using ProductInventory.DataLayer.Entities;
using ProductInventory.Model;
using System.Linq;
using System.Security.AccessControl;
using ProductInventory.DataLayer.Entities.Datalake;
using sys = System;

namespace ProductInventory.BusinessLayer
{
    class Converter
    {
        /// <summary>
        /// Business validation conversion for Product Master Model
        /// </summary>
        /// <param name="stockItem">StockItem Entity</param>
        /// <param name="companyCode">Company Code</param>
        /// <returns>ProductMaster Business Model</returns>
        public static ProductMasterModel Convert(StockItemMaster stockItem, string companyCode)
        {
            return new ProductMasterModel()
            {
                ERP_Company_Code__c = companyCode,
                CurrencyIsoCode = GetCurrencyIsoCode(companyCode),
                Name = stockItem.sc01002 +" "+ stockItem.sc01003,
                Description = stockItem.sc01002 + " " + stockItem.sc01003,
                ERP_ID__c = GetErpId(companyCode, stockItem.sc01001),
                Family = GetProductFamily(stockItem.sc01037),
                IsActive = CheckActiveProduct(string.IsNullOrWhiteSpace(stockItem.sc01120)?0:System.Convert.ToInt32(stockItem.sc01120)),
                ProductCode = stockItem.sc01001,
                SVMXC__Product_Cost__c = string.IsNullOrWhiteSpace(stockItem.sc01106) ? 0 : System.Convert.ToDouble(stockItem.sc01106),
                SVMXC__Product_Line__c = GetProductLineMapping(stockItem.sc01037),
                SVMXC__Stockable__c = GetStockable(string.IsNullOrWhiteSpace(stockItem.sc01066) ? 0 : System.Convert.ToInt32(stockItem.sc01066), stockItem.sc01001),
                SVMXC__Unit_Of_Measure__c = string.IsNullOrWhiteSpace(stockItem.sc01135) ? 0 : System.Convert.ToInt32(stockItem.sc01135)
            };
        }
        /// <summary>
        /// This method is used to get the list of  ProductWareHouse Model 
        /// </summary>
        /// <param name="itemWarehouses">list of Itemwarehouse entities</param>
        /// <param name="companyCode">company code</param>
        /// <returns>List of Itemware House</returns>
        public static List<ProductWarehouseModel> Convert(IEnumerable<ItemWarehouse> itemWarehouses, string companyCode)
        {
            var productWarehouseModels = new List<ProductWarehouseModel>();
            foreach (var itemWareHouse in itemWarehouses)
                productWarehouseModels.Add(Convert(itemWareHouse, companyCode));

            return productWarehouseModels;
        }
        /// <summary>
        /// This method is used to get the  ProductWareHouse Model with all business validations
        /// </summary>
        /// <param name="itemWarehouse">itemWarehouse</param>
        /// <param name="companyCode">Company Code</param>
        /// <returns>ProductWarehouseModel</returns>
        public static ProductWarehouseModel Convert(ItemWarehouse itemWarehouse, string companyCode)
        {

            return new ProductWarehouseModel()
            {
                SVMXC__Location__c = itemWarehouse.sc03002,
                SVMXC__Product__c = itemWarehouse.sc03001,
                SVMXC__Quantity2__c = string.IsNullOrWhiteSpace(itemWarehouse.sc03003) ? 0 : System.Convert.ToDouble(itemWarehouse.sc03003),
                SVMXC__Status__c = "Available",
                ERP_Location_ID__c = GetLocationId(itemWarehouse.sc03002, companyCode),
                ERP_Stock_Code__c = itemWarehouse.sc03001,
                ERP_Reserved_Quantity_c = GetReservedQuantity(itemWarehouse.sc03004, itemWarehouse.sc03005),
                ERP_Available_Quantity_c = GetAvailableQuantity(itemWarehouse.sc03003, itemWarehouse.sc03004, itemWarehouse.sc03005),
                ERP_Stock_Location_Code_Key__c = GetStockLocationCode(itemWarehouse.sc03002, companyCode, itemWarehouse.sc03001),
                ERP_Company_Code__c = companyCode
            };
        }
        /// <summary>
        /// This Method is used to get the product inventory by using product name.
        /// </summary>
        /// <param name="productsEntity">List of Products with stock details</param>
        /// <param name="companyCode">Company Code</param>
        /// <returns>Collection of Products</returns>
        public static List<ProductInventoryModel> ConvertProductEntity(List<ProductInventoryEntity> productsEntity, string companyCode)
        {
            List<ProductInventoryModel> productInventoryModelList = new List<ProductInventoryModel>();

            var products = productsEntity.GroupBy(item => new { item.sc01001, item.sc01002, item.sc01003, item.sc01037, item.sc01066, item.sc01106, item.sc01120, item.sc01135 },
                  (key, productStockItemWarehouse) => new
                  {
                      key.sc01001,
                      key.sc01002,
                      key.sc01003,
                      key.sc01037,
                      key.sc01066,
                      key.sc01106,
                      key.sc01120,
                      key.sc01135,
                      itemWarehouseList = productStockItemWarehouse.ToList()
                  }).ToList();

            foreach (var product in products)
            {
                var productInventoryModel = new ProductInventoryModel();
                var itemWarehouseList = new List<ProductWarehouseModel>();
                var masterModel = new ProductMasterModel
                {
                    ERP_Company_Code__c = companyCode,
                    CurrencyIsoCode = GetCurrencyIsoCode(companyCode),
                    Name = product.sc01002 + " " + product.sc01003,
                    Description = product.sc01002 + " " + product.sc01003,
                    ERP_ID__c = GetErpId(companyCode, product.sc01001),
                    Family = GetProductFamily(product.sc01037),
                    IsActive = CheckActiveProduct(string.IsNullOrWhiteSpace(product.sc01120) ? 0 : System.Convert.ToInt32(product.sc01120)),
                    ProductCode = product.sc01001,
                    SVMXC__Product_Cost__c = string.IsNullOrWhiteSpace(product.sc01106) ? 0 : System.Convert.ToDouble(product.sc01106),
                    SVMXC__Product_Line__c = GetProductLineMapping(product.sc01037),
                    SVMXC__Stockable__c = GetStockable(string.IsNullOrWhiteSpace(product.sc01066) ? 0 : System.Convert.ToInt32(product.sc01066), product.sc01001),
                    SVMXC__Unit_Of_Measure__c = string.IsNullOrWhiteSpace(product.sc01135) ? 0 : System.Convert.ToInt32(product.sc01135)
                };

                foreach (var item in product.itemWarehouseList)
                {
                    var itemWarehouse = new ProductWarehouseModel
                    {
                        SVMXC__Location__c = item.sc03002,
                        SVMXC__Product__c = item.sc03001,
                        SVMXC__Quantity2__c = string.IsNullOrWhiteSpace(item.sc03003) ? 0 : sys.Convert.ToDouble(item.sc03003),
                        ERP_Location_ID__c = GetLocationId(item.sc03002, companyCode),
                        ERP_Stock_Code__c = item.sc03001,
                        ERP_Reserved_Quantity_c = GetReservedQuantity(item.sc03004, item.sc03005),
                        ERP_Available_Quantity_c = GetAvailableQuantity(item.sc03003, item.sc03004, item.sc03005),
                        ERP_Stock_Location_Code_Key__c = GetStockLocationCode(item.sc03002, companyCode, item.sc03001),
                        ERP_Company_Code__c = companyCode
                    };

                    itemWarehouseList.Add(itemWarehouse);
                }

                productInventoryModel.ProductMasterDetails = masterModel;
                productInventoryModel.StockItemWarehouse.AddRange(itemWarehouseList);
                productInventoryModelList.Add(productInventoryModel);
            }

            return productInventoryModelList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        #region ProductMaster Conversions
        public static string GetCurrencyIsoCode(string companyCode)
        {
            switch (companyCode.ToUpper())
            {
                case "K1":
                    return "AUD";
                case "KS":
                    return "AUD";
                case "N1":
                    return "NZD";
                case "KK":
                    return "AUD";
                case "KU":
                    return "AUD";
                case "NU":
                    return "NZD";
                default:
                    return string.Empty;
            }
        }
        public static string GetErpId(string companyCode, string stockCode)
        {
            return "I" + companyCode + stockCode;
        }
        public static string GetProductFamily(string productGroupCode)
        {
            switch (productGroupCode.ToUpper())
            {
                case "DEV":
                    return "Equipment";
                case "SYS":
                    return "Equipment";
                case "AREF":
                    return "Spare Parts";
                case "ESUR":
                    return "Spare Parts";
                case "SEC":
                    return "Spare Parts";
                case "CNTL":
                    return "Spare Parts";
                case "GAS":
                    return "Spare Parts";
                default:
                    return "--None--";
            }
        }
        public static bool CheckActiveProduct(double holdStatus)
        {
            return holdStatus != 1;
        }

        public static int GetStockable(int articleStatus, string description)
        {
            //var articleStat = string.IsNullOrWhiteSpace(articleStatus) ? 0 : System.Convert.ToInt32(articleStatus);
            if (articleStatus == 0 && description.Substring(0, 1).ToLower() != "z-")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static string GetProductLineMapping(string productGroupCode)
        {
            switch (productGroupCode.ToUpper())
            {
                case "GAS":
                    return "Refrigerant";
                case "CNTL":
                    return "Metasys";
                case "SEC":
                    return "JCI Security";
                case "AREF":
                    return "York Industrial Refrigeration";
                case "ESUR":
                    return "York Commercial";
                default:
                    return string.Empty;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservedQty"></param>
        /// <param name="backOrderQty"></param>
        /// <returns></returns>
        #region ProductWarehouse Conversions
        public static double GetReservedQuantity(string reservedQty, string backOrderQty)
        {
            return (string.IsNullOrWhiteSpace(reservedQty)?0: sys.Convert.ToDouble(reservedQty)) + (string.IsNullOrWhiteSpace(backOrderQty) ? 0 : sys.Convert.ToDouble(backOrderQty));
        }
        public static double GetAvailableQuantity(string stockBalance, string reservedQty, string backOrderQty)
        {
            var stockBal = string.IsNullOrWhiteSpace(stockBalance) ? 0 : sys.Convert.ToDouble(stockBalance);
            var storeservedQuantity = string.IsNullOrWhiteSpace(reservedQty) ? 0 : sys.Convert.ToDouble(reservedQty);
            var backOrderQuantity = string.IsNullOrWhiteSpace(backOrderQty) ? 0 : sys.Convert.ToDouble(backOrderQty);

            return stockBal - (storeservedQuantity + backOrderQuantity);
        }
        public static string GetLocationId(string siteCode, string companyCode)
        {
            return "I" + companyCode + siteCode;
        }
        public static string GetStockLocationCode(string siteCode, string companyCode, string stockCode)
        {
            return "I" + companyCode + siteCode + stockCode;
        }

        #endregion
    }
}
