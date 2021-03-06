﻿using Solutio.Core.Services.ApplicationServices.ClaimDocumentServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Solutio.Core.Services.ServicesProviders.ClaimDocumentServices
{
    public class PdfMerge : IPdfMerge
    {

        public byte[] CreatePdfFromFile(byte[] sourceFile)
        {
            Document document = new Document();
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(document, ms);
                document.Open();
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(sourceFile);

                image.Alignment = Element.ALIGN_CENTER;

                image.SpacingBefore = 5f;
                image.SpacingAfter = 5f;

                float pageWidth = document.PageSize.Width - (35 + 35);
                float pageHeight = document.PageSize.Height - (35 + 35);
                image.ScaleToFit(pageWidth, pageHeight);
                               
                document.Add(image);
                document.Close();
                return ms.ToArray();
            }
        }

        public async Task<byte[]> MergeFiles(List<byte[]> sourceFiles)
        {
            Document document = new Document();
            using (MemoryStream ms = new MemoryStream())
            {
                PdfCopy copy = new PdfCopy(document, ms);
                document.Open();
                int documentPageCounter = 0;

                // Iterate through all pdf documents
                for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                {
                    // Create pdf reader
                    PdfReader reader = new PdfReader(sourceFiles[fileCounter]);
                    int numberOfPages = reader.NumberOfPages;

                    // Iterate through all pages
                    for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                    {
                        documentPageCounter++;
                        PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                        PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

                        // Write header
                        ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                            new Phrase(string.Empty), importedPage.Width / 2, importedPage.Height - 30,
                            importedPage.Width < importedPage.Height ? 0 : 1);

                        // Write footer
                        if (fileCounter > 0)
                        {
                            //ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                            //new Phrase(String.Format("Página {0}", documentPageCounter)), importedPage.Width / 2, 15,
                            //importedPage.Width < importedPage.Height ? 0 : 1);
                        }

                        pageStamp.AlterContents();

                        copy.AddPage(importedPage);
                    }

                    copy.FreeReader(reader);
                    reader.Close();
                }

                document.Close();
                return ms.GetBuffer();
            }
        }


        public async Task<byte[]> MergeFiles(List<ClaimFilePage> ClaimFilePages)
        {


            Document document = new Document();
            using (MemoryStream ms = new MemoryStream())
            {
                PdfCopy copy = new PdfCopy(document, ms);
                document.Open();
                int documentPageCounter = 0;

                // Iterate through all pdf documents
                foreach (var claimPage in ClaimFilePages)
                {
                    // Create pdf reader
                    PdfReader reader = new PdfReader(claimPage.Page);
                    int numberOfPages = reader.NumberOfPages;

                    // Iterate through all pages
                    for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                    {
                        documentPageCounter++;
                        PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                        PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

                        // Write header
                        ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                            new Phrase(string.Empty), importedPage.Width / 2, importedPage.Height - 30,
                            importedPage.Width < importedPage.Height ? 0 : 1);

                        // Write footer
                        if (claimPage.ClaimId > 0)
                        {
                            ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                            new Phrase(String.Format($"Reclamo {claimPage.ClaimId}")), importedPage.Width / 2, 15,
                            importedPage.Width < importedPage.Height ? 0 : 1);
                        }

                        pageStamp.AlterContents();

                        copy.AddPage(importedPage);
                    }

                    copy.FreeReader(reader);
                    reader.Close();
                }

                document.Close();
                return ms.GetBuffer();
            }
        }
    }
}
