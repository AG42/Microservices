using System.ComponentModel;

namespace SalesLedgerInvoicing.Common.Enums
{
    public enum InvoiceStatusType
    {
        [Description("Invoice has not been sent for collection")]
        InvoiceNotSendForCollection = 0,

        [Description("Invoice is in collection status")]
        InvoiceInCollection = 1
    }
}
