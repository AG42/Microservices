using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Web.Http.ExceptionHandling;
using OrderedSecuredMargin.Common.Logger;

namespace OrderedSecuredMargin.API.Filters
{
    public class ExceptionLogger : IExceptionLogger
    {
        /// <summary>
        /// Logger is used for logging the Exceptions occured
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            ApplicationLogger.Errorlog(context.Exception.Message, Category.Unknown, context.Exception.StackTrace,
                context.Exception.InnerException);
            //Extarct caused exception details
            StackTrace stackTrace = new StackTrace(context.Exception, true);
            var exceptionFrame =
                stackTrace.GetFrames()?.FirstOrDefault(frame => !string.IsNullOrEmpty(frame.GetFileName()));
            var fileName = exceptionFrame?.GetFileName();
            var method = exceptionFrame?.GetMethod();
            var line = exceptionFrame?.GetFileLineNumber();
            var methodDetails = "Source File : " + fileName + " Method : " + method + " Line No : " + line;
            ApplicationLogger.Errorlog(methodDetails, Category.Unknown, context.Exception.StackTrace,
                context.Exception.InnerException);
            return Task.FromResult(0);
        }
    }
}
