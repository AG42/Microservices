namespace CustomerInformation.Model.Requests
{
    public class CustomerSearchRequest
    {
        public string CustomerCode { get; set; }
        // public string CountryCode { get; set; }
        public string ComapanyCode { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ComapanyCode);
        }
    }
}
