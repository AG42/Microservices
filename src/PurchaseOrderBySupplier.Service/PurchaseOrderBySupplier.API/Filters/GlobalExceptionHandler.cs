using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using PurchaseOrderBySupplier.Common;
using PurchaseOrderBySupplier.Common.Logger;

namespace PurchaseOrderBySupplier.API.Filters
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// Handling the Exception Async
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task HandleAsync(ExceptionHandlerContext context,
                                     CancellationToken cancellationToken)
        {
            if (!ShouldHandle(context))
                return Task.FromResult(0);

            return HandleAsyncCore(context, cancellationToken);
        }
        /// <summary>
        /// Handle async Core
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task HandleAsyncCore(ExceptionHandlerContext context,
                                           CancellationToken cancellationToken)
        {
            HandleCore(context);
            return Task.FromResult(0);
        }
        /// <summary>
        /// Manage the Http Resposnes for End user
        /// </summary>
        /// <param name="context"></param>
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

        /// <summary>
        /// Checck the Catch block Level
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool ShouldHandle(ExceptionHandlerContext context)
        {
            return context.ExceptionContext.CatchBlock.IsTopLevel;
        }
    }
}
