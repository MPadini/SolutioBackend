using Solutio.Core.Services.ApplicationServices;
using Solutio.Core.Services.ApplicationServices.LoginServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.LoginServices
{
    public class SendResetPasswordService : ISendResetPasswordService
    {
        private readonly IEmailSender emailSender;

        public SendResetPasswordService(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task Send(int userId, string email, string ConfirmationToken)
        {
            await emailSender.SendEmailAsync(email, "Solutio - Modificación de contraseña", GenerateMessage(userId, ConfirmationToken));
        }

        private string GenerateMessage(int userId, string ConfirmationToken)
        {
            var callbackUrl = GetLink(userId, ConfirmationToken);
            var message = $"Para modificar su contraseña haga click <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' >aquí</a>.";

            return message;
        }

        private string GetLink(int userId, string ConfirmationToken)
        {
            var link = $"http://algunadireccion.com/?id='{userId}'&amp;code='{ConfirmationToken}'";

            return link;
        }
    }
}
