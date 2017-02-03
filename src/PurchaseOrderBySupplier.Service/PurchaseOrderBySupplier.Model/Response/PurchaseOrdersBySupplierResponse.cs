using PurchaseOrderBySupplier.Model.Models;
using System.Collections.Generic;

namespace PurchaseOrderBySupplier.Model.Response
{
   public class PurchaseOrdersBySupplierResponse:BaseResponse
    {
        public IEnumerable<Supplier> PurchaseOrdersBySupplier { get; set; }
    }
}
