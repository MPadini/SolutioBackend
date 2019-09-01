using Mapster;
using Microsoft.EntityFrameworkCore;
using Solutio.Core.Entities;
using Solutio.Core.Services.Repositories;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Infrastructure.Repositories.Claims
{
    public class ClaimStateConfigurationRepository : IClaimStateConfigurationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IClaimStateConfigurationMapper claimStateConfigurationMapper;

        public ClaimStateConfigurationRepository(ApplicationDbContext applicationDbContext, IClaimStateConfigurationMapper claimStateConfigurationMapper)
        {
            this.context = applicationDbContext;
            this.claimStateConfigurationMapper = claimStateConfigurationMapper;
        }

        public async Task<ClaimStateConfiguration> GetByParentStateId(long parentStateId)
        {
            var result = context.ClaimStateConfigurations
                .Where(x => x.ParentClaimStateId == parentStateId)
                .Include(x => x.AllowedState)
                .Include(x => x.ParentClaimState)
                .ToList();

            if (result == null || !result.Any()) return default;

            var claimStateConfiguration = claimStateConfigurationMapper.Map(result);

            return claimStateConfiguration;
        }
    }
}
