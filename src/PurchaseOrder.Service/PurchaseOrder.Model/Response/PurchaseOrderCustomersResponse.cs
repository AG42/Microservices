using PurchaseOrder.Model.Models;
using System.Collections.Generic;

namespace PurchaseOrder.Model.Response
{
    public class PurchaseOrderCustomersResponse : BaseResponse
    {
        public List<PurchaseOrderCustomerModel> PurchaseOrderCustomers;
    }
}
