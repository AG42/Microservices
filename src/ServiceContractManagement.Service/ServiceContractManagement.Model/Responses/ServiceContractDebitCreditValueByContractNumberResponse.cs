using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractDebitCreditValueByContractNumberResponse : BaseResponse
    {
        public List<ServiceContractLinesDebitCreditValueModel> ServiceContractLineItemDebitCreditValue { get; } = new List<ServiceContractLinesDebitCreditValueModel>();
    }
}
