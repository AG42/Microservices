using System.Collections.Generic;

namespace ProductInventory.Model.Response
{
   public class LocationwiseProductAvailableQuantityResponse : BaseResponse
    {
        public List<LocationWiseProductAvailableQuantityModel> ProductList { get; } = new List<LocationWiseProductAvailableQuantityModel>();
    }
}
