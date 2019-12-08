using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.FileService {
    public interface IUpdateFileService {

        Task MarkAsPrinted(List<ClaimFile> claimFiles);
    }
}
