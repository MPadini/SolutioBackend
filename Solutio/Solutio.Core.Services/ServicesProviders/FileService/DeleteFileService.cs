using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.FileService;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.FileService
{
    public class DeleteFileService : IDeleteFileService
    {
        private readonly IClaimFileRepository claimFileRepository;

        public DeleteFileService(IClaimFileRepository claimFileRepository)
        {
            this.claimFileRepository = claimFileRepository;
        }

        public async Task Delete(ClaimFile claimFile)
        {
            await claimFileRepository.Delete(claimFile);
        }
    }
}
