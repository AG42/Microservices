using System.Collections.Generic;
using ProductInventory.DataLayer.Entities;
using ProductInventory.DataLayer.Entities.Datalake;

namespace ProductInventory.DataLayer.Interfaces
{
    public interface IDatabaseContext
    {
        /// <summary>
        /// Gets product details from stock itemwarehouse based on company code, product code and location id
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="productCode">The product code.</param>
        /// <param name="locationId">The location id.</param>
        /// <returns>List of itemwarehouse</returns>
        IEnumerable<ItemWarehouse> GetItemWareHouse(string companyCode, string productCode, string locationId);
        /// <summary>
        /// Gets product details from stock item master table by company code and product code
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="productCode">The product code.</param>
        /// <returns>Single stock item</returns>
        StockItemMaster GetStockItemByProductCode(string companyCode, string productCode);
        /// <summary>
        /// Gets details from item warehouse by company code and product code.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="productCode">The product code.</param>
        /// <returns>List of item warehouse</returns>
        IEnumerable<ItemWarehouse> GetItemWareHouseByProductCode(string companyCode, string productCode); /// <summary>
        /// Gets details from item warehouse by company code and product code.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="locationId">The location id.</param>
        /// <returns>List of item warehouse</returns>
        IEnumerable<ItemWarehouse> GetItemWareHouseByLocationId(string companyCode, string locationId);
        /// <summary>
        /// Gets product details by company code and product name from stock item master and corresponding details from item warehouse
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="productName">Name of the product.</param>
        /// <returns>List of product inventory which is join of stock item master and item warehouse</returns>
        IEnumerable<ProductInventoryEntity> GetProductInvetoryByProductName(string companyCode, string productName);
        /// <summary>
        /// Gets product details from join of stock item master and item warehouse by location identifier.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="locationId">The location id.</param>
        /// <returns>List of product inventory which is join of stock item master and item warehouse</returns>
        IEnumerable<ProductInventoryEntity> GetProductInvetoryByLocationId(string companyCode, string locationId);
    }   
}
