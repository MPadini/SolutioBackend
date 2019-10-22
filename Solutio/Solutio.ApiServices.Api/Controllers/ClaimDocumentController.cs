using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimDocumentController : ControllerBase {
        private readonly IGetClaimService getClaimService;
        private readonly IGetClaimDocumentService getClaimDocumentService;

        public ClaimDocumentController(IGetClaimService getClaimService, IGetClaimDocumentService getClaimDocumentService) {
            this.getClaimService = getClaimService;
            this.getClaimDocumentService = getClaimDocumentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetByIds(long claimId) {
            try {
                var claim = await getClaimService.GetById(claimId);
                if (claim == null) {
                    return NotFound("Reclamo no encontrado.");
                }

                var file = await getClaimDocumentService.GetFile(claim);

                return await DownloadFile(file, claim.Id);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private async Task<IActionResult> DownloadFile(byte[] file, long claimId) {
            MemoryStream ms = new MemoryStream();
            ms.Write(file, 0, file.Length);
            ms.Position = 0;

            return File(fileStream: ms, contentType: "application/pdf", fileDownloadName: $"Reclamo_{claimId}" + ".pdf");
        }
    }
}