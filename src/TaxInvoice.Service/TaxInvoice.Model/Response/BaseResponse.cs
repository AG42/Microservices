using System.Collections.Generic;
using System.Linq;
using TaxInvoice.Common.Enum;
using TaxInvoice.Common.Error;

namespace TaxInvoice.Model.Response
{
    public class BaseResponse
    {
        public List<ErrorInfo> ErrorInfo { get; } = new List<ErrorInfo>();

        public ResponseStatus Status
        {
            get
            {
                if (ErrorInfo.Any())
                    return ResponseStatus.Failure;
                return ResponseStatus.Success;
            }
        }
    }
}