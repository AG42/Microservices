using TaxInvoice.Model.Models;
using System.Collections.Generic;

namespace TaxInvoice.Model.Response
{
   public  class TaxInvoiceResponses: BaseResponse
    {
        public IEnumerable<TaxInvoiceModel> TaxInvoices { get; set; }
    }
}