using System.Collections.Generic;
using ContractInformation.Model.Models;

namespace ContractInformation.Model.Response
{
    public class ContractsResponse : BaseResponse
    {
        public IEnumerable<Contract> Contracts { get; set; }
    }
}
