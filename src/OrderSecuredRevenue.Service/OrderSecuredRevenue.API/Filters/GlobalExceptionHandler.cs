﻿using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Net;
using System.Net.Http;
using OrderSecuredRevenue.Common.Logger;
using OrderSecuredRevenue.Common;

namespace OrderSecuredRevenue.API.Filters
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ExceptionMail _exceptionMail = new ExceptionMail();

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
                _exceptionMail.SendMail(context);
            }
        }

        public virtual bool ShouldHandle(ExceptionHandlerContext context)
        {
            return context.ExceptionContext.CatchBlock.IsTopLevel;
        }
    }
}
