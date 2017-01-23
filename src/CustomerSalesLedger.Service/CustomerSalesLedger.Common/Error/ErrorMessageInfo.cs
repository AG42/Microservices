using System.Net;

namespace CustomerSalesLedger.Common.Error
{
    public class ErrorMessageInfo
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }    
}
