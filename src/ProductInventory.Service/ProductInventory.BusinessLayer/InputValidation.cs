using System.Linq;
using ProductInventory.Common;
using ProductInventory.Common.Error;
using ProductInventory.Model.Response;

namespace ProductInventory.BusinessLayer
{
    public static class InputValidation
    {
        public static bool ValidateCompanyCode(string companyCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(companyCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CompanyCodeRequiredMessage));
            }

            return response.ErrorInfo.Any();
        }

        public static bool ValidateProductCode(string productcode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(productcode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CompanyCodeRequiredMessage));
            }

            return response.ErrorInfo.Any();
        }

        public static bool ValidateDescription(string description, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.DescriptionIsRequiredMessage));
            }

            return response.ErrorInfo.Any();
        }

        public static bool ValidateLocationId(string locationId, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(locationId))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.LocationIsRequiredMessage));
            }

            return response.ErrorInfo.Any();
        }
    }
}
