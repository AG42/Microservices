using OrderedSecuredMargin.Common;
using OrderedSecuredMargin.Common.Error;
using OrderedSecuredMargin.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSecuredMargin.BusinessLayer
{
    public class InputValidation
    {
        /// <summary>
        /// ValidateIsNullorWhiteSpace        /// </summary>
        /// <param name="data"></param>
        /// <param name="response"></param>
        /// <param name="errorMessage"></param>

        /// <returns></returns>
        public static bool ValidateIsNullorWhiteSpace(string data, string errorMessage, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                response.ErrorInfo.Add(new ErrorInfo(errorMessage));
            }
            return response.ErrorInfo.Any();
        }

    }
}
