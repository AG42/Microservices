using System.Collections.Generic;

namespace SalesOrder.Model.Models
{
    public class SalesOrderHeadModel
    {
        public string OrderNumber;
        public string OrderType;
        public string CustomerCodeInvoice;
        public string FlagPickList;
        public string OrderDate;
        public string DelivaryDate;
        public string OrderDeliveryStatus;
        public List<SalesOrderLineModel> SalesOrderLines;
    }
}