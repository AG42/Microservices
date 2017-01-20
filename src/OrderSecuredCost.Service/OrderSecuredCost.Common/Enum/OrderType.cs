using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSecuredCost.Common.Enum
{
    public enum OrderType
    {
        Purchase_Order_Proposal = 0,
        Normal_Order = 1,
        Back_Order = 2,
        Direct_Delivery_To_Customer = 3,
        Replenishment_Order = 4
    }
}
