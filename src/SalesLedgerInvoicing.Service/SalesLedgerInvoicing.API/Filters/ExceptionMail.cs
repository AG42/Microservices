using System;
using System.Configuration;
using System.Web.Http.ExceptionHandling;
using SalesLedgerInvoicing.Common.Enums;
using SalesLedgerInvoicing.Common.Logger;
using SalesLedgerInvoicing.Common.Mail;

namespace SalesLedgerInvoicing.API.Filters
{
    public class ExceptionMail
    {
        private readonly string _serviceName;
        private readonly string _environment;
        private readonly bool _sendExceptionMail;
        public ExceptionMail()
        {
            _serviceName = ConfigurationManager.AppSettings["ServiceName"];
            _environment = ConfigurationManager.AppSettings["Environment"];
            _sendExceptionMail = Convert.ToBoolean(ConfigurationManager.AppSettings["SendExceptionMail"]);
        }
        public void SendMail(ExceptionHandlerContext exceptionContext)
        {
            try
            {
                if (!_sendExceptionMail)
                    return;

                string mailSubject = $"{_serviceName} service exception";
                string mailMessage = "Please find below exception details<br/><br/>";
                string exceptionMessage = exceptionContext.Exception.InnerException != null
                    ? GetInnerException(exceptionContext.Exception)
                    : exceptionContext.Exception.Message;
                mailMessage += FormatException(exceptionContext.Request.RequestUri.AbsoluteUri,exceptionMessage);
                MailUtility mailUtility = new MailUtility();
                mailUtility.Send(mailSubject, mailMessage);
            }
            catch (Exception ex)
            {
                ApplicationLogger.Errorlog(ex.Message, Category.Unknown, ex.StackTrace);
            }
        }

        private string GetInnerException(Exception ex)
        {
            if (ex.InnerException == null) return string.Empty;

            string exceptionMessage = ex.InnerException.Message;
            if (ex.InnerException.InnerException != null)
                exceptionMessage += $" >> {GetInnerException(ex.InnerException)}";
            return exceptionMessage;
        }

        private string FormatException(string url, string exceptionMessage)
        {
            string finalMessage = "<table border=1 cellspacing=5 cellpadding=5 style='border-collapse:collapse;border:none'>";
            finalMessage += $"<tr><td valign=top style='border:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt'><b>Environment</b></td><td valign=top style='border:solid windowtext 1.0pt;border - left:none; padding: 0in 5.4pt 0in 5.4pt'>{_environment}</td></tr>";
            finalMessage += $"<tr><td valign=top style='border:solid windowtext 1.0pt;border-top:none;padding:0in 5.4pt 0in 5.4pt'><b>Url</b></td><td valign=top style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt'>{url}</td></tr>";
            finalMessage += $"<tr><td valign=top style='border:solid windowtext 1.0pt;border-top:none;padding:0in 5.4pt 0in 5.4pt'><b>Exception Message</b></td><td valign=top style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt'>{exceptionMessage}</td></tr>";
            finalMessage += $"<tr><td valign=top style='border:solid windowtext 1.0pt;border-top:none;padding:0in 5.4pt 0in 5.4pt'><b>Timestamp</b></td><td valign=top style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt'>{DateTime.Now}</td></tr>";
            finalMessage += "</table><br/>";

            return finalMessage;
        }
    }
}