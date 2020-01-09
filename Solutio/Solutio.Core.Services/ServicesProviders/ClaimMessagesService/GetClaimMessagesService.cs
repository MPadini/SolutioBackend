using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimMessagesService;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimMessagesService
{
    public class GetClaimMessagesService : IGetClaimMessagesService
    {
        private readonly IClaimMessagesRepository claimMessagesRepository;

        public GetClaimMessagesService(IClaimMessagesRepository claimMessagesRepository)
        {
            this.claimMessagesRepository = claimMessagesRepository;
        }

        public async Task<ClaimMessage> GetById(long id)
        {
            return await claimMessagesRepository.GetById(id);
        }

        public async Task<List<ClaimMessage>> GetByClaimId(long claimId)
        {
            return await claimMessagesRepository.GetByClaimId(claimId);
        }
    }
}
