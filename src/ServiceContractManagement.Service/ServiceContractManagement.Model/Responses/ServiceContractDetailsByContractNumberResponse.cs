namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractDetailsByContractNumberResponse : BaseResponse
    {
        public ServiceContractMasterModel ServiceContractDetails { get; set; } = new ServiceContractMasterModel();
    }
}
