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

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenBuilder tokenBuilder;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenBuilder tokenBuilder)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenBuilder = tokenBuilder;
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

                return BuildToken(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserInfoDto userInfo)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }

                return BuildToken(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private IActionResult BuildToken(UserInfoDto userInfo)
        {
            var token = tokenBuilder.WithUserInfo(userInfo).Build();
            var expiration = DateTime.UtcNow.AddHours(1);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });
        }
    }
}