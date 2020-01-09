using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.ClaimWorkflowServices {
    public interface IClaimWorkflowService {

        Task<List<ClaimWorkflow>> Get(long claimId);

        Task RegisterWorkflow(long stateId, long claimId);
    }
}
