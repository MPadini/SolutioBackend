using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimFileRepository
    {
        Task<long> Upload(ClaimFile file);

        Task Delete(ClaimFile file);

        Task<ClaimFile> GetById(long fileId, bool withBase64);

        Task DeleteClaimFiles(Claim claim);

        Task<List<FileType>> GetFileTypes();

        Task<List<ClaimFile>> GetByClaimId(long claimId, bool withBase64);
    }
}
