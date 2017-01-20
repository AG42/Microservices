using System.Collections.Generic;

namespace SalesLedgerInvoicing.Model.Responses
{
    public class SalesLedgerInvoicingByInvoiceDateResponse : BaseResponse
    {
        public List<SalesLedgerInvoicesModel> SalesLedgerInvoicingModelList { get; set; } = new List<SalesLedgerInvoicesModel>();

    }
}
