using System.Collections.Generic;

namespace PurchaseOrderBySupplier.Model.Models
{
    public class PurchaseOrder
    {
        public string PurchaseOrderNumber { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
