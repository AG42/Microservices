using System.Net;

namespace ProductInventory.Common.Error
{
    public class ErrorMessageInfo
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }    
}
