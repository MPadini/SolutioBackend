using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimMessagesService;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimMessagesService
{
    public class UpdateClaimMessagesService : IUpdateClaimMessagesService
    {
        private readonly IClaimMessagesRepository claimMessagesRepository;

        public UpdateClaimMessagesService(IClaimMessagesRepository claimMessagesRepository)
        {
            this.claimMessagesRepository = claimMessagesRepository;
        }

        public async Task MarkAsViewed(List<ClaimMessage> claimMessage)
        {
            if (claimMessage == null) return;
            if (!claimMessage.Any()) return;

            try
            {
                await claimMessagesRepository.Update(claimMessage);
            }
            catch (Exception ex)
            {
            }

        }
    }
}
