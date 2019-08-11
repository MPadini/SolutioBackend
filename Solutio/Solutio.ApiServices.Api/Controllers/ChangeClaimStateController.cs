using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/claim/{claimId}/[controller]/")]
    [ApiController]
    public class ChangeClaimStateController : ControllerBase
    {
        private readonly IChangeClaimStateService changeClaimStateService;

        public ChangeClaimStateController(IChangeClaimStateService changeClaimStateService)
        {
            this.changeClaimStateService = changeClaimStateService;
        }

        [HttpPut("{newStateId}")]
        public async Task<IActionResult> Put(long claimId, long newStateId)
        {
            try
            {
                //TODO.
                //search claim
                //Call change service

                //await changeClaimStateService.ChangeState(claimId, newStateId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
