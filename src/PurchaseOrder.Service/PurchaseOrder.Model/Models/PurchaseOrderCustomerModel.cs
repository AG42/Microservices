using System.Collections.Generic;

namespace PurchaseOrder.Model.Models
{
    public class PurchaseOrderCustomerModel
    {
        public string CustomerName;
        public string TelephoneNo;
        public List<PurchaseOrderModel> PurchaseOrders;
    }
}
