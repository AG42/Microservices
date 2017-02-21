namespace SalesOrder.Common.Enum
{
    public enum FlagPickStatus
    {
        Document_Not_Ready_For_Printing = 0,
        Print_Document = 1,
        Document_Sent = 2,
        Validates_The_Document_And_Process_It = 3
    }
}