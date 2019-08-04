using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsServices
{
    public class NewClaimService : INewClaimService
    {
        private readonly IClaimRepository claimRepository;

        public NewClaimService(IClaimRepository claimRepository)
        {
            this.claimRepository = claimRepository;
        }

        public async Task<long> Save(Claim claim)
        {
            return await claimRepository.Save(claim);
        }
    }
}
