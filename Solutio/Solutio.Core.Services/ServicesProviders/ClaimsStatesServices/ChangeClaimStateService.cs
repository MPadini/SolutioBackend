using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.Factories;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices
{
    public class ChangeClaimStateService : IChangeClaimStateService
    {
        private readonly IClaimStateFactory claimStateFactory;
        private readonly IClaimRepository claimRepository;
        private readonly IClaimStateRepository stateRepository;

        public ChangeClaimStateService(
            IClaimStateFactory claimStateFactory, 
            IClaimRepository claimRepository,
            IClaimStateRepository stateRepository)
        {
            this.claimStateFactory = claimStateFactory;
            this.claimRepository = claimRepository;
            this.stateRepository = stateRepository;
        }

        public async Task<bool> ChangeState(Claim claim, long newStateId)
        {
            await ValidateInput(claim, newStateId);

            await Change(claim, newStateId);

            return true;
        }

        private async Task Change(Claim claim, long newStateId)
        {
            var state = await stateRepository.GetById(newStateId);
            await ValidateStateExists(state, newStateId);
            await ValidateAllowedState(claim, newStateId);

            claim.StateId = newStateId;
            claim.StateModifiedDate = DateTime.Now;

            await claimRepository.UpdateState(claim, claim.Id);
        }

        private async Task<bool> ValidateAllowedState(Claim claim, long newStateId)
        {
            var allowedState = claim.State.AllowedStates.Where(x => x.Id == newStateId).ToList();
            if (allowedState == null || !allowedState.Any()) throw new ApplicationException("Invalid status change.");

            return true;
        }

        private async Task<bool> ValidateStateExists(ClaimState claimState, long newStateId)
        {
            if (claimState == null) throw new ApplicationException("Selected state does not exists.");

            return true;
        }

        private async Task<bool> ValidateInput(Claim claim, long newStateId)
        {
            if (claim == null) throw new ArgumentException(nameof(Claim), "null");
            if (newStateId <= 0) throw new ArgumentException("newStateId null");

            return true;
        }
    }
}
