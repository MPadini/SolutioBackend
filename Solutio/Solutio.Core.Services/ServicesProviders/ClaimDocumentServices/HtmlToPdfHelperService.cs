using SelectPdf;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimDocumentServices {
    public class HtmlToPdfHelperService : IHtmlToPdfHelperService {
        public async Task<byte[]> GetFile(string htmlString) {
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            PdfDocument doc = converter.ConvertHtmlString(htmlString);

            if (doc.Pages.Count > 1) {
                doc.Pages.RemoveAt(1);
            }

            byte[] pdfCover = doc.Save();
            doc.Close();

            return pdfCover;
        }
    }
}
