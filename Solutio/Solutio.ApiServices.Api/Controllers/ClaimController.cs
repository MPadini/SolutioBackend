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
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Microsoft.AspNetCore.Cors;
using Solutio.ApiServices.Api.Mappers;

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
        private readonly IClaimDtoMapper claimDtoMapper;

        public ClaimController(
            INewClaimService newClaimService, 
            IGetClaimService getClaimService, 
            IUpdateClaimService updateClaimService, 
            IDeleteClaimService deleteClaimService, 
            IClaimDtoMapper claimDtoMapper)
        {
            this.newClaimService = newClaimService;
            this.getClaimService = getClaimService;
            this.updateClaimService = updateClaimService;
            this.deleteClaimService = deleteClaimService;
            this.claimDtoMapper = claimDtoMapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userName)
        {
            try
            {
                var userToSearch = await GetUserToSerch(userName);

                var claims = await getClaimService.GetAll(userToSearch);
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

                var claimDto = claimDtoMapper.Map(claim);

                return Ok(new { claimDto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClaimDto claimDto)
        {
            try
            {
                
                if (claimDto == null) return BadRequest("ClaimDto null");

                var claim = claimDtoMapper.Map(claimDto); 
                var claimId = await newClaimService.Save(claim, User.Identity.Name);

                return Created("claim", new { claimId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] ClaimDto claimDto)
        {
            try
            {
                var claim = await getClaimService.GetById(id);
                if (claim == null)
                {
                    return NotFound();
                }

                var updatedClaim = claimDtoMapper.Map(claimDto);
                await updateClaimService.Update(updatedClaim, id);

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

        private async Task<string> GetUserToSerch(string userName) {
            string userToSearch = User.Identity.Name;

            var userAdmin = User.Claims.Select(c => new { c.Type, c.Value })
                .ToList().Where(x => x.Value.ToLower().Equals("admin"));
            if (userAdmin.Any()) {
                userToSearch = userName;
            }

            return userToSearch;
        }
    }
}
