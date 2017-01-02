using System.Runtime.Serialization;

namespace OrderSecuredRevenue.Common.Error
{
    [DataContract]
    public class ErrorInfo
    {
        public ErrorInfo(string message)
        {
            ErrorMessage = message;
        }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
