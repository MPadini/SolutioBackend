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
using Solutio.ApiServices.Api.Dtos.Requests;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Microsoft.AspNetCore.Cors;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimController : ControllerBase
    {
        private readonly INewClaimService newClaimService;
        private readonly IGetClaimService getClaimService;
        private readonly IUpdateClaimService updateClaimService;
        private readonly IDeleteClaimService deleteClaimService;

        public ClaimController(
            INewClaimService newClaimService, 
            IGetClaimService getClaimService, 
            IUpdateClaimService updateClaimService, 
            IDeleteClaimService deleteClaimService)
        {
            this.newClaimService = newClaimService;
            this.getClaimService = getClaimService;
            this.updateClaimService = updateClaimService;
            this.deleteClaimService = deleteClaimService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var claims = await getClaimService.GetAll();
                if (claims == null || !claims.Any())
                {
                    return NotFound();
                }

                var claimsDto = claims.Adapt<List<ClaimDto>>();

                return Ok(new { claimsDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var claim = await getClaimService.GetById(id);
                if (claim == null)
                {
                    return NotFound();
                }

                var claimDto = claim.Adapt<ClaimDto>();

                return Ok(new { claimDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewClaimRequest newClaimRequest)
        {
            try
            {
                if (newClaimRequest == null) return BadRequest("ClaimDto null");

                var claim = newClaimRequest.Adapt<Claim>();
                var claimId = await newClaimService.Save(claim);

                return Created("claim", new { claimId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] string value)
        {
            try
            {
                var claim = await getClaimService.GetById(id);
                if (claim == null)
                {
                    return NotFound();
                }

                await updateClaimService.Update(claim);

                return Created("claim", new { claim.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var claim = await getClaimService.GetById(id);
                if (claim == null)
                {
                    return NotFound();
                }

                await deleteClaimService.Delete(claim);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
