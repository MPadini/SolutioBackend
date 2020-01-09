using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories {
    public interface IClaimWorkflowRepository {

        Task<List<ClaimWorkflow>> Get(long claimId);

        Task Save(ClaimWorkflow claimWorkflow);
    }
}
