using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsServices
{
    public class UpdateClaimService : IUpdateClaimService
    {
        private readonly IClaimRepository claimRepository;

        public UpdateClaimService(IClaimRepository claimRepository)
        {
            this.claimRepository = claimRepository;
        }

        public async Task Update(Claim claim, long claimId)
        {
            await claimRepository.Update(claim, claimId);
        }
    }
}
