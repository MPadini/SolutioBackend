using Solutio.Core.Entities;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Mapster;
using Solutio.Core.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Solutio.Infrastructure.Repositories.Mappers;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimStateRepository : IClaimStateRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IClaimStateMapper claimStateMapper;

        public ClaimStateRepository(ApplicationDbContext applicationDbContext, IClaimStateMapper claimStateMapper)
        {
            this.context = applicationDbContext;
            this.claimStateMapper = claimStateMapper;
        }

        public async Task<List<ClaimState>> GetAll()
        {
            var result = context.ClaimStates.AsNoTracking()
                .Include(x => x.StateConfigurations)
                .ThenInclude(e => e.AllowedState).ToList();

            if (result == null || !result.Any()) return default;

            return claimStateMapper.Map(result);
        }

        public async Task<ClaimState> GetById(long stateId)
        {
            var result = context.ClaimStates.AsNoTracking()
                .Include(x => x.StateConfigurations)
                .ThenInclude(e => e.AllowedState)
                .FirstOrDefault(x => x.Id == stateId);

            if (result == null) return default;

            return claimStateMapper.Map(result);
        }
    }
}
