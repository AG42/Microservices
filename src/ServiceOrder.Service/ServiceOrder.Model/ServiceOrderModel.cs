using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ServiceOrder.Model
{
    [JsonObject(Title ="ServiceOrder")]
    public class ServiceOrderModel
    {
        public string Id { get; set; }
        public string ERP_SO_Key__c { get; set; }
        public string CurrencyIsoCode { get; set; }
        public string SVMX_MD_Problem_Summary__c { get; set; }
        public string ERP_Component__c { get; set; }
        public string SVMXC__Invoice_Number__c { get; set; }
        public string ERP_WO_Status__c { get; set; }
        public string SVMXC__Order_Type__c { get; set; }
        public string SVMXC__Company__c { get; set; }
        public string Bill_To_Account__c { get; set; }
        public string Ordering_Customer__c { get; set; }
        public string ERP_Servicing_Site_Code__c { get; set; }
        public string ERP_Project_Contract_Number__c { get; set; }
        public string ERP_Project_Key__c { get; set; }
        public string ERP_Technician_Code__c { get; set; }
        public string ERP_Cost_Center__c { get; set; }
        public string ERP_Master_SONumber__c { get; set; }
        public string ERP_Service_Order_Number__c { get; set; }
        public string ERP_Customer_PO_Number__c { get; set; }
        public DateTimeOffset? ERP_Invoice_date__c { get; set; }
        public string ERP_Payment_Terms_Code__c { get; set; }
        public bool ERP_Delivery_Blocked__c { get; set; }
        public bool ERP_Credit_Check_Passed__c { get; set; }
        public double ERP_CRM_Exchange_Rate___c { get; set; }
        public string ERP_Allocated_Team_code__c { get; set; }
        public string Job_Type__c { get; set; }
        public string ERP_Company_Code__c { get; set; }

                /// <summary>
        /// Gets or sets the service order line model.
        /// Contains the List of Labor, MOItem and Tool for a service order
        /// </summary>
        /// <value>
        /// The service order line model.
        /// </value>
        //[Display(Name = "ServiceOrderLine")]
        [JsonProperty(PropertyName = "ServiceOrderLine")]
        public ServiceOrderLineModel ServiceOrderLineModel { get; set; }
    }
}
