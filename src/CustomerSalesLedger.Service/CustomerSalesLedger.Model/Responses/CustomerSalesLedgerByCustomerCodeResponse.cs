using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSalesLedger.Model.Response
{
    public class CustomerSalesLedgerByCustomerCodeResponse : BaseResponse
    {
        public CustomerSalesLedgerModel CustomerSalesLedger { get; set; } = new CustomerSalesLedgerModel();
    }
}
