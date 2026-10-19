
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace PopPop.ExternalServices
{
    public sealed class GmailEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GmailEmailSender> _logger;

        public GmailEmailSender(
            IConfiguration configuration,
            ILogger<GmailEmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(
            string email,
            string subject,
            string htmlMessage)
        {
            var username = _configuration["Email:Username"];
            var password = _configuration["Email:Password"];
            var host = _configuration["Email:SmtpHost"]
                ?? "smtp.gmail.com";
            var port = _configuration.GetValue<int>(
                "Email:SmtpPort", 587);

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Chưa cấu hình Gmail SMTP.");
            }

            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress(
                _configuration["Email:SenderName"] ?? "PôpPôp",
                username));

            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = subject;

            mail.Body = new BodyBuilder
            {
                HtmlBody = htmlMessage
            }.ToMessageBody();

            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(
                    host, port, SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(username, password);
                await client.SendAsync(mail);

                _logger.LogInformation(
                    "Email sent successfully to {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex, "Failed to send email to {Email}", email);
                throw;
            }
            finally
            {
                if (client.IsConnected)
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}