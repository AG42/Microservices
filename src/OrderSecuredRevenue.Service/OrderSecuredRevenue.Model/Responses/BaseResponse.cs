using System.Collections.Generic;
using System.Linq;
using OrderSecuredRevenue.Common.Error;
using OrderSecuredRevenue.Common.Enum;

namespace OrderSecuredRevenue.Model.Responses
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
