using Microsoft.Extensions.Options;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices;
using Solutio.Core.Services.ApplicationServices.LoginServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace Solutio.Core.Services.ServicesProviders.LoginServices
{
    public class SendResetPasswordService : ISendResetPasswordService
    {
        private readonly IEmailSender emailSender;
        private readonly UrlLoginSettings urlLogin;

        public SendResetPasswordService(IEmailSender emailSender, IOptions<UrlLoginSettings> urlLogin)
        {
            this.emailSender = emailSender;
            this.urlLogin = urlLogin.Value;
        }

        public async Task Send(int userId, string email, string ConfirmationToken)
        {
            await emailSender.SendEmailAsync(email, "Iustum - Modificación de contraseña", GenerateMessage(email, ConfirmationToken));
        }

        private string GenerateMessage(string email, string ConfirmationToken)
        {
            var callbackUrl = GetLink(email, ConfirmationToken);
            var message = $"Hola " + email + "! <br><br>Para modificar su contraseña haga click en el siguiente enlace: <a href = '"+ HtmlEncoder.Default.Encode(callbackUrl) + "' > CAMBIAR CONTRASEÑA </a>. <br><br>Le recomendamos incluir alguna mayuscula y algun caracter númerico para mayor seguridad. <br><br> Saludos";

            return message;
        }

        private string GetLink(string email, string ConfirmationToken)
        {
            var link = $"{urlLogin.UrlResetPassword}/" + HttpUtility.UrlEncode(email) + "/" + HttpUtility.UrlEncode(ConfirmationToken);

            return link;
        }
    }
}
