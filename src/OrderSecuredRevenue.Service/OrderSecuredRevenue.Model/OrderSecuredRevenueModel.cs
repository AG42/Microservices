using System.Collections.Generic;

namespace OrderSecuredRevenue.Model
{
    public class OrderSecuredRevenueDetails {
        public string Order_Number { get; set; }
        public double Revenue { get; set; }

        public List<OrderSecuredRevenueModel> OrderSecuredRevenueModelCollection { get; } = new List<OrderSecuredRevenueModel>();
    }
    public class OrderSecuredRevenueModel
    {
        public string Order_Number { get; set; }
        public string Line_Number { get; set; }
        public string Line_Type { get; set; }
        //public string Invoice_Number { get; set; }
        public string Unit_Price { get; set; }
        public string Unit_Cost_Price { get; set; }
        public string Qty_Ordered { get; set; }
        public double Revenue { get; set; }
        public string Company_Code { get; set; }
    }
}
