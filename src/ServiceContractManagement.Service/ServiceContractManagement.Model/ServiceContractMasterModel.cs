using System.Collections.Generic;

namespace ServiceContractManagement.Model
{
    public class ServiceContractMasterModel 
    {
        public string ServiceContractNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerReference { get; set; }
        public string PaymantTerms { get; set; }
        public string ContractDate { get; set; }
        public string InvoicingInterval { get; set; }
        public string ContractCode { get; set; }
        public string ContractType { get; set; }
        public string ContractValue { get; set; }
        public string ContractStartDate { get; set; }
        public string ContractEndDate { get; set; }
        public string SalesmanNo { get; set; }
        public string OurReference { get; set; }
        public string InvoiceCurrencyCode { get; set; }
        public string ContractCurrencyCode { get; set; }
        public string CustomerAlphaSearchKey { get; set; }

        public List<ServiceContractLinesModel> ServiceContractLineDetails { get; } = new List<ServiceContractLinesModel>();
    }
}
