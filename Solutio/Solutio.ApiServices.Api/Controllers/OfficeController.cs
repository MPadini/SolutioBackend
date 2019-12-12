using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solutio.ApiServices.Api.Dtos;
using Solutio.Core.Services.ApplicationServices.OfficeServices;
using Solutio.Infrastructure.Repositories.Entities;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class OfficeController : ControllerBase {
        private readonly IOfficeService officeService;
        private readonly UserManager<ApplicationUser> userManager;

        public OfficeController(IOfficeService officeService, UserManager<ApplicationUser> userManager) {
            this.officeService = officeService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get() {
            try {
                var offices = await officeService.GetAll();

                return Ok(offices.Adapt<List<OfficeDto>>());
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUser(string userEmail) {
            try {
                if (string.IsNullOrWhiteSpace(userEmail)) return BadRequest();

                var user = await userManager.FindByEmailAsync(userEmail);
                if (user == null) return Ok();

                var offices = await officeService.GetOfficesByUser(user.Id);

                return Ok(offices.Adapt<List<OfficeDto>>());
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}