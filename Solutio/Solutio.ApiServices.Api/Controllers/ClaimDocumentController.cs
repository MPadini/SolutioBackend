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
using Solutio.ApiServices.Api.Dtos.Requests;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;

namespace Solutio.ApiServices.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimDocumentController : ControllerBase {
        private readonly IGetClaimDocumentService getClaimDocumentService;

        public ClaimDocumentController(IGetClaimDocumentService getClaimDocumentService) {
            this.getClaimDocumentService = getClaimDocumentService;
        }


        [HttpPost]
        public async Task<IActionResult> GetByIds([FromBody] ClaimDocumentRequestDto claimDocumentRequest) {
            try {
                if (claimDocumentRequest == null) return BadRequest();
                if (claimDocumentRequest.ClaimIds == null) return BadRequest();
                if (claimDocumentRequest.DocumentIds == null) return BadRequest();

                var file = await getClaimDocumentService.GetFile(claimDocumentRequest.ClaimIds, claimDocumentRequest.DocumentIds, claimDocumentRequest.ClaimFiles);

                return await DownloadFile(file);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpPost("GetByInsuranceCompanyID")]
        public async Task<IActionResult> GetByInsuranceCompanyID([FromBody] List<ClaimDocumentByCompaniesRequestDto> claimDocumentByCompaniesRequest) {
            try {
                if (claimDocumentByCompaniesRequest == null) return BadRequest();
               
                List<InsuranceCompany> insuranceCompanies = new List<InsuranceCompany>();
                foreach(var request in claimDocumentByCompaniesRequest) {
                    InsuranceCompany insuranceCompany = new InsuranceCompany();
                    insuranceCompany.Id = request.Id;
                    insuranceCompany.Name = request.CompanyName;
                    insuranceCompanies.Add(insuranceCompany);
                }

                var file = await getClaimDocumentService.GetFileByInsuranceCompany(insuranceCompanies, User.Identity.Name);

                return await DownloadFile(file);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        private async Task<IActionResult> DownloadFile(byte[] file) {
            MemoryStream ms = new MemoryStream();
            ms.Write(file, 0, file.Length);
            ms.Position = 0;
            var docId = DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Minute;

            return File(fileStream: ms, contentType: "application/pdf", fileDownloadName: $"Reclamo_{docId}" + ".pdf");
        }
    }
}