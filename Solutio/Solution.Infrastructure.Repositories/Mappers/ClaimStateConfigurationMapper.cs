using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solutio.Core.Entities;
using Mapster;
using Solutio.Infrastructure.Repositories.Entities;

namespace Solutio.Infrastructure.Repositories.Mappers
{
    public class ClaimStateConfigurationMapper : IClaimStateConfigurationMapper
    {
        public ClaimStateConfiguration Map(List<ClaimStateConfigurationDB> claimStateConfigurations)
        {
            var claimStateConfig = ClaimStateConfiguration.NewInstance();
            //claimStateConfig.ParentClaimState = claimStateConfigurations.FirstOrDefault().ParentClaimState.Adapt<ClaimState>();
            //claimStateConfigurations.ForEach(item =>
            //{
            //    claimStateConfig.ChildrenClaimState.Add(item.AllowedState.Adapt<ClaimState>());
            //});

            return claimStateConfig;
        }
    }
}
