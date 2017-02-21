using PurchaseLedger.Model.Models;
using System.Collections.Generic;

namespace PurchaseLedger.Model.Response
{
    public class PurchaseLedgersResponse:BaseResponse
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
