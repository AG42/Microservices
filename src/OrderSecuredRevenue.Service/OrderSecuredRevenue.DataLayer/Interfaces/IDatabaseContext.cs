using System.Collections.Generic;
using OrderSecuredRevenue.DataLayer.Entities.Datalake;

namespace OrderSecuredRevenue.DataLayer.Interfaces
{
    public interface IDatabaseContext
    {
       IEnumerable<OR03> GetOrderSecuredRevenueByOrderNumber(string companyCode, string orderNumber);
       IEnumerable<OR01> GetSalesOrderDetailsByOrderNumber(string companyCode, string orderNumber);
       IEnumerable<OR03> GetSalesOrderLineDetailsByOrderNumber(string companyCode, string orderNumber);
    }
}
