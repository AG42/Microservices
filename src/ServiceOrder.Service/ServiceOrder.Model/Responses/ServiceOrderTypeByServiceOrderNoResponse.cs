using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOrder.Model.Responses
{
    public class ServiceOrderTypeByServiceOrderNoResponse : BaseResponse
    {
        public string OrderType { get; set; }
    }
}

