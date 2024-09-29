using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Options;
using AnimalAidPlatform.API.Services.Interface;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace AnimalAidPlatform.API.Services.Strategy
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        private readonly GmailOptions gmailOptions;
        private readonly ILogger<NotificationBackgroundService> _logger;

        public EmailNotificationStrategy(IOptions<GmailOptions> gmailOptions, ILogger<NotificationBackgroundService> logger)
        {
            this.gmailOptions = gmailOptions.Value;
            this._logger = logger;
        }


        public async Task SendNotificationAsync(Notification notification)
        {
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(gmailOptions.Email),
                Subject = "Animail Aid Platform test",
                Body = notification.Message
            };

            /*mailMessage.To.Add(new MailAddress("darcsibarbara@gmail.com"));

            using var smtpClient = new SmtpClient();
            smtpClient.Host = gmailOptions.Host;
            smtpClient.Port = gmailOptions.Port;
            smtpClient.Credentials = new NetworkCredential(
                gmailOptions.Email, gmailOptions.Password
                );
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mailMessage);*/
            this._logger.Log(LogLevel.Information, message: "Kiküldött email " + notification.Message);

        }
    }
}
