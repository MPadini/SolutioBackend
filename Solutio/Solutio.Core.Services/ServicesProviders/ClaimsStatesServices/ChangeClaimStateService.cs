using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices
{
    public class ChangeClaimStateService : IChangeClaimStateService
    {
        private readonly IClaimStateFactory claimStateFactory;

        public ChangeClaimStateService(IClaimStateFactory claimStateFactory)
        {
            this.claimStateFactory = claimStateFactory;
        }

        public async Task<bool> ChangeState(Claim claim, long newStateId)
        {
            await ValidateInput(claim, newStateId);

            var stateValidator = await claimStateFactory.GetStateValidator(newStateId);
            if (stateValidator == null) throw new ApplicationException("El estado al que quiere pasar no existe o no ha sido configurado.");

            var result = await stateValidator.CanChangeState(claim);
            if (result)
            {
                await Change(claim, newStateId);
            }

            return true;
        }

        private async Task Change(Claim claim, long newStateId)
        {
            claim.StateId = newStateId;
            //TODO:
            //llamar al repo para guardar
        }

        private async Task<bool> ValidateInput(Claim claim, long newStateId)
        {
            if (claim == null) throw new ArgumentException(nameof(Claim), "null");
            if (newStateId <= 0) throw new ArgumentException("newStateId null");

            return true;
        }
    }
}
