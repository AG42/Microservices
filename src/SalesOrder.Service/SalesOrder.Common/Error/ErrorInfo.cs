using System.Runtime.Serialization;
namespace SalesOrder.Common.Error
{
    [DataContract]
    public class ErrorInfo
    {
        public ErrorInfo(string message)
        {
            Message = message;
        }

        [DataMember]
        public string Message;
    }
}
