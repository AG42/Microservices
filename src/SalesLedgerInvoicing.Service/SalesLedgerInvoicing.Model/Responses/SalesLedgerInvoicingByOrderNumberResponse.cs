using System.Collections.Generic;

namespace SalesLedgerInvoicing.Model.Responses
{
    public class SalesLedgerInvoicingByOrderNumberResponse : BaseResponse
    {
        public List<SalesLedgerInvoicesModel> SalesLedgerInvoicingModelList { get; set; } = new List<SalesLedgerInvoicesModel>();

    }
}
