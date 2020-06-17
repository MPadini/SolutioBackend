using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.ApplicationServices.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimDocumentServices
{
    public class GetClaimDocumentService : IGetClaimDocumentService
    {
        private readonly IPdfMerge pdfMerge;
        private readonly IGetHtmlTemplatesService getHtmlTemplatesService;
        private readonly IHtmlToPdfHelperService htmlToPdfHelperService;
        private readonly IGetClaimService getClaimService;
        private readonly IGetFileService getFileService;
        private readonly IUpdateFileService updateFileService;
        private readonly IUpdateClaimService updateClaimService;
        private readonly IChangeClaimStateService changeClaimStateService;

        public GetClaimDocumentService(
            IPdfMerge pdfMerge,
            IGetHtmlTemplatesService getHtmlTemplatesService,
            IHtmlToPdfHelperService htmlToPdfHelperService,
            IGetClaimService getClaimService,
            IGetFileService getFileService,
            IUpdateFileService updateFileService,
            IUpdateClaimService updateClaimService,
            IChangeClaimStateService changeClaimStateService)
        {
            this.pdfMerge = pdfMerge;
            this.getHtmlTemplatesService = getHtmlTemplatesService;
            this.htmlToPdfHelperService = htmlToPdfHelperService;
            this.getClaimService = getClaimService;
            this.getFileService = getFileService;
            this.updateFileService = updateFileService;
            this.updateClaimService = updateClaimService;
            this.changeClaimStateService = changeClaimStateService;
        }

        public async Task<byte[]> GetFile(List<long> claimIds, List<long> documentsIds, List<long> claimFileIds)
        {
            if (claimIds == null) throw new ArgumentException("Claims null");
            if (documentsIds == null) throw new ArgumentException("Documents null");

            var htmlTemplates = await getHtmlTemplatesService.GetHtmlTemplates();
            if (htmlTemplates == null || !htmlTemplates.Any()) throw new ArgumentException("No hay templates configurados.");

            List<byte[]> files = new List<byte[]>();
            List<Claim> claims = new List<Claim>();

            foreach (var claimId in claimIds)
            {
                var claim = await getClaimService.GetById(claimId);
                if (claim == null) continue;

                claims.Add(claim);

                var documents = await ReplaceHtmlTags(htmlTemplates, claim);
                if (documents == null || !documents.Any()) throw new ArgumentException("No hay templates configurados.");


                foreach (var document in documents)
                {
                    if (documentsIds.Contains(document.Id))
                    {
                        if (await IsCover(document.Id)) continue;

                        var pdf = await htmlToPdfHelperService.GetFile(document.HtmlTemplate);
                        if (await CanAdd(pdf))
                        {
                            files.Add(pdf);
                        }
                    }
                }
            }

            foreach (var claimfileId in claimFileIds)
            {
                var file = await getFileService.GetById(claimfileId);
                if (file != null)
                {
                    if (file.FileName.ToLower().Contains(".pdf"))
                    {
                        //Si es pdf lo agrego directamente
                        byte[] bFile = Convert.FromBase64String(file.Base64);
                        if (await CanAdd(bFile))
                        {
                            files.Add(bFile);
                        }
                    }
                    else
                    {
                        //sino debe crear una nueva pagina de pdf a partir del archivo imagen, y luego la agrego
                        byte[] bFile = Convert.FromBase64String(file.Base64);
                        byte[] bFileFromImage = pdfMerge.CreatePdfFromFile(bFile);
                        if (await CanAdd(bFileFromImage))
                        {
                            files.Add(bFileFromImage);
                        }
                    }


                }
            }

            if (documentsIds.Contains(1))
            {
                var cover = await GenerateCover(claims, htmlTemplates.Where(x => x.Id == 1).FirstOrDefault(), "");
                if (await CanAdd(cover))
                {
                    files.Add(cover);
                }
            }

            if (!files.Any()) throw new ApplicationException("No se han podido generar los documentos solicitados");

            //aqui deberian llegar solo archivos pdf
            var result = await pdfMerge.MergeFiles(files);
            return result;
        }

        private async Task<bool> IsCover(long documentId)
        {
            if (documentId == 1) return true;

            return false;
        }

        private async Task<byte[]> GenerateCover(List<Claim> claims, ClaimDocument claimDocument, string company)
        {
            if (claims == null) return null;
            if (claimDocument == null) return null;

            var document = claimDocument.HtmlTemplate;
            StringBuilder str = new StringBuilder();


            foreach (var claim in claims)
            {

                int daysDiff = ((TimeSpan)(DateTime.Now - claim.Created)).Days;

                str.Append($"<tr>" +
                    $"<td>{claim.Id.ToString()}</td>" +
                    $"<td>{claim.ClaimThirdInsuredVehicles.FirstOrDefault().Patent ?? string.Empty} </td>" +
                    $"<td>{claim.SinisterNumber} </td>" +
                    $"<td>{claim.State.Description ?? string.Empty} </td>" +
                    $"<td>{daysDiff.ToString()}  </td>" +
                    $"<td> {await AddOfferedAmount(claim)} </td>" +
                    $"</tr>" +
                    $"<tr><td colspan='6'> Notas: </td></tr>");
            }

            document = document.Replace("[contenido]", str.ToString());
            document = document.Replace("[compania]", company);
            document = document.Replace("[fechaImpresion]", DateTime.Now.ToString("dd/MM/yyyy"));

            var pdf = await htmlToPdfHelperService.GetFile(document);

            return pdf;
        }

        private async Task<List<ClaimDocument>> ReplaceHtmlTags(List<ClaimDocument> claimDocuments, Claim claim)
        {
            if (claimDocuments == null) throw new ArgumentException(nameof(ClaimDocument), "null");

            List<ClaimDocument> result = new List<ClaimDocument>();
            claimDocuments.ForEach(async document =>
            {
                ClaimDocument doc = new ClaimDocument();
                doc.HtmlTemplate = await ReemplaceTags(document.HtmlTemplate, claim);
                doc.Id = document.Id;
                result.Add(doc);
            });

            return result;
        }

        private async Task<string> ReemplaceTags(string htmlString, Claim claim)
        {

            htmlString = htmlString.Replace("[claimId]", claim.Id.ToString());
            htmlString = htmlString.Replace("[thirdCompany]", await GetCompanyName(claim));
            htmlString = htmlString.Replace("[sinisterDate]", claim.Date.ToString("dd/MM/yyyy"));
            htmlString = htmlString.Replace("[thirdVehicleDomain]", await GetThirdVehicleDomain(claim));
            htmlString = htmlString.Replace("[sinisterNumber]", claim.SinisterNumber);


            htmlString = htmlString.Replace("[montoOfrecimiento]", await AddOfferedAmount(claim));
            var value = claim.TotalBudgetAmount == null ? string.Empty : claim.TotalBudgetAmount.ToString();
            htmlString = htmlString.Replace("[montoReclamado]", value);

            //Claim page
            int index = 1;
            if (claim.ClaimInsuredPersons != null)
            {
                foreach (var person in claim.ClaimInsuredPersons)
                {
                    htmlString = htmlString.Replace($"[nombrePersona{index}]", person.Name + " " + person.Surname);
                    htmlString = htmlString.Replace($"[dniPersona{index}]", person.DocumentNumber.Equals("0") ? string.Empty : person.DocumentNumber);
                    htmlString = htmlString.Replace($"[enCaracterDePersona{index}]", (person.WasInjured && person.IsCarHolder) ? "Titular y Lesionado" : (person.WasInjured ? "Lesionado" : "Titular"));
                    index++;
                }
            }

            index = 1;
            if (claim.ClaimInsuredVehicles != null)
            {
                foreach (var vehicle in claim.ClaimInsuredVehicles)
                {
                    htmlString = htmlString.Replace($"[dominioVehiculo{index}]", vehicle.Patent);
                    if (vehicle.VehicleType != null)
                    {
                        htmlString = htmlString.Replace($"[tipoDeVehiculo{index}]", vehicle.VehicleType.Description);
                    }
                    index++;
                }
            }

            if (claim.Adress != null)
            {
                htmlString = htmlString.Replace("[DireccionDelSiniestro]", claim.Adress.Street + " " + claim.Adress.Number);
                htmlString = htmlString.Replace("[entreCalles]", claim.Adress.BetweenStreets);
                htmlString = htmlString.Replace("[Localidad]", claim.Adress.Province == null ? string.Empty : claim.Adress.Province.Name);
                htmlString = htmlString.Replace("[Provincia]", claim.Adress.Province == null ? string.Empty : claim.Adress.Province.Name);
                htmlString = htmlString.Replace("[Pais]", "Argentina");

                htmlString = htmlString.Replace("[FechaDeSiniestro]", claim.Date.ToString("dd/MM/yyyy"));
                htmlString = htmlString.Replace("[HoraSiniestro]", claim.Hour);
                htmlString = htmlString.Replace("[relato]", claim.Story);
            }

            htmlString = htmlString.Replace("[montoAReclamar]]", value);

            return await cleanTemplate(htmlString);
        }

        private async Task<string> cleanTemplate(string htmlString)
        {
            htmlString = htmlString.Replace($"[nombrePersona1]", string.Empty);
            htmlString = htmlString.Replace($"[nombrePersona2]", string.Empty);
            htmlString = htmlString.Replace($"[nombrePersona3]", string.Empty);
            htmlString = htmlString.Replace($"[nombrePersona4]", string.Empty);
            htmlString = htmlString.Replace($"[nombrePersona5]", string.Empty);
            htmlString = htmlString.Replace($"[nombrePersona6]", string.Empty);

            htmlString = htmlString.Replace($"[dniPersona1]", string.Empty);
            htmlString = htmlString.Replace($"[dniPersona2]", string.Empty);
            htmlString = htmlString.Replace($"[dniPersona3]", string.Empty);
            htmlString = htmlString.Replace($"[dniPersona4]", string.Empty);
            htmlString = htmlString.Replace($"[dniPersona5]", string.Empty);
            htmlString = htmlString.Replace($"[dniPersona6]", string.Empty);

            htmlString = htmlString.Replace($"[dominioVehiculo1]", string.Empty);
            htmlString = htmlString.Replace($"[dominioVehiculo2]", string.Empty);
            htmlString = htmlString.Replace($"[dominioVehiculo3]", string.Empty);
            htmlString = htmlString.Replace($"[dominioVehiculo4]", string.Empty);
            htmlString = htmlString.Replace($"[dominioVehiculo5]", string.Empty);
            htmlString = htmlString.Replace($"[dominioVehiculo6]", string.Empty);

            htmlString = htmlString.Replace($"[tipoDeVehiculo1]", string.Empty);
            htmlString = htmlString.Replace($"[tipoDeVehiculo2]", string.Empty);
            htmlString = htmlString.Replace($"[tipoDeVehiculo3]", string.Empty);
            htmlString = htmlString.Replace($"[tipoDeVehiculo4]", string.Empty);
            htmlString = htmlString.Replace($"[tipoDeVehiculo5]", string.Empty);
            htmlString = htmlString.Replace($"[tipoDeVehiculo6]", string.Empty);

            htmlString = htmlString.Replace($"[enCaracterDePersona1]", string.Empty);
            htmlString = htmlString.Replace($"[enCaracterDePersona2]", string.Empty);
            htmlString = htmlString.Replace($"[enCaracterDePersona3]", string.Empty);
            htmlString = htmlString.Replace($"[enCaracterDePersona4]", string.Empty);
            htmlString = htmlString.Replace($"[enCaracterDePersona5]", string.Empty);
            htmlString = htmlString.Replace($"[enCaracterDePersona6]", string.Empty);

            return htmlString;
        }

        private async Task<string> GetCompanyName(Claim claim)
        {
            if (claim.ClaimThirdInsuredVehicles == null) return string.Empty;

            var vehicle = claim.ClaimThirdInsuredVehicles.FirstOrDefault();
            if (vehicle == null) return string.Empty;
            if (vehicle.InsuranceCompany == null) return string.Empty;

            return vehicle.InsuranceCompany.Name;
        }

        private async Task<string> GetThirdVehicleDomain(Claim claim)
        {
            if (claim.ClaimThirdInsuredVehicles == null) return string.Empty;

            var vehicle = claim.ClaimThirdInsuredVehicles.FirstOrDefault();
            if (vehicle == null) return string.Empty;

            return vehicle.Patent;
        }

        public async static Task<bool> CanAdd(byte[] data)
        {
            if (data == null)
            {
                return false;
            }
            if (data.Length <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<byte[]> GetFileByInsuranceCompany(List<InsuranceCompany> insuranceCompanies, string userName)
        {
            var htmlTemplates = await getHtmlTemplatesService.GetHtmlTemplates();
            if (htmlTemplates == null || !htmlTemplates.Any()) throw new ArgumentException("No hay templates configurados.");

            List<ClaimFilePage> claimFilePages = new List<ClaimFilePage>();

            foreach (var company in insuranceCompanies)
            {
                ClaimFilePage claimFilePage = new ClaimFilePage();

                var claims = await getClaimService.GetClaimByInsuranceCompany(company.Id);
                if (claims == null) continue;

                var cover = await GenerateCover(claims, htmlTemplates.Where(x => x.Id == 1).FirstOrDefault(), company.Name);
                if (await CanAdd(cover))
                {
                    claimFilePage.Page = cover;
                    claimFilePages.Add(claimFilePage);
                }

                foreach (var claim in claims)
                {
                    var allDoc = await GenerateAllDocumentation(claim);
                    if (allDoc != null && allDoc.Any())
                    {
                        claimFilePages.AddRange(allDoc);
                    }

                    if (claim.StateId == (long)ClaimState.eId.Pendiente_de_Presentación || !claim.Printed)
                    {
                        var claimDoc = htmlTemplates.Where(x => x.Id == 2).FirstOrDefault();
                        var claimDocPage = await GenerateClaimPage(claim, claimDoc.HtmlTemplate, userName);
                        if (claimDocPage != null)
                        {
                            claimFilePages.Add(claimDocPage);
                        }
                    }

                    var convenio = await GenerateConvenioFirmado(claim, userName);
                    if (convenio != null && convenio.Any())
                    {
                        claimFilePages.AddRange(convenio);
                    }

                    var reconsideration = htmlTemplates.Where(x => x.Id == 3).FirstOrDefault();
                    var reconsiderationPage = await GenerateReconsideration(claim, reconsideration.HtmlTemplate);
                    if (reconsiderationPage != null)
                    {
                        claimFilePages.Add(reconsiderationPage);
                    }

                    //no estoy tan seguro de esto.. pero el cambio de estado estaba cuando se generaba el documento reclamo por primera vez y eso estaba incorrecto
                    //await changeClaimStateService.ChangeState(claim, (long)ClaimState.eId.Presentado);
                }

                await updateClaimService.MarkAsPrinted(claims);
            }


            var result = await pdfMerge.MergeFiles(claimFilePages);
            return result;
        }

        private async Task<string> AddOfferedAmount(Claim claim)
        {
            if (claim.StateId == (long)ClaimState.eId.Ofrecimiento_Rechazado ||
                claim.StateId == (long)ClaimState.eId.Esperando_Ofrecimiento ||
                claim.StateId == (long)ClaimState.eId.Firmar_Convenio ||
                claim.StateId == (long)ClaimState.eId.Pendiente_de_Pago ||
                claim.StateId == (long)ClaimState.eId.Ofrecimiento_Aceptado)
            {

                if (claim.ClaimOffers != null)
                {
                    var claimOffer = claim.ClaimOffers.OrderByDescending(x => x.Created).FirstOrDefault();
                    if (claimOffer != null)
                    {
                        return "$" + claimOffer.OfferedAmount.ToString();
                    }
                }
            }

            return string.Empty;
        }

        private async Task<List<ClaimFilePage>> GenerateAllDocumentation(Claim claim)
        {
            List<ClaimFilePage> claimFilePages = new List<ClaimFilePage>();

            if (claim.StateId != (long)ClaimState.eId.Pendiente_de_Presentación) return null;

            var claimFiles = await getFileService.GetByClaimId(claim.Id, true);
            if (claimFiles == null) return null;

            var filesToPrint = claimFiles.Where(file => !file.Printed).ToList();

            foreach (var file in filesToPrint)
            {
                if (file.FileTypeId == (long)FileType.eId.reclamo) continue;

                if (file != null)
                {
                    if (file.FileName.ToLower().Contains(".pdf"))
                    {
                        //Si es pdf lo agrego directamente
                        byte[] bFile = Convert.FromBase64String(file.Base64);
                        if (await CanAdd(bFile))
                        {
                            ClaimFilePage claimDocPage = new ClaimFilePage();
                            claimDocPage.Page = bFile;
                            claimDocPage.ClaimId = claim.Id;
                            claimFilePages.Add(claimDocPage);
                        }
                    }
                    else
                    {
                        //sino debe crear una nueva pagina de pdf a partir del archivo imagen, y luego la agrego
                        byte[] bFile = Convert.FromBase64String(file.Base64);
                        byte[] bFileFromImage = pdfMerge.CreatePdfFromFile(bFile);
                        if (await CanAdd(bFileFromImage))
                        {
                            ClaimFilePage claimDocPage = new ClaimFilePage();
                            claimDocPage.Page = bFileFromImage;
                            claimDocPage.ClaimId = claim.Id;
                            claimFilePages.Add(claimDocPage);
                        }
                    }
                }
            }

            await updateFileService.MarkAsPrinted(filesToPrint);

            return claimFilePages;
        }

        private async Task<List<ClaimFilePage>> GenerateConvenioFirmado(Claim claim, string userName)
        {
            List<ClaimFilePage> claimFilePages = new List<ClaimFilePage>();

            if (claim.StateId != (long)ClaimState.eId.Convenio_Firmado) return null;

            var claimFiles = await getFileService.GetByClaimId(claim.Id, true);
            if (claimFiles == null) return null;

            var filesToPrint = claimFiles.Where(file => file.FileTypeId == (long)FileType.eId.ConvenioFirmado).ToList();
            if (filesToPrint == null) return null;

            foreach (var file in filesToPrint)
            {
                ClaimFilePage claimDocPage = new ClaimFilePage();
                byte[] bFile = Convert.FromBase64String(file.Base64);
                if (await CanAdd(bFile))
                {
                    claimDocPage.Page = bFile;
                    claimDocPage.ClaimId = claim.Id;
                    claimFilePages.Add(claimDocPage);
                }
            }
            await changeClaimStateService.ChangeState(claim, (long)ClaimState.eId.Pendiente_de_Pago, userName);

            await updateFileService.MarkAsPrinted(filesToPrint);

            return claimFilePages;
        }

        private async Task<ClaimFilePage> GenerateReconsideration(Claim claim, string htmlTemplate)
        {
            if (claim.StateId != (long)ClaimState.eId.Ofrecimiento_Rechazado) return null;
            ClaimFilePage claimDocPage = new ClaimFilePage();

            var template = await ReemplaceTags(htmlTemplate, claim);

            var pdf = await htmlToPdfHelperService.GetFile(template);
            if (await CanAdd(pdf))
            {
                claimDocPage.Page = pdf;
                claimDocPage.ClaimId = claim.Id;
            }

            return claimDocPage;
        }

        private async Task<ClaimFilePage> GenerateClaimPage(Claim claim, string htmlTemplate, string userName)
        {
            if (claim.StateId != (long)ClaimState.eId.Pendiente_de_Presentación)
                return null;
            ClaimFilePage claimDocPage = new ClaimFilePage();

            //Change state
            await changeClaimStateService.ChangeState(claim, (long)ClaimState.eId.Presentado, userName);

            var template = await ReemplaceTags(htmlTemplate, claim);

            var pdf = await htmlToPdfHelperService.GetFile(template);
            if (await CanAdd(pdf))
            {
                claimDocPage.Page = pdf;
                claimDocPage.ClaimId = claim.Id;
            }

            return claimDocPage;
        }
    }
}
