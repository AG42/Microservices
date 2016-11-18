using System.Collections.Generic;

namespace CustomerProjectOrder.Model.Response
{
    public class CustomerProjectOrdersResponse : BaseResponse
    {
        public List<CustomerProjectOrderModel> CustomerProjectOrders { get; set; } 
    }
}
