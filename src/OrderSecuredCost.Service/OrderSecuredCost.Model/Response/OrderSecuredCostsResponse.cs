using System.Collections.Generic;

namespace OrderSecuredCost.Model.Response
{
    public class OrderSecuredCostsResponse : BaseResponse
    {
        public List<OrderSecuredCostModel> OrderSecuredCosts { get; set; }
    }
}
