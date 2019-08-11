using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators
{
    public class OfferedValidator : IClaimStateValidator
    {
        public async Task<bool> CanChangeState(Claim claim)
        {
            if (!claim.State.CanOffered)
            {
                throw new ApplicationException($"No es posible cambiar el estado { claim.State.Description } a Ofrecido/Reconsiderado");
            }

            return true;
        }
    }
}
