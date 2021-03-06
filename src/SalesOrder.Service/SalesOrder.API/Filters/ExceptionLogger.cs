﻿using SalesOrder.Common.Logger;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace SalesOrder.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class ExceptionLogger : IExceptionLogger
    {
        /// <summary>
        /// Logging Exception Details
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            ApplicationLogger.Errorlog(context.Exception.Message, Category.Unknown, context.Exception.StackTrace,
                context.Exception.InnerException);
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