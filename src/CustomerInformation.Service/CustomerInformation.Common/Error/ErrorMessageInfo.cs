using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInformation.Common.Error
{
    public class ErrorMessageInfo
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public static class ErrorCode
    {
       
    }

    public static class Error
    {
        private static List<ErrorMessageInfo> _errors;

        public static List<ErrorMessageInfo> Errors
        {
            get
            {
                if (_errors == null)
                { return new List<ErrorMessageInfo>(); }

                return _errors;
            }
        }

        public static void AddError(string message, HttpStatusCode statusCode)
        {
            Error.Errors.Clear();
            Error.Errors.Add(new ErrorMessageInfo { Message = message, StatusCode = statusCode });
        }
    }
}
