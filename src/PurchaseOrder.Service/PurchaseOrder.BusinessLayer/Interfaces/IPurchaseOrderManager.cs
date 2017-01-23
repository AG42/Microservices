using PurchaseOrder.Model.Response;
namespace PurchaseOrder.BusinessLayer.Interfaces
{
    public interface IPurchaseOrderManager
    {
        PurchaseOrdersResponse GetPurchaseOrderByCompanyCode(string companyCode);
        PurchaseOrderResponse GetPurchaseOrderByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber);
        PurchaseOrdersResponse GetPurchaseOrderByOrderType(string companyCode, string orderType);
        PurchaseOrderCustomersResponse GetPurchaseOrdersByCustomerName(string companyCode, string customerName);
        PurchaseOrdersResponse GetPurchaseOrderByProjectNumber(string companyCode, string projectNumber);
        PurchaseOrdersResponse GetPurchaseOrderByOrderDateRange(string companyCode, string orderStartDate, string orderEndDate);
        PurchaseOrdersResponse GetPurchaseOrderByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate);
    }
}
