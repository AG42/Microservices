using OrderSecuredCost.Common;
using OrderSecuredCost.Common.Error;
using OrderSecuredCost.Model.Response;
using System.Linq;

namespace OrderSecuredCost.BusinessLayer
{
    static class InputValidation
    {
        /// <summary>
        /// This method is used to validate if given data is null or empty or white space
        /// </summary>
        /// <param name="data">data to check of null or empty</param>
        /// <param name="errorMessage">error message if data is null or empty</param>
        /// <param name="response">response message for which response will gets updated</param>
        /// <returns></returns>
        public static bool ValidateNullOrEmpty(string data, string errorMessage, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                response.ErrorInfo.Add(new ErrorInfo(errorMessage));
            }
            return response.ErrorInfo.Any();
        }
    }
}