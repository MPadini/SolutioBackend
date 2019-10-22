using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
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

        public GetClaimDocumentService(
            IPdfMerge pdfMerge, 
            IGetHtmlTemplatesService getHtmlTemplatesService,
            IHtmlToPdfHelperService htmlToPdfHelperService) {
            this.pdfMerge = pdfMerge;
            this.getHtmlTemplatesService = getHtmlTemplatesService;
            this.htmlToPdfHelperService = htmlToPdfHelperService;
        }

        public async Task<byte[]> GetFile(Claim claim) {
            if (claim == null) throw new ArgumentException(nameof(Claim), "null");

            var htmlTemplates = await getHtmlTemplatesService.GetHtmlTemplates();
            if (htmlTemplates == null || !htmlTemplates.Any()) throw new ArgumentException("No hay templates configurados.");

            var documents = await ReplaceHtmlTags(htmlTemplates, claim);
            if (documents == null || !documents.Any()) throw new ArgumentException("No hay templates configurados.");

            List<byte[]> files = new List<byte[]>();

            foreach (var document in documents) {
                var pdf = await htmlToPdfHelperService.GetFile(document.HtmlTemplate);
                if (await CanAdd(pdf)) {
                    files.Add(pdf);
                }
            }
          
            var result = await pdfMerge.MergeFiles(files);
            return result;
        }

        private async Task<List<ClaimDocument>> ReplaceHtmlTags(List<ClaimDocument> claimDocuments, Claim claim) {
            if (claimDocuments == null) throw new ArgumentException(nameof(ClaimDocument), "null");

            List<ClaimDocument> result = new List<ClaimDocument>();
            claimDocuments.ForEach(async document => {
                ClaimDocument doc = new ClaimDocument();
                doc.HtmlTemplate = await ReemplaceTags( document.HtmlTemplate, claim);

                result.Add(doc);
            });

            return result;
        }

        private async Task<string> ReemplaceTags(string htmlString, Claim claim) {

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
    }
}
