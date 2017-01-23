using System;
using System.Net;

namespace OrderSecuredCost.Common.Error
{
    public class ErrorMessageInfo
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
