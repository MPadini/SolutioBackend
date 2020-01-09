using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.ApiServices.Api.Dtos;
using Solutio.Core.Services.ApplicationServices.ClaimWorkflowServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkflowController : ControllerBase {
        private readonly IClaimWorkflowService claimWorkflowService;

        public WorkflowController(IClaimWorkflowService claimWorkflowService) {
            this.claimWorkflowService = claimWorkflowService;
        }

        [HttpGet("{claimId}")]
        public async Task<IActionResult> Get(long claimId) {
            try {
                var workflows = await claimWorkflowService.Get(claimId);
                if (workflows == null) {
                    return Ok();
                }

                var workflowsDto = workflows.Adapt<List<ClaimWorkflowDto>>();

                return Ok(workflowsDto);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}