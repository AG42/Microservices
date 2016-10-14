using ServiceOrder.Common;
using ServiceOrder.Common.Enum;
using ServiceOrder.Common.Logger;
using ServiceOrder.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceOrder.API.Controllers
{
    public class BaseController: ApiController
    {
        [NonAction]
        protected HttpResponseMessage GetErrorJsonResponse(BaseResponse response, Category category)
        {
            ApplicationLogger.Errorlog(response.ErrorInfo.FirstOrDefault().ErrorMessage, category, null);

            return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, Constants.NoDataFoundMessage);
        }

        [NonAction]
        protected HttpResponseMessage GetErrorJsonResponse(string displayMessage, Category category, Exception ex)
        {
            ApplicationLogger.Errorlog(ex.Message, category, ex.StackTrace, ex.InnerException);
            return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, Constants.NoDataFoundMessage);
        }
    }
}
