using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace P9YS.Common
{
    public static class SendEmailHelper
    {
        public static void SendEmail(EmailConfig config)
        {
            SmtpClient client = new SmtpClient(config.Host)
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(config.FromEmail, config.FromPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            MailMessage message = new MailMessage(config.FromEmail, config.ToEmail, config.Title, config.Body)
            {
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = config.IsHtml
            };
            client.Send(message);
        }

        public static async Task SendEmailAsync(EmailConfig config)
        {
            SmtpClient client = new SmtpClient(config.Host)
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(config.FromEmail, config.FromPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            MailMessage message = new MailMessage(config.FromEmail, config.ToEmail, config.Title, config.Body)
            {
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = config.IsHtml
            };
            await client.SendMailAsync(message);
        }
    }

    public class EmailConfig
    {
        public string Host { get; set; }

        public string FromEmail { get; set; }

        public string FromPassword { get; set; }

        public string ToEmail { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }
    }
}
