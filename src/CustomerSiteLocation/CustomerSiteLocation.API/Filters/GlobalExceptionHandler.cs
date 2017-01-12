using System.Threading;
using System.Threading.Tasks;
using CustomerSiteLocation.Common.Logger;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Net;
using System.Net.Http;
using CustomerSiteLocation.Common;

namespace CustomerSiteLocation.API.Filters
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public virtual Task HandleAsync(ExceptionHandlerContext context,
                                     CancellationToken cancellationToken)
        {
            if (!ShouldHandle(context))
                return Task.FromResult(0);

            return HandleAsyncCore(context, cancellationToken);
        }

        public virtual Task HandleAsyncCore(ExceptionHandlerContext context,
                                           CancellationToken cancellationToken)
        {
            HandleCore(context);
            return Task.FromResult(0);
        }

        public virtual void HandleCore(ExceptionHandlerContext context)
        {
            if (context.Exception is HttpResponseException)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                context.Request.CreateResponse(HttpStatusCode.NotFound, Constants.NoDataFoundMessage);
            }
            else
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                context.Request.CreateResponse(HttpStatusCode.InternalServerError, context.Exception.Message);
            }
        }

        public virtual bool ShouldHandle(ExceptionHandlerContext context)
        {
            return context.ExceptionContext.CatchBlock.IsTopLevel;
        }
    }
}
