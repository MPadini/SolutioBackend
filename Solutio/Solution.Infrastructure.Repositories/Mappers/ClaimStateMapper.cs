using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mapster;
using Solutio.Core.Entities;
using Solutio.Infrastructure.Repositories.Entities;

namespace Solutio.Infrastructure.Repositories.Mappers
{
    public class ClaimStateMapper : IClaimStateMapper
    {
        public List<ClaimState> Map(List<ClaimStateDB> claimStateDBs)
        {
            var claimStates = claimStateDBs.Adapt<List<ClaimState>>();
            if (claimStates == null || !claimStates.Any()) return default;

            foreach (var claimState in claimStates)
            {
                var stateDb = claimStateDBs.FirstOrDefault(x => x.Id == claimState.Id);
                var stateConfigurations = stateDb.StateConfigurations.Where(x => x.ParentClaimStateId == claimState.Id).ToList();

                foreach (var stateConfig in stateConfigurations)
                {
                    claimState.AllowedStates.Add(stateConfig.AllowedState.Adapt<ClaimState>());
                }
            }

            return claimStates;
        }

        public ClaimState Map(ClaimStateDB claimStateDB)
        {
            var claimState = claimStateDB.Adapt<ClaimState>();
            if (claimState == null) return default;

            var stateConfigurations = claimStateDB.StateConfigurations.Where(x => x.ParentClaimStateId == claimState.Id).ToList();

            foreach (var stateConfig in stateConfigurations)
            {
                claimState.AllowedStates.Add(stateConfig.AllowedState.Adapt<ClaimState>());
            }

            return claimState;
        }
    }
}
