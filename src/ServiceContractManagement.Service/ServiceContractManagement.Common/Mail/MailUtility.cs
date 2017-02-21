using System.Net.Mail;

namespace ServiceContractManagement.Common.Mail
{
    public class MailUtility
    {
        private readonly MailConfiguration _mailConfiguration;

        public MailUtility()
        {
            _mailConfiguration = new MailConfiguration();
        }
        public void Send(string subject, string message)
        {
            SmtpClient smtpClient = new SmtpClient(_mailConfiguration.Server);
            smtpClient.Send(CreateMailMessage(subject, message));
        }

        private MailMessage CreateMailMessage(string subject, string message)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_mailConfiguration.From),
                Subject = subject
            };
            mailMessage.To.Add(_mailConfiguration.To);
            if (!string.IsNullOrWhiteSpace(_mailConfiguration.CC))
                mailMessage.CC.Add(_mailConfiguration.CC);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = FormatBody(message);
            return mailMessage;
        }

        private string FormatBody(string bodyMessage)
        {
            string bodyStart = Constants.MailBodyStart;
            string bodyEnd = Constants.MailBodyEnd;
            return bodyStart + bodyMessage + bodyEnd;
        }
    }
}
