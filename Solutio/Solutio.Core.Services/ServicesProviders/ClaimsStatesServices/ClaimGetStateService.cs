using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices
{
    public class ClaimGetStateService : IClaimGetStateService
    {
        private readonly IClaimStateRepository claimStateRepository;

        public ClaimGetStateService(IClaimStateRepository claimStateRepository)
        {
            this.claimStateRepository = claimStateRepository;
        }

        public async Task<List<ClaimState>> GetAll()
        {
            var claimState  = await claimStateRepository.GetAll();
            return claimState;
        }
    }
}
