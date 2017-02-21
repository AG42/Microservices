using System.ComponentModel;


namespace SalesOrder.Common.Enum
{
    public enum OrderType
    {
        [Description("Quotation")]
        Quotation = 0,
        [Description("Normal Order")]
        Normal_Order = 1,
        [Description("Invoice Order")]
        Invoice_Order = 2,
        [Description("Direct Order")]
        Direct_Order = 3,
        [Description("Back Order")]
        Back_Order = 4,
        [Description("Repeat Order")]
        Repeat_Order = 5,
        [Description("Work Order")]
        Work_Order = 6,
        [Description("Direct Credit Order")]
        Direct_Credit_Order = 7,
        [Description("Credit Order")]
        Credit_Order = 8,
        [Description("Reinvoice Order")]
        Re_Invoice_Order = 9
    }
}
