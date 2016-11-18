using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CustomerInformation.Common;
using CustomerInformation.Common.Logger;

namespace CustomerInformation.API.Filters
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
