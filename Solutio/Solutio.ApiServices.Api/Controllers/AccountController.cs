using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Solutio.Infrastructure.Repositories.Entities;
using Solutio.ApiServices.Api.Dtos;
using Solutio.ApiServices.Api.Builder;
using Microsoft.AspNetCore.Cors;
using Solutio.Core.Services.ApplicationServices;
using Solutio.Core.Services.ServicesProviders.LoginServices;
using Solutio.Core.Services.ApplicationServices.LoginServices;
using System.Web;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenBuilder tokenBuilder;
        private readonly ISendConfirmationEmailService sendConfirmationEmailService;
        private readonly ISendResetPasswordService sendResetPasswordService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenBuilder tokenBuilder,
            ISendConfirmationEmailService sendConfirmationEmailService,
            ISendResetPasswordService sendResetPasswordService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenBuilder = tokenBuilder;
            this.sendConfirmationEmailService = sendConfirmationEmailService;
            this.sendResetPasswordService = sendResetPasswordService;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfoDto userInfo)
        {
            try
            {
                var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };
                var result = await userManager.CreateAsync(user, userInfo.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.ToList());
                }

                await sendConfirmationEmailService.Send(user.Id, user.Email, await userManager.GenerateEmailConfirmationTokenAsync(user));

                return BuildToken(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Login([FromBody] UserInfoDto userInfo)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    return BadRequest(new { message = GetErrorMessage(result) });
                }

                return BuildToken(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ResendConfirmationEmail")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] UserEmailDto userEmail)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userEmail.Email);

                if (user == null)
                {
                    return NotFound();
                }

                await sendConfirmationEmailService.Send(user.Id, user.Email, await userManager.GenerateEmailConfirmationTokenAsync(user));

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ConfirmEmail")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> ConfirmEmail([FromBody] UserConfirmEmailDto userConfirmEmail)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userConfirmEmail.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await userManager.ConfirmEmailAsync(user, userConfirmEmail.Token);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.ToList());
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("SendPasswordResetLink")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> SendPasswordResetLink([FromBody] UserEmailDto userEmail)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userEmail.Email);

                if (user == null)
                {
                    return NotFound();
                }

                await sendResetPasswordService.Send(user.Id, user.Email, await userManager.GeneratePasswordResetTokenAsync(user));

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto userResetPassword)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userResetPassword.Email);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await userManager.ResetPasswordAsync(user, userResetPassword.Token, userResetPassword.NewPassword);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.ToList());
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        #region private methods
        private IActionResult BuildToken(UserInfoDto userInfo)
        {
            var token = tokenBuilder.WithUserInfo(userInfo).Build();
            var expiration = DateTime.UtcNow.AddHours(1);
            var expirationTimeInSeconds = 1 * 60 * 60;

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration,
                expiresIn = expirationTimeInSeconds
            });
        }

        private string GetErrorMessage(Microsoft.AspNetCore.Identity.SignInResult result)
        {
            var errorMessage = "Intento de logueo inválido.";

            if (result != null)
            {
                if (result.IsLockedOut) errorMessage = "Usuario bloqueado.";
                if (result.IsNotAllowed) errorMessage = "Para iniciar sesion debe confirmar su correo elesctrónico.";
            }

            return errorMessage;
        }

        #endregion private methods

    }
}