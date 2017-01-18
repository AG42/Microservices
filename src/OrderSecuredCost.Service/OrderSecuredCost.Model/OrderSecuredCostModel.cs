using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSecuredCost.Model
{
    public class OrderSecuredCostModel
    {
        public string PurchOrderNo { get; set; }
        public string OrderType { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
        public string Remark { get; set; }
        public string UserID { get; set; }
        public string OrderSecuredCost { get; set; }
    }
}
