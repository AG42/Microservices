using System.Collections.Generic;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractDetailsByContractDateResponse : BaseResponse
    {
        public List<ServiceContractMasterModel> ServiceContractDetails { get;  } = new List<ServiceContractMasterModel>();
    }
}
