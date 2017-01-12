using CustomerSiteLocation.Common;
using CustomerSiteLocation.Common.Error;
using CustomerSiteLocation.Model.Response;
using System.Linq;

namespace CustomerSiteLocation.BusinessLayer
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
        public static bool ValidateFaxNo(string faxNo, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(faxNo))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.FaxNoRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        public static bool ValidateSiteNo(string siteNo, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(siteNo))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.SiteNoRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        public static bool ValidateTelephoneNo(string telephoneNo, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(telephoneNo))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.TelephoneNoRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        public static bool ValidateCountry(string country, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CountryRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }
        /// <summary>
        /// Validate Zip Code
        /// </summary>
        /// <param name="zipCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateZip(string zipCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ZipRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        public static bool ValidateEmailId(string emailId, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(emailId))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.EmailIdRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        /// <summary>
        /// This method is used to validate Customer Name
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ValidateCustomerName(string customerName, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerNameLengthErrorMessage));
            }
            else if (customerName.Length > 25)
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerNameLengthErrorMessage));
            }
            return response.ErrorInfo.Any();
        }

        public static bool ValidateCustomerCode(string customerCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerCodeIsRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

        public static bool ValidateServiceEngineerCode(string serviceEngineerCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(serviceEngineerCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.ServiceEngineerCodeRequiredMessage));
            }
            return response.ErrorInfo.Any();
        }

    }
}
