using System.Collections.Generic;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractUnitPriceByContractNumberResponse: BaseResponse
    {
        public List<ServiceContractLinesUnitPriceModel> ServiceContractLineItemUnitPrice { get; } = new List<ServiceContractLinesUnitPriceModel>();
    }
}
