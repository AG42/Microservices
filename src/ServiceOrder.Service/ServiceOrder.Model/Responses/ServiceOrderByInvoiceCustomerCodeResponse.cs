using Newtonsoft.Json;
using System.Collections.Generic;

namespace ServiceOrder.Model.Responses
{
    public class ServiceOrderByInvoiceCustomerCodeResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "ServiceOrders")]
        public List<ServiceOrderModel> ServiceOrderModels { get; } = new List<ServiceOrderModel>();
    }
}
