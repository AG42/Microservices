using System.Collections.Generic;

namespace ProductInventory.Model.Response
{
    public class ProductSearchByLocationIdResponse:BaseResponse
    {
        public List<ProductInventoryModel> ProductInventoryList { get; } = new List<ProductInventoryModel>();
    }
}
