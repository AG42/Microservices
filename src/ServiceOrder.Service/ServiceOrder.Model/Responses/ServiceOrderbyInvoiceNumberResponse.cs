using System.Collections.Generic;
using Newtonsoft.Json;

namespace ServiceOrder.Model.Responses
{
    public class ServiceOrderByInvoiceNumberResponse:BaseResponse
    {
        [JsonProperty(PropertyName = "ServiceOrders")]
        public List<ServiceOrderModel> ServiceOrderModels { get; } = new List<ServiceOrderModel>();
    }
}
