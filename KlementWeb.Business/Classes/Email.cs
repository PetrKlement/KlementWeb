using KlementWeb.Business.Classes;
using KlementWeb.Business.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace KlementWeb.Business.Services
{
    public class Email : IEmail
    {
        private const string emailSender = "postmaster@klementpetr.cz";
        private EmailConfiguration emailConfiguration;

        public Email(EmailConfiguration emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration;
        }

        public void SendEmail(string receiverEmail, string subject, string emailBody, string parameterEmailSender = null)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(string.Empty, receiverEmail));
            message.From.Add(new MailboxAddress(string.Empty,  emailSender));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody + "-" + parameterEmailSender
            };

            using (SmtpClient emailClient = new MailKit.Net.Smtp.SmtpClient())
            {
                emailClient.Connect(emailConfiguration.SmtpServer, emailConfiguration.SmtpPort, SecureSocketOptions.StartTls);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
    }
}
