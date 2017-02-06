using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractValueByContractNumberResponse:BaseResponse
    {
        public string ContractValue { get; set; }
    }
}
