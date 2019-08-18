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
using Solutio.Core.Services.ApplicationServices.FileService;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IUploadFileService uploadFileService;
        private readonly IDeleteFileService deleteFileService;
        private readonly IGetFileService getFileService;

        public FileController(
            IUploadFileService uploadFileService, 
            IDeleteFileService deleteFileService, 
            IGetFileService getFileService)
        {
            this.uploadFileService = uploadFileService;
            this.deleteFileService = deleteFileService;
            this.getFileService = getFileService;
        }

        [HttpGet("{fileId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] ClaimFileDto claimFile)
        {
            try
            {
                if (claimFile == null) return BadRequest("ClaimFileDto null");

                //TODO:
                //validate if claim exists

                var file = claimFile.Adapt<ClaimFile>();
                var fileId = await uploadFileService.Upload(file);

                return Ok(new { fileId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{fileId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
