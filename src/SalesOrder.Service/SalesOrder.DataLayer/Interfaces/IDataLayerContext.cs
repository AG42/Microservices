using SalesOrder.DataLayer.Entities.Datalake;
using System.Collections.Generic;

namespace SalesOrder.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        IEnumerable<Or01> GetSalesOrderByCompanyCode(string companyCode);

        IEnumerable<Or01> GetSalesOrderBySalesOrderNumber(string companyCode, string salesOrderNumber);

        IEnumerable<Or01> GetSalesOrderByOrderType(string companyCode, string orderType);

        IEnumerable<Or01> GetSalesOrderByCustomerInvoiceCode(string companyCode, string customerInvoiceCode);

        IEnumerable<Or01> GetSalesOrderByFlagPickList(string companyCode, string flagPickList);

        IEnumerable<Or01> GetSalesOrderByOrderDateRange(string companyCode, string minOrderDate, string maxOrderDate);

        IEnumerable<Or01> GetSalesOrderByDeliveryDateRange(string companyCode, string minDeliveryDate, string maxDeliveryDate);
    }
}
