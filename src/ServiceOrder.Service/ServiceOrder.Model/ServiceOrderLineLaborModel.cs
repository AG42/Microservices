using System;

namespace ServiceOrder.Model
{
    public class ServiceOrderLineLaborModel
    {
        public string Id { get; set; }
        public string ERP_SO_Line_Number__c { get; set; }
        public string SVMXC__Service_Order__c { get; set; }
        public double Line_Cost_per_unit__c { get; set; }
        public string SVMXC__Line_Status__c { get; set; }
        public string SVMXC__Line_Type__c { get; set; }
        public string Invoice_Number__c { get; set; }
        public DateTimeOffset? Invoice_Date__c { get; set; }
        public double ERP_Billable_Hours__c { get; set; }

    }
}
