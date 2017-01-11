namespace OrderSecuredRevenue.Model
{
    //public class OrderSecuredRevenueDetails {
    //    public string Order_Number { get; set; }
    //    public double Revenue { get; set; }

    //    public List<OrderSecuredRevenueModel> OrderSecuredRevenueModelCollection { get; } = new List<OrderSecuredRevenueModel>();
    //}
    public class OrderSecuredRevenueModel
    {
        public string OrderNumber { get; set; }
        public double Revenue { get; set; }

        //public SalesOrderHeadModel SalesOrderDetails { get; set; } 
        //public List<SalesOrderDetailsLineModel> SalesOrderLineDetailsList { get; set; }
    }
}
