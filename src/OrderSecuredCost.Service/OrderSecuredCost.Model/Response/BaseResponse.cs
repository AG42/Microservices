using System;
using System.Collections.Generic;
using System.Linq;
using OrderSecuredCost.Common.Enum;
using OrderSecuredCost.Common.Error;

namespace OrderSecuredCost.Model.Response
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
