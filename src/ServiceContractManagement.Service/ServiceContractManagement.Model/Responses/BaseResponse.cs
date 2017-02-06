using ServiceContractManagement.Common.Enums;
using ServiceContractManagement.Common.Error;
using System.Collections.Generic;
using System.Linq;

namespace ServiceContractManagement.Model.Responses
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
