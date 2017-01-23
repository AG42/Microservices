using System.Collections.Generic;
using PurchaseOrder.Model.Models;

namespace PurchaseOrder.Model.Response
{
    public class PurchaseOrdersResponse : BaseResponse
    {
        public List<PurchaseOrderModel> PurchaseOrders;
    }
}
