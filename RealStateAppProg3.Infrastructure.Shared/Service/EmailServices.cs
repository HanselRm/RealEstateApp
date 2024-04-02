using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RealStateAppProg3.Core.Application.Dtos.Email;
using RealStateAppProg3.Core.Domain.Settings;
using MailKit.Net.Smtp;

namespace RealStateAppProg3.Infrastructure.Shared.Service
{
    public class EmailServices
    {
        private MailSettings _mailSettings { get; }
        public EmailServices(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task sendAsync(EmailRequest request)
        {

            try
            {
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();


                using SmtpClient smtp = new();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
