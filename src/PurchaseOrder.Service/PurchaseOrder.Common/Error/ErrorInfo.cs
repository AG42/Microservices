using System.Runtime.Serialization;

namespace PurchaseOrder.Common.Error
{
    [DataContract]
    public class ErrorInfo
    {
        public ErrorInfo(string message)
        {
            Message = message;
        }

        [DataMember]
        public string Message { get; set; }
    }
}
