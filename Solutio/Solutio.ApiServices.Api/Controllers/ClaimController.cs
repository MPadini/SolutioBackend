using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.ApiServices.Api.Dtos.Requests;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly INewClaimService newClaimService;

        public ClaimController(INewClaimService newClaimService)
        {
            this.newClaimService = newClaimService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewClaimRequestDto newClaimRequest)
        {
            try
            {
                var claim = newClaimRequest.Claim.Adapt<Claim>();
                var claimId = await newClaimService.Save(claim);

                return Ok(new { claimId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
