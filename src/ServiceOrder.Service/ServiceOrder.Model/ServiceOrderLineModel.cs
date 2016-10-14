using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ServiceOrder.Model
{

    public class ServiceOrderLineModel
    {
        /// <summary>
        /// Gets the service order line labor list.
        /// </summary>
        /// <value>
        /// The service order line labor list.
        /// </value>
        [JsonProperty(PropertyName = "Labor")]
        public List<ServiceOrderLineLaborModel> ServiceOrderLineLaborList { get; } = new List<ServiceOrderLineLaborModel>();

        /// <summary>
        /// Gets the service order line mo item list.
        /// </summary>
        /// <value>
        /// The service order line mo item list.
        /// </value>
        [JsonProperty(PropertyName = "MOITEM")]
        public List<ServiceOrderLineMOItemModel> ServiceOrderLineMOItemList { get; } = new List<ServiceOrderLineMOItemModel>();

        ///// <summary>
        ///// Gets the service order line tool list.
        ///// </summary>
        ///// <value>
        ///// The service order line tool list.
        ///// </value>
        [JsonProperty(PropertyName = "TOOL")]
        public List<ServiceOrderLineToolModel> ServiceOrderLineToolList { get; } = new List<ServiceOrderLineToolModel>();

    }
}
