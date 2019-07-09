using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TrivagoFinance.Ui.Controllers.Services
{
    public interface IEmailService {
        bool SendEmail(string email, string name, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailConfig ec;

        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            ec = emailConfig.Value;
        }

        public bool SendEmail(String email, String name, String subject, String message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(ec.FromName, ec.FromAddress));
                emailMessage.To.Add(new MailboxAddress(name, email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = message };

                using (var client = new SmtpClient())
                {
                    //client.LocalDomain = ec.LocalDomain;

                    client.Connect(ec.MailServerAddress, Convert.ToInt32(ec.MailServerPort), SecureSocketOptions.Auto);
                    client.Authenticate(new NetworkCredential(ec.UserId, ec.UserPassword));
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }

    public class EmailConfig
    {
        public String FromName { get; set; }
        public String FromAddress { get; set; }

        public String LocalDomain { get; set; }

        public String MailServerAddress { get; set; }
        public String MailServerPort { get; set; }

        public String UserId { get; set; }
        public String UserPassword { get; set; }
    }

}
