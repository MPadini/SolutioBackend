using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimWorkflowServices;
using Solutio.Core.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimWorkflowServices {
    public class ClaimWorkflowService : IClaimWorkflowService {
        private readonly IClaimWorkflowRepository claimWorkflowRepository;

        public ClaimWorkflowService(IClaimWorkflowRepository claimWorkflowRepository) {
            this.claimWorkflowRepository = claimWorkflowRepository;
        }

        public async Task<List<ClaimWorkflow>> Get(long claimId) {
            return await claimWorkflowRepository.Get(claimId);
        }

        public async Task RegisterWorkflow(long stateId, long claimId, string userName) {
            var workflow = ClaimWorkflow.NewInstance();
            workflow.ClaimStateId = stateId;
            workflow.ClaimId = claimId;
            workflow.UserName = userName;

            await claimWorkflowRepository.Save(workflow);
        }
    }
}
