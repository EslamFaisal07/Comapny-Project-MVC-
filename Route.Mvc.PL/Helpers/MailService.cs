using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MimeKit;
using Route.Mvc.PL.Settings;
using Route.Mvc.PL.Utilites;

namespace Route.Mvc.PL.Helpers
{
    public class MailService(IOptions<MailSettings> _options) : IMailService
    {
        public void Send(Utilites.Email email)
        {

            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Value.Email),
                Subject = email.Subject,
               
                
            };


            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(_options.Value.Email , _options.Value.DisplayName));


            var builder = new BodyBuilder();
            builder.TextBody = email.Body;

            mail.Body = builder.ToMessageBody();


            using var smtp = new SmtpClient();

            smtp.Connect(_options.Value.Host , _options.Value.Port , MailKit.Security.SecureSocketOptions.StartTls);

            smtp.Authenticate(_options.Value.Email, _options.Value.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);








        }

      
    }
}
