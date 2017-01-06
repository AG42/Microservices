using OrderSecuredRevenue.Model;
using System.Collections.Generic;


namespace OrderSecuredRevenue.Model.Responses
{
    public class OrderSecuredRevenueByInvoiceNumberReponse:BaseResponse
    {
        public List<OrderSecuredRevenueModel> OrderSecuredRevenueModels { get; } = new List<OrderSecuredRevenueModel>();
    }
}
