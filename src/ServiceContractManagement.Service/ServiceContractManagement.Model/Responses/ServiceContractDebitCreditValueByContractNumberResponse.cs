using System.Collections.Generic;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractDebitCreditValueByContractNumberResponse : BaseResponse
    {
        public List<ServiceContractLinesDebitCreditValueModel> ServiceContractLineItemDebitCreditValue { get; } = new List<ServiceContractLinesDebitCreditValueModel>();
    }
}
