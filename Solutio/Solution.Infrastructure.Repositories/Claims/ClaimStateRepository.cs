using Solutio.Core.Entities;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Mapster;
using Solutio.Core.Services.Repositories;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimStateRepository : IClaimStateRepository
    {
        private readonly ApplicationDbContext context;

        public ClaimStateRepository(ApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }

        public async Task<List<ClaimState>> GetAll()
        {
            var result = context.ClaimStates.ToList();
            var claimState = result.Adapt<List<ClaimState>>();

            return claimState;
        }
    }
}
