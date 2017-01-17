using System.Collections.Generic;

namespace SalesLedgerInvoicing.Model.Responses
{
    public class SalesLedgerInvoicingByCustomerCodeResponse : BaseResponse
    {
        public List<SalesLedgerInvoicesModel> SalesLedgerInvoicingModelList { get; set; } = new List<SalesLedgerInvoicesModel>();
    }
}
