using ProductInventory.Model.Response;

namespace ProductInventory.BusinessLayer.Interfaces
{
    public interface IProductInventoryManager
    {
        ProductSearchByIdResponse GetProductById(string companyCode, string productCode);
        ProductSearchByDescriptionResponse GetProductByDescription(string companyCode, string description);
        ProductSearchByLocationIdResponse GetProductByLocationId(string companyCode, string locationId);
        ProductResponse GetProduct(string companyCode, string productCode, string locationId);

        ProductStockBalanceQuantityResponse GetProductStockBalanceQuantity(string companyCode, string productCode, string locationId);
        ProductStockBalanceQuantityForAllLocationResponse GetProductStockBalanceQuantityForAllLocation(string companyCode, string productCode);
        LocationwiseProductStockBalanceQuantityResponse GetLocationwiseProductStockBalanceQuantity(string companyCode, string locationId);

        ProductAvailableQuantityResponse GetProductAvailableQuantity(string companyCode, string productCode, string locationId);
        ProductAvailableQuantityForAllLocationResponse GetProductAvailableQuantityForAllLocation(string companyCode, string productCode);
        LocationwiseProductAvailableQuantityResponse GetLocationwiseProductAvailableQuantity(string companyCode, string locationId);

        ProductFamilyTypeResponse GetProductFamilyType(string companyCode, string productCode);
        ProductLineTypeResponse GetProductLineType(string companyCode, string productCode);
        ProductStatusResponse IsProductActive(string companyCode, string productCode);
        ProductStockStatusResponse IsProductStockable(string companyCode, string productCode);
    }
}
