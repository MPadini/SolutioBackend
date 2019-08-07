using Solutio.Core.Services.ServicesProviders.LoginServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.LoginServices
{
    public class SendConfirmationEmailService : ISendConfirmationEmailService
    {
        private readonly IEmailSender emailSender;

        public SendConfirmationEmailService(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task Send(int userId, string email, string ConfirmationToken)
        {
            await emailSender.SendEmailAsync(email, "Solutio - Por favor confirme su correo electronico", GenerateMessage(userId, ConfirmationToken));
        }

        private string GenerateMessage(int userId, string ConfirmationToken)
        {
            var callbackUrl = GetLink(userId, ConfirmationToken);
            var message = $"Por favor confirme su cuenta haciendo click <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' >aquí</a>.";

            return message;
        }

        private string GetLink(int userId, string ConfirmationToken)
        {
            var link = $"http://algunadireccion.com/?id='{userId}'&amp;code='{ConfirmationToken}'";

            return link;
        }
    }
}
