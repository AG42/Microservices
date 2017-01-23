using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderedSecuredMargin.Common.Enum;
using OrderedSecuredMargin.Common.Error;

namespace OrderedSecuredMargin.Model.Response
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
