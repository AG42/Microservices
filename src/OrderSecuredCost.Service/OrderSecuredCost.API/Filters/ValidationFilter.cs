using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using OrderSecuredCost.Common.Error;
using OrderSecuredCost.Model.Response;

namespace OrderSecuredCost.API.Filters
{
   public class ValidationFilter : ActionFilterAttribute
    {
        /// <summary>
        /// This method will execute before the Action is getting executed.
        /// It will valdate the Route parameters.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            BaseResponse response = new BaseResponse();
            string validationMessage = string.Empty;
            foreach (var routeParam in actionContext.Request.GetRouteData().Values)
            {
                if (string.IsNullOrWhiteSpace(Convert.ToString(routeParam.Value)))
                {
                    response.ErrorInfo.Add(new ErrorInfo(Convert.ToString(routeParam.Key) + "  Is Required"));
                }
            }
            if (response.ErrorInfo.Any())
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
