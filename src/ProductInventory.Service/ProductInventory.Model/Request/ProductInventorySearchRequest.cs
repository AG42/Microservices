namespace ProductInventory.Model.Request
{
   public class ProductInventorySearchRequest
    {
        public string CustomerCode { get; set; }
       
        public string ProductCode { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ProductCode);
        }
    }
}
