using CustomerProjectOrder.Common;
using CustomerProjectOrder.Common.Error;
using CustomerProjectOrder.Model.Response;
using System.Linq;

namespace CustomerProjectOrder.BusinessLayer
{
    /// <summary>
    /// This Calss is used for performing business validations on model fields
    /// </summary>
    public class InputValidation
    {
        /// <summary>
        /// This method is used to validate Company Code
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
        /// This method is used to validate Project Number
        /// </summary>
        /// <param name="projectNumber"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateProjectNumber(string projectNumber, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(projectNumber))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ProjectNumberRequired));
            }
            else if (projectNumber.Length > 12)
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ProjectNumberLengthErrorMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// This method is used to validate Project Name
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateProjectName(string projectName, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ProjectNameRequired));
            }
            else if (projectName.Length > 25)
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ProjectNameLengthErrorMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// This Method is used to validate Project duration i.e Start Date and End Date
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateDuration(string startDate, string endDate, BaseResponse response)
        {
            if ( string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ProjectDurationRequired));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// This method is used to validate Customer Po Number
        /// </summary>
        /// <param name="customerPONo"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateCustomerPONo(string customerPONo, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(customerPONo))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerPoNoRequired));
            }
            else if (customerPONo.Length > 20)
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerPoNoLengthErrorMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// This method is used to validate Account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateAccount(string account, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.AccountRequired));
            }
            else if (account.Length > 18)
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.AccountLengthErrorMessage));
            }
            return response.ErrorInfo.Any();
        }
    }
}
