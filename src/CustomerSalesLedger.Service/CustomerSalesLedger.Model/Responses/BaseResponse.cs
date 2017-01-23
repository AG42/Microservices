using CustomerSalesLedger.Common.Enums;
using CustomerSalesLedger.Common.Error;
using System.Collections.Generic;
using System.Linq;

namespace CustomerSalesLedger.Model.Response
{
    public class BaseResponse
    {
        public List<ErrorInfo> ErrorInfo { get; } = new List<ErrorInfo>();

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
