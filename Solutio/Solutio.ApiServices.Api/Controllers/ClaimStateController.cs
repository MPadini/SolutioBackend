using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.ApiServices.Api.Dtos;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Microsoft.AspNetCore.Cors;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimStateController : ControllerBase
    {
        private readonly IClaimGetStateService claimStateService;
        private readonly IGetClaimStateConfigurationService getClaimStateConfigurationService;

        public ClaimStateController(IClaimGetStateService claimStateService, IGetClaimStateConfigurationService getClaimStateConfigurationService)
        {
            this.claimStateService = claimStateService;
            this.getClaimStateConfigurationService = getClaimStateConfigurationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var claimStates = await claimStateService.GetAll();

                return Ok(claimStates.Adapt<List<ClaimStateDto>>()); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet("{stateId}")]
        public async Task<IActionResult> GetByStateId(long stateId)
        {
            try
            {
                var state = await claimStateService.GetById(stateId);
                if (state == null)
                {
                    return NotFound();
                }

                return Ok(state.Adapt<ClaimStateDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
