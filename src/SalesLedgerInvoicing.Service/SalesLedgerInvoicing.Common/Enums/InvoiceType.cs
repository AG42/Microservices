using System.ComponentModel;

namespace SalesLedgerInvoicing.Common.Enums
{
    public enum InvoiceType
    {
        [Description("Regular Invoice")]
        RegularInvoice = 0,

        [Description("Credit Note")]
        CreditNote = 1,

        [Description("Re-Invoice")]
        ReInvoice = 2,

        [Description("Discount")]
        Discount = 3,

        [Description("Other Discount")]
        OtherDiscount = 4,

        [Description("User Defined")]
        UserDefined = 5
    }
}
