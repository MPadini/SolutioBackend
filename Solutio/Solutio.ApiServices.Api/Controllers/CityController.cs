using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.Core.Services.ApplicationServices.Location;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/Province/{provinceId}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long provinceId)
        {
            try
            {
                var cities = await cityService.GetByProvinceId(provinceId);
                if (cities == null || !cities.Any())
                {
                    return NotFound();
                }

                return Ok(new { cities });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
