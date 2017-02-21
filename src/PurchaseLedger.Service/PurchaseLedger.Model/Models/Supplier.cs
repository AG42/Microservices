using System.Collections.Generic;

namespace PurchaseLedger.Model.Models
{
    public class Supplier
    {
        public string SupplierCode;
        public string SupplierName;
        public List<PurchaseLedgerModel> PurchaseLedgers;
    }
}
