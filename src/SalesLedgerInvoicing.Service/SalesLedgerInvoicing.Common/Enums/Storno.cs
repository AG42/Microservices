using System.ComponentModel;

namespace SalesLedgerInvoicing.Common.Enums
{
    public enum Storno
    {
        [Description("The invoice is a normally entered/created invoice")]
        CreatedInvoice = 0,

        [Description("invoice was created as a storno invoice (booked with reversed sings)")]
        StornoInvoice = 1,
    }
}
