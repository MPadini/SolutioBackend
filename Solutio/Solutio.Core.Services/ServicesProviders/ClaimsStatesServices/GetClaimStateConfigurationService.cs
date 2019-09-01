using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices
{
    public class GetClaimStateConfigurationService : IGetClaimStateConfigurationService
    {
        private readonly IClaimStateConfigurationRepository claimStateConfigurationRepository;

        public GetClaimStateConfigurationService(IClaimStateConfigurationRepository claimStateConfigurationRepository)
        {
            this.claimStateConfigurationRepository = claimStateConfigurationRepository;
        }

        public async Task<ClaimStateConfiguration> GetByParentStateId(long parentStateId)
        {
            return await claimStateConfigurationRepository.GetByParentStateId(parentStateId);
        }
    }
}
