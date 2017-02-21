using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ServiceContractManagement.Common.Error;
using ServiceContractManagement.Common.Logger;

namespace ServiceContractManagement.API.Attributes
{
    public class InputValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var errorInfo = new List<ErrorInfo>();

            if (errorInfo.Any())
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
