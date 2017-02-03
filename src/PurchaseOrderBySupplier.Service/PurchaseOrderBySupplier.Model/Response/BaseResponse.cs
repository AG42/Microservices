using PurchaseOrderBySupplier.Common.Enum;
using PurchaseOrderBySupplier.Common.Error;
using System.Collections.Generic;
using System.Linq;

namespace PurchaseOrderBySupplier.Model.Response
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
