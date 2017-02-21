using SalesOrder.Common.Enum;
using System.Collections.Generic;
using System.Linq;
using SalesOrder.Common.Error;

namespace SalesOrder.Model.Response
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
                else
                    return ResponseStatus.Success;
            }
        }
    }
}
