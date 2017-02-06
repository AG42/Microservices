using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContractManagement.Model.Responses
{
    public class ServiceContractInvoiceQuantityByContractNumberResponse : BaseResponse
    {
        public List<ServiceContractLinesInvoiceQtyModel> ServiceContractLineItemQuantity { get; } = new List<ServiceContractLinesInvoiceQtyModel>();
    }
}
