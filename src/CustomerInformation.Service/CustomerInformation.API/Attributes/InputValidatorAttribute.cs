using CustomerInformation.Common;
using CustomerInformation.Common.Error;
using CustomerInformation.Common.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CustomerInformation.API.Attributes
{
    public class InputValidatorAttribute: ActionFilterAttribute
    {        
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var errorInfo = new List<ErrorInfo>();

            ValidateStringField(actionContext, Constants.CompanyCode, errorInfo, Constants.CompanyCodeRequiredMessage);
            ValidateStringField(actionContext, Constants.CustomerCode, errorInfo, Constants.CustomerCodeIsRequiredMessage);
            ValidateStringField(actionContext, Constants.CustomerName, errorInfo, Constants.CustomerNameIsRequiredMessage);
            ValidateStringField(actionContext, Constants.EmailId, errorInfo, Constants.CustomerEmailIsRequiredMessage);
            ValidateStringField(actionContext, Constants.AlternateCustomerName, errorInfo, Constants.CustomerAlternateNameIsRequiredMessage);
            ValidateStringField(actionContext, Constants.PhoneNumber, errorInfo, Constants.CustomerPhoneNumberIsRequiredMessage);
            ValidateStringField(actionContext, Constants.Category, errorInfo, Constants.CustomerCategoryIsRequiredMessage);
            ValidateStringField(actionContext, Constants.CountryCode, errorInfo, Constants.CustomerCountryCodeIsRequiredMessage);
            
            if(errorInfo.Any())
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorInfo);
        }

        private static void ValidateStringField(HttpActionContext actionContext, string argumentName, List<ErrorInfo> errorInfo, string validationMessage)
        {
            if (actionContext.ActionArguments.ContainsKey(argumentName) && string.IsNullOrWhiteSpace(Convert.ToString(actionContext.ActionArguments[argumentName])))
            {
                errorInfo.Add(new ErrorInfo(validationMessage));
                ApplicationLogger.InfoLogger($"{argumentName} is empty");
            }
        }
    }
}
