using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOrder.Model.Responses
{
    public class ServiceOrderByServiceOrderNoResponse: BaseResponse
    {
        [Display(Name = "ServiceOrder")]
        public ServiceOrderModel ServiceOrderModel { get; set; }
    }
}
