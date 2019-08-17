using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.FileService;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.FileService
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IClaimFileRepository claimFileRepository;

        public UploadFileService(IClaimFileRepository claimFileRepository)
        {
            this.claimFileRepository = claimFileRepository;
        }

        public async Task<long> Upload(ClaimFile file)
        {
            return await claimFileRepository.Upload(file);
        }
    }
}
