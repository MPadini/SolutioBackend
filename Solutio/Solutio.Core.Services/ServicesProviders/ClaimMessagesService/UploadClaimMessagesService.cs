using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimMessagesService;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimMessagesService
{
    public class UploadClaimMessagesService : IUploadClaimMessagesService
    {
        private readonly IClaimMessagesRepository claimMessagesRepository;

        public UploadClaimMessagesService(IClaimMessagesRepository claimMessagesRepository)
        {
            this.claimMessagesRepository = claimMessagesRepository;
        }

        public async Task<long> Upload(ClaimMessage message)
        {
            return await claimMessagesRepository.Upload(message);
        }
    }
}
