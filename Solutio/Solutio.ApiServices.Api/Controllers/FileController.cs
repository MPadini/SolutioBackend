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
using Solutio.Core.Services.ApplicationServices.FileService;
using Microsoft.AspNetCore.Cors;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FileController : ControllerBase
    {
        private readonly IUploadFileService uploadFileService;
        private readonly IDeleteFileService deleteFileService;
        private readonly IGetFileService getFileService;
        private readonly IGetClaimService getClaimService;

        public FileController(
            IUploadFileService uploadFileService, 
            IDeleteFileService deleteFileService, 
            IGetFileService getFileService,
            IGetClaimService getClaimService)
        {
            this.uploadFileService = uploadFileService;
            this.deleteFileService = deleteFileService;
            this.getFileService = getFileService;
            this.getClaimService = getClaimService;
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> Get(long fileId)
        {
            try
            {
                var file = await getFileService.GetById(fileId);
                if (file == null)
                {
                    return NotFound();
                }

                return Ok( new { file = file.Adapt<ClaimFileDto>() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClaimFileDto claimFile)
        {
            try
            {
                if (claimFile == null) return BadRequest("ClaimFileDto null");

                var claim = await getClaimService.GetById(claimFile.ClaimId);
                if (claim == null)
                {
                    return NotFound("Claim does not exists.");
                }

                var file = claimFile.Adapt<ClaimFile>();
                var fileId = await uploadFileService.Upload(file);

                return Created("file", new { fileId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> Delete(long fileId)
        {
            try
            {
                var file = await getFileService.GetById(fileId);
                if (file == null)
                {
                    return NotFound();
                }

                await deleteFileService.Delete(file);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
