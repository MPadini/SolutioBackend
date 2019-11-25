using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.ApiServices.Api.Dtos;
using Solutio.Core.Services.ApplicationServices.CompanyServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompanyController : ControllerBase {
        private readonly IGetInsuranceCompanyService getInsuranceCompanyService;

        public CompanyController(IGetInsuranceCompanyService getInsuranceCompanyService) {
            this.getInsuranceCompanyService = getInsuranceCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            try {
                var claimStates = await getInsuranceCompanyService.GetCompanies();

                return Ok(claimStates.Adapt<List<InsuranceCompanyDto>>());
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet ("GetInsuranceCompanyClaims")]
        public async Task<IActionResult> GetInsuranceCompanyClaims()
        {
            try
            {
                var claimStates = await getInsuranceCompanyService.GetInsuranceCompanyClaims();

                return Ok(claimStates.Adapt<List<InsuranceCompanyClaimsDto>>());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

    }
}