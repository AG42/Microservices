using System.ComponentModel;

namespace SalesLedgerInvoicing.Common.Enums
{
    public enum TransactionType
    {
        [Description("Normal Invoice")]
        NormalInvoice = 0,

        [Description("Interest Invoice")]
        InterestInvoice = 1,

        [Description("Temporary value assigned during entry of invoices, indicates that entry of invoice has not been completed")]
        TemporaryValueAssigned = 9
    }
}
