using Microsoft.Extensions.Options;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices;
using Solutio.Core.Services.ApplicationServices.LoginServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace Solutio.Core.Services.ServicesProviders.LoginServices
{
    public class SendConfirmationEmailService : ISendConfirmationEmailService
    {
        private readonly IEmailSender emailSender;
        private readonly UrlLoginSettings urlLogin;

        public SendConfirmationEmailService(IEmailSender emailSender, IOptions<UrlLoginSettings> urlLogin)
        {
            this.emailSender = emailSender;
            this.urlLogin = urlLogin.Value;
        }

        public async Task Send(int userId, string email, string password, string ConfirmationToken)
        {
            try {
                await emailSender.SendEmailAsync(email, "IUSTUM - Activación de su cuenta", GenerateMessage(email, password, ConfirmationToken));
            }
            catch (System.Exception) {
                //log
            }
        }

        private string GenerateMessage(string email, string password, string ConfirmationToken)
        {
            var callbackUrl = GetLink(email, ConfirmationToken);
            var message = $"Por favor confirme su cuenta haciendo click <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' >aquí</a>. <br><br>" +
                "Luego de confirmar podra accededer a su cuenta ingresando su correo y la contraseña que hemos generado para usted: <b>" + password + "</b><br><br>" +
                "Podrá cambiar su contraseña ingresando a la opción <b> Recuperar / Cambiar contraseña <br> disponible en login de la aplicación. </b><br><br>" +
                "Gracias por confiar en IUSTUM y sea bienvenido";

            return message;
        }

        private string GetLink(string email, string ConfirmationToken)
        {
            var link = $"{urlLogin.UrlConfirmMail}/" + HttpUtility.UrlEncode(email) + "/" + HttpUtility.UrlEncode(ConfirmationToken);

            return link;
        }
    }
}
