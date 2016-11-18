using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.Model.Responses
{
    public class ServiceOrderByServiceOrderNoResponse: BaseResponse
    {
        [Display(Name = "ServiceOrder")]
        public ServiceOrderModel ServiceOrderModel { get; set; }
    }
}
