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

        public async Task Send(int userId, string email, string ConfirmationToken)
        {
            await emailSender.SendEmailAsync(email, "Solutio - Por favor confirme su correo electronico", GenerateMessage(email, ConfirmationToken));
        }

        private string GenerateMessage(string email, string ConfirmationToken)
        {
            var callbackUrl = GetLink(email, ConfirmationToken);
            var message = $"Por favor confirme su cuenta haciendo click <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' >aquí</a>.";

            return message;
        }

        private string GetLink(string email, string ConfirmationToken)
        {
            var link = $"{urlLogin.UrlConfirmMail}/" + HttpUtility.UrlEncode(email) + "/" + HttpUtility.UrlEncode(ConfirmationToken);

            return link;
        }
    }
}
