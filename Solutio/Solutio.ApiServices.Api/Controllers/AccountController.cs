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
using Solutio.Core.Services.ApplicationServices.AppUsers;
using Solutio.Core.Services.ApplicationServices.AdressServices;
using Solutio.Core.Services.ApplicationServices.OfficeServices;

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
        private readonly IGetUserService getUserService;
        private readonly ICreateAdressService createAdressService;
        private readonly IOfficeService officeService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            ITokenBuilder tokenBuilder,
            ISendConfirmationEmailService sendConfirmationEmailService,
            ISendResetPasswordService sendResetPasswordService,
            IRefreshTokenService refreshTokenService,
            IGetUserService getUserService,
            ICreateAdressService createAdressService,
            IOfficeService officeService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenBuilder = tokenBuilder;
            this.sendConfirmationEmailService = sendConfirmationEmailService;
            this.sendResetPasswordService = sendResetPasswordService;
            this.roleManager = roleManager;
            this.refreshTokenService = refreshTokenService;
            this.getUserService = getUserService;
            this.createAdressService = createAdressService;
            this.officeService = officeService;
        }

        [Route("Create")]
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateUser([FromBody] NewUserDto userInfo)
        {
            try {
                if (userInfo == null) return BadRequest();

                var adressId = await CreateAdress(userInfo);

                var user = new ApplicationUser {
                    UserName = userInfo.Email,
                    Email = userInfo.Email,
                    PhoneNumber = userInfo.PhoneNumber,
                    Matricula = userInfo.Matricula,
                    AdressId = adressId,
                    IsEnabled = true};

                var result = await userManager.CreateAsync(user, userInfo.Password);

                await officeService.SaveUserOffices(user.Id, userInfo.Offices);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.ToList());
                }

                await AddRole(user, userInfo.RoleName);
                await sendConfirmationEmailService.Send(user.Id, user.Email, userInfo.Password, await userManager.GenerateEmailConfirmationTokenAsync(user));           

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private async Task<long> CreateAdress(NewUserDto userInfo) {
            if (userInfo.City == null) return 0;
            if (userInfo.Country == null) return 0;
            if (userInfo.Province == null) return 0;
            if (string.IsNullOrWhiteSpace(userInfo.Street)) return 0;
            if (string.IsNullOrWhiteSpace(userInfo.StreetNumber)) return 0;

            Adress adress = new Adress();
            adress.CityId = (long)userInfo.City;
            adress.ProvinceId = (long)userInfo.Province;
            adress.Street = userInfo.Street;
            adress.Number = userInfo.StreetNumber;

           return await createAdressService.Save(adress);
        }

        [Route("DisableUser")]
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DisableUser([FromBody] UserEmailDto userInfo) {
            try {
                if (userInfo == null) return BadRequest();

                var user = await userManager.FindByEmailAsync(userInfo.Email);
                if (user == null) throw new ApplicationException("Usuario no encontrado.");

                user.IsEnabled = false;
                var result = await userManager.UpdateAsync(user);

                return Ok();
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [Route("GetUsers")]
        [HttpGet]
        [EnableCors("AllowOrigin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsers() {
            try {

                var users = await getUserService.GetAllUsers();
                if (users == null || !users.Any()) return NotFound();

                return Ok(users);
            }
            catch (Exception ex) {
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
                if (userInfo == null) return BadRequest();

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
                if (userInfo == null) return BadRequest();

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
                if (userInfo == null) return BadRequest();

                var user = await userManager.FindByEmailAsync(userInfo.Email);
                if (user == null) throw new ApplicationException("Usuario no encontrado.");
                if (!user.IsEnabled) throw new ApplicationException("Usuario deshabilitado.");

                var result = await signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    return BadRequest(new { message = GetErrorMessage(result) });
                }
                               
                var role = await userManager.GetRolesAsync(user);

                return await BuildToken(user.Id, userInfo.Email, role.ToList(), 0);
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
                if (refreshToken == null) return BadRequest();

                var refreshTokenDb = await refreshTokenService.GetByRefreshToken(refreshToken.RefreshToken);
                if (refreshTokenDb == null)return NotFound("RefreshToken does not exist");

                var user = await userManager.FindByEmailAsync(refreshToken.Email);
                if (user == null) return NotFound("User does not exist");
                if (!user.IsEnabled) throw new ApplicationException("Usuario deshabilitado.");

                var role = await userManager.GetRolesAsync(user);
                if (role == null) return NotFound("Role does not exist");

                return await BuildToken(user.Id, refreshToken.Email, role.ToList(), refreshToken.OfficeId);
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
                if (userEmail == null) return BadRequest();

                var user = await userManager.FindByEmailAsync(userEmail.Email);
                if (user == null) return NotFound();
                if (!user.IsEnabled) throw new ApplicationException("Usuario deshabilitado.");

                if (user.EmailConfirmed)
                {
                    throw new ApplicationException("El correo ingresado ya fue confirmado");
                }

                await sendConfirmationEmailService.Send(user.Id, user.Email, "", await userManager.GenerateEmailConfirmationTokenAsync(user));

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
                if (userConfirmEmail == null) return BadRequest();

                var user = await userManager.FindByEmailAsync(userConfirmEmail.Email);
                if (user == null) return NotFound();
                if (!user.IsEnabled) throw new ApplicationException("Usuario deshabilitado.");

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
                if (userEmail == null) return BadRequest();

                var user = await userManager.FindByEmailAsync(userEmail.Email);
                if (user == null) return NotFound();
                if (!user.IsEnabled) throw new ApplicationException("Usuario deshabilitado.");

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
                if (userResetPassword == null) return BadRequest();

                var user = await userManager.FindByEmailAsync(userResetPassword.Email);
                if (user == null) return NotFound();
                if (!user.IsEnabled) throw new ApplicationException("Usuario deshabilitado.");

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
        private async Task< IActionResult> BuildToken(int userId, string userName, List<string> rolesName, long officeId)
        {
            var token = tokenBuilder.WithUserName(userName).WithUserId(userId).WithRole(rolesName).WithOfficeId(officeId).Build();
            var expiration = DateTime.UtcNow.AddHours(1);
            var expirationTimeInSeconds = 1 * 60 * 60;

            var refreshToken = await GenerateRefreshToken(userName);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration,
                expiresIn = expirationTimeInSeconds,
                rol = rolesName.FirstOrDefault(),
                user = userName,
                office = officeId,
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