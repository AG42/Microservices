using ProductInventory.Common;

namespace ProductInventory.Model
{
    public class ProductWarehouseModel
    {
        //public int SVMXC__Allocated_Qty__c { get; set; }
        //public int SVMXC__Available_Qty__c { get; set; }
        //public string SVMXC__IsPartner__c { get; set; }
        //public string SVMXC__IsPartnerRecord__c { get; set; }
        public string SVMXC__Location__c { get; set; }
        //public string SVMXC__Partner_Account__c { get; set; }
        //public string SVMXC__Partner_Contact__c { get; set; }
        public string SVMXC__Product__c { get; set; }
        //public float SVMXC__Product_Cost__c { get; set; }
        public double SVMXC__Quantity2__c { get; set; }
        //public string SVMXC__Reorder_Level2__c { get; set; }
        //public int SVMXC__Reorder_Quantity2__c { get; set; }
        //public int SVMXC__Required_Quantity2__c { get; set; }
        public string SVMXC__Status__c { get; set; } = Constants.SvmxcStatus;
        //public string SVMXC__Stock_Value__c { get; set; }
        //public string Name { get; set; }
        public string ERP_Location_ID__c { get; set; }
        public string ERP_Stock_Code__c { get; set; }
        public double ERP_Reserved_Quantity_c { get; set; }
        public double ERP_Available_Quantity_c { get; set; }
        public string ERP_Stock_Location_Code_Key__c { get; set; }
        public string ERP_Company_Code__c { get; set; }
    }
}
