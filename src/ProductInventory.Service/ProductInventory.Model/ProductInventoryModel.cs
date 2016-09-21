using System.Collections.Generic;

namespace ProductInventory.Model
{
    public class ProductInventoryModel
    {
        public ProductMasterModel ProductMasterDetails { get; set; }
        public List<ProductWarehouseModel> StockItemWarehouse { get; } = new List<ProductWarehouseModel>();
    }
}
