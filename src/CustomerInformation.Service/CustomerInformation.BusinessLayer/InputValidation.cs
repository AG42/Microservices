using CustomerInformation.Common;
using CustomerInformation.Common.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInformation.BusinessLayer
{
    public static class InputValidation
    {
        public static bool ValidateCompanyCode(string companyCode)
        {
            if (string.IsNullOrWhiteSpace(companyCode))
            {
                Error.AddError(Constants.CompanyCodeRequired, HttpStatusCode.Ambiguous);
            }            

            return Error.Errors.Any();
        }
        public static bool ValidateCustomerName(string customerName)
        {
            if (customerName.Length > 35)
            {
                Error.AddError(Constants.CustomerNameLengthError, HttpStatusCode.Ambiguous);
            }

            return Error.Errors.Any();
        }

        public static bool ValidateCustomerCode(string customerCode)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                Error.AddError(Constants.CusotmerCodeIsRequired, HttpStatusCode.Ambiguous);
            }

            return Error.Errors.Any();
        }
    }
}
