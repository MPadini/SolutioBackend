using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.ApiServices.Api.Dtos;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimStateController : ControllerBase
    {
        private readonly IClaimGetStateService claimStateService;

        public ClaimStateController(IClaimGetStateService claimStateService)
        {
            this.claimStateService = claimStateService;
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
    }
}
