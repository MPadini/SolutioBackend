using Solutio.Core.Services.ServicesProviders.ClaimDocumentServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimDocumentServices {
    public interface IPdfMerge {
        Task<byte[]> MergeFiles(List<byte[]> sourceFiles);

        Task<byte[]> MergeFiles(List<ClaimFilePage> ClaimFilePages);

        byte[] CreatePdfFromFile(byte[] sourceFile);
    }
}
