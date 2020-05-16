using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.ApplicationServices.ClaimWorkflowServices;
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
        private readonly IClaimWorkflowService claimWorkflowService;

        public NewClaimService(IClaimRepository claimRepository, IClaimWorkflowService claimWorkflowService)
        {
            this.claimRepository = claimRepository;
            this.claimWorkflowService = claimWorkflowService;
        }

        public async Task<long> Save(Claim claim, string userName)
        {
            var result = await claimRepository.Save(claim, userName);

            if (claim.State != null) {
                await claimWorkflowService.RegisterWorkflow(claim.StateId, result, userName);
            }
           

            return result;
        }
    }
}
