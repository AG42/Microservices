using System;
using System.Collections.Generic;

namespace CustomerSalesLedger.Model.Response
{
    public class CustomerSalesLedgerByAlternateNameResponse : BaseResponse
    {
        public List<CustomerSalesLedgerModel> CustomerSalesLedger { get; } = new List<CustomerSalesLedgerModel>();
    }
}
