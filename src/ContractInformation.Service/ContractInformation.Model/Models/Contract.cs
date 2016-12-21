
namespace ContractInformation.Model.Models
{
    public class Contract
    {
        public string ServiceContractNumber { get; set; }
        public string CustomerCodeInvoice { get; set; }
        public string Remarks { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Terms { get; set; }
        public string TypeWarranty { get; set; }
        public Customer CustomerInformation { get; set; }
       // public string OurReference { get; set; }
    }
}
