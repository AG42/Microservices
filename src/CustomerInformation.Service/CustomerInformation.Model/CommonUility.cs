//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CustomerInformation.Model
//{
//    public class CommonUility
//    {

//    }

//    public class Error
//    {
//        public ErrorMessageInfo ErrorMessage { get; set; }
//        private Dictionary<int, ErrorMessageInfo> _errorList;
//        public Dictionary<int, ErrorMessageInfo> ErrorList
//        {
//            get
//            {
//                if (_errorList == null)
//                {
//                    return new Dictionary<int, ErrorMessageInfo>();
//                }

//                return _errorList;
//            }
//        }
//    }

//    public class ErrorMessageInfo
//    {
//        public string Message { get; set; }

//        public int ErrorCode { get; set; }
//    }

//    public static class ErrorCodes
//    {
//        [Description("Un Authorized request")]
//        public const int UnAuthorized = 401;
//    }

//    public sealed class DescriptionAttribute : Attribute
//    {
//        public DescriptionAttribute(string description)
//        {
//            Description = description;
//        }

//        public string Description { get; set; }
//    }

//}
