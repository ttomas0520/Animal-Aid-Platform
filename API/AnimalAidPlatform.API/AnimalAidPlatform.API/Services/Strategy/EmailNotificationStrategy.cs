using AnimalAidPlatform.API.Options;
using AnimalAidPlatform.API.Services.Interface;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AnimalAidPlatform.API.Services.Strategy
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        private readonly GmailOptions gmailOptions;

        public EmailNotificationStrategy(IOptions<GmailOptions> gmailOptions)
        {
            this.gmailOptions = gmailOptions.Value;
        }


        public async Task SendNotificationAsync(string message)
        {
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(gmailOptions.Email),
                Subject = "Animail Aid Platform test",
                Body = message
            };

            mailMessage.To.Add(new MailAddress("darcsibarbara@gmail.com"));

            using var smtpClient = new SmtpClient();
            smtpClient.Host = gmailOptions.Host;
            smtpClient.Port = gmailOptions.Port;
            smtpClient.Credentials = new NetworkCredential(
                gmailOptions.Email, gmailOptions.Password
                );
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);
            
        }
    }
}
