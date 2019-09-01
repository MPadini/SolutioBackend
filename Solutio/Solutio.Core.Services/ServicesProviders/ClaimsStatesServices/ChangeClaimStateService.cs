using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.Factories;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

            //var stateValidator = await claimStateFactory.GetStateValidator(claim.StateId);
            //if (stateValidator == null) throw new ApplicationException("El estado al que quiere pasar no existe o no ha sido configurado.");

            //var result = await stateValidator.CanChangeState(newStateId);
            //if (result)
            //{
            //    await Change(claim, newStateId);
            //}
            await Change(claim, newStateId);

            return true;
        }

        private async Task Change(Claim claim, long newStateId)
        {
            var state = await stateRepository.GetById(newStateId);
            claim.StateId = newStateId;

            await claimRepository.Update(claim, claim.Id);
        }

        private async Task<bool> ValidateInput(Claim claim, long newStateId)
        {
            if (claim == null) throw new ArgumentException(nameof(Claim), "null");
            if (newStateId <= 0) throw new ArgumentException("newStateId null");

            return true;
        }
    }
}
