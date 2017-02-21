using SalesOrder.Model.Models;
using System.Collections.Generic;

namespace SalesOrder.Model.Response
{
   public class SalesOrdersResponse : BaseResponse
    {
        public IEnumerable<SalesOrderHeadModel> SalesOrders;
    }
}