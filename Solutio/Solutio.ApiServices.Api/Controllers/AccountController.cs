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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Solutio.Core.Services.ApplicationServices.RefreshTokenServices;
using Solutio.Core.Entities;

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
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IRefreshTokenService refreshTokenService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            ITokenBuilder tokenBuilder,
            ISendConfirmationEmailService sendConfirmationEmailService,
            ISendResetPasswordService sendResetPasswordService,
            IRefreshTokenService refreshTokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenBuilder = tokenBuilder;
            this.sendConfirmationEmailService = sendConfirmationEmailService;
            this.sendResetPasswordService = sendResetPasswordService;
            this.roleManager = roleManager;
            this.refreshTokenService = refreshTokenService;
        }

        [Route("Create")]
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> CreateUser([FromBody] NewUserDto userInfo)
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
                await AddRole(user, userInfo.RoleName);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Route("AddRole")]
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddRole([FromBody] UserRolDto userInfo)
        {
            try
            {
                var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };
                await AddRole(user, userInfo.RoleName);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Route("RemoveRole")]
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RemoveRole([FromBody] UserRolDto userInfo)
        {
            try
            {
                var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };
                await RemoveRole(user, userInfo.RoleName);

                return Ok();
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

                var user = await userManager.FindByEmailAsync(userInfo.Email);
                var role = await userManager.GetRolesAsync(user);

                return await BuildToken(userInfo.Email, role.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto refreshToken)
        {
            try
            {
                var refreshTokenDb = await refreshTokenService.GetByRefreshToken(refreshToken.RefreshToken);
                if (refreshTokenDb == null)
                {
                    return NotFound("RefreshToken does not exist");
                }

                var user = await userManager.FindByEmailAsync(refreshToken.Email);
                if (refreshToken == null)
                {
                    return NotFound("User does not exist");
                }

                var role = await userManager.GetRolesAsync(user);
                if (refreshToken == null)
                {
                    return NotFound("Role does not exist");
                }

                return await BuildToken(refreshToken.Email, role.ToList());
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

                if (user.EmailConfirmed)
                {
                    throw new ApplicationException("El correo ingresado ya fue confirmado");
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
                var user = await userManager.FindByEmailAsync(userConfirmEmail.Email);
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
        private async Task< IActionResult> BuildToken(string userName, List<string> rolesName)
        {
            var token = tokenBuilder.WithUserInfo(userName).WithRole(rolesName).Build();
            var expiration = DateTime.UtcNow.AddHours(1);
            var expirationTimeInSeconds = 1 * 60 * 60;

            var refreshToken = await GenerateRefreshToken(userName);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration,
                expiresIn = expirationTimeInSeconds,
                refreshToken = refreshToken.Refreshtoken
            });
        }

        private async Task<RefreshToken> GenerateRefreshToken(string userName)
        {
            var refreshTokenDb = await refreshTokenService.Get(userName);
            if (refreshTokenDb != null)
            {
                await refreshTokenService.Revoke(refreshTokenDb);
            }

            RefreshToken refreshToken = new RefreshToken();
            refreshToken.Refreshtoken = Guid.NewGuid().ToString();
            refreshToken.UserName = userName;
            await refreshTokenService.Save(refreshToken);

            return refreshToken;
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

        private async Task AddRole(ApplicationUser applicationUser, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (role == null || user == null)
            {
                throw new ApplicationException("User or Role name invalid.");
            }

            await userManager.AddToRoleAsync(user, role.Name);
        }

        private async Task RemoveRole(ApplicationUser applicationUser, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (role == null || user == null)
            {
                throw new ApplicationException("User or Role name invalid.");
            }

            await userManager.RemoveFromRoleAsync(user, role.Name);
        }

        #endregion private methods

    }
}