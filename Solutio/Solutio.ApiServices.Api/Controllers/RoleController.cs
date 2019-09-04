using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solutio.ApiServices.Api.Dtos;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public RoleController(RoleManager<IdentityRole<int>> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Post(RoleDto role)
        {
            try
            {
                if (role == null) return BadRequest();
                if (string.IsNullOrWhiteSpace(role.Name)) return BadRequest();

                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role.Name));
                }

                return Ok(roleManager.Roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{role}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Delete(string role)
        {
            try
            {
                var roleDb = await roleManager.FindByNameAsync(role);
                if (roleDb != null)
                {
                    await roleManager.DeleteAsync(roleDb);
                }

                return Ok(roleManager.Roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(roleManager.Roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
