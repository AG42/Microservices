using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSiteLocation.Model
{
   public class CustomerSiteLocationModel
    {
        public string Name { get; set; }
        public string ERP_Customer_Code__c { get; set; }
        public string ERP_Site_Code__c { get; set; }
        public string SVMXC__Street__c { get; set; }
        public string SVMXC__City__c { get; set; }
        public string County__c { get; set; }
        public string SVMXC__State__c { get; set; }
        public string Region__c { get; set; }
        public string SVMXC__Country__c { get; set; }
        public string SVMXC__Zip__c { get; set; }
        public string SVMXC__Site_Fax__c { get; set; }
        public string SVMXC__Site_Phone__c { get; set; }
        public string SVMXC__Email__c { get; set; }
        public string SVMXC__Latitude__c { get; set; }
        public string SVMXC__Longitude__c { get; set; }
        public string Altitude__c { get; set; }
        public string SVMXC__Service_Engineer__c { get; set; }
        public string Service_Engineer_Code__c { get; set; }
        public bool SVMXC__Stocking_Location__c { get; set; }
        public string ERP_Location_ID__c { get; set; }

    }
}
