using OrderSecuredCost.DataLayer.Entities.Datalake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSecuredCost.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        IEnumerable<Pc01> GetOrderSecuredCostByCompanyCode(string companyCode);
        Pc01 GetOrderSecuredCostByPurchaseOrderNumber(string companyCode, string purchaseOrderNumber);

        IEnumerable<Pc01> GetOrderSecuredCostByOrderType(string companyCode, string orderType);
        IEnumerable<Pc01> GetOrderSecuredCostByOrderDateRange(string companyCode, string orderStartDate, string orderEndDate);
        IEnumerable<Pc01> GetOrderSecuredCostByDeliveryDateRange(string companyCode, string deliveryStartDate, string deliveryEndDate);
        IEnumerable<Pc01> GetOrderSecuredCostByUserID(string companyCode, string userId);
        IEnumerable<Pc01> GetOrderSecuredCostByOrderCostRange(string companyCode, string minOrderCost,string maxOrderCost);

    }
}
