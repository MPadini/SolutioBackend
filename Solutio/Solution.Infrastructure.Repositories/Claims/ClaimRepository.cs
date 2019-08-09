using Mapster;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext context;

        public ClaimRepository(ApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }

        public async Task<long> Save(Claim claim)
        {
            try
            {
                var claimDb = claim.Adapt<ClaimDB>();

                context.Claims.Add(claimDb);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
