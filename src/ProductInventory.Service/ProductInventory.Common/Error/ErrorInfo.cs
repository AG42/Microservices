using System;

namespace ProductInventory.Common.Error
{
    [Serializable]
    public class ErrorInfo
    {
        public ErrorInfo(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
