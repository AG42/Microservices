using SalesOrder.Model.Response;

namespace SalesOrder.BusinessLayer.Interfaces
{
    public interface ISalesOrderManager
    {
        SalesOrdersResponse GetSalesOrderByCompanyCode(string companyCode);

        SalesOrdersResponse GetSalesOrderBySalesOrderNumber(string companyCode, string salesOrderNumber);

        SalesOrdersResponse GetSalesOrderByOrderType(string companyCode, string orderType);

        SalesOrdersResponse GetSalesOrderByCustomerInvoiceCode(string companyCode, string customerInvoiceCode);

        SalesOrdersResponse GetSalesOrderByFlagPickList(string companyCode, string flagPickList);

        SalesOrdersResponse GetSalesOrderByOrderDateRange(string companyCode, string minOrderDate, string maxOrderDate);

        SalesOrdersResponse GetSalesOrderByDeliveryDateRange(string companyCode, string minDeliveryDate, string maxDeliveryDate);
    }
}
