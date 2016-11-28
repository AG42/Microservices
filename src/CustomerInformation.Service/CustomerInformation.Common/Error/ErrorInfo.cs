namespace CustomerInformation.Common.Error
{
    public class ErrorInfo
    {
        public ErrorInfo(string message)
        {
            ErrorMessage = message;
        }

        public string ErrorMessage { get; set; }
    }
}
