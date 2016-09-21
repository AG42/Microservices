using System.Collections.Generic;

namespace ProductInventory.Model.Response
{
   public class LocationwiseProductStockBalanceQuantityResponse: BaseResponse
    {
        public List<LocationWiseProductQuantityModel> ProductList { get; } = new List<LocationWiseProductQuantityModel>();
    }
}
