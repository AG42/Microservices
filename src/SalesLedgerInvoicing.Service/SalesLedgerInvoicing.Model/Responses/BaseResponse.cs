using System.Collections.Generic;
using System.Linq;
using SalesLedgerInvoicing.Common.Error;
using SalesLedgerInvoicing.Common.Enums;

namespace SalesLedgerInvoicing.Model.Responses
{
    public class BaseResponse
    {
        public List<ErrorInfo> ErrorInfo { get; set; } = new List<ErrorInfo>();

        public string SuccessMessage { get; set; }

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

