using Newtonsoft.Json;
using System.Collections.Generic;

namespace ServiceOrder.Model
{
    public class ServiceOrderDetailModel
    {
        /// <summary>
        /// Gets or sets the service order details. 
        /// </summary>
        /// <value>
        /// The service order model.
        /// </value>
        [JsonProperty(PropertyName ="ServiceOrders")]
        public List<ServiceOrderModel> ServiceOrderModel { get; set; }
    }
}
