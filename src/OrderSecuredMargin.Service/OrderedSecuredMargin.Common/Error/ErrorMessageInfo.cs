using System.Net;
namespace OrderedSecuredMargin.Common.Error
{
    public class ErrorMessageInfo
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
