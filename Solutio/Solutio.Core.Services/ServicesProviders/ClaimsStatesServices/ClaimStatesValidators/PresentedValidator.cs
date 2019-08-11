using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators
{
    public class PresentedValidator : IClaimStateValidator
    {
        public async Task<bool> CanChangeState(Claim claim)
        {
            if (!claim.State.CanPresented)
            {
                throw new ApplicationException($"No es posible cambiar el estado { claim.State.Description } a Presentado");
            }

            return true;
        }
    }
}
