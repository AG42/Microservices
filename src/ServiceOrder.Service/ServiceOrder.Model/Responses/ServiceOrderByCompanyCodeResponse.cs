using Newtonsoft.Json;
using System.Collections.Generic;

namespace ServiceOrder.Model.Responses
{
    public class ServiceOrderByCompanyCodeResponse: BaseResponse
    {
        [JsonProperty(PropertyName = "ServiceModels")]
        public List<ServiceOrderModel> ServiceOrderModels { get; } = new List<ServiceOrderModel>(); 
    }
}
