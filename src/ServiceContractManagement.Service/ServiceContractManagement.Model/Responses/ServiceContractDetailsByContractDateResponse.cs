using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractDetailsByContractDateResponse : BaseResponse
    {
        public List<ServiceContractMasterModel> ServiceContractDetails { get;  } = new List<ServiceContractMasterModel>();
    }
}
