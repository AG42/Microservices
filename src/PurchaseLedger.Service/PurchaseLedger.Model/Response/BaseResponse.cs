using PurchaseLedger.Common.Error;
using System.Collections.Generic;
using System.Linq;
using PurchaseLedger.Common.Enum;

namespace PurchaseLedger.Model.Response
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
