using System.Collections.Generic;

namespace ProductInventory.Model.Response
{
    public class ProductAvailableQuantityForAllLocationResponse : BaseResponse
    {
        public List<ProductAvailableQuantity> ProductList { get; } = new List<ProductAvailableQuantity>();
    }
}
