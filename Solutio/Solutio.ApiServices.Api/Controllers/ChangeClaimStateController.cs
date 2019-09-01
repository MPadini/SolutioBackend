using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/claim/{claimId}/[controller]/")]
    [ApiController]
    public class ChangeClaimStateController : ControllerBase
    {
        private readonly IChangeClaimStateService changeClaimStateService;
        private readonly IGetClaimService getClaimService;

        public ChangeClaimStateController(IChangeClaimStateService changeClaimStateService, IGetClaimService getClaimService)
        {
            this.changeClaimStateService = changeClaimStateService;
            this.getClaimService = getClaimService;
        }

        [HttpPut("{newStateId}")]
        public async Task<IActionResult> Put(long claimId, long newStateId)
        {
            try
            {
                var claim = await getClaimService.GetById(claimId);
                if (claim == null)
                {
                    return NotFound();
                }

                await changeClaimStateService.ChangeState(claim, newStateId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
