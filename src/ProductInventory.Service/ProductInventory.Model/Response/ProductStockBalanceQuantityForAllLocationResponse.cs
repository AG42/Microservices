using System.Collections.Generic;

namespace ProductInventory.Model.Response
{
    public class ProductStockBalanceQuantityForAllLocationResponse: BaseResponse
    {
        public List<ProductLocationModel> LocationList { get; } = new List<ProductLocationModel>();
    }   
}
