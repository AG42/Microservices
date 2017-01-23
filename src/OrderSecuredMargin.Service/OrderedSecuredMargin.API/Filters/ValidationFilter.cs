using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using OrderedSecuredMargin.Common.Error;
using OrderedSecuredMargin.Model.Response;

namespace OrderedSecuredMargin.API.Filters
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
            foreach (var routeParam in actionContext.Request.GetRouteData().Values)
            {
                if (string.IsNullOrWhiteSpace(Convert.ToString(routeParam.Value)))
                {
                    response.ErrorInfo.Add(new ErrorInfo(Convert.ToString(routeParam.Key) + " is Required"));
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
