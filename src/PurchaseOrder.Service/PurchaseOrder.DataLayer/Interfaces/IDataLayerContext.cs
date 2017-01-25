using PurchaseOrder.DataLayer.Entities.Datalake;
using System.Collections.Generic;

namespace PurchaseOrder.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        IEnumerable<Pc01> GetPurchaseOrderByCompanyCode(string companyCode);
        Pc01 GetPurchaseOrderByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber);
        IEnumerable<Pc01> GetPurchaseOrderByOrderType(string companyCode, string orderType);
        IEnumerable<Pc04> GetPurchaseOrderByCustomerName(string companyCode, string customerName);
        IEnumerable<Pc01> GetPurchaseOrderByProjectNumber(string companyCode, string projectNumber);
        IEnumerable<Pc01> GetPurchaseOrderByOrderDateRange(string companyCode, string orderStartDate,string orderEndDate);
        IEnumerable<Pc01> GetPurchaseOrderByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate);

    }
}
