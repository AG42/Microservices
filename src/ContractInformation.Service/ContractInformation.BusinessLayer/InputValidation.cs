using System.Linq;
using ContractInformation.Common;
using ContractInformation.Model.Response;
using ContractInformation.Common.Error;

namespace ContractInformation.BusinessLayer
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

        /// <summary>
        /// Validate poNumber
        /// </summary>
        /// <param name="poNumber"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidatePONumber(string poNumber, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(poNumber))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerPONumberIsRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// Validate Request Number
        /// </summary>
        /// <param name="requestNumber"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateRequestNumber(string requestNumber, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(requestNumber))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.RequestNumberIsRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// Validate Contract Number
        /// </summary>
        /// <param name="serviceContractNumber"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateServiceContractNumber(string serviceContractNumber, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(serviceContractNumber))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ServiceContractNumberIsRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// Validate Status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateStatus(string status, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.StatusIsRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// Validate Reference
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateReference(string reference, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ReferenceIsRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// Validate Search Key
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateSearchKey(string searchKey, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.SearchKeyIsRequired));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// Validate Date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateDate(string date, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.DateIsRequired));
            }
            return response.ErrorInfo.Any();
        }
    }
}
