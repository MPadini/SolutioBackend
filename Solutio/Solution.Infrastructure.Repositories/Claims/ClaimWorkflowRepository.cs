using Microsoft.EntityFrameworkCore;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Mapster;
using Solutio.Infrastructure.Repositories.Entities;

namespace Solutio.Infrastructure.Repositories.Claims {
    public class ClaimWorkflowRepository : IClaimWorkflowRepository {
        private readonly ApplicationDbContext applicationDbContext;

        public ClaimWorkflowRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<ClaimWorkflow>> Get(long claimId) {
            var claimWorkflowDB = applicationDbContext.ClaimWorkflows.AsNoTracking()
                .Where(x => x.ClaimId == claimId)
                .Include(x => x.ClaimState)
                .ToList();

            return claimWorkflowDB.Adapt<List<ClaimWorkflow>>();
        }

        public async Task Save(ClaimWorkflow claimWorkflow) {
            var claimworkflowDB = claimWorkflow.Adapt<ClaimWorkflowDB>();

            applicationDbContext.ClaimWorkflows.Add(claimworkflowDB);
            applicationDbContext.SaveChanges();
        }
    }
}
