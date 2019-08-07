using Microsoft.Extensions.Options;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential(emailSettings.Sender, emailSettings.Password);

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(emailSettings.Sender, emailSettings.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = emailSettings.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = emailSettings.MailServer,
                    EnableSsl = true,
                    Credentials = credentials
                };

                // Send it...         
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}

