using PurchaseOrder.Common.Enum;
using System.Collections.Generic;
using System.Linq;
using PurchaseOrder.Common.Error;

namespace PurchaseOrder.Model.Response
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
