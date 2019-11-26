using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.ApplicationServices.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimDocumentServices {
    public class GetClaimDocumentService : IGetClaimDocumentService {
        private readonly IPdfMerge pdfMerge;
        private readonly IGetHtmlTemplatesService getHtmlTemplatesService;
        private readonly IHtmlToPdfHelperService htmlToPdfHelperService;
        private readonly IGetClaimService getClaimService;
        private readonly IGetFileService getFileService;

        public GetClaimDocumentService(
            IPdfMerge pdfMerge, 
            IGetHtmlTemplatesService getHtmlTemplatesService,
            IHtmlToPdfHelperService htmlToPdfHelperService,
            IGetClaimService getClaimService,
            IGetFileService getFileService) {
            this.pdfMerge = pdfMerge;
            this.getHtmlTemplatesService = getHtmlTemplatesService;
            this.htmlToPdfHelperService = htmlToPdfHelperService;
            this.getClaimService = getClaimService;
            this.getFileService = getFileService;
        }

        public async Task<byte[]> GetFile(List<long> claimIds, List<long> documentsIds, List<long> claimFileIds) {
            if (claimIds == null) throw new ArgumentException("Claims null");
            if (documentsIds == null) throw new ArgumentException("Documents null");

            var htmlTemplates = await getHtmlTemplatesService.GetHtmlTemplates();
            if (htmlTemplates == null || !htmlTemplates.Any()) throw new ArgumentException("No hay templates configurados.");

            List<byte[]> files = new List<byte[]>();
            List<Claim> claims = new List<Claim>();

            foreach (var claimId in claimIds) {
                var claim = await getClaimService.GetById(claimId);
                if (claim == null) continue;

                claims.Add(claim);

                var documents = await ReplaceHtmlTags(htmlTemplates, claim);
                if (documents == null || !documents.Any()) throw new ArgumentException("No hay templates configurados.");


                foreach (var document in documents) { 
                    if (documentsIds.Contains(document.Id)) {
                        if (await IsCover(document.Id)) continue;

                        var pdf = await htmlToPdfHelperService.GetFile(document.HtmlTemplate);
                        if (await CanAdd(pdf)) {
                            files.Add(pdf);
                        }
                    }
                }
            }

            foreach (var claimfileId in claimFileIds) {
                var file = await getFileService.GetById(claimfileId);
               if (file != null) {
                    byte[] bFile = Convert.FromBase64String(file.Base64);
                    if (await CanAdd(bFile)) {
                        files.Add(bFile);
                    }
                }
            }

            if (documentsIds.Contains(1)) {
                var cover = await GenerateCover(claims, htmlTemplates.Where(x => x.Id == 1).FirstOrDefault(),"");
                if (await CanAdd(cover)) {
                    files.Add(cover);
                }
            }

            if (!files.Any()) throw new ApplicationException("No se han podido generar los documentos solicitados");

            var result = await pdfMerge.MergeFiles(files);
            return result;
        }

        private async Task<bool> IsCover(long documentId) {
            if (documentId == 1) return true;

            return false;
        }

        private async Task<byte[]> GenerateCover(List<Claim> claims,ClaimDocument claimDocument, string company) {
            if (claims == null) return null;
            if (claimDocument == null) return null;

            var document = claimDocument.HtmlTemplate;
            StringBuilder str = new StringBuilder();

            
            foreach (var claim in claims) {

                int daysDiff = ((TimeSpan)(DateTime.Now - claim.Created)).Days;

                str.Append($"<tr>" +
                    $"<td>{claim.Id.ToString()}</td>" +
                    //$"<td>{claim.ClaimInsuredVehicles.FirstOrDefault().Patent ?? string.Empty} </td>" +
                    $"<td>{"GAD125"} </td>" +
                    $"<td>{"123456"} </td>" +
                    //$"<td>{claim.State.Description ?? string.Empty} </td>" +
                    $"<td>{"Presentado"} </td>" +
                    $"<td>{daysDiff.ToString()}  </td>" +
                    $"<td> </td>" +
                    $"</tr>" +
                    $"<tr><td colspan='6'> Notas: </td></tr>");
            }

            document = document.Replace("[contenido]", str.ToString());
            document = document.Replace("[compania]", company);
            document = document.Replace("[fechaImpresion]", DateTime.Now.ToString("dd/MM/yyyy"));

            var pdf = await htmlToPdfHelperService.GetFile(document);

            return pdf;
        }

        private async Task<List<ClaimDocument>> ReplaceHtmlTags(List<ClaimDocument> claimDocuments, Claim claim) {
            if (claimDocuments == null) throw new ArgumentException(nameof(ClaimDocument), "null");

            List<ClaimDocument> result = new List<ClaimDocument>();
            claimDocuments.ForEach(async document => {
                ClaimDocument doc = new ClaimDocument();
                doc.HtmlTemplate = await ReemplaceTags( document.HtmlTemplate, claim);
                doc.Id = document.Id;
                result.Add(doc);
            });

            return result;
        }

        private async Task<string> ReemplaceTags(string htmlString, Claim claim) {

            htmlString = htmlString.Replace("[claimId]", claim.Id.ToString());
            htmlString = htmlString.Replace("[thirdCompany]", "Compañia de prueba");
            htmlString = htmlString.Replace("[sinisterDate]", DateTime.Now.ToString("dd/MM/yyyy"));
            htmlString = htmlString.Replace("[thirdVehicleDomain]", "Dominio de prueba");
            htmlString = htmlString.Replace("[sinisterNumber]", "1234567");
            htmlString = htmlString.Replace("[montoOfrecimiento]", "$500");
            htmlString = htmlString.Replace("[montoReclamado]", "$100000000");

            return htmlString;
        }

        public async static Task<bool> CanAdd(byte[] data) {
            if (data == null) {
                return false;
            }
            if (data.Length <= 0) {
                return false;
            }

            return true;
        }

        public async Task<byte[]> GetFileByInsuranceCompany(List<InsuranceCompany> insuranceCompanies) {
            var htmlTemplates = await getHtmlTemplatesService.GetHtmlTemplates();
            if (htmlTemplates == null || !htmlTemplates.Any()) throw new ArgumentException("No hay templates configurados.");

            List<byte[]> files = new List<byte[]>();

            foreach(var company in insuranceCompanies) {
                var claims = await getClaimService.GetClaimByInsuranceCompany(company.Id);
                if (claims == null) continue;

                var cover = await GenerateCover(claims, htmlTemplates.Where(x => x.Id == 1).FirstOrDefault(), company.Name);
                if (await CanAdd(cover)) {
                    files.Add(cover);
                }
            }
          
            //TODO: agregar los documentos de cada reclamo

            var result = await pdfMerge.MergeFiles(files);
            return result;
        }
    }
}
