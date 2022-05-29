using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Tamgy_API.Helpers
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                //Draft Email
                var mail = new MimeMessage();
                mail.Subject = subject;
                mail.From.Add(MailboxAddress.Parse("sahil.malhotra@TangyApp.com"));
                mail.To.Add(MailboxAddress.Parse(email));
                mail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

                //Send Email
                using var emailClient = new SmtpClient();
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("sahildevtest@gmail.com", "Admin@123");
                await emailClient.SendAsync(mail);
                emailClient.Disconnect(true);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
