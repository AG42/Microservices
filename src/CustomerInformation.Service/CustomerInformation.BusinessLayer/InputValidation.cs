using CustomerInformation.Common;
using CustomerInformation.Common.Error;
using CustomerInformation.Model.Response;
using System.Linq;

namespace CustomerInformation.BusinessLayer
{
    public static class InputValidation
    {
        public static bool ValidateCompanyCode(string companyCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(companyCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CompanyCodeRequiredMessage ));
            }            

            return response.ErrorInfo.Any();
        }
        public static bool ValidateCustomerName(string customerName, BaseResponse response)
        {
            if (customerName.Length > 35)
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerNameLengthErrorMessage));
            }

            return response.ErrorInfo.Any();
        }

        public static bool ValidateCustomerCode(string customerCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CusotmerCodeIsRequiredMessage));
            }

            return response.ErrorInfo.Any();
        }
    }
}
