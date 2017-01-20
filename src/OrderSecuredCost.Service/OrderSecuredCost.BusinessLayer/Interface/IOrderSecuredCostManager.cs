using OrderSecuredCost.Model.Response;

namespace OrderSecuredCost.BusinessLayer.Interface
{
    public interface IOrderSecuredCostManager
    {
        OrderSecuredCostsResponse GetOrderSecuredCostByCompanyCode(string companyCode);
        OrderSecuredCostResponse GetOrderSecuredCostByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber);
        OrderSecuredCostsResponse GetOrderSecuredCostByOrderType(string companyCode, string orderType);
        OrderSecuredCostsResponse GetOrderSecuredCostByOrderDateRange(string companyCode, string orderStartDate, string OrderEndDate);
        OrderSecuredCostsResponse GetOrderSecuredCostByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate);
        OrderSecuredCostsResponse GetOrderSecuredCostByUserID(string companyCode, string userId);
        OrderSecuredCostsResponse GetOrderSecuredCostByOrderCostRange(string companyCode, string minOrderCost, string maxOrderCost);
    }
}