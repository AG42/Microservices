using System.Collections.Generic;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractInvoiceQuantityByContractNumberResponse : BaseResponse
    {
        public List<ServiceContractLinesInvoiceQtyModel> ServiceContractLineItemQuantity { get; } = new List<ServiceContractLinesInvoiceQtyModel>();
    }
}
