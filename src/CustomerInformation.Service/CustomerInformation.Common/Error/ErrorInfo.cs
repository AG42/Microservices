namespace CustomerInformation.Common.Error
{
    public class ErrorInfo
    {
        public ErrorInfo(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
