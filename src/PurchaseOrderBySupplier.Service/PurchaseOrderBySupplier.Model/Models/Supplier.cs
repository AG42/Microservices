using System.Collections.Generic;

namespace PurchaseOrderBySupplier.Model.Models
{
    public class Supplier
    {
        public string SupplierCode { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
