﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSecuredRevenue.Model.Responses
{
    public class OrderDeliveryDateByOrderNoResponse: BaseResponse
    {
        public DateTimeOffset? DeliveryDate { get; set; }
    }
}
