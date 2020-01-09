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
using Solutio.Core.Services.ApplicationServices.ClaimMessagesService;
using Microsoft.AspNetCore.Cors;
using Solutio.ApiServices.Api.Mappers;
using System.Security.Claims;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimMessageController : ControllerBase
    {
        private readonly IDeleteClaimMessagesService deleteClaimMessagesService;
        private readonly IGetClaimMessagesService getClaimMessagesService;
        private readonly IUpdateClaimMessagesService updateClaimMessagesService;
        private readonly IUploadClaimMessagesService uploadClaimMessagesService;
        private readonly IGetClaimService getClaimService;

        public ClaimMessageController(
            IDeleteClaimMessagesService deleteClaimMessagesService,
            IGetClaimMessagesService getClaimMessagesService,
            IUpdateClaimMessagesService updateClaimMessagesService,
            IUploadClaimMessagesService uploadClaimMessagesService,
            IGetClaimService getClaimService)
        {
            this.deleteClaimMessagesService = deleteClaimMessagesService;
            this.getClaimMessagesService = getClaimMessagesService;
            this.updateClaimMessagesService = updateClaimMessagesService;
            this.uploadClaimMessagesService = uploadClaimMessagesService;
            this.getClaimService = getClaimService;
        }

        [HttpGet("GetByClaim/{claimId}")]
        public async Task<IActionResult> GetByClaimId(long claimId)
        {
            try
            {
                var messages = await getClaimMessagesService.GetByClaimId(claimId);
                if (messages == null)
                {
                    return NotFound();
                }

                return Ok(new { messages = messages.Adapt<List<ClaimMessageDto>>() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClaimMessageDto claimMessageDto)
        {
            try
            {
                var claim = await getClaimService.GetById(claimMessageDto.ClaimId);
                if (claim == null)
                {
                    return Ok();
                }
                var claimMessage = claimMessageDto.Adapt<ClaimMessage>();

                var messageId = await uploadClaimMessagesService.Upload(claimMessage);

                return Created("message", new { messageId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{claimId}")]
        public async Task<IActionResult> Put(long claimId)
        {
            try
            {
                var messages = await getClaimMessagesService.GetByClaimId(claimId);
                if (messages == null)
                {
                    return NotFound();
                }
                await updateClaimMessagesService.MarkAsViewed(messages);

                return Created("markedAsViewed", new { messages });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

    }
}
