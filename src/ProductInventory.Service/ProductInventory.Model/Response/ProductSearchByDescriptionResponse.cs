using System.Collections.Generic;

namespace ProductInventory.Model.Response
{
    public class ProductSearchByDescriptionResponse: BaseResponse
    {
        public List<ProductInventoryModel> ProductList { get; } = new List<ProductInventoryModel>();
    }
}
