using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.Core.Services.Repositories.Location;
using Microsoft.AspNetCore.Cors;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/Country/{countryId}/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceRepository provinceRepository;

        public ProvinceController(IProvinceRepository provinceRepository)
        {
            this.provinceRepository = provinceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long countryId)
        {
            try
            {
                var provinces = await provinceRepository.GetByCountryId(countryId);
                if (provinces == null || !provinces.Any())
                {
                    return NotFound();
                }

                return Ok(new { provinces });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
