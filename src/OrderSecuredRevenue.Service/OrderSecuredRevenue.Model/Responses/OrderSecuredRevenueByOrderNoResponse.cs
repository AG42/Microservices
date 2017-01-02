using OrderSecuredRevenue.Model;
using System.Collections.Generic;

namespace OrderSecuredRevenue.Model.Responses
{
    public class OrderSecuredRevenueByOrderNoResponse : BaseResponse
    {
        //public List<OrderSecuredRevenueModel> OrderSecuredRevenueModels { get; } = new List<OrderSecuredRevenueModel>();
        public OrderSecuredRevenueDetails OrderSecuredRevenueDetails { get; set; } = new OrderSecuredRevenueDetails();
    }
}
