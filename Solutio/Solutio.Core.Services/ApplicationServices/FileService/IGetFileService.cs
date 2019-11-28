using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.FileService
{
    public interface IGetFileService
    {
        Task<ClaimFile> GetById(long id, bool withBase64 = true);

        Task<List<FileType>> GetFileTypes();

        Task<List<ClaimFile>> GetByClaimId(long claimId, bool withBase64 = false);
    }
}
