using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractTypeByContractNumberResponse : BaseResponse
    {
        public string ContractType { get; set; }
    }
}
