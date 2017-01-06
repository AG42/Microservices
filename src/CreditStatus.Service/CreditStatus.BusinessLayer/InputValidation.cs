using System.Linq;
using CreditStatus.Common;
using CreditStatus.Model.Response;
using CreditStatus.Common.Error;

namespace CreditStatus.BusinessLayer
{
    public class InputValidation
    {
        /// <summary>
        /// Validate Company Code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateCompanyCode(string companyCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(companyCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CompanyCodeRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// Validate Customer Code
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateCustomerCode(string customerCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerCodeRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// Validate customerName
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateCustomerName(string customerName, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerNameIsRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

    }
}
