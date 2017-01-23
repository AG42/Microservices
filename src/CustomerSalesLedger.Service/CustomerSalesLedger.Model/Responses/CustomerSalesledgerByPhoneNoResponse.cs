using System.Collections.Generic;

namespace CustomerSalesLedger.Model.Response
{
    public class CustomerSalesledgerByPhoneNoResponse:BaseResponse
    {
        public List<CustomerSalesLedgerModel> CustomerSalesLedger { get; } = new List<CustomerSalesLedgerModel>();

    }
}
