using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSalesLedger.Model.Response
{
    public class CustomerSalesLedgerByCustomerNameResponse:BaseResponse
    {
        public List<CustomerSalesLedgerModel> CustomerSalesLedger { get; } = new List<CustomerSalesLedgerModel>();

    }
}
