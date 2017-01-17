using SalesLedgerInvoicing.Common;
using SalesLedgerInvoicing.Common.Error;
using SalesLedgerInvoicing.Common.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SalesLedgerInvoicing.API.Attributes
{
    public class InputValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var errorInfo = new List<ErrorInfo>();

            ValidateStringField(actionContext, Constants.CustomerCode, errorInfo, Constants.CustomerCodeRequiredMessage);
            ValidateStringField(actionContext, Constants.CustomerName, errorInfo, Constants.CustomerNameRequiredMessage);
            ValidateStringField(actionContext, Constants.OrderNumber, errorInfo, Constants.OrderNumberRequiredMessage);
            ValidateStringField(actionContext, Constants.InvoiceNumber, errorInfo, Constants.InvoiceNumberRequiredMessage);
            ValidateStringField(actionContext, Constants.InvoiceFromDate, errorInfo, Constants.InvoiceFromDateRequiredMessage);
            ValidateStringField(actionContext, Constants.InvoiceToDate, errorInfo, Constants.InvoiceToDateRequiredMessage);

            ValidateDateField(actionContext, Constants.InvoiceFromDate, Constants.InvoiceToDate, errorInfo, Constants.InvoiceFromDateInvalidFormatMessage);

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

        private static void ValidateDateField(HttpActionContext actionContext, string invoiceFromDate, string invoiceToDate, List<ErrorInfo> errorInfo, string validationMessage)
        {
            if (!actionContext.ActionArguments.ContainsKey(invoiceFromDate) &&
                !actionContext.ActionArguments.ContainsKey(invoiceToDate)) return;

            if (string.IsNullOrWhiteSpace(invoiceFromDate) || string.IsNullOrWhiteSpace(invoiceToDate))
            {
                errorInfo.Add(new ErrorInfo(validationMessage));
                ApplicationLogger.InfoLogger("Input date cannot be empty");
                return;
            }

            var fromDateValue = Convert.ToDateTime(actionContext.ActionArguments[invoiceFromDate]);
            var toDateValue = Convert.ToDateTime(actionContext.ActionArguments[invoiceToDate]);

            if (fromDateValue <= toDateValue) return;

            errorInfo.Add(new ErrorInfo(Constants.InvoiceFromDateGreaterMessage));
            ApplicationLogger.InfoLogger($" {invoiceToDate} should be greater than {invoiceFromDate}");
        }
    }
}
