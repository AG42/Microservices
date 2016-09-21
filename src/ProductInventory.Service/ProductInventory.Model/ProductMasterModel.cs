namespace ProductInventory.Model
{
    public  class ProductMasterModel
    {
        public string ERP_Company_Code__c { get; set; }
        public string CurrencyIsoCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SVMXC__Stockable__c { get; set; }
        public int SVMXC__Unit_Of_Measure__c { get; set; }
        public string Family { get; set; }
        public bool IsActive { get; set; }

        public string ProductCode { get; set; }
        //public string ERP_Available_Quantity_c { get; set; }

        public string ERP_ID__c { get; set; }
        public double SVMXC__Product_Cost__c { get; set; }
        public string SVMXC__Product_Line__c { get; set; }
        //public string ERP_Location_ID_c { get; set; }
        //public string ERP_Reserved_Quantity_c { get; set; }
        //public string ERP_Stock_Code__c { get; set; }
        //public string ERP_Stock_Location_Code_Key__c { get; set; }

        //public string SVMXC__Allocated_Qty__c { get; set; }
        //public string SVMXC__Available_Qty__c { get; set; }
        //public string SVMXC__IsPartner__c { get; set; }
        //public string SVMXC__IsPartnerRecord__c { get; set; }
        //public string SVMXC__Location__c { get; set; }
        //public string SVMXC__Partner_Account__c { get; set; }
        //public string SVMXC__Partner_Contact__c { get; set; }
        //public string SVMXC__Product__c { get; set; }

        //public string SVMXC__Quantity2__c { get; set; }
        //public string SVMXC__Reorder_Level2__c { get; set; }
        //public string SVMXC__Reorder_Quantity2__c { get; set; }
        //public string SVMXC__Required_Quantity2__c { get; set; }
        //public string SVMXC__Status__c { get; set; }
        //public string SVMXC__Stock_Value__c { get; set; }

    }
}
