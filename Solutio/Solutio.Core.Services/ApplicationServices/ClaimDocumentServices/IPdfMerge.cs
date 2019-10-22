using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimDocumentServices {
    public interface IPdfMerge {
        Task<byte[]> MergeFiles(List<byte[]> sourceFiles);
    }
}
