using System.ComponentModel;

namespace SalesLedgerInvoicing.Common.Enums
{
    public enum OrderType
    {
        Unknown = 0,

        [Description("Sales Order")]
        SalesOrder = 1,

        [Description("On-account invoice for service order")]
        OnAccountInvoiceForServiceOrder = 2,

        [Description("Project")]
        Project = 4,

        [Description("Service Order")]
        ServiceOrder = 5,

        [Description("Contract Invoice")]
        ContractInvoice = 6,

        [Description("Merge invoice in sales ledger")]
        MergeInvoiceInSalesLedger = 8

    }
}
