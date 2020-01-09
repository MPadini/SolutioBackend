using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimMessagesService;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimMessagesService
{
    public class DeleteClaimMessagesService : IDeleteClaimMessagesService
    {
        private readonly IClaimMessagesRepository claimMessagesRepository;

        public DeleteClaimMessagesService(IClaimMessagesRepository claimMessagesRepository)
        {
            this.claimMessagesRepository = claimMessagesRepository;
        }

        public async Task Delete(ClaimMessage claimMessage)
        {
            await claimMessagesRepository.Delete(claimMessage);
        }

    }
}
